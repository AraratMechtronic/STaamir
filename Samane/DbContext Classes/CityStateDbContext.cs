namespace Samane
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CityStateDbContext : DbContext
    {
        public CityStateDbContext()
            : base("name=SamaneDbWithCityConnectionString")
        {
        }

        public virtual DbSet<T_City> T_City { get; set; }
        public virtual DbSet<T_State> T_State { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
