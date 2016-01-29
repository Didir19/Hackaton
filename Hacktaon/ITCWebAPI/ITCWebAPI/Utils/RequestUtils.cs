using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;

namespace ITCWebAPI.Utils
{
    public static class RequestUtils
    {
        public static A AnalyzeResponse<A>(WebResponse response)
        {
            A objectResponse;
            var dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            using (var reader = new StreamReader(dataStream))
            {
                var ser = new DataContractJsonSerializer(typeof(A));
                objectResponse = (A)ser.ReadObject(reader.BaseStream);
            }
            return objectResponse;
        }
    }
}