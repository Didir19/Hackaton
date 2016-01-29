using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ITCWebAPI.Models
{
    [DataContract]
    public class Timeline
    {
        [DataMember]
        public string created_at { get; set; }
        [DataMember]
        public string text { get; set; }
    }
}