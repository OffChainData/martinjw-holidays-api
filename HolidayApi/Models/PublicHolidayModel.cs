using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayApi.Models
{
    public class PublicHolidayModel
    {
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
    }
 
}
