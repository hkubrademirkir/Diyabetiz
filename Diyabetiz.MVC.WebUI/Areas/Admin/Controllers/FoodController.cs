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
    public class FoodController : Controller
    {
        UnitOfWork _unitOfWork;
        FoodVM _foodVM;

        public FoodController()
        {
            _unitOfWork = new UnitOfWork();
            _foodVM = new FoodVM();
        }
        // GET: Admin/Food
        public ActionResult FoodList()
        {
            var foodList = _unitOfWork.FoodRepository.Select().ToList();
            return View(foodList);
        }
        [HttpGet]
        public ActionResult FoodAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FoodAdd(FoodVM model, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                string physicalPath = "~/images/Food/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("FoodAdd");
                    }
                }

                foreach (var item in resultModel.Keys)
                {

                    _unitOfWork.FoodRepository.Insert(new Food { Name = model.food.Name, ImageURL = item.UploadPath, Description = model.food.Description, CreatedDate = DateTime.Now, CarbValue = model.food.CarbValue, CreatedByID = 1, IsActive = true, UpdatedDate = DateTime.Now });
                    _unitOfWork.Save();

                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            return RedirectToAction("FoodList");
        }
        [HttpGet]
        public ActionResult FoodUpdate(int id)
        {
            Food food = _unitOfWork.FoodRepository.Find(x => x.ID == id);
            _foodVM.food = food;
            return View(_foodVM);
        }
        [HttpPost]
        public ActionResult FoodUpdate(FoodVM model, IEnumerable<HttpPostedFileBase> file)
        {

            try
            {
                string physicalPath = "~/images/Food/";
                int maxFileSize = 500000;

                Dictionary<FileResultItem, FileResultType> resultModel = FileDocumentUpload(file, maxFileSize, physicalPath, new string[] { "image/gif", "image/png", "image/jpeg", "image/pjpeg", "image/bmp", "image/x-png", "image/jpg" });

                foreach (var item in resultModel)
                {
                    if (item.Value == FileResultType.Error || item.Value == FileResultType.NoneFile || item.Value == FileResultType.SizeOver || item.Value == FileResultType.WrongType)
                    {
                        RemoveAll(resultModel.Keys, physicalPath);
                        TempData["NoteCss"] = "warning";
                        TempData["NoteText"] = ControlMessages(item.Value, maxFileSize).Keys.FirstOrDefault().ToString();

                        return View("FoodUpdate");
                    }
                }

                foreach (var item in resultModel.Keys)
                {
                    model.food.ImageURL = item.UploadPath;
                }
            }
            catch (Exception ex)
            {
                TempData["NoteCss"] = "danger";
                TempData["NoteText"] = "Bilinmeyen Hata!";
                TempData["NoteError"] = ex.Message;
            }
            model.food.IsActive = true;
            model.food.UpdatedDate = DateTime.Now;
            model.food.CreatedDate = DateTime.Now;
            model.food.CreatedByID = 1;

            var oldFood = _unitOfWork.FoodRepository.Find(x => x.ID == model.food.ID);
            if (ModelState.IsValid)
            {
                List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = oldFood.ImageURL } };
                RemoveAll(fileResultItems, "~/images/Food/");
                _unitOfWork.FoodRepository.Detach(oldFood);
                _unitOfWork.FoodRepository.Update(model.food);
                _unitOfWork.Save();
            }
            return RedirectToAction("FoodList");
        }

        public ActionResult FoodDelete(int id)
        {
            Food food = _unitOfWork.FoodRepository.Find(x => x.ID == id);
            _unitOfWork.FoodRepository.Delete(food);
            _unitOfWork.Save();

            List<FileResultItem> fileResultItems = new List<FileResultItem> { new FileResultItem { UploadPath = food.ImageURL } };
            RemoveAll(fileResultItems, "~/images/Food/");

            return RedirectToAction("FoodList");
        }
    }
}