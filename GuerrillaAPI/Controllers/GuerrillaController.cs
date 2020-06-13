using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuerrillaAPI.Models;
using Microsoft.Data.SqlClient;

namespace GuerrillaAPI.Controllers
{
    [Route("api/[controller]")]
    //[AllowAnonymous]
    [ApiController]
    public class GuerrillaController : ControllerBase
    {
        private readonly GuerillaAppJLDContext _context;

        public GuerrillaController(GuerillaAppJLDContext context)
        {
            _context = context;
        }

        [Route("[action]")]
        //[EnableCors("GetAllPolicy")]
        [HttpGet]
        public IActionResult GetAllGuerrillas()
        {
            try
            {
                //var students = _context.Student.FromSqlRaw($"SelectStudent").AsEnumerable();
                var guerrillas = _context.Guerrilla.ToList();
                return Ok(guerrillas);
            }
            catch
            {
                return Problem();
            }
        }

        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetGuerrilla(int id)
        {
            try
            {
                SqlParameter studentId = new SqlParameter("@StudentId", id);
                //var student = _context.Student.FromSqlRaw($"SelectStudentById @StudentId", studentId).AsEnumerable().Single();
                var guerrilla = _context.Guerrilla.Where(g => g.IdGuerrilla == id).Single();
                if (guerrilla == null)
                {
                    return NotFound();
                }
                return Ok(guerrilla);
            }
            catch
            {
                return Problem();
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

        [Route("[action]")]
        [HttpPost]
        public IActionResult CreateGuerrilla(Guerrilla guerrilla)
        {
            try
            {
                int result = 0;
                SqlParameter nombre = new SqlParameter("@nombre", guerrilla.Nombre);
                SqlParameter correo = new SqlParameter("@correo", guerrilla.Correo);
                SqlParameter tipo = new SqlParameter("@tipo_guerrilla", guerrilla.TipoGuerrilla);
                //result = _context.Database.ExecuteSqlRaw($"InsertUpdateStudent @StudentId, @Name, @Age, @NationalityId, @MajorId, @Action",
                //    studentId, name, age, nationality, major, action);
                 _context.Guerrilla.Add(guerrilla);
                _context.SaveChanges();
                return Ok(result);
            }
            catch
            {
                throw;
                //return Problem();
            }
        }

        ////[EnableCors("GetAllPolicy")]
        //[Route("[action]")]
        //[HttpPut]
        //public IActionResult UpdateGuerrilla(Guerrilla guerrilla)
        //{
        //    try
        //    {
        //        int result = 0;
        //        SqlParameter studentId = new SqlParameter("@StudentId", student.StudentId);
        //        SqlParameter name = new SqlParameter("@Name", student.Name);
        //        SqlParameter age = new SqlParameter("@Age", student.Age);
        //        //SqlParameter interests = new SqlParameter("@Interests", student.Interests);
        //        //SqlParameter entryDate = new SqlParameter("@EntryDate", student.EntryDate);
        //        SqlParameter nationality = new SqlParameter("@NationalityId", student.NationalityId);
        //        SqlParameter major = new SqlParameter("@MajorId", student.MajorId);
        //        //SqlParameter seniority = new SqlParameter("@SeniorityId", student.Seniority.Id);
        //        SqlParameter action = new SqlParameter("@Action", "Update");
        //        result = _context.Database.ExecuteSqlRaw($"InsertUpdateStudent @StudentId, @Name, @Age, @NationalityId, @MajorId, @Action",
        //            studentId, name, age, nationality, major, action);
        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        throw;
        //        //return Problem();
        //    }
        //}
    }
}
