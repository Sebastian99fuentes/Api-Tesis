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
        public DbSet<Comments> Comments {get; set; } 

        public DbSet<Implemento> Implemento {get; set; } 

        public DbSet<Horario> Horario {get; set; }

        public DbSet<Reserva> Reserva {get; set;}

        public DbSet<ReservaAreas> ReservaAreas {get; set;}

         public DbSet<ReservasImplementos> ReservasImplementos {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
                builder.Entity<Comments>()
        .HasOne(c => c.Area)
        .WithMany(a => a.Comments)
        .HasForeignKey(c => c.AreaId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comments>()
        .HasOne(c => c.Implemento)
        .WithMany(i => i.Comments)
        .HasForeignKey(c => c.ImplementoId)
        .OnDelete(DeleteBehavior.Cascade);

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
                Id = "1", // ID único
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
                    UserId = "1",
                    RoleId = "1" // Admin Role
                }
            );
   // Configuración para almacenar CreatedOn como UTC
            builder.Entity<Comments>()
                .Property(c => c.CreatedOn)
                .HasConversion(
                    v => v.ToUniversalTime(), // Almacena en UTC
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Devuelve en UTC
                );


        }

    }
}