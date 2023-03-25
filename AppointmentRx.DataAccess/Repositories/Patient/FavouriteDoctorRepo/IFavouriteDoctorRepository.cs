using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Patient.FavouriteDoctorRepo
{
    public interface IFavouriteDoctorRepository
    {
        Task<FavouriteDoctor> AddFavouriteDoctor(string patientId, string doctorId);
        Task<List<FavouriteDoctorListVM>> GetAllFavouriteDoctors(string patientId);
        Task<HttpResponseModel> DeleteFavouriteDoctor(string patientId, string doctorId);
        Task<FavouriteDoctor> AlreadyFavourite(string patientId, string doctorId);
    }
}
