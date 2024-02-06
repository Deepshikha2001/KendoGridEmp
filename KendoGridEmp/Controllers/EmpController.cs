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

        //[HttpGet]
        //public IActionResult GetCategories()
        //{
        //    var categories = context.Categories.Select(c => new { categoryId = c.CategoryId, name = c.CategoryName }).ToList();
        //    return Json(categories);
        //}

        //public IActionResult GetData()
        //{
        //    var categories = (from c in context.Employees
        //                      join emp in context.Categories on c.Id equals emp.CategoryId
        //                      select new
        //                      {
        //                          categoryId = c.CategoryId,
        //                          name = c.CategoryName
        //                      }).ToList();
        //    return Json(categories);
        //}
        public IActionResult GetData()
        {
            var result = context.Employees.Include(e => e.Categories).ToList();
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = context.Categories.Select(c => new { categoryId = c.CategoryId, name = c.CategoryName }).ToList();
            return Json(categories);
        }

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
                                            CategoryId = c.CategoryId,
                                            CategoryName = c.CategoryName
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




































//using KendoGridEmp.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace KendoGridEmp.Controllers
//{
//    public class EmpController : Controller
//    {
//        private readonly EmpDbContext _context;

//        public EmpController(EmpDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult GetData()
//        {
//            var result = _context.Employees.Include(e => e.Categories).ToList();
//            return Json(result);
//        }

//        [HttpGet]
//        public IActionResult GetCategories()
//        {
//            var categories = _context.Categories.Select(c => new { categoryId = c.CategoryId, name = c.Name }).ToList();
//            return Json(categories);
//        }

//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            try
//            {
//                var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
//                if (employee == null)
//                    return NotFound();

//                _context.Employees.Remove(employee);
//                _context.SaveChanges();

//                return Json(employee);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error deleting employee: {ex.Message}");
//            }
//        }

//        [HttpPost]
//        public IActionResult Create(Employee model, int categoryId)
//        {
//            try
//            {
//                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
//                if (category == null)
//                    return BadRequest("Invalid category");

//                model.Categories = category;

//                _context.Employees.Add(model);
//                _context.SaveChanges();

//                return Json(model);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error creating employee: {ex.Message}");
//            }
//        }

//        [HttpPost]
//        public IActionResult Edit(Employee model)
//        {
//            try
//            {
//                var existingEmployee = _context.Employees.Include(e => e.Categories).FirstOrDefault(e => e.Id == model.Id);
//                if (existingEmployee == null)
//                    return NotFound("Employee not found");

//                existingEmployee.Name = model.Name;
//                existingEmployee.Age = model.Age;
//                existingEmployee.Contact = model.Contact;
//                existingEmployee.Designation = model.Designation;

//                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == model.Categories.CategoryId);
//                if (category == null)
//                    return BadRequest("Invalid category");

//                existingEmployee.Categories = category;

//                _context.SaveChanges();

//                return Ok("Record updated successfully.");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error updating employee: {ex.Message}");
//            }
//        }
//    }
//}
