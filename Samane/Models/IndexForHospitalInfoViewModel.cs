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
     
        SamaneDbContext SamanehDB = new SamaneDbContext();
        private ApplicationUserForHospitals applicationUserForHospitals = new ApplicationUserForHospitals();
        private Hospital _hospital = new Hospital();
        public Instrument instrument = new Instrument();
        List<string> errorMessage = new List<string>();
        List<ApplicationUserForEngineers> EngineerList = new List<ApplicationUserForEngineers>();
        //List<Requests> requests = new List<Requests>();

        public Hospital hospital
        {
            get { return _hospital; }
            set { _hospital = value; }
        }

        public ApplicationUserForHospitals HospitalUser
        {
            get { return applicationUserForHospitals; }
            set
            {
                applicationUserForHospitals = value;
                GetHospitalInformation();
            }
        }
        public IndexForHospitalInfoViewModel(string username = null)
        {
            GetHospitalInformation(username);
            GetThisHospitalInstrument(this.hospital);
        }
        public void GetHospitalInformation(string username = null)
        {
            if (username is null && applicationUserForHospitals.UserName is null)
                return;

            string _username = (applicationUserForHospitals.UserName != null) 
                                ? applicationUserForHospitals.UserName
                                : username;

            if (this._hospital.UserNamee == null)
                this._hospital = SamanehDB.Hospitals
                    .Where(hos => hos.UserNamee == _username)
                    .Include(h => h.instruments)
                    .FirstOrDefault();

        }

        public List<Instrument> GetThisHospitalInstrument(Hospital thisHospital)
        {
            if (_hospital.instruments is null)
                GetHospitalInformation(username: thisHospital.UserNamee);
                 //_hospital.instruments =SamanehDB.Instruments.Where(ins => ins.UserName == thisHospital.UserNamee).ToList();
            return _hospital.instruments;
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
            //instrument = new Instrument() { InstrumentId = instrumentId };
            //SamanehDB.Entry<Instrument>(instrument).State = EntityState.Deleted;
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
                GetHospitalInformation();
            return this.hospital.instruments.Where(i => i.InstrumentId == insturmentID).FirstOrDefault();
        }

        //private Hospital GetHospitalInfo()
        //{
        //    var hospitalUsers = Userdb.Users.OfType<ApplicationUserForHospitals>().ToList();
        //    var userforhospital = hospitalUsers.FirstOrDefault(m => m.UserName == User.Identity.Name);
        //    var hospital = new Hospital
        //    {
        //        UserNamee = userforhospital.UserName,
        //        City = userforhospital.City,
        //        Province = userforhospital.Province,
        //        PhoneNumber = userforhospital.PhoneNumber,
        //        InChargePerson = userforhospital.NameAndLastName,
        //        HospitalName = userforhospital.HospitalName
        //    };
        //    var hospitalInDb = db.Hospitals.Find(userforhospital.UserName);
        //    if (hospitalInDb != null)
        //    {
        //        hospital.City = hospitalInDb.City;
        //        hospital.Province = hospitalInDb.Province;
        //    }

        //    return hospital;
        //}

    }
}