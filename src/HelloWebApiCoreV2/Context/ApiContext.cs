using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HelloWebApiCoreV2.Context
{
    public class ApiContext
    {
        private ApiContext()
        { }
        private static ApiContext current;
        public static ApiContext Current
        {
            get
            {
                if (current == null)
                    current = new ApiContext();
                return current;
            }
        }

        public IConfigurationRoot Configuration { get; set; }
    }
}
