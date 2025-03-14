using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DAL.BogusHandling
{
    public static class ProfileDataSeed
    {
        public static void SeedProfiles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserProfile>().HasData( new AppUserProfile {
                ID = 1,
                FirstName = "Kadir",
                LastName = "Güngör"
            });

            modelBuilder.Entity<AppUserProfile>().HasData(new AppUserProfile
            {
                ID = 2,
                FirstName = "Mehmet",
                LastName = "Güngör"
            });

        }
    }
}
