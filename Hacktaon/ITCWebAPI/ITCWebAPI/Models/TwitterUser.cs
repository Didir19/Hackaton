using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ITCWebAPI.Models
{
    [DataContract]
    public class TwitterUser
    {
        [DataMember]
        public string id { get; set; }
    }
}