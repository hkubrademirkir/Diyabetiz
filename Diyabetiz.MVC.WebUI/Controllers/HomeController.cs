using Diyabetiz.DAL.UnitOfWork;
using Diyabetiz.Entities.Entities;
using Diyabetiz.MVC.WebUI.Helper;
using Diyabetiz.MVC.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Diyabetiz.MVC.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        UnitOfWork _unitOfWork;
        ContactUsVM _contactUsVM;
        CarbCountingVM _carbCountingVM;
        DiaEduVM _diaEduVM;
        List<DiabetesEducation> _list;
        List<DiabetesEducation> _titleDiaList;
        int _id;
        HomeVM _homeVM;
        NewsVM _newsVM;
        List<NewsVM> _newsVMs;
        List<News> _newsList;
        List<News> _titleNewsList;
        Areas.Admin.Models.EventVM _eventVM;
        List<Areas.Admin.Models.EventVM> _eventVMs;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork();
            _contactUsVM = new ContactUsVM();
            _carbCountingVM = new CarbCountingVM();
            _diaEduVM = new DiaEduVM();
            _list = new List<DiabetesEducation>();
            _homeVM = new HomeVM();
            _newsVM = new NewsVM();
            _newsVMs = new List<NewsVM>();
            _newsList = new List<News>();
            _titleNewsList = new List<News>();
            _titleDiaList = new List<DiabetesEducation>();
            _eventVM = new Areas.Admin.Models.EventVM();
            _eventVMs = new List<Areas.Admin.Models.EventVM>();

        }
       
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ContactUsVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ContactUsRepository.Insert(new ContactUs { Name = model.Name, Email = model.EMail, Message = model.Message, CreatedDate = DateTime.Now, IsActive = true, CreatedByID = 1 });
                    _unitOfWork.Save();
                    #region sendMail1

                    //var toAddress = model.EMail.ToString();
                    //var fromAddress = "someadress@yahoo.co.uk";
                    //var subject = "Test enquiry from " + model.Name;
                    //var message = new StringBuilder();
                    //message.Append("Name: " + model.Name + "\n");
                    //message.Append("Email: " + model.EMail + "\n");                     
                    //message.Append(model.Message);

                    ////start email Thread
                    //var tEmail = new Thread(() => SendMail.SendEmail(toAddress, fromAddress, subject, message.ToString()));
                    //tEmail.Start();
                    #endregion
                    #region sendMail2
                    GMailer.GmailUsername = "hkubrademirkir@gmail.com";
                    GMailer.GmailPassword = "Hkd126..";

                    GMailer mailer = new GMailer();
                    mailer.ToEmail = model.EMail.ToString(); 
                    mailer.Subject = "Verify your email id";
                    mailer.Body = "Thanks for Registering your account.<br> please verify your email id by clicking the link <br> <a href='youraccount.com/verifycode=12323232'>verify</a>";
                    mailer.IsHtml = true;
                    mailer.Send();
                    TempData["NoteCss"] = "danger";
                    TempData["NoteText"] = "Bilinmeyen Hata!";
                    TempData["SuccessMail"] = "Mesajınızı Aldık! E-Postanızı Kontrol Edin.";
                    #endregion

                }
                catch (Exception ex)
                {
                    TempData["NoteCss"] = "danger";
                    TempData["NoteText"] = "Bilinmeyen Hata!";
                    TempData["NoteError"] = ex.Message;

                }      
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CarbCounting()
        {
            _carbCountingVM.carbCounting = _unitOfWork.CarbohydrateCountingRepository.Select().FirstOrDefault();
           _carbCountingVM.carbCountings= _unitOfWork.CarbohydrateCountingRepository.Select().ToList();

            return View(_carbCountingVM);
        }

        public PartialViewResult CarbCountingPartial(int CarbCountingId)
        {
           _carbCountingVM.carbCounting= _unitOfWork.CarbohydrateCountingRepository.FindById(CarbCountingId);

            return PartialView("_CarbCountingPartial", _carbCountingVM);
        }
        [HttpGet]
        public ActionResult DiaEdu(int? id)
        {
            if (id==null)
            {

            _diaEduVM.diabetesEducation = _unitOfWork.DiabetesEducationRepository.Select().FirstOrDefault();
            _diaEduVM.diabetesEducations = _unitOfWork.DiabetesEducationRepository.Select().ToList();
            }
            else
            {
                _id = Convert.ToInt32(id);
                DiabetesEducation diabetesEducation = _unitOfWork.DiabetesEducationRepository.FindById(_id);
               
                _list.Add(diabetesEducation);
                _diaEduVM.diabetesEducations = _list;
            }
            _titleDiaList = _unitOfWork.DiabetesEducationRepository.Select().ToList();
            _diaEduVM.titleList = _titleDiaList;
            return View(_diaEduVM);
        }
        public PartialViewResult DiaEduPartial(int diaEduId)
        {
            DiabetesEducation diabetesEducation = _unitOfWork.DiabetesEducationRepository.FindById(diaEduId);
            
            _list.Add(diabetesEducation);
            _diaEduVM.diabetesEducations = _list;
            
           

            return PartialView("_DiaEduPartial", _diaEduVM);
        }
        public PartialViewResult DiaEduLayout()
        {
           _diaEduVM.diabetesEducations= _unitOfWork.DiabetesEducationRepository.Select().ToList();



            return PartialView("_LayoutPartial", _diaEduVM);
        }
        
        [HttpGet]
        public ActionResult Home()
        {
          _homeVM.Foods =  _unitOfWork.FoodRepository.Select().ToList();
          _homeVM.newsList = _unitOfWork.NewsRepository.Select().ToList();
          
            foreach (var item in _homeVM.newsList)
            {
                _newsVMs.Add(new NewsVM { title = item.Title, createdDate = item.CreatedDate, imageUrl = item.ImageURL, shortDescription = item.Description.Substring(0, 50),news=item});

            }
            _homeVM.newsVMList = _newsVMs;
            return View(_homeVM);
        }
        [HttpGet]
        public ActionResult News(int? id)
        {
            if (id==null)
            {
                _homeVM.newsList = _unitOfWork.NewsRepository.Select().ToList();
                foreach (var item in _homeVM.newsList)
                {
                    _newsVMs.Add(new NewsVM { title = item.Title, createdDate = item.CreatedDate, imageUrl = item.ImageURL, shortDescription = item.Description.Substring(0, 50), news = item });
                }
                _homeVM.newsVMList = _newsVMs;
            }
            else
            {
                News news = _unitOfWork.NewsRepository.FindById(Convert.ToInt32(id));
                _newsList.Add(news);
                _homeVM.newsList = _newsList;
                _titleNewsList = _unitOfWork.NewsRepository.Select().ToList();
                foreach (var item in _titleNewsList)
                {
                    _newsVMs.Add(new NewsVM { title = item.Title, createdDate = item.CreatedDate, imageUrl = item.ImageURL, shortDescription = item.Description.Substring(0, 50), news = item });
                }
                _homeVM.newsVMList = _newsVMs;

            }
           

            return View(_homeVM);
        }
        public PartialViewResult NewsPartial(int newsId)
        {
            News news = _unitOfWork.NewsRepository.FindById(newsId);
            _newsList.Add(news);
            _homeVM.newsList = _newsList;



            return PartialView("_NewsPartial", _homeVM);
        }
      
        public JsonResult GetEvents()
        {

            var events = _unitOfWork.EventRepository.Select().ToList();
           
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult AddCarbCount(int foodId)
        {
            Food food = _unitOfWork.FoodRepository.FindById(foodId);
            if (Session["cart"] == null)
            {
                List<Food> list = new List<Food>();

                list.Add(food);
                Session["cart"] = list;
                ViewBag.cart = list.Count();


                Session["count"] = 1;


            }
            else
            {
                List<Food> list = (List<Food>)Session["cart"];
                list.Add(food);
                Session["cart"] = list;
                ViewBag.cart = list.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("CountingHomePartial", "Home");


        }
        public PartialViewResult CountingHomePartial()
        {

            return PartialView("_CountingHomePartial", (List<Food>)Session["cart"]);
        }

        public ActionResult Remove()
        {
            List<Food> li = (List<Food>)Session["cart"];
            li.Clear();
            Session["cart"] = li;
            
            return RedirectToAction("Home", "Home");

        }
    }
}