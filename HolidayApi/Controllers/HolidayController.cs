using EdjCase.JsonRpc.Router;
using HolidayApi.Models;
using HolidayApi.Services;
using System.Collections.Generic;

namespace HolidayApi.Controllers
{
    [RpcRoute]
    public class HolidayController : RpcController
    {
        public HolidayService holidayService;
        public HolidayController()
        {
            holidayService = new HolidayService();
        }

        public PublicHolidayResponseModel PublicHolidays(int start, int end)
        {
            return holidayService.GetAllPublicHolidays(start, end);
        }
    }
}