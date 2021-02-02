using System;
using System.Diagnostics;
using System.Text;

namespace UpdateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConfigInfo configInfo = Parser.Parse(@"C:\Users\User\Desktop\rp_folders\config\config_info_server.txt");
                Process firstProc = new Process();
                firstProc.StartInfo.FileName = configInfo.AvailableResourcesCheckExeLocation;
                StringBuilder arguments = new StringBuilder();
                arguments.Append(configInfo.ConfigInfoForScriptLocation);
                arguments.Append(" ");
                arguments.Append(configInfo.JsonFilesFolder);
                arguments.Append(" ");
                arguments.Append(configInfo.WhereToSaveChangedResources);
                firstProc.StartInfo.Arguments = arguments.ToString();
                firstProc.EnableRaisingEvents = true;

                firstProc.Start();

                firstProc.WaitForExit();

                //You may want to perform different actions depending on the exit code.
                Console.WriteLine("First process exited: " + firstProc.ExitCode);

            }
            catch (InvalidParamInConfigFileException ex)
            {
                Console.WriteLine("Invalid format of congig info txt file!");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
