using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace UpdateServer
{
    class Program
    {
        static void Main(string[] args)
        {         
            try
            {
                ConfigInfo configInfo = Parser.Parse(args[0]); 
                Process resourceCheckProcess = new Process();
                resourceCheckProcess.StartInfo.FileName = configInfo.ScriptExeLocation;
                StringBuilder arguments = new StringBuilder();
                arguments.Append(configInfo.ScriptConfigFileLocation);
                arguments.Append(" ");
                arguments.Append(configInfo.JsonFilesDestinationFolder);
                arguments.Append(" ");
                arguments.Append(configInfo.ChangesTxtFile);
                resourceCheckProcess.StartInfo.Arguments = arguments.ToString();
                resourceCheckProcess.EnableRaisingEvents = true;

                resourceCheckProcess.Start();
                resourceCheckProcess.WaitForExit();
                                
                Console.WriteLine("Resource check process exited: " + resourceCheckProcess.ExitCode);

                DateTime startTime = DateTime.UtcNow;
                int durationInMinutes = 10;
                TimeSpan breakDuration = TimeSpan.FromMinutes(durationInMinutes);
                Console.WriteLine(startTime);

                //here starts server for some time
                IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 1234);
                TcpListener listener = new TcpListener(ep);
                listener.Start();

                Console.WriteLine(@" 
            ===================================================  
                   Started listening requests at: {0}:{1}  
            ===================================================",
                ep.Address, ep.Port);

                while (DateTime.UtcNow - startTime < breakDuration)
                {
                    const int bytesize = 1024 * 1024;

                    string message = null;
                    byte[] buffer = new byte[bytesize];

                    var sender = listener.AcceptTcpClient();
                    sender.GetStream().Read(buffer, 0, bytesize);

                    // Read the message and perform different actions  
                    message = cleanMessage(buffer);
                    Console.WriteLine(message);

                    // Save the data sent by the client;  
                    Request request = JsonConvert.DeserializeObject<Request>(message);
                   
                    switch (request.Type)
                    {
                        case RequestType.RESOURCES_FOR_LANGUAGES:                            
                            HandleResourcesForLanguagesRequest(request.Params, sender, configInfo);
                            break;
                        case RequestType.CHANGED_LANGUAGES:
                            HandleChangedLanguagesRequest(request.Params, sender, configInfo);
                            break;
                        case RequestType.ALL_AVAILABLE_LANGUAGES:
                            HandleAllAvailableLanguages(sender,configInfo);
                            break;
                        default:
                            HandleUnknownTypeRequest(sender);
                            break;
                    }

                }

                Console.WriteLine(DateTime.Now);

            }
            catch (InvalidParamInConfigFileException ex)
            {
                Console.WriteLine("Invalid format of congig info txt file!");
                Console.WriteLine(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }           
        }

        private static void HandleAllAvailableLanguages(TcpClient sender, ConfigInfo configInfo)
        {
            List<string> languages = new List<string>();
            string fileName = configInfo.DetectedChangesFileLocation + "languages.json";
            if (File.Exists(fileName))
            {
                languages = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(fileName).Trim());
            }            

            ResponseAllAvailableLanguages response = new ResponseAllAvailableLanguages(ResponseStatus.OK, RequestType.ALL_AVAILABLE_LANGUAGES, languages);
            byte[] bytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(response));
            sender.GetStream().Write(bytes, 0, bytes.Length);
        }

        private static void HandleUnknownTypeRequest(TcpClient sender)
        {
            Response response = new ResponseUnknown();
            byte[] bytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(response));
            sender.GetStream().Write(bytes, 0, bytes.Length);
        }

        private static void HandleChangedLanguagesRequest(List<string> senderLanguages, TcpClient sender, ConfigInfo configInfo)
        {
            if (senderLanguages == null)
            {
                HandleUnknownTypeRequest(sender);
                return;
            }

            List<string> changed = new List<string>();
            string fileName = configInfo.DetectedChangesFileLocation + "changes.json";
            if (File.Exists(fileName))
            {
                changed = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(fileName).Trim());
            }
            
            ResponseChangedLanguages response = new ResponseChangedLanguages(ResponseStatus.OK, RequestType.CHANGED_LANGUAGES, changed);
            byte[] bytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(response));
            sender.GetStream().Write(bytes, 0, bytes.Length);
        }

        private static void HandleResourcesForLanguagesRequest(List<string> languages, TcpClient sender, ConfigInfo configInfo)
        {
            if (languages == null)
            {
                HandleUnknownTypeRequest(sender);
                return;
            }

            Dictionary<string, List<string>> resourcesForLanguages = new Dictionary<string, List<string>>();
            foreach (var language in languages)
            {
                string fileName = configInfo.JsonFilesDestinationFolder + language + ".json";
                if (File.Exists(fileName))
                {
                    LanguageWithResources lwr = JsonConvert.DeserializeObject<LanguageWithResources>(File.ReadAllText(fileName).Trim());
                    resourcesForLanguages.Add(lwr.Name, lwr.Resources);                  
                }                
            }

            ResponseResourcesForLanguages response = new ResponseResourcesForLanguages(ResponseStatus.OK, RequestType.RESOURCES_FOR_LANGUAGES, resourcesForLanguages);
            byte[] bytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(response));
            sender.GetStream().Write(bytes, 0, bytes.Length);
        }

        private static string cleanMessage(byte[] bytes)
        {
            string message = Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }


    }
}
