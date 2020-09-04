using Samane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samane.DbContext_Classes
{
    public class SeedDataForHospitalsInstruments
    {
        public  static List<Hospital> GetHospitals()
        {
            var hospitals = new List<Hospital>()
            {
                new Hospital()
                {
                    HospitalName="Namazi",
                    Province="Fars",
                    City="Shiraz",
                    InChargePerson="Rezayi"
                },

                new Hospital()
                {
                    HospitalName="Motahari",
                    Province="Fars",
                    City="Shiraz",
                    InChargePerson="Merikhi"
                },

                new Hospital()
                {
                    HospitalName="Markaz Behdasht",
                    Province="Hamedan",
                    City="Nahavand",
                    InChargePerson="Ahmadvand"
                },

                new Hospital()
                {
                    HospitalName="Milad",
                    Province="Tehran",
                    City="Tehran",
                    InChargePerson="Kalate"
                },

            };

            return (hospitals);
        }       

        public static List<Instrument> GetInstruments(SamaneDbContext context)
        {
            var instruments = new List<Instrument>()
            {
                new Instrument()
                {
                    InstrumentId=1,
                    Category="Autochemistry",
                    Model="CS-400",
                    SerialNo="S1700400CS0156",
                    UserName=context.Hospitals.Find("Namazi").HospitalName,
                   
                },
                new Instrument()
                {
                    InstrumentId=2,
                    Category="Autochemistry",
                    Model="CS-400",
                    SerialNo="S1700400CS0166",
                    UserName=context.Hospitals.Find("Namazi").HospitalName,
                   
                },
                new Instrument()
                {
                    InstrumentId=3,
                    Category="Autochemistry",
                    Model="CS-800",
                    SerialNo="S1100800CS0176",
                    UserName=context.Hospitals.Find("Namazi").HospitalName,
                   
                },
                new Instrument()
                {
                    InstrumentId=4,
                    Category="Autochemistry",
                    Model="CS-1200",
                    SerialNo="S1601200CS0156",
                    UserName=context.Hospitals.Find("Namazi").HospitalName,
                   
                },

                new Instrument()
                {
                    InstrumentId=5,
                    Category="Autochemistry",
                    Model="CS-400",
                    SerialNo="S1100400CS0003",
                    UserName=context.Hospitals.Find("Motahari").HospitalName,
                   
                },
                new Instrument()
                {
                    InstrumentId=6,
                    Category="Autochemistry",
                    Model="CS-400",
                    SerialNo="S1100400CS0004",
                    UserName=context.Hospitals.Find("Motahari").HospitalName,
                 
                },
                new Instrument()
                {
                    InstrumentId=7,
                    Category="Autochemistry",
                    Model="CS-800",
                    SerialNo="S1100800CS0005",
                    UserName=context.Hospitals.Find("Motahari").HospitalName,
                    
                },


                new Instrument()
                {
                    InstrumentId=8,
                    Category="Autochemistry",
                    Model="CS-800",
                    SerialNo="S1100800CS0010",
                    UserName=context.Hospitals.Find("Milad").HospitalName,
                   
                },
                new Instrument()
                {
                    InstrumentId=9,
                    Category="Autochemistry",
                    Model="CS-800",
                    SerialNo="S1100800CS0011",
                    UserName=context.Hospitals.Find("Milad").HospitalName,
                    
                },

                new Instrument()
                {
                    InstrumentId=10,
                    Category="Autochemistry",
                    Model="CS-400",
                    SerialNo="S1100400CS0075",
                    UserName=context.Hospitals.Find("Markaz Behdasht").HospitalName,
                   
                },
            };

            return (instruments);
        }



    }
}