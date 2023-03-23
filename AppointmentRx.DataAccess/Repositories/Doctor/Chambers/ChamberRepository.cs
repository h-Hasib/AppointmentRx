using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using AppointmentRx.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Chambers
{
    public class ChamberRepository:IChamberRepositoy
    {
        private readonly PortalDbContext _dbContext;

        public ChamberRepository(PortalDbContext context)
        {
            _dbContext = context;
        }
        
        public async Task<HttpResponseModel> Create(DoctorChamberScheduleDto model)
        {
            var id = "bd1d4d84-e4aa-466d-943a-b19cabad8308";

            Chamber chambers = new Chamber();

            chambers.Name = model.Name;
            chambers.Address = model.Address;
            chambers.Fees = model.Fees;
            chambers.OpeningTime = model.OpeningTime;
            chambers.ClosingTime = model.ClosingTime;
            chambers.DoctorId = id;

            await _dbContext.Chambers.AddAsync(chambers);
            await _dbContext.SaveChangesAsync();

            var chamberId = chambers.Id;

            Schedule c = new Schedule();

            c.Saturday = model.Saturday;
            c.Friday = model.Friday;
            c.Tuesday = model.Tuesday;
            c.Wednesday = model.Wednesday;
            c.Sunday = model.Sunday;
            c.Thursday = model.Thursday;
            c.Monday = model.Monday;
            c.ChamberId = chamberId;

            await _dbContext.Schedules.AddAsync(c);
            await _dbContext.SaveChangesAsync();

            return new HttpResponseModel(model);
        }



        public async Task<HttpResponseModel> Update(int Id ,DoctorChamberScheduleDto model)
        {

            var chamberdata = await _dbContext.Chambers.FirstOrDefaultAsync(x => x.Id == Id);
            var scheduledata = await _dbContext.Schedules.FirstOrDefaultAsync(x => x.ChamberId == chamberdata.Id);

            if(chamberdata == null)
            {
                return new HttpResponseModel(null,false,"Chamber Not Found") ;
            }

            chamberdata.Name = model.Name;
            chamberdata.Address = model.Address;
            chamberdata.Fees = model.Fees;

            _dbContext.Update(chamberdata);
            await _dbContext.SaveChangesAsync();

            var scheduleId = chamberdata.Id;

            scheduledata.Saturday=model.Saturday;
            scheduledata.Sunday=model.Sunday;
            scheduledata.Monday=model.Monday;
            scheduledata.Tuesday=model.Tuesday;
            scheduledata.Wednesday=model.Wednesday;
            scheduledata.Thursday=model.Thursday;
            scheduledata.Friday=model.Friday;

            scheduledata.ChamberId = scheduleId;

            _dbContext.Update(scheduledata);
            await _dbContext.SaveChangesAsync();

            return new HttpResponseModel(model);
        }

        public async Task<HttpResponseModel> Delete(int Id)
        {
             
            var chamberdata = await _dbContext.Chambers.FirstOrDefaultAsync(x => x.Id == Id);
            var scheduledata = await _dbContext.Schedules.FirstOrDefaultAsync(x => x.ChamberId == chamberdata.Id);

            if (chamberdata == null)
            {
                return new HttpResponseModel(null, false, "Chamber Not Deleted");
            }

            _dbContext.Chambers.Remove(chamberdata);
            await _dbContext.SaveChangesAsync();

            return new HttpResponseModel(chamberdata);
        }

        public async Task<HttpResponseModel> GetList()
        {
            var data = (from c in _dbContext.Chambers
                       join s in _dbContext.Schedules on c.Id equals s.ChamberId
                       select new DoctorChemberViewModel()
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Address = c.Address,
                           Fees = c.Fees,

                           Friday = s.Friday,
                           Saturday = s.Saturday,
                           Sunday = s.Sunday,
                           Monday = s.Monday,
                           Tuesday = s.Tuesday,
                           Wednesday = s.Wednesday,
                           Thursday = s.Thursday,
                       }).ToList();
            return new HttpResponseModel(data);
        }



    }
}
