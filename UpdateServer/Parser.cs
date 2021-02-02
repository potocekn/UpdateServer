using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UpdateServer
{
    static class Parser
    {
        public static ConfigInfo Parse(string path)
        {
            ConfigInfo result = new ConfigInfo();

            using (StreamReader file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string beginning = line.Split("=")[0].Trim();
                    string param = line.Split("=")[1].Trim();

                    switch (beginning)
                    {
                        case "availableResourcesCheckExeLocation": 
                            result.AvailableResourcesCheckExeLocation = param;
                            break;
                        case "configInfo":
                            result.ConfigInfoForScriptLocation = param;
                            break;
                        case "jsonLocation":
                            result.JsonFilesFolder = param;
                            break;
                        case "whereToSaveChanges":
                            result.WhereToSaveChangedResources = param;
                            break;
                        default:
                            throw new InvalidParamInConfigFileException(beginning);                            
                    }
                }
            }

            return result;
        }
    }
}
