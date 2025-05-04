using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Infrastructure;
using RepositoryPattern.Models;

namespace RepositoryPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployee _employee;

        public HomeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employee.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _employee.Insert(employee);
            _employee.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var employee = _employee.GetById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            var existingEmployee = _employee.GetById(employee.Id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            _employee.Delete(existingEmployee);
            _employee.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            var employee = _employee.GetById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            var existingEmployee = _employee.GetById(employee.Id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.City = employee.City;
            _employee.Update(existingEmployee);
            _employee.Save();

            return RedirectToAction("Index");
        }
    }
}
