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
                Process recourceCheckProcess = new Process();
                recourceCheckProcess.StartInfo.FileName = configInfo.ScriptExeLocation;
                StringBuilder arguments = new StringBuilder();
                arguments.Append(configInfo.ScriptConfigFileLocation);
                arguments.Append(" ");
                arguments.Append(configInfo.JsonFilesDestinationFolder);
                arguments.Append(" ");
                arguments.Append(configInfo.DetectedChangesFileLocation);
                recourceCheckProcess.StartInfo.Arguments = arguments.ToString();
                recourceCheckProcess.EnableRaisingEvents = true;

                recourceCheckProcess.Start();
                recourceCheckProcess.WaitForExit();
                                
                Console.WriteLine("Resource check process exited: " + recourceCheckProcess.ExitCode);

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
