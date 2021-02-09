using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class ResponseChangedLanguages: Response
    {
       List<string> ChangedLanguages { get; }

        public ResponseChangedLanguages(ResponseStatus status)
        {
            Status = status;
            Type = RequestType.ChangedLanguages;
            ChangedLanguages = new List<string>();
        }

        public ResponseChangedLanguages(ResponseStatus status, List<string> parameters)
        {
            Status = status;
            Type = RequestType.ChangedLanguages;
            ChangedLanguages = parameters;
        }
    }
}
