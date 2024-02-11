using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAll(string ime_prezime)
        {
            var data = _dbContext.Student
                .Include(s => s.opstina_rodjenja.drzava)
                .Where(x => (ime_prezime == null || (x.ime + " " + x.prezime).StartsWith(ime_prezime) || (x.prezime + " " + x.ime).StartsWith(ime_prezime)) && !x.obrisan)
                .OrderByDescending(s => s.id)
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpPost]
        [Route("/NoviStudent")]
        public ActionResult DodajStudenta([FromBody]NoviStudent student) {
            Student novi;
            if(student.id==0)
            {
                novi = new Student();
                _dbContext.Student.Add(novi);
            }
            else
                novi = _dbContext.Student.Find(student.id);
            novi.ime=student.ime;
            novi.prezime=student.prezime;
            novi.opstina_rodjenja_id = student.opstina_rodjenja_id;
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("/SoftDelete")]
        public ActionResult SoftDelete([FromBody] int id)
        {
            var std = _dbContext.Student.Where(s=> s.id == id).First();
            if (std != null)
            {
                std.obrisan = true;
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        [Route("/GetStudentById")]
        public ActionResult GetStudentById([FromQuery]int id)
        {
            var student = _dbContext.Student.Where(s => s.id == id).First();
            return Ok(student);
        }
    }
}
