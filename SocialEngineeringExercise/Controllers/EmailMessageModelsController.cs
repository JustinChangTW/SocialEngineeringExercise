using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialEngineeringExercise.Models;
using System.Net.Mail;
using System.Text;

namespace SocialEngineeringExercise.Controllers
{
    public class EmailMessageModelsController : Controller
    {
        private SocialEnginnringModel db = new SocialEnginnringModel();

        // GET: EmailMessageModels
        public async Task<ActionResult> Index()
        {
            return View(await db.EmailMessageModel.ToListAsync());
        }

        // GET: EmailMessageModels/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailMessageModel emailMessageModel = await db.EmailMessageModel.FindAsync(id);
            if (emailMessageModel == null)
            {
                return HttpNotFound();
            }
            return View(emailMessageModel);
        }

        // GET: EmailMessageModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailMessageModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Guid,Subject,IsBodyHtml,Body,Prority,MailEncoding,Attachment,AttachmentInline")] EmailMessageModel emailMessageModel)
        {
            if (ModelState.IsValid)
            {
                emailMessageModel.Guid = Guid.NewGuid();
                db.EmailMessageModel.Add(emailMessageModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(emailMessageModel);
        }

        // GET: EmailMessageModels/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailMessageModel emailMessageModel = await db.EmailMessageModel.FindAsync(id);
            if (emailMessageModel == null)
            {
                return HttpNotFound();
            }
            return View(emailMessageModel);
        }

        // POST: EmailMessageModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Guid,Subject,IsBodyHtml,Body,Prority,MailEncoding,Attachment,AttachmentInline")] EmailMessageModel emailMessageModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailMessageModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(emailMessageModel);
        }

        // GET: EmailMessageModels/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailMessageModel emailMessageModel = await db.EmailMessageModel.FindAsync(id);
            if (emailMessageModel == null)
            {
                return HttpNotFound();
            }
            return View(emailMessageModel);
        }

        // POST: EmailMessageModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EmailMessageModel emailMessageModel = await db.EmailMessageModel.FindAsync(id);
            db.EmailMessageModel.Remove(emailMessageModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SentMail()
        {
            Mail mail = new Mail();
            var error = await Task.Run(() => { return mail.BatchSentMail(); });//= await mail.BatchSentMail();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    internal class Mail
    {
        private SocialEnginnringModel db = new SocialEnginnringModel();
        public Mail() { }

        public string BatchSentMail()
        {
            var list = db.EmailMessageModel.ToList();
            var mail = db.SocialEnginnringReply.ToList();

            foreach (var l in list)
            {
                foreach (var m in mail)
                {
                    MailAddress mailaddress = new MailAddress(m.EmployeeEmail, m.EmployeeName);
                }
            }

            return "";
        }


        public string SentMail(EmailMessageModel mail, MailAddress Mailto)
        {
            string result = "";

            string myMailEncoding = mail.MailEncoding;// "utf-8";
            string myFromEmail = "testtest.com";
            string myFromName = "測試寄件者";
            //string myToEmail = Mailto;
            //string myToName = "測試收件者";
            MailAddress from = new MailAddress(myFromEmail, myFromName, Encoding.GetEncoding(myMailEncoding));
            MailAddress to = Mailto;//new MailAddress(myToEmail, myToName, Encoding.GetEncoding(myMailEncoding));
            MailMessage myMessage = new MailMessage(from, to);
            myMessage.Subject = "郵件主旨";
            myMessage.SubjectEncoding = Encoding.GetEncoding(myMailEncoding);
            myMessage.Body = mail.Body;// "<h1>這是郵件內容</h1><hr/><img src=\"Logo.gif\" />";
            myMessage.BodyEncoding = Encoding.GetEncoding(myMailEncoding);
            myMessage.IsBodyHtml = mail.IsBodyHtml;// true;
            myMessage.Priority =  MailPriority.High;

            // 設定附件檔案(Attachment)
            System.Net.Mail.Attachment attachment =  new System.Net.Mail.Attachment(mail.Attachment);
            attachment.Name = System.IO.Path.GetFileName(mail.Attachment);
            attachment.NameEncoding = Encoding.GetEncoding(myMailEncoding);
            attachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            // 設定該附件為一個內嵌附件(Inline Attachment)
            attachment.ContentDisposition.Inline = true;
            attachment.ContentDisposition.DispositionType =
               System.Net.Mime.DispositionTypeNames.Inline;
            myMessage.Attachments.Add(attachment);
            SmtpClient smtp = new SmtpClient("localhost");
            //smtp.
            try
            {
                smtp.Send(myMessage);
                result =DateTime.Now.ToString() + " 寄信成功";
                return result;
            }
            catch
            {
                result = DateTime.Now.ToString() + " 寄信失敗!!!";
                return result;
            }
            //Console.ReadKey();
        }

    }
}
