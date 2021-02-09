using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class ResponseResourcesForLanguages: Response
    {
        Dictionary<string, List<string>> ResourcesForLanguages { get; }

        public ResponseResourcesForLanguages(ResponseStatus status)
        {
            Status = status;
            Type = RequestType.ResourcesForLanguages;
            ResourcesForLanguages = new Dictionary<string, List<string>>();
        }

        public ResponseResourcesForLanguages(ResponseStatus status, Dictionary<string, List<string>> parameters)
        {
            Status = status;
            Type = RequestType.ResourcesForLanguages; 
            ResourcesForLanguages = parameters;
        }
    }
}
