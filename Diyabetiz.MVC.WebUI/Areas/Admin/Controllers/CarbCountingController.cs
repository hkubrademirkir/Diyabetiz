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
    public class CarbCountingController : Controller
    {
        UnitOfWork _unitOfWork;
        CarbCountingVM _carbCountingVM;
        public CarbCountingController()
        {
            _unitOfWork = new UnitOfWork();
            _carbCountingVM = new CarbCountingVM();
        }
        // GET: Admin/CarbCounting
        [HttpGet]
        public ActionResult CarbCountingList()
        {
            var carbCountingList = _unitOfWork.CarbohydrateCountingRepository.Select().ToList();
            return View(carbCountingList);
        }
        [HttpGet]
        public ActionResult CarbCountingAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CarbCountingAdd(CarbCountingVM model, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                string physicalPath = "~/images/CarbCounting/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("CarbCountingAdd");
                    }
                }

                foreach (var item in resultModel.Keys)
                {

                    _unitOfWork.CarbohydrateCountingRepository.Insert(new CarbohydrateCounting { Title = model.carbohydrateCounting.Title, ImageURL = item.UploadPath, Description = model.carbohydrateCounting.Description, CreatedDate = DateTime.Now });
                    _unitOfWork.Save();

                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            return RedirectToAction("CarbCountingList");
        }

        [HttpGet]
        public ActionResult CarbCountingUpdate(int id)
        {
            CarbohydrateCounting carbohydrateC = _unitOfWork.CarbohydrateCountingRepository.Find(x => x.ID == id);
            _carbCountingVM.carbohydrateCounting = carbohydrateC;
            return View(_carbCountingVM);
        }
        [HttpPost]
        public ActionResult CarbCountingUpdate(CarbCountingVM model, IEnumerable<HttpPostedFileBase> file)
        {

            try
            {
                string physicalPath = "~/images/CarbCounting/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("CarbCountingUpdate");
                    }
                }

                foreach (var item in resultModel.Keys)
                {
                    model.carbohydrateCounting.ImageURL = item.UploadPath;
                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            model.carbohydrateCounting.IsActive = true;
            model.carbohydrateCounting.UpdatedDate = DateTime.Now;
            model.carbohydrateCounting.CreatedDate = DateTime.Now;
            model.carbohydrateCounting.CreatedByID = 1;

            var oldCarbCounting = _unitOfWork.CarbohydrateCountingRepository.Find(x => x.ID == model.carbohydrateCounting.ID);
            if (ModelState.IsValid)
            {
                List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = oldCarbCounting.ImageURL } };
                RemoveAll(fileResultItems, "~/images/CarbCounting/");
                _unitOfWork.CarbohydrateCountingRepository.Detach(oldCarbCounting);
                _unitOfWork.CarbohydrateCountingRepository.Update(model.carbohydrateCounting);
                _unitOfWork.Save();
            }
            return RedirectToAction("CarbCountingList");
        }

        public ActionResult CarbCountingDelete(int id)
        {
            CarbohydrateCounting carbohydrateC = _unitOfWork.CarbohydrateCountingRepository.Find(x => x.ID == id);
            _unitOfWork.CarbohydrateCountingRepository.Delete(carbohydrateC);
            _unitOfWork.Save();

            List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = carbohydrateC.ImageURL } };
            RemoveAll(fileResultItems, "~/images/CarbCounting/");

            return RedirectToAction("CarbCountingList");
        }
    }
}
