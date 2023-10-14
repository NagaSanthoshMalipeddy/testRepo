using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExerciseCoreWebProj.Models;

namespace ExerciseCoreWebProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecializationsController : ControllerBase
    {
        private readonly CureWellDbContext _context;

        public DoctorSpecializationsController(CureWellDbContext context)
        {
            _context = context;
        }

        // GET: api/DoctorSpecializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorSpecialization>>> GetDoctorSpecializations()
        {
          if (_context.DoctorSpecializations == null)
          {
              return NotFound();
          }
            return await _context.DoctorSpecializations.ToListAsync();
        }

        //// GET: api/DoctorSpecializations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DoctorSpecialization>> GetDoctorSpecialization(int id)
        //{
        //  if (_context.DoctorSpecializations == null)
        //  {
        //      return NotFound();
        //  }
        //    var doctorSpecialization = await _context.DoctorSpecializations.FindAsync(id);

        //    if (doctorSpecialization == null)
        //    {
        //        return NotFound();
        //    }

        //    return doctorSpecialization;
        //}




        [HttpGet("{id1}/{id2}")]
        public async Task<ActionResult<DoctorSpecialization>> GetUserItem(int id1, string id2)
        {
            if (_context.DoctorSpecializations == null)
            {
                return NotFound();
            }
            var doctorSpec = await _context.DoctorSpecializations.FindAsync(id1, id2);
            if (doctorSpec == null)
            {
                return NotFound();
            }
            return doctorSpec;
        }


       


        // PUT: api/DoctorSpecializations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id1}/{id2}")]
        public async Task<IActionResult> PutDoctorSpecialization(int id1,string id2, DoctorSpecialization doctorSpecialization)
        {
            if (id1 != doctorSpecialization.DoctorId && id2!=doctorSpecialization.SpecializationCode)
            {
                return BadRequest();
            }

            _context.Entry(doctorSpecialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorSpecializationExists(id1,id2))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DoctorSpecializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoctorSpecialization>> PostDoctorSpecialization(DoctorSpecialization doctorSpecialization)
        {
          if (_context.DoctorSpecializations == null)
          {
              return Problem("Entity set 'CureWellDbContext.DoctorSpecializations'  is null.");
          }
            _context.DoctorSpecializations.Add(doctorSpecialization);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoctorSpecializationExists(doctorSpecialization.DoctorId,doctorSpecialization.SpecializationCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctorSpecialization", new { id = doctorSpecialization.DoctorId }, doctorSpecialization);
        }

        // DELETE: api/DoctorSpecializations/5
        [HttpDelete("{id1}/{id2}")]
        public async Task<IActionResult> DeleteDoctorSpecialization(int id1,string id2)
        {
            if (_context.DoctorSpecializations == null)
            {
                return NotFound();
            }
            var doctorSpecialization = await _context.DoctorSpecializations.FindAsync(id1,id2);
            if (doctorSpecialization == null)
            {
                return NotFound();
            }

            _context.DoctorSpecializations.Remove(doctorSpecialization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorSpecializationExists(int id1,string id2)
        {
            return (_context.DoctorSpecializations?.Any(e => (e.DoctorId == id1) && (e.SpecializationCode==id2))).GetValueOrDefault();
        }
    }
}
