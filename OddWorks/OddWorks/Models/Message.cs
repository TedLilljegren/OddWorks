using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OddWorks.Models
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int Id { get; set; }
        [Required]
        [DataMember(IsRequired = true)]
        public string Content { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
    }
}