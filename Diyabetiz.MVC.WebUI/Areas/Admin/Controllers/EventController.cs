using Diyabetiz.DAL.UnitOfWork;
using Diyabetiz.Entities.Entities;
using Diyabetiz.MVC.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Diyabetiz.MVC.WebUI.Helper.FileHelper;

namespace Diyabetiz.MVC.WebUI.Areas.Admin.Controllers
{
    public class EventController : Controller
    {
        UnitOfWork _unitOfWork;
        EventVM _eventVM;
        List<EventVM> _eventVMs;
        public EventController()
        {
            _eventVM = new EventVM();
            _unitOfWork = new UnitOfWork();
            _eventVMs = new List<EventVM>();
        }
        // GET: Admin/Event
        public ActionResult EventList()
        {
            _eventVM.Events = _unitOfWork.EventRepository.Select().ToList();
            return View(_eventVM);
        }
        [HttpGet]
        public ActionResult EventAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EventAdd(EventVM model, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                string physicalPath = "~/images/Event/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("EventAdd");
                    }
                }

                foreach (var item in resultModel.Keys)
                {

                    _unitOfWork.EventRepository.Insert(new Event { Title = model.Event.Title, ImageURL = item.UploadPath, Description = model.Event.Description, CreatedDate = model.Event.CreatedDate, CreatedByID = 1, IsActive = true, UpdatedDate = DateTime.Now, Adress=model.Event.Adress, EventDate=model.Event.EventDate, ThemeColor=model.Event.ThemeColor, IsFullDay=model.Event.IsFullDay });
                    _unitOfWork.Save();                                                                                     

                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            return RedirectToAction("EventList");
        }
        [HttpGet]
        public ActionResult EventUpdate(int id)
        {
            Event Event = _unitOfWork.EventRepository.Find(x => x.ID == id);
            _eventVM.Event = Event;
            return View(_eventVM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EventUpdate(EventVM model, IEnumerable<HttpPostedFileBase> file)
        {

            try
            {
                string physicalPath = "~/images/Event/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("EventUpdate");
                    }
                }

                foreach (var item in resultModel.Keys)
                {
                    model.Event.ImageURL = item.UploadPath;
                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            model.Event.IsActive = true;
            model.Event.UpdatedDate = DateTime.Now;            
            model.Event.CreatedByID = 1;

            var oldEvent = _unitOfWork.EventRepository.Find(x => x.ID == model.Event.ID);
            if (ModelState.IsValid)
            {
                List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = oldEvent.ImageURL } };
                RemoveAll(fileResultItems, "~/images/Event/");
                _unitOfWork.EventRepository.Detach(oldEvent);
                _unitOfWork.EventRepository.Update(model.Event);
                _unitOfWork.Save();
            }
            return RedirectToAction("EventList");
        }

        public ActionResult EventDelete(int id)
        {
            Event Event = _unitOfWork.EventRepository.Find(x => x.ID == id);
            _unitOfWork.EventRepository.Delete(Event);
            _unitOfWork.Save();

            List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = Event.ImageURL } };
            RemoveAll(fileResultItems, "~/images/Event/");

            return RedirectToAction("EventList");
        }
        public ActionResult FullCalender()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            
                var events = _unitOfWork.EventRepository.Select().ToList();
          
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            
                if (e.ID > 0)
                {
                //Update the event
                    var v = _unitOfWork.EventRepository.FindById(e.ID);
                    if (v != null)
                    {
                        v.Title = e.Title;
                        v.EventDate = e.EventDate;
                        v.CreatedDate = e.CreatedDate;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;                        
                    _unitOfWork.EventRepository.Update(v);
                    _unitOfWork.Save();
                }
                }
                else
                {
                    e.Adress = "Serdivan";
                    e.CreatedByID = 1;
                    e.UpdatedDate = DateTime.Now;
                   _unitOfWork.EventRepository.Insert(e);
                   _unitOfWork.Save();
                }
               
                status = true;
          
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEventC(int eventID)
        {
            var status = false;

            var v = _unitOfWork.EventRepository.FindById(eventID);
                if (v != null)
                {
                _unitOfWork.EventRepository.Delete(v);
                _unitOfWork.Save();
                    status = true;
                }
            
            return new JsonResult { Data = new { status = status } };
        }

    }
}