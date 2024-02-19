using KendoGridEmp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public IActionResult GetCategories()
        {
            var categories = context.Categories.Select(c => new { categoryId = c.CategoryId, name = c.CategoryName }).ToList();
            return Json(categories);
        }

        public IActionResult GetData()
        {
            var result = (from e in context.Employees
                          join c in context.Categories on e.CategoryId equals c.CategoryId
                          select new
                          {
                              id = e.Id,
                              name = e.Name,
                              age = e.Age,
                              contact = e.Contact,
                              designation = e.Designation,
                              //CategoryId = c.CategoryId,
                              categoryName = c.CategoryName
                          }).ToList();

            return  Json (result);
        }

        //public IActionResult GetData()
        //{
        //    var result = context.Employees.Include(e => e.Categories).ToList();
        //    return Json(result);
        //}

       

        public IActionResult Delete(int id)
        {
            var model = context.Employees.FirstOrDefault(e => e.Id == id);
            if (model != null)
            {
                context.Employees.Remove(model);
                context.SaveChanges();
                return Json(model);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Employee model, int categoryId)
        {
            var category = context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            model.Categories = category;

            context.Employees.Add(model);
            context.SaveChanges();

            var employeeWithCategory = (from e in context.Employees
                                        join c in context.Categories on e.CategoryId equals c.CategoryId
                                        where e.Id == model.Id
                                        select new 
                                        {
                                            EmployeeId = e.Id,
                                            EmployeeName = e.Name,
                                            EmployeeAge = e.Age,
                                            EmployeeContact = e.Contact,
                                            EmployeeDesignation = e.Designation,
                                            categoryId = c.CategoryId,
                                            categoryName = c.CategoryName
                                        }).FirstOrDefault();

            if (employeeWithCategory == null)
            {
                return BadRequest("Failed to retrieve created employee with category");
            }

            return Json(employeeWithCategory);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            if(model.CategoryId > 0)
            {
                context.Employees.Update(model);
                context.SaveChanges();               
            }
            return Json(model);
               
            
          
        }
    }
}


































