using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web_MVC.Net_Core_QuanLyHocSinh.Models;
using Web_MVC.Net_Core_QuanLyHocSinh.Models.DBContext;
using Web_MVC.Net_Core_QuanLyHocSinh.Models.DTO;
using Web_MVC.Net_Core_QuanLyHocSinh.Models.Services;


namespace Web_MVC.Net_Core_QuanLyHocSinh.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentServices _studentService;
        EStudentDbContext _eStudentDbContext = new EStudentDbContext();
        public StudentController(StudentServices studentService)
        {
            _studentService = studentService;
        }
        
        public async Task<IActionResult> Add_Student(Student student)
        {
            var returnData = new StudentAddReturnData();
            try
            {
                if (student == null || string.IsNullOrEmpty(student.StudentName))
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.DataInValid;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return (IActionResult)returnData;
                }
                var currentStudent = _eStudentDbContext.Students.ToList().Where(s => s.StudentName == student.StudentName).FirstOrDefault();
                if (currentStudent.StudentName == student.StudentName
                    && currentStudent.DateOfBirth.ToString("yyyy/mm/dd") == student.DateOfBirth.ToString("yyyy/mm/dd")
                    && currentStudent.Email == student.Email)
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.DuplicateData;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return (IActionResult)returnData;

                }
                await _eStudentDbContext.Students.AddAsync(student);
                var result = await _eStudentDbContext.SaveChangesAsync();
                returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Success;
                returnData.ReturnMsg = "Thêm dữ liệu thành công";
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.DataInValid;
                returnData.ReturnMsg = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn";
            }

            return (IActionResult)returnData;
        }
        public IActionResult Add_Student()
        {
            return View();
        }

        public async Task<IActionResult> Edit_Student(Student student)
        {
            var returnData = new StudentUpdateReturnData();
            try
            {
                if (student == null || string.IsNullOrEmpty(student.StudentName))
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.DataInValid;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return (IActionResult)returnData;
                }

                _eStudentDbContext.Students.Update(student);
                var result = await _eStudentDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Success;
                    returnData.ReturnMsg = "Cập nhật dữ liệu thành công";
                }
                else
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Failure;
                    returnData.ReturnMsg = "Không có bản ghi nào được cập nhật";
                }
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Failure;
                returnData.ReturnMsg = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn";
            }

            return (IActionResult)returnData;
        }
        public IActionResult Edit_Student()
        {
            return View();
        }
        public async Task<StudentDeleteReturnData> Delete_Student(int ID)
        {
            var returnData = new StudentDeleteReturnData();
            try
            {
                var student = await _eStudentDbContext.Students.FindAsync(ID);
                if (student == null)
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.DataInValid;
                    returnData.ReturnMsg = "Không tìm thấy sinh viên để xóa";
                    return returnData;
                }

                _eStudentDbContext.Students.Remove(student);
                var result = await _eStudentDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Success;
                    returnData.ReturnMsg = "Xóa sinh viên thành công";
                }
                else
                {
                    returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Failure;
                    returnData.ReturnMsg = "Không có bản ghi nào được xóa";
                }
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = (int)Models.EShop.Common.Enum_ReturnCode.Failure;
                returnData.ReturnMsg = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn";
            }

            return returnData;
        }
        public IActionResult Delete_Student()
        {
            return View();
        }
    }
}
