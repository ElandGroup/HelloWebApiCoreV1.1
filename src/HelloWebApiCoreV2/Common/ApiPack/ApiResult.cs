using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWebApiCoreV2.Common.ApiPack
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public IDictionary<string, object> Error { get; set; }
    }
}
