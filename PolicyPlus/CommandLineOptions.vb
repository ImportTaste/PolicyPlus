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

    Public Sub New(inputFilePath As String, outputFilePath As String, policySection As String)
        Me.InputFilePath = inputFilePath
        Me.OutputFilePath = outputFilePath
        Me.InputExtension = Path.GetExtension(inputFilePath)
        Me.OutputExtension = Path.GetExtension(outputFilePath)
        Me.PolicySection = policySection

        Me.UserPolicyLoader = New PolicyLoader(PolicyLoaderSource.LocalGpo, "", True)
        Me.CompPolicyLoader = New PolicyLoader(PolicyLoaderSource.LocalGpo, "", False)

        Me.UserPolicySource = UserPolicyLoader.OpenSource
        Me.CompPolicySource = CompPolicyLoader.OpenSource

        Convert()
    End Sub

    Private Sub Convert()
        If InputExtension = ".reg" And OutputExtension = ".pol" Then
            Dim policySource = If(IsUser(PolicySection), UserPolicySource, CompPolicySource)
            Dim prefix = GetPrefix()
            Dim reg As RegFile = RegFile.Load(InputFilePath, prefix)

            reg.Apply(policySource)

            Dim polFile = GetOrCreatePolFromPolicySource(policySource)
            polFile.Save(OutputFilePath)

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
                'MsgBox("REG exported successfully.", MsgBoxStyle.Information)
            Catch ex As Exception
                'MsgBox("Failed to export REG!", MsgBoxStyle.Exclamation)
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
