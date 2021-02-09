using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class Request
    {
        public RequestType Type { get; set; }
        public List<string> Params { get; set; }

        public Request(RequestType type, List<string> parameters) 
        {
            Type = type;
            Params = parameters;
        }
    }
}
