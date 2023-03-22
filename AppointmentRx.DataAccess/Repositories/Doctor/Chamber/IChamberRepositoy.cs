using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Chamber
{
    public interface IChamberRepositoy
    {
        Task<HttpResponseModel> Update(int Id, DoctorChamberScheduleDto model);
        Task<HttpResponseModel> Create(DoctorChamberScheduleDto model);

        //Task<HttpResponseModel> GetById(int Id);
        Task<HttpResponseModel> Delete(int Id);
        Task<HttpResponseModel> GetList();
    }
}
