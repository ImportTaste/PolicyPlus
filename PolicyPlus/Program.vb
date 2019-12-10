
Imports System.DirectoryServices.ActiveDirectory
Imports McMaster.Extensions.CommandLineUtils

Module Program
    <STAThread>
    Function Main(args As String()) As Integer
        Dim app = New CommandLineApplication
        app.Name = "policyplus"
        app.FullName = "Policy Plus"
        app.HelpOption()

        Dim convert As CommandOption = app.Option("-c|--convert", "Convert file format", CommandOptionType.NoValue)
        Dim input As CommandArgument = app.Argument("<InputFilePath>", "Path to the input file.")
        Dim output As CommandArgument = app.Argument("<OutputFilePath>", "Path to the output file.")
        Dim policySection As CommandArgument = app.Argument("<PolicySection>", "Section of the policy.")

        'Dim parameters = args.Select(Function(x) x.Substring(1).Split("=")).ToDictionary(Function(y) y(0), Function(y) y(1))
        'Dim commandLineOptions = New CommandLineOptions(parameters("input"), parameters("output"), parameters("policysection"))

        app.OnExecute(Sub()
                          If Not convert.HasValue() Then
                              Application.EnableVisualStyles()
                              Application.SetCompatibleTextRenderingDefault(False)
                              Application.Run(New Main)
                          Else
                              Console.WriteLine("Hello World")
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
