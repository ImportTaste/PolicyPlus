Imports System.ComponentModel
Imports McMaster.Extensions.CommandLineUtils

Public Module Program
    Function Main(args As String()) As Integer
        Dim app = New CommandLineApplication
        app.Name = "policyplus"
        app.FullName = "Policy Plus CLI"
        app.HelpOption("-h|--help")

        'Dim commandLineOptions = New CommandLineOptions(parameters("input"), parameters("output"), parameters("policysection"))

        app.Command("convertto", Sub(configCmd)
            Dim format As CommandArgument = New CommandArgument With 
                    {
                    .Description = "Format to convert to. Either reg or pol.", 
                    .Name="<reg|pol>",
                    .MultipleValues = False
                    }
            Dim input As CommandOption = New CommandOption("-i|--input", CommandOptionType.SingleValue) With{
                    .Description = "Path to the input file."                                     
                    }

            Dim output As CommandOption = New CommandOption("-o|--output", CommandOptionType.SingleValue) With{
                    .Description = "Path to the output file."                                     
                    }
            configCmd.Arguments.Add(format)
            configCmd.Options.AddRange({input, output})
            configCmd.HelpOption("-h|--help")
            configCmd.OnExecute(Sub()
                If (format Is Nothing Or Not input.HasValue() Or Not output.HasValue)
                    configCmd.ShowHelp()
                Else

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