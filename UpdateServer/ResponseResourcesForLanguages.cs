using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class ResponseResourcesForLanguages: Response
    {
        public Dictionary<string, List<string>> ResourcesForLanguages { get; set; }

        public ResponseResourcesForLanguages(ResponseStatus status, RequestType type, Dictionary<string, List<string>> parameters)
        {
            Status = status;
            Type = RequestType.ResourcesForLanguages; 
            ResourcesForLanguages = parameters;
        }
    }
}
