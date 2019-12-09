Imports System.Runtime.Remoting.Messaging

Module Program
    <STAThread>
    Sub Main(args As String())
        If args.Length <= 0 Then
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Main)
        Else

            Dim parameters = args.Select(Function(x) x.Substring(1).Split("=")).ToDictionary(Function(y) y(0), Function(y) y(1))
            Dim commandLineOptions = New CommandLineOptions(parameters("input"), parameters("output"), parameters("policysection"))
        End If


    End Sub
End Module
