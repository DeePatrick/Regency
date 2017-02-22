using AutoMapper;
using Regency.Dtos;
using Regency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Regency.Controllers.Api
{
    [AllowAnonymous]
    public class ActorsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ActorsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: api/Actors

        public IEnumerable<ActorDto> GetActors(string query = null)
        {
            var actorsQuery = _context.Actors
                .Where(m => !m.IsAvailable);

            if (!String.IsNullOrWhiteSpace(query))
                actorsQuery = actorsQuery.Where(m => m.Name.Contains(query));

            return actorsQuery
                .ToList()
                .Select(Mapper.Map<Actor, ActorDto>);
        }


        // GET: api/Actors/5
        public IHttpActionResult GetActors(int id)
        {
            var actor = _context.Actors.Single(a => a.Id == id);
            if (actor == null)
                return BadRequest();

            return Ok(Mapper.Map<Actor, ActorDto>(actor));
        }

        // POST: api/Actors
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageActors)]
        public IHttpActionResult CreateActor(ActorDto actorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var actor = Mapper.Map<ActorDto, Actor>(actorDto);
            _context.Actors.Add(actor);
            _context.SaveChanges();

            actorDto.Id = actor.Id;
            return Created(new Uri(Request.RequestUri + "/" + actor.Id), actorDto);
        }

        // PUT: api/Actors/5
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageActors)]
        public void UpdateActor(int id, ActorDto actorDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var actorInDb = _context.Actors.Single(a => a.Id == id);
            if (actorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(actorDto, actorInDb);
            _context.SaveChanges();
        }

        // DELETE: api/Actors/5
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageActors)]
        public void Delete(int id)
        {

            var actorInDb = _context.Actors.Single(a => a.Id == id);
            if (actorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Actors.Remove(actorInDb);
            _context.SaveChanges();
        }
    }
}
