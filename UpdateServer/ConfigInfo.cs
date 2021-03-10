using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class ConfigInfo
    {
        public string ScriptExeLocation { get; set; }
        public string ScriptConfigFileLocation { get; set; }
        public string JsonFilesDestinationFolder { get; set; }
        public string DetectedChangesFileLocation { get; set; }
        public string ChangesTxtFile { get; set; }
        public string LanguagesTxtFile { get; set; }
               
    }
}
