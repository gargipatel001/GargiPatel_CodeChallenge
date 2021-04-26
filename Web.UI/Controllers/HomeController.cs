using Data.Models;
using FileOperations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.UI.Utilities;
namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Remove FilePath from session
            Session.Remove("FilePath");
            return View();
        }

        public ActionResult Save()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FileUploadSuccess(string path)
        {
            return View();
        }

        public ActionResult ReadFile()
        {
            // Get file path from session
            var path = Convert.ToString(Session["FilePath"]);

            //Read uploaded CSV file
            IFileOperations fileOperation = new FileOperationsCSV();
            string[] data = fileOperation.Read(path);

            List<CarModel> carModel = new List<CarModel>();
            foreach (string row in data.Skip(1))
            {
                if (!string.IsNullOrEmpty(row))
                {

                    carModel.Add(Utility.MapCarModel(Utility.FormatRow(row)));


                }
            }

            // Get Count of Max Sold Car
            var maxcount = carModel.GroupBy(t => t.Vehicle)
                            .Select(t => new
                            {
                                Vehicle = t.Key,
                                Count = t.Count()

                            }).Max(y => y.Count);
            
            var MaxCars = carModel.GroupBy(t => t.Vehicle)
                            .Select(t => new
                            {
                                Vehicle = t.Key,
                                Count = t.Count()

                            }).Where(x => x.Count == maxcount).ToList();

            string Message = String.Empty;

            if (MaxCars != null)
            {
                foreach (var car in MaxCars)
                {
                    Message += string.Format("{0} was sold {1} times\n", car.Vehicle, car.Count);
                }
            }
            ViewBag.CarsSold = Message;

            return View("~/Views/Home/Display.cshtml", carModel);
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            ILogger logger = new Logger();
            if (ModelState.IsValid)
            {
                // Check if file is null or not
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {

                    string[] AllowedFileExtensions = new string[] { ".csv" };

                    // Validate for file type
                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please upload file of type: " + string.Join(", ", AllowedFileExtensions));
                    }


                    else
                    {

                        FileDetailsModel fileModel = new FileDetailsModel();
                        fileModel.FileDetail = file;
                        fileModel.SavePath = Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName);
                        fileModel.FileName = file.FileName;
                        IFileOperations fileOperation = new FileOperationsCSV();
                        bool isUploadSuccess = fileOperation.Upload(fileModel);

                        if (isUploadSuccess)
                        {
                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully";
                            logger.Log(new LoggerModel() { FileName = file.FileName, FileSize = file.ContentLength, isSuccess = true, Message = "", Path = Server.MapPath("~/UploadedFiles") });
                            Session["FilePath"] = fileModel.SavePath;
                            return RedirectToAction("FileUploadSuccess");
                        }
                        else
                        {
                            logger.Log(new LoggerModel() { FileName = file.FileName, FileSize = file.ContentLength, isSuccess = false, Message = "", Path = Server.MapPath("~/UploadedFiles") });
                            ModelState.AddModelError("File", "Error occured during upload");
                        }


                    }
                }
            }
            return RedirectToAction("Index");
        }

        // Download Sample CSV file
        public FileResult Download()
        {
            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), "Sample.csv");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Sample.csv";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}