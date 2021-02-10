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
                    if (!line.Contains("\": \""))
                    {
                        continue;
                    }

                    string beginningBefore = line.Split(": ")[0].Trim();
                    string beginning = beginningBefore.Substring(1, beginningBefore.Length - 2);
                    string paramBefore = line.Split(": ")[1].Trim();
                    string param = paramBefore.Substring(1, paramBefore.Length - 3);

                    switch (beginning)
                    {
                        case "ScriptExeLocation": 
                            result.ScriptExeLocation = param;
                            break;
                        case "ScriptConfigFileLocation":
                            result.ScriptConfigFileLocation = param;
                            break;
                        case "TxtFilesDestinationFolder":
                            result.TxtFilesDestinationFolder = param;
                            break;
                        case "DetectedChangesFileLocation":
                            result.DetectedChangesFileLocation = param;
                            break;
                        default:
                            throw new InvalidParamInConfigFileException(beginning);                            
                    }
                }
            }

            result.ChangesTxtFile = result.DetectedChangesFileLocation + "changes.txt";
            result.LanguagesTxtFile = result.DetectedChangesFileLocation + "languages.txt";

            return result;
        }
    }
}
