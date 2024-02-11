using FIT_Api_Examples.Data;
using FIT_Api_Examples.Modul2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FIT_Api_Examples.Modul2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("/UpisGodine")]
        public ActionResult UpisGodine([FromBody] UpisGodine upisanaGodina)
        {
            if (_dbContext.UpisanaGodina.ToList().Exists(u => u.studentId == upisanaGodina.studentId && u.godinaStudija == upisanaGodina.godinaStudija))
            {
                if (upisanaGodina.obnova)
                {
                    var godina = new UpisanaGodina()
                    {
                        studentId = upisanaGodina.studentId,
                        akademskaGodinaId = upisanaGodina.akademskaGodinaId,
                        cijenaSkolarine = upisanaGodina.cijenaSkolarine,
                        godinaStudija = upisanaGodina.godinaStudija,
                        datumUpisa = upisanaGodina.datumUpisa,
                        obnova = upisanaGodina.obnova,
                        evidentiraoId = upisanaGodina.evidentiraoId,
                    };
                    _dbContext.UpisanaGodina.Add(godina);
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            else
            {
                var godina = new UpisanaGodina()
                {
                    studentId = upisanaGodina.studentId,
                    akademskaGodinaId = upisanaGodina.akademskaGodinaId,
                    cijenaSkolarine = upisanaGodina.cijenaSkolarine,
                    godinaStudija = upisanaGodina.godinaStudija,
                    datumUpisa = upisanaGodina.datumUpisa,
                    obnova = upisanaGodina.obnova,
                    evidentiraoId = upisanaGodina.evidentiraoId,
                };
                _dbContext.UpisanaGodina.Add(godina);
                _dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("/GetUpisaneById")]
        public ActionResult<List<UpisanaGodina>> GetUpisaneById([FromQuery] int  id)
        {
            var list = _dbContext.UpisanaGodina.Include("akademskaGodina").Include("evidentirao").Where(y=> y.studentId==id).ToList();
            return Ok(list);
        }

        [HttpPost]
        [Route("/OvjeriSemestar")]
        public ActionResult Ovjera([FromBody] OvjeraSemestra ovjeraSemestra)
        {
            var ug = _dbContext.UpisanaGodina.Where(x=>ovjeraSemestra.Id==x.Id).FirstOrDefault();
            ug.datumOvjere = ovjeraSemestra.datumOvjere;
            ug.napomena = ovjeraSemestra.napomena;
            return Ok();
        }
    }
}
