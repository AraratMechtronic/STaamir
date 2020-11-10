using Samane.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Samane.DbContext_Classes
{
    public class SamaneDbContext : DbContext
    {

        public SamaneDbContext() : base ("SamaneConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer<SamaneDbContext>(new DropCreateDatabaseAlways<SamaneDbContext>());
           
        }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

    }
}