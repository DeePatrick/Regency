using Regency.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Regency.Controllers
{
    [AllowAnonymous]
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [Authorize(Roles = RoleName.CanManageActors)]
        public ActionResult New()
        {
            var b1 = new Actor();
            return View("ActorForm", b1);
        }

        [HttpPost]
        [System.Web.Http.Authorize(Roles = RoleName.CanManageActors)]
        public ActionResult Save(Actor model, HttpPostedFileBase image1)
        {
            var _context = new ApplicationDbContext();

            if (model.Id == 0)
            {
                try
                {
                    if (image1 != null)
                    {
                        model.ActorImage = new byte[image1.ContentLength];
                        image1.InputStream.Read(model.ActorImage, 0, image1.ContentLength);
                    }

                }
                catch (DbEntityValidationException e /* dex */)
                {
                    Console.WriteLine(e);
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("",
                        "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }

            }
            else
            {
                var actorInDb = _context.Actors
                    .Single(a => a.Id == model.Id);

                actorInDb.Name = model.Name;
                actorInDb.DateJoined = model.DateJoined;
                actorInDb.DateActive = model.DateActive;
                actorInDb.Category = model.Category;
                actorInDb.Gender = model.Gender;
                actorInDb.ActorImage = model.ActorImage;
            }

            var actor = new Actor
            {
                Name = model.Name,
                DateJoined = model.DateJoined,
                DateActive = model.DateActive,
                Category = model.Category,
                Gender = model.Gender,
                ActorImage = model.ActorImage
            };

            _context.Actors.Add(actor);
            _context.SaveChanges();
            return RedirectToAction("Index", "Actors");
        }




        public ActionResult Index()
        {
            var actors = _context.Actors
                .ToList();
            if (!User.IsInRole(RoleName.CanManageActors))
                return View("ReadOnlyList", actors);

            return View("List", actors);
        }


        public ActionResult Detail(int id)
        {

            var actor = _context.Actors
                .SingleOrDefault(c => c.Id == id);

            if (actor == null)
            {
                return HttpNotFound();
            }

            return View(actor);
        }

        public ActionResult Edit(int id)
        {
            var actor = _context.Actors
                        .SingleOrDefault(a => a.Id == id);
            if (actor == null)
                return HttpNotFound();



            return View("ActorForm", actor);

        }


    }
}


