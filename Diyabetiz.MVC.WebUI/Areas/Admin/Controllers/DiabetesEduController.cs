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
    public class DiabetesEduController : Controller
    {
        UnitOfWork _unitofWork;
        DiabetesEduVM _diabetesEduVM;
        public DiabetesEduController()
        {
            _unitofWork = new UnitOfWork();
            _diabetesEduVM = new DiabetesEduVM();
        }

        [HttpGet]
        public ActionResult DiabetesEduList()
        {
            var diabetesEduList = _unitofWork.DiabetesEducationRepository.Select().ToList();
            return View(diabetesEduList);
        }
        [HttpGet]
        public ActionResult DiabetesEduAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DiabetesEduAdd(DiabetesEduVM model, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                string physicalPath = "~/images/DiabetesEdu/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("DiabetesEduAdd");
                    }
                }

                foreach (var item in resultModel.Keys)
                {

                    _unitofWork.DiabetesEducationRepository.Insert(new DiabetesEducation { Title = model.diabetesEducation.Title, ImageURL = item.UploadPath, Description = model.diabetesEducation.Description, CreatedDate = DateTime.Now });
                    _unitofWork.Save();

                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            return RedirectToAction("DiabetesEduList");
        }

        [HttpGet]
        public ActionResult DiabetesEduUpdate(int id)
        {
            DiabetesEducation diabetesEducation = _unitofWork.DiabetesEducationRepository.Find(x => x.ID == id);
            _diabetesEduVM.diabetesEducation = diabetesEducation;
            return View(_diabetesEduVM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DiabetesEduUpdate(DiabetesEduVM model, IEnumerable<HttpPostedFileBase> file)
        {

            try
            {
                string physicalPath = "~/images/DiabetesEdu/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("DiabetesEduUpdate");
                    }
                }

                foreach (var item in resultModel.Keys)
                {
                    model.diabetesEducation.ImageURL = item.UploadPath;
                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            model.diabetesEducation.IsActive = true;
            model.diabetesEducation.UpdatedDate = DateTime.Now;
            model.diabetesEducation.CreatedDate = DateTime.Now;
            model.diabetesEducation.CreatedByID = 1;

            var oldDiabetesEdu = _unitofWork.DiabetesEducationRepository.Find(x => x.ID == model.diabetesEducation.ID);
            if (ModelState.IsValid)
            {
                List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = oldDiabetesEdu.ImageURL } };
                RemoveAll(fileResultItems, "~/images/DiabetesEdu/");
                _unitofWork.DiabetesEducationRepository.Detach(oldDiabetesEdu);
                _unitofWork.DiabetesEducationRepository.Update(model.diabetesEducation);
                _unitofWork.Save();
            }
            return RedirectToAction("DiabetesEduList");
        }

        public ActionResult DiabetesEduDelete(int id)
        {
            DiabetesEducation diabetesEducation = _unitofWork.DiabetesEducationRepository.Find(x => x.ID == id);
            _unitofWork.DiabetesEducationRepository.Delete(diabetesEducation);
            _unitofWork.Save();

            List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = diabetesEducation.ImageURL } };
            RemoveAll(fileResultItems, "~/images/DiabetesEdu/");

            return RedirectToAction("DiabetesEduList");
        }
    }
}