using KendoGridEmp.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KendoGridEmp.Controllers
{
    public class EmpController : Controller
    {
        private readonly EmpDbContext context;

        public EmpController(EmpDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetData()
        {
            var result = context.Employees.ToList();
            return Json(result);
        }
        public IActionResult Create(Employee model) 
        {
            var data = model;
            context.Employees.Add(model);
            context.SaveChanges();
            return new JsonResult(data);
        }
        public IActionResult Edit(Employee model)
        {
            context.Employees.Update(model);
            context.SaveChanges();
            return new JsonResult(model);
        }
        public IActionResult Delete(Employee model ) 
        {
            context.Employees.Remove(model);
            context.SaveChanges();
            return Json(model);
        }
    }
}