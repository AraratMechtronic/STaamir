namespace Samane.Migrations.ApplicationDbContext
{
    using Samane.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Samane.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
        }

        protected override void Seed(Samane.Models.ApplicationDbContext context)
        {

            //  This method will be called after migrating to the latest version.
            #region Adding roles in intializing database

            if (!context.Roles.Where(i => i.Name.Contains("AdminUser") && i.Name.Contains("HospitalUsers") && i.Name.Contains("EngineerUsers")).Any())//if none of these exist pleasse add
            {
                if (!context.Roles.Where(i => i.Name.Contains("AdminUser")).Any())
                    context.Roles.Add(new ApplicationRole() { Name = "AdminUser", Id = "1", PersianRoelName = "ادمین" });

                if (!context.Roles.Where(i => i.Name.Contains("HospitalUsers")).Any())
                    context.Roles.Add(new ApplicationRole() { Name = "HospitalUsers", Id = "2", PersianRoelName = "کاربر کارشناس علوم آزمایشگاهی" });

                if (!context.Roles.Where(i => i.Name.Contains("EngineerUsers")).Any())
                    context.Roles.Add(new ApplicationRole() { Name = "EngineerUsers", Id = "3", PersianRoelName = "کاربر مهندس تجهیزات پزشکی" });
                context.SaveChanges();
            }


            #endregion Adding roles in intializing database

            #region Add Admin Developer User Ararad@admin.com

            if (!context.Users.Where(i => i.UserName.Contains("Ararad@admin.com")).Any())
            {
                //var user = new ApplicationUser { City = "Tehran", Province = "Tehran", PhoneNumber = "02188305969", NameAndLastName = "Ararad", Email = "Ararad@admin.com", UserName = "Ararad@admin.com", userRole = "AdminUser", PasswordHash = "AFkS6uU09U8vLVAL6by8safKLgNwVjgm3/eYxeDz3Rt4ZXt7+5a1AYskgYjdB6YhFQ==", SecurityStamp = "fbe7a723-aeb3-4305-b9e0-a4af863b8fc6", LockoutEnabled = true };
                //context.Users.Add(user);
                //context.SaveChanges();
                context.Database.ExecuteSqlCommand("INSERT INTO[dbo].[AspNetUsers] ([Id], [userRole], [NameAndLastName], [Province], [City], [PhoneNumber], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ExpertInInstruments], [YearsOfWork], [HospitalName], [Discriminator]) VALUES(N'de683252-aefc-4e09-b876-1ba448a92950', N'AdminUser', N'Ararad', N'Tehran', N'Tehran', N'02188305969', N'Ararad@admin.com', 0, N'AFkS6uU09U8vLVAL6by8safKLgNwVjgm3/eYxeDz3Rt4ZXt7+5a1AYskgYjdB6YhFQ==', N'fbe7a723-aeb3-4305-b9e0-a4af863b8fc6', 0, 0, NULL, 1, 0, N'Ararad@admin.com', NULL, NULL, NULL, N'ApplicationUser')");
                context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES ((Select top 1 Id from [AspNetUsers] where [UserName]=N'Ararad@admin.com') , (Select top 1 Id from AspNetRoles where [Name]='AdminUser'))");
                context.SaveChanges();
            }

            #endregion Add Admin Developer User Ararad@admin.com

           
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
        }
    }
}
