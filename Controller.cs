using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using System.Data;
using System.Data.OleDb;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        List<Cabinet> cabinets = new List<Cabinet>();
        List<Schedule> schedules = new List<Schedule>();
        List<Doctor> doctors = new List<Doctor>();
        List<Patient> patients = new List<Patient>();

        DataSet dataSet = new DataSet();
        DataTable table;

        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Ace.OLEDB.12.0;Data Source=C:\Users\gizat\Desktop\University\Exam\db.accdb");
        OleDbDataAdapter adapter;

        public DataSet FillingFromTableCabinets()
        {
            OleDbDataAdapter adapterCabinets = new OleDbDataAdapter("SELECT * FROM DictCabinet", connection);
            adapterCabinets.Fill(dataSet, "cabinets");

            return dataSet;
        }

        public DataSet FillingFromTableDoctors()
        {
            OleDbDataAdapter adapterDoctors = new OleDbDataAdapter("SELECT * FROM DictDoctors", connection);
            adapterDoctors.Fill(dataSet, "doctors");

            return dataSet;
        }

        public DataSet FillingFromTableSchedules()
        {
            OleDbDataAdapter adapterSchedules = new OleDbDataAdapter("SELECT * FROM DoctorSchedule", connection);
            adapterSchedules.Fill(dataSet, "schedules");

            return dataSet;
        }

        public DataSet FillingFromTablePatients()
        {
            OleDbDataAdapter adapterPatients = new OleDbDataAdapter("SELECT * FROM Patients", connection);
            adapterPatients.Fill(dataSet, "patients");

            return dataSet;
        }

        public ActionResult Index()
        {
            dataSet = FillingFromTableCabinets();
            DataTable tableCabinets = dataSet.Tables["cabinets"];

            
            var query_cabinets = from CabinetsRow in tableCabinets.AsEnumerable() orderby CabinetsRow.ItemArray[0] select CabinetsRow;
            DataRow[] resultCabinets = query_cabinets.ToArray();

            for (int i = 0; i < resultCabinets.Length; i++)
            {
                
                cabinets.Add(
                    new Cabinet {
                        ID = (int)resultCabinets[i]["ID"],
                        Number = (string)resultCabinets[i]["Number"],
                        Description  = (string)resultCabinets[i]["Description"]
                    }
                ); 
            }

            IEnumerable<Cabinet> cb = cabinets.AsEnumerable<Cabinet>();
            ViewBag.Cabinets = cb;

            return View();
        }

        [HttpGet]
        public ActionResult Schedule(int ID)
        {
            dataSet = FillingFromTableSchedules();
            DataTable tableSchedules = dataSet.Tables["schedules"];

            
            var query_schedules =
                from SchedulesRow in tableSchedules.AsEnumerable()
                where (int)SchedulesRow.ItemArray[2] == ID
                orderby SchedulesRow.ItemArray[0]
                select SchedulesRow;

            DataRow[] resultSchedules = query_schedules.ToArray();

            for (int i = 0; i < resultSchedules.Length; i++)
            {
                
                schedules.Add(
                    new Schedule
                    {
                        ID = (int)resultSchedules[i]["ID"],
                        DoctorID = (int)resultSchedules[i]["DoctorID"],
                        CabinetId = (int)resultSchedules[i]["CabinetId"],
                        EventTypeID = (int)resultSchedules[i]["EventTypeID"],
                        EventBegin = (DateTime)resultSchedules[i]["EventBegin"],
                        EventDuration = (int)resultSchedules[i]["EventDuration"],
                        PatientID = (int)resultSchedules[i]["PatientID"],
                        IsActive = (bool)resultSchedules[i]["IsActive"],
                        IsReady = (bool)resultSchedules[i]["IsReady"],
                        IsLiveQueue = (bool)resultSchedules[i]["IsLiveQueue"]
                    }
                );
            }

        IEnumerable<Schedule> sd = schedules.AsEnumerable<Schedule>();
            ViewBag.Schedules = sd;

            return View();
        }

        [HttpGet]
        public ActionResult Doctor(int id)
        {

                dataSet = FillingFromTableDoctors();
                DataTable tableDoctors = dataSet.Tables["doctors"];

                var query_doctors = from DoctorsRow in tableDoctors.AsEnumerable() where (int)DoctorsRow.ItemArray[0] == id orderby DoctorsRow.ItemArray[0] select DoctorsRow;
                DataRow[] resultDoctors= query_doctors.ToArray();

                for (int i = 0; i < resultDoctors.Length; i++)
                {
                    doctors.Add(
                        new Doctor
                        {
                            ID = (int)resultDoctors[i]["ID"],
                            LastName = (string)resultDoctors[i]["LastName"],
                            FirstName = (string)resultDoctors[i]["FirstName"],
                            MiddleName = (string)resultDoctors[i]["MiddleName"],
                            Division = (int)resultDoctors[i]["Division"],
                            EmployeeClass = (int)resultDoctors[i]["EmployeeClass"],
                            Positions = (string)resultDoctors[i]["Positions"],
                            Specialities = (int)resultDoctors[i]["Specialities"],
                            CabinetID = (int)resultDoctors[i]["CabinetID"],
                            IsVisible = (bool)resultDoctors[i]["IsVisible"]
                        }
                    );
                }
                IEnumerable<Doctor> pt = doctors.AsEnumerable<Doctor>();
                ViewBag.Info = pt;

            
            return View();
        }

        [HttpGet]
        public ActionResult Patient(int id, string type)
        {

                dataSet = FillingFromTablePatients();
                DataTable tablePatients = dataSet.Tables["patients"];

                var query_patients = from PatientsRow in tablePatients.AsEnumerable() where (int)PatientsRow.ItemArray[0] == id orderby PatientsRow.ItemArray[0] select PatientsRow;
                DataRow[] resultPatients = query_patients.ToArray();

                for (int i = 0; i < resultPatients.Length; i++)
                {
                    patients.Add(
                        new Patient
                        {
                            PatientID = (int)resultPatients[i]["PatientID"],
                            LastName = (string)resultPatients[i]["LastName"],
                            FirstName = (string)resultPatients[i]["FirstName"],
                            MiddleName = (string)resultPatients[i]["MiddleName"],
                            WorkPlace = (string)resultPatients[i]["WorkPlace"],
                            WorkPost = (string)resultPatients[i]["WorkPost"],
                            Proffesion = (string)resultPatients[i]["Proffesion"],
                            SocStatus = (int)resultPatients[i]["SocStatus"],
                            FamilyStatus = (int)resultPatients[i]["FamilyStatus"],
                            Citizenship = (int)resultPatients[i]["Citizenship"],
                            Phone = (string)resultPatients[i]["Phone"],
                            PhoneWork = (string)resultPatients[i]["PhoneWork"],
                            DopInfo = (string)resultPatients[i]["DopInfo"],
                            BloodType = (int)resultPatients[i]["BloodType"],
                            BloodRh = (int)resultPatients[i]["BloodRh"],
                        }
                    );
                 }
                IEnumerable<Patient> pt = patients.AsEnumerable<Patient>();
                ViewBag.Info = pt;

           
            return View();
        }
    }
}
