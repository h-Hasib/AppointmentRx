using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.Framework;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PortalDbContext _dbContext;
        private readonly UserManager<PortalUser> _userManager;
        private readonly IPatientProfileRepository _profileRepository;

        public AppointmentRepository(PortalDbContext dbContext, UserManager<PortalUser> userManager, IPatientProfileRepository patientProfile)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _profileRepository = patientProfile;
        }

        public async Task<HttpResponseModel> Create(DoctorAppointmentDto model)
        { 
            var doctorId = "bd1d4d84-e4aa-466d-943a-b19cabad8308";

            var patientId = "02ed34fd-34c0-4062-9df9-18d8cf9bf333";

            //var user = await _dbContext.PatientProfiles.FindAsync(patientId);

            var user = await _dbContext.PortalUsers.FirstOrDefaultAsync(x => x.Id == patientId);


            if (user == null)
            {
                //PatientRegistrationDto dto = new PatientRegistrationDto();
                var userEntity = new PortalUser
                {
                    UserName = model.PatientName,
                    PasswordHash=model.PhoneNumber,
                    RoleId = (int)ApplicationRole.Patient
                };
                var userCreateResponse = await _userManager.CreateAsync(userEntity);
                if (!userCreateResponse.Succeeded)
                    return new HttpResponseModel(data: userCreateResponse.Errors, success: false, message: "Internal server error.");

                var profile = new PatientProfile
                {
                    Id = userEntity.Id
                };
                await _profileRepository.Create(profile);
                await _dbContext.SaveChangesAsync();

                Appointment appointment = new Appointment();
                var paitentinfo = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == model.PatientName);
                var NewPatientId = await _profileRepository.GetById(paitentinfo.Id);

                appointment.Age = model.Age;
                appointment.PhoneNumber = model.PhoneNumber;
                appointment.SerialNumber = model.SerialNumber;

                appointment.ChamberId = model.ChamberId;
                appointment.PatientName = model.PatientName;
                appointment.DoctorId = doctorId;
                appointment.PatientId = NewPatientId.Id;

                return new HttpResponseModel(model);
            }
            else
            {
                Appointment appointment1 = new Appointment();

                appointment1.Age = model.Age;
                appointment1.PhoneNumber = model.PhoneNumber;
                appointment1.SerialNumber = model.SerialNumber;
                appointment1.ChamberId = model.ChamberId;
                appointment1.PatientId = patientId;
                appointment1.PatientName = model.PatientName;
                appointment1.DoctorId = doctorId;

                await _dbContext.Appointments.AddAsync(appointment1);

                if (_dbContext.SaveChanges() > 0)
                {
                    return new HttpResponseModel(model);
                }

                return new HttpResponseModel(null, false, "Appointment Not Created");
            }

            /*public async Task<HttpResponseModel> Create(Appointment appointment)
            {
                await _dbContext.Appointments.AddAsync(appointment);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return new HttpResponseModel(appointment);
                }
                return new HttpResponseModel(null, false, "Failed");
            }*/
        }
        public async Task<HttpResponseModel> Update(int Id, DoctorAppointmentDto model)
        {
            var data = await _dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == Id);

            if (data == null)
            {
                return new HttpResponseModel(null, false, "Not Found");
            }

            data.Age = model.Age;
            data.PhoneNumber = model.PhoneNumber;
            data.SerialNumber = model.SerialNumber;
            data.ChamberId = model.ChamberId;
            data.AppointmentTime = model.AppointmentTime;

            _dbContext.Appointments.Update(data);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return new HttpResponseModel(model);
            }

            return new HttpResponseModel(model, false, "not updated");
        }

        public async Task<HttpResponseModel> Delete(int Id)
        {
            var data = _dbContext.Appointments.FirstOrDefault(x => x.Id == Id);

            if (data == null)
            {
                new HttpResponseModel(null, false, "Appointment Not Deleted");
            }
            _dbContext.Appointments.Remove(data);

            await _dbContext.SaveChangesAsync();

            return new HttpResponseModel(data);
        }

        public async Task<HttpResponseModel> GetList()
        {
            var List = await _dbContext.Appointments.ToListAsync();

            return new HttpResponseModel(List);
        }




    }
}
