using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class ResponseUnknown: Response
    {
        public ResponseUnknown()
        {
            Status = ResponseStatus.INVALID;
        }
    }
}
