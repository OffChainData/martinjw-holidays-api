using HolidayApi.Models;
using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayApi.Services
{
    public class HolidayService
    {
        public List<CountryPublicHolidayModel> publicHolidayHandlers;
        public List<string> canadaProvinces =  new List<string> { "AB", "BC", "MB", "NS", "ON", "PE", "SK", "NL", "QC", "YT", "NT", "NU" };
        
        public HolidayService()
        {
            publicHolidayHandlers = GetPublicHolidayHandlers();
        }


        public List<CountryPublicHolidayModel> GetPublicHolidayHandlers()
        {
            var publicHolidayHandlers =  new List<CountryPublicHolidayModel>()
            {
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new AustriaPublicHoliday(), CountryCode = "AT/AUT", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new BelgiumPublicHoliday(), CountryCode = "BE/BEL", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new CzechRepublicPublicHoliday(), CountryCode = "CZ/CZE", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new DutchPublicHoliday(), CountryCode = "NL/NLD", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new EcbTargetClosingDay(), CountryCode = "", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new FrancePublicHoliday(), CountryCode = "FR/FRA", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new IrelandPublicHoliday(), CountryCode = "IE/IRL", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new ItalyPublicHoliday(), CountryCode = "IT/ITA", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new JapanPublicHoliday(), CountryCode = "JP/JPN", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new KazakhstanPublicHoliday(), CountryCode = "KZ/KAZ", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new NewZealandPublicHoliday(), CountryCode = "NZ/NZL", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new NorwayPublicHoliday(), CountryCode = "NO/NOR", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new PolandPublicHoliday(), CountryCode = "PL/POL", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new SlovakiaPublicHoliday(), CountryCode = "SK/SVK", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new SpainPublicHoliday(), CountryCode = "ES/ESP", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new SwedenPublicHoliday(), CountryCode = "SE/SWE", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new UKBankHoliday(), CountryCode = "GB/GBR", State = "" },
                new CountryPublicHolidayModel(){ CountryPublicHoliday = new USAPublicHoliday(), CountryCode = "US/USA", State = "" },
            };

            // Germany
            foreach (GermanPublicHoliday.States state in (GermanPublicHoliday.States[])Enum.GetValues(typeof(GermanPublicHoliday.States)))
            {
                if (state != GermanPublicHoliday.States.ALL)
                {
                    var handler = new GermanPublicHoliday();
                    handler.State = state;

                    var model = new CountryPublicHolidayModel() { CountryPublicHoliday = handler, CountryCode = "DE/DEU", State = state.ToString() };
                    publicHolidayHandlers.Add(model);
                }
            }

            // Australia
            foreach (AustraliaPublicHoliday.States state in (AustraliaPublicHoliday.States[])Enum.GetValues(typeof(AustraliaPublicHoliday.States)))
            {
                var handler = new AustraliaPublicHoliday();
                handler.State = state;

                var model = new CountryPublicHolidayModel() { CountryPublicHoliday = handler, CountryCode = "AU/AUS", State = state.ToString() };
                publicHolidayHandlers.Add(model);
            }

            // Canada
            foreach (var province in canadaProvinces)
            {
                var handler = new CanadaPublicHoliday(province);
                var model = new CountryPublicHolidayModel() { CountryPublicHoliday = handler, CountryCode = "CA/CAN", State = province };
                publicHolidayHandlers.Add(model);
            }

            publicHolidayHandlers.Add(new CountryPublicHolidayModel() { CountryPublicHoliday = new SwitzerlandPublicHoliday(hasSecondJanuary: true, hasLaborDay: true, hasCorpusChristi: true, hasChristmasEve: true, hasNewYearsEve: true), CountryCode = "CH/CHE", State = "" });
            
            return publicHolidayHandlers;
        }

        public List<PublicHolidayModel> GetPublicHolidaysOfYear(int year, ref List<string> errors)
        {
            var publicHolidays = new List<PublicHolidayModel>();
            foreach (var handler in publicHolidayHandlers)
            {
                try
                {
                    var holidays = handler.CountryPublicHoliday.PublicHolidayNames(year);
                    var publicHolidayList = holidays.Select(x => new PublicHolidayModel
                    {
                        Date = x.Key,
                        HolidayName = x.Value,
                        CountryCode = handler.CountryCode,
                        State = handler.State
                    }).ToList();
                    publicHolidays.AddRange(publicHolidayList);
                }
                catch (Exception ex)
                {
                    string name = handler.CountryPublicHoliday.ToString();
                    string handlerName = name.Substring(name.LastIndexOf('.') + 1);
                    errors.Add($"An exception occured in {handlerName}. Exception Message:{ex.Message}");
                }
            }

            return publicHolidays;
        }

        public PublicHolidayResponseModel GetAllPublicHolidays(int start, int end)
        {
            var allPublicHolidays = new List<PublicHolidayModel>();
            List<string> errors = new List<string>();
            for (int i = start; i <= end; i++)
            {
                allPublicHolidays.AddRange(GetPublicHolidaysOfYear(i, ref errors));
            }

            var response = new PublicHolidayResponseModel
            {
                Data = allPublicHolidays,
                Errors = errors
            };

            return response;
        }



    }
}
