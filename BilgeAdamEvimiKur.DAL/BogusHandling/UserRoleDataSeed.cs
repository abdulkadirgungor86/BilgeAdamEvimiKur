using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DAL.BogusHandling
{
    public static class UserRoleDataSeed
    {
        public static void SeedUsers(ModelBuilder modelBuilder)
        {
            AppRole adminRole = new AppRole()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            modelBuilder.Entity<AppRole>().HasData(adminRole);

            AppRole memberRole = new AppRole()
            {
                Id = 2,
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            modelBuilder.Entity<AppRole>().HasData(memberRole);

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();

            AppUser adminUser = new AppUser()
            {
                Id=1,
                UserName = "kdr123",
                Email = "kdr123@gmail.com",
                NormalizedEmail = "KDR123@GMAIL.COM",
                NormalizedUserName = "KDR123",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = passwordHasher.HashPassword(null,"kdr123")
            };
            modelBuilder.Entity<AppUser>().HasData(adminUser);

            AppUser memberUser = new AppUser()
            {
                Id = 2,
                UserName = "gungor123",
                Email = "gungor123@gmail.com",
                NormalizedEmail = "GUNGOR123@GMAIL.COM",
                NormalizedUserName = "GUNGOR123",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = passwordHasher.HashPassword(null, "gungor123")
            };
            modelBuilder.Entity<AppUser>().HasData(memberUser);

            IdentityUserRole<int> adminUserRole = new IdentityUserRole<int>()
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            };
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRole);


            IdentityUserRole<int> memberUserRole = new IdentityUserRole<int>()
            {
                RoleId = memberRole.Id,
                UserId = memberUser.Id
            };
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(memberUserRole);
        }
    }
}
