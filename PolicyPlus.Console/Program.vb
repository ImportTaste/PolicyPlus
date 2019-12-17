Imports System.ComponentModel
Imports McMaster.Extensions.CommandLineUtils
Imports PolicyPlus.Gui

Public Module Program
    Function Main(args As String()) As Integer
        Dim app = New CommandLineApplication
        app.Name = "policyplus"
        app.FullName = "Policy Plus CLI"
        app.HelpOption("-h|--help")

        app.Command("convertto", Sub(converttoCommand)
            converttoCommand.Description = "Converts Group Policy files either to .reg or .pol."

            Dim format = New CommandArgument With 
                    {
                    .Description = "Format to convert to. Either reg or pol.", 
                    .Name="reg|pol",
                    .MultipleValues = False
                    }
            Dim input = New CommandOption("-i|--input", CommandOptionType.SingleValue) With{
                    .Description = "Path to the input file."                                     
                    }

            Dim output = New CommandOption("-o|--output", CommandOptionType.SingleValue) With{
                    .Description = "Path to the output file."                                     
                    }

            Dim scope = New CommandOption("-s|--scope", CommandOptionType.SingleValue) With{
                    .Description = "Scope (User|Machine) that is used when converting from pol to reg. Default is User."
                    }

            converttoCommand.Arguments.Add(format)
            converttoCommand.Options.AddRange({input, output, scope})
            converttoCommand.HelpOption("-h|--help")
            converttoCommand.OnExecute(Sub()
                If (format Is Nothing Or Not input.HasValue() Or Not output.HasValue)
                    converttoCommand.ShowHelp()
                Else
                    Dim commandLineOptions = New CommandLineOptions(input.Value(), output.Value, scope.Value)
                    commandLineOptions.ConvertTo(format.Value)
                End If
            End Sub)
        End Sub)

        app.OnExecute(Sub()
            If args Is Nothing Or args.Length <= 0 Then
                Dim formsApplication = New FormsApplication
                formsApplication.StartFormsApplication()
            End If
        End Sub)

        Try
            Return app.Execute(args)
        Catch ex As CommandParsingException
            app.ShowHelp()
            Return 102
        End Try
    End Function
End Module