using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace JSONHelper
{
    public static partial class JSONHelper
    {

        public static HttpCookie DeserializeToCookie(string rawJSONString)
        {
            var cookie = JsonConvert.DeserializeObject<HttpCookie>(rawJSONString);

            return cookie;
        }



    }
}
