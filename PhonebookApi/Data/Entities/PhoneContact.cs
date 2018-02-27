using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PhonebookApi.Data.Entities
{
    public class PhoneContact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
