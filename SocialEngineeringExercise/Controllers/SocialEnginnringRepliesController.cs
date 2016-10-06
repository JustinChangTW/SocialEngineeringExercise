using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SocialEngineeringExercise.Models;

namespace SocialEngineeringExercise.Controllers
{
    public class SocialEnginnringRepliesController : ApiController
    {
        private SocialEnginnringModel db = new SocialEnginnringModel();

        // GET: api/SocialEnginnringReplies
        public IQueryable<SocialEnginnringReplyModel> GetSocialEnginnringReply()
        {
            return db.SocialEnginnringReply;
        }

        // GET: api/SocialEnginnringReplies/5
        [ResponseType(typeof(SocialEnginnringReplyModel))]
        public IHttpActionResult GetSocialEnginnringReply(Guid id)
        {
            //    SocialEnginnringReply socialEnginnringReply = db.SocialEnginnringReply.Find(id);
            //    if (socialEnginnringReply == null)
            //    {
            //        return NotFound();
            //    }

            //    return Ok(socialEnginnringReply);
            //}
            SocialEnginnringReplyModel socialEnginnringReply = db.SocialEnginnringReply.Where(a => a.SocialEnginnringGuid == id).SingleOrDefault();

            socialEnginnringReply.ClickTime = socialEnginnringReply.ClickTime + 1;

            db.Entry(socialEnginnringReply).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialEnginnringReplyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

// PUT: api/SocialEnginnringReplies/5
[ResponseType(typeof(void))]
        public IHttpActionResult PutSocialEnginnringReply(Guid id, SocialEnginnringReplyModel socialEnginnringReply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialEnginnringReply.SocialEnginnringGuid)
            {
                return BadRequest();
            }

            db.Entry(socialEnginnringReply).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialEnginnringReplyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //透過EMAIL點擊回傳將次數加1
        // PUT: api/SocialEnginnringReplies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSocialEnginnringReply(Guid id)
        {
            SocialEnginnringReplyModel socialEnginnringReply = db.SocialEnginnringReply.Where(a => a.SocialEnginnringGuid == id).SingleOrDefault();

            socialEnginnringReply.ClickTime = socialEnginnringReply.ClickTime + 1;

            db.Entry(socialEnginnringReply).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialEnginnringReplyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SocialEnginnringReplies
        [ResponseType(typeof(SocialEnginnringReplyModel))]
        public IHttpActionResult PostSocialEnginnringReply(SocialEnginnringReplyModel socialEnginnringReply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SocialEnginnringReply.Add(socialEnginnringReply);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SocialEnginnringReplyExists(socialEnginnringReply.SocialEnginnringGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = socialEnginnringReply.SocialEnginnringGuid }, socialEnginnringReply);
        }

        // DELETE: api/SocialEnginnringReplies/5
        [ResponseType(typeof(SocialEnginnringReplyModel))]
        public IHttpActionResult DeleteSocialEnginnringReply(Guid id)
        {
            SocialEnginnringReplyModel socialEnginnringReply = db.SocialEnginnringReply.Find(id);
            if (socialEnginnringReply == null)
            {
                return NotFound();
            }

            db.SocialEnginnringReply.Remove(socialEnginnringReply);
            db.SaveChanges();

            return Ok(socialEnginnringReply);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SocialEnginnringReplyExists(Guid id)
        {
            return db.SocialEnginnringReply.Count(e => e.SocialEnginnringGuid == id) > 0;
        }
    }
}