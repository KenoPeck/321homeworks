//-----------------------------------------------------------------------
// Program.cs
// contains implementation of UI class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace HW4
{
    /// <summary>
    /// Main Program class for spreadsheet application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}