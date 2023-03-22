//using AppointmentRx.DataAccess.Entitites;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppointmentRx.DataAccess.Data
//{
//    public class SecurityDbContext : IdentityDbContext<PortalUser>
//    {
//        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
//            : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.Entity<PortalUser>().ToTable("PortalUser");
//            //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
//        }
//    }
//}
