using ElectronicRaffle.Data;
using ElectronicRaffle.Data.Repositories;
using ElectronicRaffle.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ElectronicRaffle.Web.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teacher(0)
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    ContactNumber = model.ContactNumber,
                    School = null
                };

                TeacherRepository.Insert(teacher);
            }

            return View(model);
        }
    }
}