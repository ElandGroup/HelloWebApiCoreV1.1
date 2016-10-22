using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HelloWebApiCoreV2.Context
{
    public class DapperContext
    {
        private DapperContext()
        { }
        private static DapperContext current;
        public static DapperContext Current
        {
            get
            {
                if (current == null)
                    current = new DapperContext();
                return current;
            }
        }

        public IConfigurationRoot Configuration { get; set; }
    }
}
