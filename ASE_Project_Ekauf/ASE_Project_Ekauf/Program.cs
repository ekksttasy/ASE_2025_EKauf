namespace ASE_Project_Ekauf
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException); //handler for thus unhandle exceptions
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.Run(new Form1());
        }

        /// <summary>
        /// Application handler for unandled exceptions. Launches a message box showing the error.
        /// </summary>
        /// <param name="sender">The object sending the exception. </param>
        /// <param name="e">Exception to be handled. </param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"An error occurred: {e.Exception.Message}", "Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}