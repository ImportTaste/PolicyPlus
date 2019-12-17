Imports System.IO
Imports Microsoft.Win32
Imports PolicyPlus.Gui

Public Class CommandLineOptions

    Private InputFilePath As String
    Private OutputFilePath As String
    Private OutputExtension As String
    Private InputExtension As String
    Private Scope As String
    Private UserPolicySource, CompPolicySource As IPolicySource
    Private UserPolicyLoader, CompPolicyLoader As PolicyLoader

    Public Sub New(inputFilePath As String, outputFilePath As String, scope As String)
        Me.InputFilePath = inputFilePath
        Me.OutputFilePath = outputFilePath
        Me.InputExtension = Path.GetExtension(inputFilePath)
        Me.OutputExtension = Path.GetExtension(outputFilePath)
        Me.Scope = If(scope, "User")

        Dim selectedComputer = New PolicyLoader(PolicyLoaderSource.Null, "", False)
        Dim selectedUser = new PolicyLoader(PolicyLoaderSource.Null, "", True)

        OpenPolicyLoaders(selectedUser, selectedComputer)
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
            System.Console.WriteLine("Cleanup did not complete fully because the loaded resources are open in other programs.")
        End If
    End Sub

    Sub OpenPolicyLoaders(User As PolicyLoader, Computer As PolicyLoader)
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

        Dim userStatus = policyStatus(User)
        Dim compStatus = policyStatus(Computer)
        'UserSourceLabel.Text = UserPolicyLoader.GetDisplayInfo
        'ComputerSourceLabel.Text = CompPolicyLoader.GetDisplayInfo
        If Not allOk Then
            System.Console.WriteLine("Not all policy sources are fully writable.")
            System.Console.WriteLine("The user source " & userStatus & "." & vbCrLf & vbCrLf & "The computer source " & compStatus & ".")
            'Dim msgText = "Not all policy sources are fully writable." & vbCrLf & vbCrLf &
            '   "The user source " & userStatus & "." & vbCrLf & vbCrLf & "The computer source " & compStatus & "."
            'MsgBox(msgText, MsgBoxStyle.Exclamation)
        End If
    End Sub

    Public Sub ConvertTo(format As String)
        If format <> "reg" And format <> "pol" Then
            System.Console.WriteLine("Argument must be either reg or pol! Check the argument!")
        Else 
            If format = "reg" Then
                If IsValidInputFile("pol") And IsValidOutputFile(format) Then
                    ConvertToReg()
                End If
            Else IF format = "pol"
                If IsValidInputFile("reg") And IsValidOutputFile(format) Then
                    ConvertToPol()
                End If
            End If
        End If
    End Sub

    Private Sub ConvertToReg()
        Dim prefix = If(IsUser(Scope), "HKEY_CURRENT_USER\", "HKEY_LOCAL_MACHINE\")
        Dim file = PolFile.Load(InputFilePath)
        Dim pol = GetOrCreatePolFromPolicySource(file)

        Dim reg As New RegFile
        reg.SetPrefix(prefix)
        reg.SetSourceBranch("")
        Try
            pol.Apply(reg)
            reg.Save(OutputFilePath)
            System.Console.WriteLine("REG exported successfully.")
        Catch ex As Exception
            System.Console.WriteLine("Failed to export REG!")
        End Try
    End Sub

    Private Sub ConvertToPol()
        Dim prefix = GetPrefix()

        If(Not prefix.StartsWith("HKEY")) Then
            System.Console.WriteLine("No hive name in key!")
            Return
        End If
        
        Dim policySource = If(IsUser(prefix), UserPolicySource, CompPolicySource)
        Dim reg As RegFile = RegFile.Load(InputFilePath, prefix)

        reg.Apply(policySource)

        Dim polFile = GetOrCreatePolFromPolicySource(policySource)
        Try
            polFile.Save(OutputFilePath)
            System.Console.WriteLine("POL exported successfully.")
        Catch ex As Exception
            System.Console.WriteLine("Failed to export POL!")
        End Try
    End Sub

    Private Function IsUser(section As String) As Boolean
        Return section.ToUpper().Contains("USER")
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

    Private Function IsValidInputFile(format As String) As Boolean
        If Not InputExtension.Equals($".{format}") Then
            System.Console.WriteLine("Specified input file has wrong format. Must either be .reg or .pol!")
            Return False
        End If
        If Not File.Exists(InputFilePath) Then
            System.Console.WriteLine($"Specified input file doesn't exist! Check the file location: {InputFilePath}")
            Return False
        End If
        Return True
    End Function

    Private Function IsValidOutputFile(format As String) As Boolean
        If Not OutputExtension.Equals($".{format}") Then
            System.Console.WriteLine($"Specified convert argument ({format}) doesn't match output file extension ({OutputExtension})!")
            Return False
        End If
        Return True
    End Function

End Class
