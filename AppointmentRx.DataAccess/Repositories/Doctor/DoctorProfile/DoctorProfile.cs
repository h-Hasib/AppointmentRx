using AutoMapper;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.DoctorProfile
{
    internal class DoctorProfile : CommonRepository<DoctorProfile>, IDoctorProfile
    {
        public DoctorProfile(DbContext dbContext) : base(dbContext)
        {
        }

    }
}
