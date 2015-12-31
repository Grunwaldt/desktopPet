using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace desktopPet
{
    static class Program
    {
        // Mutex can be made static so that GC doesn't recycle
        // same effect with GC.KeepAlive(mutex) at the end of main
        static Mutex mutex = new Mutex(false, "eSheep_Running");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            // if you like to wait a few seconds in case that the instance is just 
            // shutting down
            if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false))
            {
                MessageBox.Show("eSheep is already running!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            mutex.ReleaseMutex();
        }
    }
}

