using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuerrillaAPI.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace GuerrillaAPI.Controllers
{
    [Route("[controller]")]
    //[AllowAnonymous]
    [ApiController]
    public class GuerrillaController : ControllerBase
    {
        private readonly GuerillaAppJLDContext _context;

        public GuerrillaController(GuerillaAppJLDContext context)
        {
            _context = context;
        }

        //[EnableCors("GetAllPolicy")]
        [HttpGet]
        public string GetAllGuerrillas()
        {
            try
            {
                //var students = _context.Student.FromSqlRaw($"SelectStudent").AsEnumerable();
                var guerrillas = _context.Guerrilla.ToList();
                return JsonConvert.SerializeObject(guerrillas);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{name}")]
        public string GetGuerrilla(string name)
        {
            try
            {
                SqlParameter studentId = new SqlParameter("@name", name);
                //var student = _context.Student.FromSqlRaw($"SelectStudentById @StudentId", studentId).AsEnumerable().Single();
                var guerrilla = _context.Guerrilla.Where(g => g.guerrillaName.Equals(name)).Single();
                return JsonConvert.SerializeObject(guerrilla);
            }
            catch
            {
                throw;
            }
        }

        ////[EnableCors("GetAllPolicy")]
        //[Route("[action]/{id}")]
        //[HttpDelete("{id}")]
        //public IActionResult DeleteStudentSP(int id)
        //{
        //    try
        //    {
        //        int result = 0;
        //        SqlParameter studentId = new SqlParameter("@StudentId", id);
        //        result = _context.Database.ExecuteSqlRaw($"DeleteStudent @StudentId", studentId);
        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        return Problem();
        //    }
        //}

        //[EnableCors("GetAllPolicy")]

        [HttpPost("{name}")]
        public string CreateGuerrilla(Guerrilla guerrilla, string name)
        {
            try
            {
                SqlParameter nombre = new SqlParameter("@nombre", guerrilla.guerrillaName);
                SqlParameter correo = new SqlParameter("@correo", guerrilla.email);
                SqlParameter tipo = new SqlParameter("@tipo_guerrilla", guerrilla.faction);
                //result = _context.Database.ExecuteSqlRaw($"InsertUpdateStudent @StudentId, @Name, @Age, @NationalityId, @MajorId, @Action",
                //    studentId, name, age, nationality, major, action);
                _context.Guerrilla.Add(guerrilla);
                _context.SaveChanges();
                var guerrillaAdded = _context.Guerrilla.Where(g => g.guerrillaName.Equals(guerrilla.guerrillaName)).Single();
                return JsonConvert.SerializeObject(guerrillaAdded);
            }
            catch
            {
                throw;
            }
        }
    }
}
