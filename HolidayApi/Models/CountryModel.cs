using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicHoliday;

namespace HolidayApi.Models
{
    public class CountryPublicHolidayModel
    {
        public IPublicHolidays CountryPublicHoliday { get; set; }

        public string CountryCode { get; set; }

        public string State { get; set; }
    }
}
