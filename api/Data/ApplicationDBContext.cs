using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // public class ApplicationDBContext: DbContext
     public class ApplicationDBContext: IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) 
        : base(dbContextOptions)
        {
            
        }
        
        public DbSet<Area> Area {get; set; }

        public DbSet<Implemento> Implemento {get; set; } 

        public DbSet<ReservaAreas> ReservaAreas {get; set;}

         public DbSet<ReservasImplementos> ReservasImplementos {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            var adminRole = new IdentityRole
            {
                Id = "1", // Asegúrate de usar IDs únicas
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var userRole = new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            };

            builder.Entity<IdentityRole>().HasData(adminRole, userRole);

            // 2. Crear Usuario Admin
            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = "11111111-1111-1111-1111-111111111111", // ID único
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };

            // 3. Hashear la contraseña
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "AdminPassword123!");

            builder.Entity<AppUser>().HasData(adminUser);

            // 4. Asignar el Rol de Admin al Usuario
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "11111111-1111-1111-1111-111111111111",
                    RoleId = "1" // Admin Role
                }
            );


        }

    }
}