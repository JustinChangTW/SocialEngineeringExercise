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
using System.Threading;

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

        //[Authorize(Roles ="Admin")]
        //[ValidateAntiForgeryToken]
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
            var smtp = db.SmtpConfigModel.SingleOrDefault();

            //有BUG//GUID不會跳動～～～只會使用第一筆///////////////////////////////////////////
            foreach (var l in list)
            {
                foreach (var m in mail)
                {
                    MailAddress mailto = new MailAddress(m.EmployeeEmail, m.EmployeeName);
                    SentMail(l ,m, mailto, smtp);
                }
            }

            return "";
        }


        public string SentMail(EmailMessageModel emailMessage,SocialEnginnringReplyModel reply , MailAddress mailto,SmtpConfigModel smtp)
        {
            string result = "";


                string myMailEncoding = emailMessage.MailEncoding;// "utf-8";
                                                                  //string myFromEmail = emailMessage.Address;
                string myFromEmail = emailMessage.Address.Replace(Convert.ToChar(13), Convert.ToChar(32)).Replace(Convert.ToChar(10), Convert.ToChar(32));
                //string myFromName = emailMessage.DisplayName;
                string myFromName = emailMessage.DisplayName.Replace(Convert.ToChar(13), Convert.ToChar(32)).Replace(Convert.ToChar(10), Convert.ToChar(32));
            //.Replace( (13), ""), Chr(10), "")
            MailAddress from = null;
            MailAddress to = null;
            MailMessage myMessage = null;
            try
            {
                //string myToEmail = Mailto;
                //string myToName = "測試收件者";
                //from = new MailAddress(myFromEmail, myFromName, Encoding.GetEncoding(myMailEncoding));
                to = mailto;//new MailAddress(myToEmail, myToName, Encoding.GetEncoding(myMailEncoding));
                myMessage = new MailMessage();
                myMessage.From=new MailAddress(myFromName + "<" + myFromEmail + ">");
                myMessage.To.Add(to);
                myMessage.Subject = emailMessage.Subject;// "郵件主旨";

                myMessage.SubjectEncoding = Encoding.GetEncoding(myMailEncoding);
            }
            catch(Exception e)
            {
                var ee = e.Message;
            }
            //設定回覆網址
            var host = "";// HttpContext..Current.Request.ToString();
            //有BUG//GUID不會跳動～～～只會使用第一筆///////////////////////////////////////////
            host = reply.HostUrlRoot + String.Format("api/SocialEnginnringReplies/{0}", reply.SocialEnginnringGuid.ToString());
            //var a = ActionLink("test", "Details", new { id = 2 })
            //host = host + String.Format("~/api/SocialEnginnringReplies/{0}", reply.SocialEnginnringGuid.ToString());
            //var b = RedirectToAction("Index");
            emailMessage.Body = emailMessage.Body.Replace("#href#", host);//<img src="#img#" href="#href#“/>


            //設定圖片附件位址
            var file = emailMessage.Attachment.Split('\\');
            emailMessage.Body = emailMessage.Body.Replace("#img#", file[file.GetLength(0) - 1]);
            myMessage.Body = emailMessage.Body;// "<h1>這是郵件內容</h1><hr/><img src=\"Logo.gif\" />";
            myMessage.BodyEncoding = Encoding.GetEncoding(myMailEncoding);
            myMessage.IsBodyHtml = emailMessage.IsBodyHtml;// true;
            myMessage.Priority = MailPriority.High;

            if (emailMessage.AttachmentInline)
            {
                // 設定附件檔案(Attachment)
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(emailMessage.Attachment);
                attachment.Name = System.IO.Path.GetFileName(emailMessage.Attachment);
                attachment.NameEncoding = Encoding.GetEncoding(myMailEncoding);
                attachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                // 設定該附件為一個內嵌附件(Inline Attachment)
                attachment.ContentDisposition.Inline = emailMessage.AttachmentInline;//  true;
                attachment.ContentDisposition.DispositionType =
                   System.Net.Mime.DispositionTypeNames.Inline;

                myMessage.Attachments.Add(attachment);

            }

            //SMTP
            SmtpClient smtpObject = new SmtpClient();
            smtpObject.Host = smtp.Host;
            smtpObject.Port = smtp.Port;
            smtpObject.Credentials = new NetworkCredential(smtp.Id, smtp.Password);
            smtpObject.EnableSsl = smtp.EnableSsl;

            //回條開啟&送達
            myMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            var replyMail = String.Format("\" Mail \" {0}@{1}", smtp.Id, smtp.Host);
            myMessage.Headers.Add("Disposition-Notification-To", replyMail);//"\"Michael\" huanlin.tsai@xmail.com");
            smtpObject.DeliveryMethod = SmtpDeliveryMethod.Network;


            //寄信
            try
            {
                smtpObject.Send(myMessage);
                result =DateTime.Now.ToString() + " 寄信成功";
                Thread.Sleep(60000); //Delay 1秒
                return result;
            }
            catch(Exception e)
            {
                var a = e;
                result = DateTime.Now.ToString() + " 寄信失敗!!!";
                return result;
            }
            //Console.ReadKey();
        }

    }
}
