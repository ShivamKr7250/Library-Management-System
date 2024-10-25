using LMS.DataAccess.Repository.IRepository;
using LMS.Models;
using LMS.Models.ViewModels;
using LMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Books = LMS.Models.Books;

namespace LMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Books> objBooksList = _unitOfWork.Books.GetAll(includeProperties: "Category").ToList();
            return View(objBooksList);
        }

        public IActionResult Upsert(int? id)
        {
            // Initialize the ViewModel with CategoryList and Books model
            BookVM BooksVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString(),
                }),
                Books = new Books()
            };

            if (id == null || id == 0)
            {
                // Create mode
                return View(BooksVM);
            }
            else
            {
                // Update mode - load the specific book
                BooksVM.Books = _unitOfWork.Books.Get(u => u.Id == id, includeProperties: "Category");

                // Load available codes for the existing category of the book
                if (BooksVM.Books.Category != null)
                {
                    BooksVM.CodeList = BooksVM.Books.Category.Code
                        .Where(code => !_unitOfWork.Books.GetAll(b => b.CategoryId == BooksVM.Books.CategoryId && b.SerialName == code).Any())
                        .ToList();
                }

                return View(BooksVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(BookVM BooksVM, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                int categoryId = BooksVM.Books.CategoryId;
                var category = _unitOfWork.Category.Get(c => c.Id == categoryId);

                if (BooksVM.Books.Id == 0)
                {
                    // Assign the first available code from the category's code list
                    var availableCode = category.Code
                        .FirstOrDefault(code => !_unitOfWork.Books.GetAll(b => b.CategoryId == categoryId && b.SerialName == code).Any());

                    if (availableCode != null)
                    {
                        BooksVM.Books.SerialName = availableCode; // Assign the available code
                    }
                    else
                    {
                        TempData["error"] = "No available codes left for this category.";
                        return RedirectToAction("Upsert");
                    }

                    _unitOfWork.Books.Add(BooksVM.Books);
                }
                else
                {
                    // Update book details, keep the existing code if not changed
                    _unitOfWork.Books.Update(BooksVM.Books);
                }

                _unitOfWork.Save();
                TempData["success"] = "Book Created/Updated Successfully";
                return RedirectToAction("Index", "Books");
            }
            else
            {
                // Reload CategoryList and CodeList if the model state is invalid
                BooksVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString(),
                });

                // Reload available codes for the selected category
                BooksVM.CodeList = _unitOfWork.Category.Get(c => c.Id == BooksVM.Books.CategoryId)?.Code;
                return View(BooksVM);
            }
        }



        //public IActionResult DeleteImage(int imageId)
        //{
        //    var imageToBeDeleted = _unitOfWork.BooksImage.Get(u => u.Id == imageId);
        //    int BooksId = imageToBeDeleted.BooksId;
        //    if (imageToBeDeleted != null)
        //    {
        //        if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
        //        {
        //            //delete th old Image
        //            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));

        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }

        //        }
        //        _unitOfWork.BooksImage.Remove(imageToBeDeleted);
        //        _unitOfWork.Save();

        //        TempData["success"] = "Deleted Successfully";
        //    }

        //    return RedirectToAction(nameof(Upsert), new {id = BooksId});
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Books> objBooksList = _unitOfWork.Books.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objBooksList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var BooksToBeDeleted = _unitOfWork.Books.Get(u => u.Id == id);
            if (BooksToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }


            string BooksPath = @"images\Bookss\Books-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, BooksPath);

            if (!Directory.Exists(finalPath))
            {
                string[] filepaths = Directory.GetFiles(finalPath);
                foreach (string filepath in filepaths)
                {
                    System.IO.File.Delete(filepath);
                }
                Directory.Delete(finalPath);
            }
               

            _unitOfWork.Books.Remove(BooksToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successful" });
        }
        #endregion
    }
}
