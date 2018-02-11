using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HolidaysController : Controller
    {
        // GET: Holidays
        public ActionResult Index()
        {
            //return "This is my default action...";
            //List<Holiday> listOfHolidays = new List<Holiday> ();
            DbBroker showSelected = new DbBroker();
            List<Holiday> allSelected = showSelected.SelectAll();
            //Debug.WriteLine("vdvdsvf");

            //for (int i = 0; i < allSelected.Count(); i++)
            //{
            //    Debug.WriteLine(allSelected[i].Name);
            //}

            ViewBag.Holidays = allSelected;
       
            return View();


        }
        // Get: Holidays/AddNew
       
        public ActionResult AddNew() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveHoliday()
        {
            string holidayId = Request["holidayID"];
            string holidayName = Request["Name"];
            string holidayStart = Request["StartsAt"];
            string holidayEnd = Request["EndsAt"];
            // string holidayAnual = Request["OccursAnually"];
            bool holidayAnual = false;
            string holidayCheck = Request.Form["OccursAnnually"];
            Debug.WriteLine("Holiday check "+holidayCheck);
            //holidayCheck = "ftruedd";
            if (!string.IsNullOrEmpty(holidayCheck) && holidayCheck.Contains("on")) {
                holidayAnual = true;
            }
            string holidayDays = Request["DaysOff"];
            if (!String.IsNullOrEmpty(holidayId))
            {
                Debug.WriteLine("Update is on");

                DbBroker broker = new DbBroker();
                int updated = broker.Update(holidayName, DateTime.Parse(holidayStart), DateTime.Parse(holidayEnd), holidayAnual, Int32.Parse(holidayId));


            }
            else
            {
                DbBroker insertNew = new DbBroker();
                Debug.WriteLine(holidayAnual);
                int rowsAffected = insertNew.Insert(holidayName, DateTime.Parse(holidayStart), DateTime.Parse(holidayEnd), holidayAnual);
            }
           DbBroker showSelected = new DbBroker();
           List<Holiday> allSelected = showSelected.SelectAll();
            ViewBag.Holidays = allSelected;

            return View("Index");
        }

        public string DeleteHoliday()
        {
            string toDelete = Request["deleteHolidays"];
            string[] arrayDelete = toDelete.Split(',');
            for (int i = 0; i < arrayDelete.Length; i++)
            {
                DbBroker deletenew = new DbBroker();
                int deletedOnes = deletenew.Delete(Int32.Parse(arrayDelete[i]));
                Debug.WriteLine(toDelete);
            }

            return "OK";

        }

        public ActionResult UpdateHoliday() {

            string holidayId = Request["holidayid"];

            DbBroker broker = new DbBroker();
            Holiday selectedOne = broker.SelectOne(Int32.Parse(holidayId));
            Debug.WriteLine(selectedOne.Name);
            ViewBag.Holiday = selectedOne;

            return View("UpdateHoliday");

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete()
        //{
        //    DbBroker deleteSelected = new DbBroker();

        //    return View("Index");

        //}
    }
}