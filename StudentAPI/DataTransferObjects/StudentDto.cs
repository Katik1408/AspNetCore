using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace StudentAPI.DataTransferObjects
{
    [DataContract]
    public class StudentDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public int? Age { get; set; }
        [DataMember]
        public int? Contact { get; set; }
        [DataMember]

        public string SocialMediaAccount { get; set; }
    }
}
