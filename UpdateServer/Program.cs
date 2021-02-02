using System;
using System.Diagnostics;

namespace UpdateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Process firstProc = new Process();
                firstProc.StartInfo.FileName = @"C:\Users\User\source\repos\AvailableResourcesCheck\AvailableResourcesCheck\bin\Debug\netcoreapp3.1\AvailableResourcesCheck.exe";
                firstProc.StartInfo.Arguments = @"C:\Users\User\Desktop\rp_folders\config\config_info.txt C:\Users\User\Desktop\rp_folders\json_test\ C:\Users\User\Desktop\rp_folders\changes\changes.txt";
                firstProc.EnableRaisingEvents = true;

                firstProc.Start();

                firstProc.WaitForExit();

                //You may want to perform different actions depending on the exit code.
                Console.WriteLine("First process exited: " + firstProc.ExitCode);               

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred!!!: " + ex.Message);
                return;
            }
        }
    }
}
