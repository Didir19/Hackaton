using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ITCWebAPI.Models
{
    [DataContract]
    public class AuthResponse
    {
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}