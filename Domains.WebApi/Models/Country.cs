using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
namespace Domains.WebApi.Models
{
    public class Country
    {
        [DataMember]
        public string country { get; set; }
    }
}
