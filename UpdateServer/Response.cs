using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    abstract class Response
    {
        public ResponseStatus Status { get; protected set; }
        public RequestType Type { get; protected set; }
    }
}
