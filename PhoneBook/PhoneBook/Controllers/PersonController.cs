using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Helpers;
using PhoneBook.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "The Phone Book | All contacts in one place";
            ViewBag.Home = "active";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string lastName)
        {
            try
            {
                if (lastName.Length < 65 && lastName != "" && lastName != null)
                {
                    ViewBag.Title = "Search Result | The Phone Book";
                    ViewBag.SearchLastName = lastName;
                    ViewBag.Subtitle = $"Search by the surname: {ViewBag.SearchLastName}";
                    var searchList = SourceManager.GetByLastName(lastName);
                    return View("List", searchList);
                }
            }
            catch (NullReferenceException)
            {
                TempData["WrongData"] = "Wrong data. Try again.";
                return View();
            }
            catch (ArgumentNullException)
            {
                TempData["WrongData"] = "Wrong data. Try again.";
                return View();
            }
            catch (Exception)
            {
                TempData["WrongData"] = "Wrong data. Try again.";
                return View();
            }

            return View();
        }

        public IActionResult List(int page = 1)
        {
            ViewBag.PersonId = page;
            ViewBag.Title = "List of all entries | The Phone Book";
            ViewBag.Subtitle = "List of all entries";
            ViewBag.List = "active";
            return View(SourceManager.Get(page, 6));
        }

        public IActionResult SortedList(int page = 1)
        {
            ViewBag.Title = "Sorted list | The Phone Book";
            ViewBag.Subtitle = "Sorted list";
            ViewBag.SortedList = "active";
            var sortedList = SourceManager.SortedByLastName(page, 6).OrderBy(x => x.LastName).ToList();
            return View("SortedList", sortedList);
        }

        [HttpGet]
        public IActionResult Email()
        {
            ViewBag.Title = "Sorted list | The Phone Book";
            ViewBag.Subtitle = $"Search by email address";
            ViewBag.Email = "active";
            return View();
        }

        [HttpPost]
        public IActionResult Email(string email)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Search Result | The Phone Book";
                ViewBag.SearchEmail = email;
                ViewBag.Subtitle = $"Search by email address: {ViewBag.SearchEmail}";
                var searchList = SourceManager.GetByEmail(email);
                return View("List", searchList);
            }
            TempData["WrongData"] = "Wrong data. Try again.";
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add a new person | The Phone Book";
            ViewBag.Add = "active";
            return View();
        }

        [HttpPost]
        public IActionResult Add(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                SourceManager.Add(personModel);
                ViewBag.Title = "Add a new person | The Phone Book";
                TempData["added"] = "Successfully added!";
                return View();
            }
            return View(personModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ToEditID = id;
            ViewBag.Title = "Edit an entry | The Phone Book";
            return View(SourceManager.GetByID(id));
        }

        [HttpPost]
        public IActionResult Edit(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                SourceManager.Update(personModel);
                ViewBag.Title = "Edit an entry | The Phone Book";
                TempData["edited"] = "Successfully edited!";
                return View();
            }
            return View(personModel);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            ViewBag.ToRemoveId = id;
            ViewBag.Title = "Delete an entry | The Phone Book";
            return View(SourceManager.GetByID(id));
        }

        [HttpPost]
        public IActionResult RemoveConfirm(int id)
        {
            SourceManager.Remove(id);
            ViewBag.Title = "Delete an entry | The Phone Book";
            TempData["deleted"] = "Successfully deleted!";
            return View();
        }
    }
}
