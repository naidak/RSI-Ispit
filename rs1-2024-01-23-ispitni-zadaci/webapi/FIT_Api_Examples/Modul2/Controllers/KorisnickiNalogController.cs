using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FIT_Api_Examples.Modul2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnickiNalogController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public KorisnickiNalogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut]
        [Route("/DodajDeafultOpstine")]
        public ActionResult DodajDefaultOpstine()
        {
            foreach (var item in _dbContext.KorisnickiNalog)
            {
                item.defaultOpstinaId = 10;
            }
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
