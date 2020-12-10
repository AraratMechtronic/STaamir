using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Samane.Models;
using System.Data.Entity.Migrations;

namespace Samane.DbContext_Classes
{
    public class SamaneDbContextInitializerForModelChange : CreateDatabaseIfNotExists<SamaneDbContext>
    {


        protected override void Seed(SamaneDbContext context)
        {
            //var x = new List<Hospital>();
            //var y = new List<Instrument>();

            //x = SeedDataForHospitalsInstruments.GetHospitals();

            //foreach (var i in x)
            //{
            //    context.Hospitals.AddOrUpdate(i);
            //}
            //context.SaveChanges();


            //y = SeedDataForHospitalsInstruments.GetInstruments(context);

            //foreach (var j in y)
            //{
            //    context.Instruments.AddOrUpdate(j);
            //}
            //context.SaveChanges();
        }
    }
}