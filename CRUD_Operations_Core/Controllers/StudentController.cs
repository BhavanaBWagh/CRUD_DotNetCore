using CRUD_Operations_Core.Models;
using CRUD_Operations_Core.Models.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.IO;

namespace CRUD_Operations_Core.Controllers
{
    public class StudentController : Controller
    {
        IStudentServices _studentServices = null;
        List<Student> students = new List<Student>();

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public string GenerateEmptyExcel()
        {
            byte[] fileContents = null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Students");
                ws.Cells["A1:C1"].Merge = true;
                ws.Cells["A1:C1"].Value = "School Name";
                ws.Cells["A1:C1"].Style.Font.Bold = true;
                ws.Cells["A1:C1"].Style.Font.Size = 16;
                ws.Cells["A1:C1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells["A2"].Value = "Student list";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A2:C2"].Merge = true;

                ws.Cells["A3"].Value = "Name";
                ws.Cells["B3"].Value = "Roll";
                ws.Cells["C3"].Value = "Age";
                ws.Cells["A3:C3"].Style.Font.Bold = true;
                ws.Cells["A3:C3"].Style.Font.Size = 12;
                ws.Cells["A3:C3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A3:C3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);
                ws.Cells["A3:C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A3:C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                pck.Save();
                fileContents = pck.GetAsByteArray();
            }
            return Convert.ToBase64String(fileContents);
        }


        public string GenerateAndDownloadExcel(int studentid, string name)
        {
            List<Student> students = _studentServices.GetStudents();

            DataTable dataTable = CommonMethods.ConvertToDataTable(students);

            dataTable.Columns.Remove("StudentId");

            byte[] fileContents = null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Students");
                ws.Cells["A1:C1"].Merge = true;
                ws.Cells["A1:C1"].Value = "School Name";
                ws.Cells["A1:C1"].Style.Font.Bold = true;
                ws.Cells["A1:C1"].Style.Font.Size = 16;
                ws.Cells["A1:C1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
              

                ws.Cells["A2"].Value = "Student list";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A2:C2"].Merge = true;

                ws.Cells["A3"].LoadFromDataTable(dataTable, true);
                ws.Cells["A3:C3"].Style.Font.Bold = true;
                ws.Cells["A3:C3"].Style.Font.Size = 12;
                ws.Cells["A3:C3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A3:C3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);
                ws.Cells["A3:C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A3:C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                pck.Save();
                fileContents = pck.GetAsByteArray();
            }
          return  Convert.ToBase64String(fileContents);
        }

        private MemoryStream ConvertStringToMemoryStream(string input)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(input);
            return new MemoryStream(byteArray);
        }

        public List<Student> ReadDataFromExcel(IFormFile filePath)
        {
            var Students = new List<Student>();
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                 filePath.CopyTo(stream);
                stream.Position = 0; 
                using (var package = new ExcelPackage(stream))
                {
                    var workbook = package.Workbook;
                    int cnt = workbook.Worksheets.Count;
                    var worksheet = workbook.Worksheets[0];

                    for (int row = 4; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var student = new Student
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString(),
                            Roll = worksheet.Cells[row, 2].Value.ToString(),
                            Age = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                        };

                        Students.Add(student);
                    }
                }
            }
           return   Students;
        }

        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "File not selected or file is empty";
                return View("Index");
            }

            if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
            {
                ViewBag.Message = "Invalid file type. Please upload an Excel file.";
                return View("Index");
            }

            var Students = new List<Student>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    var workbook = package.Workbook;
                    int cnt = workbook.Worksheets.Count;
                    var worksheet = workbook.Worksheets[0];

                    for (int row = 4; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var student = new Student
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString(),
                            Roll = worksheet.Cells[row, 2].Value.ToString(),
                            Age = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                        };

                        Students.Add(student);
                    }
                }
            }

            _studentServices.SaveStudents(Students);

            return View("Index");
        }

        public IActionResult ExcelTableDataPartial()
        {
            students = _studentServices.GetStudents();
            return PartialView("_ExcelTableData", students);
        }
        }
}
