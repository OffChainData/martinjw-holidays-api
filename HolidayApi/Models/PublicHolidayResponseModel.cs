using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayApi.Models
{
    public class PublicHolidayResponseModel
    {
        public List<PublicHolidayModel> Data { get; set; }

        public List<string> Errors { get; set; }
    }
}
