using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 2.2.0
//dotnet tool install -g dotnet-aspnet-codegenerator --version 2.2.0
//dotnet aspnet-codegenerator controller -api -name SpeakersController -m Speaker -dc BackEnd.Models.ApplicationDbContext -outDir Controllers

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeakerController : ControllerBase
    {

        private readonly ApplicationDBContext _db;

        public SpeakerController(ApplicationDBContext context)
        {
            _db = context;
        }

        //Get api/Speaker
        [HttpGet]
        public async Task<ActionResult<List<ConferenceDTO.SpeakerResponse>>> GetSpeaker()
        {
            //return await _db.Speakers.ToListAsync();

            var speakers = await _db.Speakers.AsNoTracking().Include(s => s.SessionSpeakers).ThenInclude(ss => ss.Session).Select(s => s.MapSpeakerResponse()).ToListAsync();

            return speakers;
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDTO.SpeakerResponse>> GetSpeaker(int id)
        {
            var speaker = await _db.Speakers.AsNoTracking()
                                            .Include(s => s.SessionSpeakers)
                                                .ThenInclude(ss => ss.Session)
                                            .SingleOrDefaultAsync(s => s.ID == id);
            if (speaker == null)
            {
                return NotFound();
            }
            return speaker.MapSpeakerResponse();
        }


        // PUT: api/Speakers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeaker(int id, Speaker speaker)
        {
            if (id != speaker.ID)
            {
                return BadRequest();
            }

            _db.Entry(speaker).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerExists(id))
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

        // POST: api/Speakers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ConferenceDTO.SpeakerResponse>> PostSpeaker(ConferenceDTO.Speaker input)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                WebSite = input.WebSite,
                Bio = input.Bio
            };
            _db.Speakers.Add(speaker);
            await _db.SaveChangesAsync();

            var result = speaker.MapSpeakerResponse();



            return CreatedAtAction(nameof(GetSpeaker), new { id = speaker.ID }, speaker);
        }

        // DELETE: api/Speakers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConferenceDTO.SpeakerResponse>> DeleteSpeaker(int id)
        {
            var speaker = await _db.FindAsync<Speaker>(id);
            if (speaker == null)
            {
                return NotFound();
            }

            _db.Speakers.Remove(speaker);
            await _db.SaveChangesAsync();

            return speaker.MapSpeakerResponse();
        }

        private bool SpeakerExists(int id)
        {
            return _db.Speakers.Any(e => e.ID == id);
        }


    }
}