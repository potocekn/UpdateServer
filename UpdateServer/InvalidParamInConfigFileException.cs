using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateServer
{
    class InvalidParamInConfigFileException: Exception
    {
        public InvalidParamInConfigFileException(){}

        public InvalidParamInConfigFileException(string name): base(String.Format("Invalid parameter in config file: {0}", name)){}
    }
}
