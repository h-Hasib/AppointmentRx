using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Appointments
{
    public interface IAppointmentRepository
    {
        public Task<HttpResponseModel> Create(DoctorAppointmentDto model);
        public Task<HttpResponseModel> Update(int Id, DoctorAppointmentDto model);
        public Task<HttpResponseModel> Delete(int Id);
        public Task<HttpResponseModel> GetList();
        public Task<HttpResponseModel> GetDetails(int AppointmentId);
        public Task<HttpResponseModel> UpcomingAppointment();
        public Task<HttpResponseModel> TodayAppointment();
    }
}
