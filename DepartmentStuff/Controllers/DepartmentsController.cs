using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentStuff.Models.ViewModels;
using DepartmentStuff.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DepartmentStuff.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IConfiguration _config;

        public DepartmentsController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Departments
        public ActionResult Index()
        {
            string sql = @" SELECT d.Id,
                                d.Name,
                                d.Budget
                            FROM Department d 
                        ";


            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Department> departments = new List<Department>();
                    while (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Budget = reader.GetInt32(reader.GetOrdinal("Budget"))
                         
                        };

                        departments.Add(department);
                    }

                    reader.Close();
                    return View(departments);
                }
            }
         
        }

        // GET: Departments/Details/5
        public ActionResult Details(int id)
        {
            DepartmentDetailsView detailsView = new DepartmentDetailsView();
            detailsView.department = null;
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                             SELECT d.Id,
                                d.Name,
                                d.Budget,
                                e.Id as EmployeeId,
                                e.FirstName,
                                e.LastName,
                                e.IsSuperVisor
                            FROM Department d
                            LEFT JOIN Employee e ON e.DepartmentId = d.Id
                            WHERE d.Id = @DepartmentId";
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (detailsView.department == null)
                        {
                            detailsView.department = new Department
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Budget = reader.GetInt32(reader.GetOrdinal("Budget"))
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("EmployeeId")))
                        {
                            detailsView.department.employees.Add(new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                IsSuperVisor = reader.GetBoolean(reader.GetOrdinal("IsSuperVisor"))

                            });
                        }
                    }
                    
                    var employeeSuperVisor = detailsView.department.employees.Find(e => e.Id == id);
                    detailsView.SuperVisor = $"{employeeSuperVisor.FirstName} {employeeSuperVisor.LastName}";
                    reader.Close();
                    return View(detailsView);
                }
            }
        }
    


        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        { 
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Departments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}