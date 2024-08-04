using CrudWithImageUploading.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace CrudWithImageUploading.Controllers
{
    public class StudentController : Controller
    {
        private readonly MyDbContext _myDb;
        private readonly IWebHostEnvironment _web;
        public StudentController(MyDbContext myDb, IWebHostEnvironment web)
        {
            _myDb = myDb;
            _web = web;
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ViewModel viewModel, IFormFile Std_Image) 
        {
            if (Std_Image == null || Std_Image.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a file.");
                return View(viewModel);
            }
            var path = Path.Combine(_web.WebRootPath,"Student_Images");
            string fileName = Path.GetFileName(Std_Image.FileName);
            var fileExten = Path.GetExtension(fileName);
            var ds = DateTime.Now.Millisecond;
            string imageName = "StdImage" + ds + fileExten;
            var fileSavePath = Path.Combine(path,imageName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(fileSavePath,FileMode.Create))
            {
                Std_Image.CopyTo(stream);
            }
            viewModel.studentInfo.Std_Image = imageName;

            _myDb.Students.Add(viewModel.students);
            _myDb.SaveChanges();
            viewModel.studentInfo.Student_Id = viewModel.students.Std_Id;
            _myDb.StudentsInfo.Add(viewModel.studentInfo);
            _myDb.SaveChanges();
            TempData["SuccessMessage"] = "Swal.fire({title: 'Congratulations!',text: 'Your Data Has Been SuccessFully Submitted!',icon: 'success'})";
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult ShowData()
        {
            var studentData = _myDb.Students.Include(student => student.StudentInfos).Select(std => new ViewModel
            {
                students = std,
                studentInfo = std.StudentInfos.FirstOrDefault()!
            }).OrderByDescending(id => id.students.Std_Id).ToList();
            return View(studentData);
        }

        [HttpGet]
        public IActionResult EditData(int id)
        {
            var countries = _myDb.StudentsInfo.Select(country => country.Country).Distinct().ToList();
            var cities = _myDb.StudentsInfo.Select(city => city.City).Distinct().ToList();

            var studentData = _myDb.Students.Include(student => student.StudentInfos).Where(stdId => stdId.Std_Id == id).Select(std => new ViewModel
            {
                students = std,
                studentInfo = std.StudentInfos.FirstOrDefault()!
            }).FirstOrDefault();
            ViewBag.Country = countries;
            ViewBag.City = cities;
            return View(studentData);
        }

        [HttpPost]
        public IActionResult EditData(ViewModel viewModel, IFormFile Std_Image)
        {
            if (Std_Image != null)
            {
                if (!string.IsNullOrEmpty(viewModel.studentInfo.Std_Image))
                {
                    var oldImage = Path.Combine(_web.WebRootPath, "Student_Images", viewModel.studentInfo.Std_Image!);
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                var path = Path.Combine(_web.WebRootPath, "Student_Images");
                string fileName = Path.GetFileName(Std_Image.FileName);
                var fileExten = Path.GetExtension(fileName);
                var ds = DateTime.Now.Millisecond;
                string imageName = "StdImage" + ds + fileExten;
                var fileSavePath = Path.Combine(_web.WebRootPath, "Student_Images", imageName);
                var saveDirectory = Path.GetDirectoryName(fileSavePath);
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }
                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                {
                    Std_Image.CopyTo(stream);
                }
                viewModel.studentInfo.Std_Image = imageName;
            }

            _myDb.Students.Update(viewModel.students);
            _myDb.SaveChanges();
            viewModel.studentInfo.Student_Id = viewModel.students.Std_Id;
            _myDb.StudentsInfo.Update(viewModel.studentInfo);
            _myDb.SaveChanges();

            return View(viewModel);
        }


        public IActionResult DeleteData(int id)
        {
            var stdInfo = _myDb.StudentsInfo.FirstOrDefault(sInfo => sInfo.Student_Id == id);
            var student = _myDb.Students.FirstOrDefault(s => s.Std_Id == id);

            if (!string.IsNullOrEmpty(stdInfo.Std_Image))
            {
                var oldImage = Path.Combine(_web.WebRootPath, "Student_Images", stdInfo.Std_Image!);
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }
            
            if (stdInfo != null)
            {
                _myDb.StudentsInfo.Remove(stdInfo);
            }

            if (student != null)
            {
                _myDb.Students.Remove(student);
            }
            
            _myDb.SaveChanges();

            return RedirectToAction("ShowData");
        }






    }
}
