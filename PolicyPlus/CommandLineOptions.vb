Imports System.IO
Imports Microsoft.Win32

Public Class CommandLineOptions

    Private InputFilePath As String
    Private OutputFilePath As String
    Private OutputExtension As String
    Private InputExtension As String
    Private PolicySection As String
    Private UserPolicySource, CompPolicySource As IPolicySource
    Private UserPolicyLoader, CompPolicyLoader As PolicyLoader
    Private UserComments, CompComments As Dictionary(Of String, String)

    Public Sub New(inputFilePath As String, outputFilePath As String, policySection As String)
        Me.InputFilePath = inputFilePath
        Me.OutputFilePath = outputFilePath
        Me.InputExtension = Path.GetExtension(inputFilePath)
        Me.OutputExtension = Path.GetExtension(outputFilePath)
        Me.PolicySection = policySection


        Dim selectedComputer = New PolicyLoader(PolicyLoaderSource.Null, "", False)
        Dim selectedUser = new PolicyLoader(PolicyLoaderSource.Null, "", True)

        OpenPolicyLoaders(selectedUser, selectedComputer, False)

        Convert()
    End Sub

    Sub ClosePolicySources()
        ' Clean up the policy sources
        Dim allOk As Boolean = True
        If UserPolicyLoader IsNot Nothing Then
            If Not UserPolicyLoader.Close() Then allOk = False
        End If
        If CompPolicyLoader IsNot Nothing Then
            If Not CompPolicyLoader.Close() Then allOk = False
        End If
        If Not allOk Then
            Console.WriteLine("Cleanup did not complete fully because the loaded resources are open in other programs.")
        End If
    End Sub

    Sub OpenPolicyLoaders(User As PolicyLoader, Computer As PolicyLoader, Quiet As Boolean)
        ' Create policy sources from the given loaders
        If CompPolicyLoader IsNot Nothing Or UserPolicyLoader IsNot Nothing Then ClosePolicySources()
        Me.UserPolicyLoader = User
        Me.UserPolicySource = User.OpenSource
        Me.CompPolicyLoader = Computer
        Me.CompPolicySource = Computer.OpenSource
        Dim allOk As Boolean = True
        Dim policyStatus = Function(Loader As PolicyLoader) As String
                               Select Case Loader.GetWritability
                                   Case PolicySourceWritability.Writable
                                       Return "is fully writable"
                                   Case PolicySourceWritability.NoCommit
                                       allOk = False
                                       Return "cannot be saved"
                                   Case Else ' No writing
                                       allOk = False
                                       Return "cannot be modified"
                               End Select
                           End Function
        Dim loadComments = Function(Loader As PolicyLoader) As Dictionary(Of String, String)
                               Dim cmtxPath = Loader.GetCmtxPath
                               If cmtxPath = "" Then
                                   Return Nothing
                               Else
                                   Try
                                       IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(cmtxPath))
                                       If IO.File.Exists(cmtxPath) Then
                                           Return CmtxFile.Load(cmtxPath).ToCommentTable
                                       Else
                                           Return New Dictionary(Of String, String)
                                       End If
                                   Catch ex As Exception
                                       Return Nothing
                                   End Try
                               End If
                           End Function
        Dim userStatus = policyStatus(User)
        Dim compStatus = policyStatus(Computer)
        UserComments = loadComments(User)
        CompComments = loadComments(Computer)
        'UserSourceLabel.Text = UserPolicyLoader.GetDisplayInfo
        'ComputerSourceLabel.Text = CompPolicyLoader.GetDisplayInfo
        If allOk Then
            If Not Quiet Then
                Console.WriteLine("Both the user and computer policy sources are loaded and writable.")
                'MsgBox("Both the user and computer policy sources are loaded and writable.", MsgBoxStyle.Information)
            End If
        Else
            Console.WriteLine("Not all policy sources are fully writable.")
            Console.WriteLine("The user source " & userStatus & "." & vbCrLf & vbCrLf & "The computer source " & compStatus & ".")
            'Dim msgText = "Not all policy sources are fully writable." & vbCrLf & vbCrLf &
            '   "The user source " & userStatus & "." & vbCrLf & vbCrLf & "The computer source " & compStatus & "."
            'MsgBox(msgText, MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Convert()
        If InputExtension = ".reg" And OutputExtension = ".pol" Then
            Dim policySource = If(IsUser(PolicySection), UserPolicySource, CompPolicySource)
            Dim prefix = GetPrefix()
            Dim reg As RegFile = RegFile.Load(InputFilePath, prefix)

            reg.Apply(policySource)

            Dim polFile = GetOrCreatePolFromPolicySource(policySource)
            Try
                polFile.Save(OutputFilePath)
                Console.WriteLine("POL exported successfully.")
            Catch ex As Exception
                Console.WriteLine("Failed to export POL!")
            End Try
        ElseIf InputExtension = ".pol" And OutputExtension = ".reg" Then
            Dim prefix = If(IsUser(PolicySection), "HKEY_CURRENT_USER\", "HKEY_LOCAL_MACHINE\")
            Dim file = PolFile.Load(InputFilePath)
            Dim pol = GetOrCreatePolFromPolicySource(file)

            Dim reg As New RegFile
            reg.SetPrefix(prefix)
            reg.SetSourceBranch("")
            Try
                pol.Apply(reg)
                reg.Save(OutputFilePath)
                Console.WriteLine("REG exported successfully.")
            Catch ex As Exception
                Console.WriteLine("Failed to export REG!")
            End Try
        End If
    End Sub

    Private Function IsUser(section As String) As Boolean
        Return section.Equals("User")
    End Function

    Private Function GetPrefix() As String
        Dim reg = RegFile.Load(InputFilePath, "")
        Return reg.GuessPrefix()
    End Function

    Function GetOrCreatePolFromPolicySource(Source As IPolicySource) As PolFile
        If TypeOf Source Is PolFile Then
            ' If it's already a POL, just save it
            Return Source
        ElseIf TypeOf Source Is RegistryPolicyProxy Then
            ' Recurse through the Registry branch and create a POL
            Dim regRoot = CType(Source, RegistryPolicyProxy).EncapsulatedRegistry
            Dim pol As New PolFile
            Dim addSubtree As Action(Of String, RegistryKey)
            addSubtree = Sub(PathRoot As String, Key As RegistryKey)
                             For Each valName In Key.GetValueNames
                                 Dim valData = Key.GetValue(valName, Nothing, RegistryValueOptions.DoNotExpandEnvironmentNames)
                                 pol.SetValue(PathRoot, valName, valData, Key.GetValueKind(valName))
                             Next
                             For Each subkeyName In Key.GetSubKeyNames
                                 Using subkey = Key.OpenSubKey(subkeyName, False)
                                     addSubtree(PathRoot & "\" & subkeyName, subkey)
                                 End Using
                             Next
                         End Sub
            For Each policyPath In RegistryPolicyProxy.PolicyKeys
                Using policyKey = regRoot.OpenSubKey(policyPath, False)
                    addSubtree(policyPath, policyKey)
                End Using
            Next
            Return pol
        Else
            Throw New InvalidOperationException("Policy source type not supported")
        End If
    End Function

End Class
