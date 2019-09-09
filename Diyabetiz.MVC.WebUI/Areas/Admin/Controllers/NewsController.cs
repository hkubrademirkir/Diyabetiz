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
    public class NewsController : Controller
    {
       
        UnitOfWork _unitOfWork;
        NewsVM _newsVM;

        public NewsController()
        {
            _unitOfWork = new UnitOfWork();
            _newsVM = new NewsVM();
        }
        // GET: Admin/News
        public ActionResult NewsList()
        {
            _newsVM.newsList=  _unitOfWork.NewsRepository.Select().ToList();
            return View(_newsVM);
        }
        [HttpGet]
        public ActionResult NewsAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsAdd(NewsVM model, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                string physicalPath = "~/images/News/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("NewsAdd");
                    }
                }

                foreach (var item in resultModel.Keys)
                {

                    _unitOfWork.NewsRepository.Insert(new News { Title = model.news.Title, ImageURL = item.UploadPath, Description = model.news.Description, CreatedDate = DateTime.Now, CreatedByID = 1, IsActive = true, UpdatedDate = DateTime.Now });
                    _unitOfWork.Save();

                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            return RedirectToAction("NewsList");
        }
        [HttpGet]
        public ActionResult NewsUpdate(int id)
        {
            News News = _unitOfWork.NewsRepository.Find(x => x.ID == id);
            _newsVM.news = News;
            return View(_newsVM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsUpdate(NewsVM model, IEnumerable<HttpPostedFileBase> file)
        {

            try
            {
                string physicalPath = "~/images/News/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("NewsUpdate");
                    }
                }

                foreach (var item in resultModel.Keys)
                {
                    model.news.ImageURL = item.UploadPath;
                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            model.news.IsActive = true;
            model.news.UpdatedDate = DateTime.Now;
            model.news.CreatedDate = DateTime.Now;
            model.news.CreatedByID = 1;

            var oldNews = _unitOfWork.NewsRepository.Find(x => x.ID == model.news.ID);
            if (ModelState.IsValid)
            {
                List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = oldNews.ImageURL } };
                RemoveAll(fileResultItems, "~/images/News/");
                _unitOfWork.NewsRepository.Detach(oldNews);
                _unitOfWork.NewsRepository.Update(model.news);
                _unitOfWork.Save();
            }
            return RedirectToAction("NewsList");
        }

        public ActionResult NewsDelete(int id)
        {
            News news = _unitOfWork.NewsRepository.Find(x => x.ID == id);
            _unitOfWork.NewsRepository.Delete(news);
            _unitOfWork.Save();

            List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = news.ImageURL } };
            RemoveAll(fileResultItems, "~/images/News/");

            return RedirectToAction("NewsList");
        }
    }
}