using Samane.DbContext_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Samane.Models
{

    public class IndexForHospitalInfoViewModel
    {
        private SamaneDbContext SamanehDB = new SamaneDbContext();
        private ApplicationUserForHospitals applicationUserForHospitals = new ApplicationUserForHospitals();
        public Instrument instrument = new Instrument();
        List<string> errorMessage = new List<string>();
        List<ApplicationUserForEngineers> EngineerList = new List<ApplicationUserForEngineers>();

        public Hospital hospital { get; set; }
        public ApplicationUserForHospitals HospitalUser
        {
            get { return applicationUserForHospitals; }
            set
            {
                applicationUserForHospitals = value;
                FillhosPropFromDbtoViewModel();
            }
        }
        public IndexForHospitalInfoViewModel(string username = null)
        {
            FillhosPropFromDbtoViewModel(username);
            GetThisHospitalInstrument(this.hospital);
        }
        public void FillhosPropFromDbtoViewModel(string username = null)
        {
            if (username == null && applicationUserForHospitals.UserName == null)
                return;
            if (username!=null && applicationUserForHospitals.UserName == null)
            {
                applicationUserForHospitals.UserName = username;
            }
            this.hospital = new Hospital();
            if (this.hospital.UserNamee is null)
                this.hospital = SamanehDB.Hospitals
                    .Where(h => h.UserNamee == applicationUserForHospitals.UserName)
                    .Include(i => i.instruments)
                    .FirstOrDefault();
        }


        public List<Instrument> GetThisHospitalInstrument(Hospital thisHospital)
        {
            if (this.hospital.instruments is null)
                FillhosPropFromDbtoViewModel(username: thisHospital.UserNamee);
            return this.hospital.instruments;
        }

        public bool UpdateHospitalInfo(Hospital hospital, out List<string> errorMessage)
        {
            errorMessage = new List<string>();
            SamanehDB.Hospitals.AddOrUpdate(hospital);
            try
            {
                if (SamanehDB.SaveChanges() >= 0)
                    return true;
                else
                    return false;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                errorMessage.Add(ex.Message);
            }
            catch (Exception ex)
            {
                errorMessage.Add(ex.Message);
            }
            return false;
        }

        public bool DeleteInstrument(int instrumentId,out List<string> errorMessage)
        {
            errorMessage = new List<string>();
            instrument = SamanehDB.Instruments.Where(i => i.InstrumentId == instrumentId).FirstOrDefault();
            if (instrument == null)
            {
                errorMessage.Add("Its empty");
                return false;
            }
            SamanehDB.Instruments.Remove(instrument);

            try
            {
                if (SamanehDB.SaveChanges() >= 0)
                    return true;
                else
                    return false;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                errorMessage.Add(ex.Message);
            }
            catch (Exception ex)
            {
                errorMessage.Add(ex.Message);
            }
            return false;
        }

        public bool EditInstrument(Instrument updatedinstrument, out List<string> errorMessage)
        {
            errorMessage = new List<string>();
            bool isDataChanged = false;
            SamanehDB.Instruments.AddOrUpdate(updatedinstrument);
            try
            {
                if (SamanehDB.SaveChanges() >= 0)
                    isDataChanged=true;
                else
                    isDataChanged= false;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                errorMessage.Add(ex.Message);
            }
            catch (Exception ex)
            {
                errorMessage.Add(ex.Message);
            }
            if (isDataChanged)
            {
                this.hospital.instruments.Remove(updatedinstrument);
                this.hospital.instruments.Add(updatedinstrument);
            }
            return isDataChanged;
        }

        public Instrument GetHospitalInstrument(int insturmentID)
        {
            if (this.hospital.instruments is null)
                FillhosPropFromDbtoViewModel();
            return this.hospital.instruments.Where(i => i.InstrumentId == insturmentID).FirstOrDefault();
        }


    }
}