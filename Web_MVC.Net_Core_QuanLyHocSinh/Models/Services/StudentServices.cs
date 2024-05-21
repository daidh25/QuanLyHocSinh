using System;
using Web_MVC.Net_Core_QuanLyHocSinh.Models.DBContext;
using Web_MVC.Net_Core_QuanLyHocSinh.Models.DTO;

namespace Web_MVC.Net_Core_QuanLyHocSinh.Models.Services
{
    public class StudentServices : IStudentServices
    {
        EStudentDbContext _eStudentDbContext = new EStudentDbContext();

        public async Task<StudentAddReturnData> Add_Student(Student student)
        {
            var returnData = new StudentAddReturnData();
            try
            {
                if (student == null || string.IsNullOrEmpty(student.StudentName))
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.DataInValid;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;
                }
                var currentStudent= _eStudentDbContext.Students.ToList().Where(s=>s.StudentName == student.StudentName).FirstOrDefault();
                if (currentStudent.StudentName == student.StudentName
                    && currentStudent.DateOfBirth.ToString("yyyy/mm/dd")==student.DateOfBirth.ToString("yyyy/mm/dd")
                    && currentStudent.Email == student.Email) {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.DuplicateData;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;

                }
                await _eStudentDbContext.Students.AddAsync(student);
                var result = await _eStudentDbContext.SaveChangesAsync();
                returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Success;
                returnData.ReturnMsg = "Thêm dữ liệu thành công";
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.DataInValid;
                returnData.ReturnMsg = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn";
            }

            return returnData;
        }
        public async Task<StudentUpdateReturnData> Edit_Student(Student student)
        {
            var returnData = new StudentUpdateReturnData();
            try
            {
                if (student == null || string.IsNullOrEmpty(student.StudentName))
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.DataInValid;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;
                }

                _eStudentDbContext.Students.Update(student); 
                var result = await _eStudentDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Success;
                    returnData.ReturnMsg = "Cập nhật dữ liệu thành công";
                }
                else
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Failure;
                    returnData.ReturnMsg = "Không có bản ghi nào được cập nhật";
                }
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Failure;
                returnData.ReturnMsg = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn";
            }

            return returnData;
        }

        public async Task<StudentDeleteReturnData> Delete_Student(int ID)
        {
            var returnData = new StudentDeleteReturnData();
            try
            {
                var student = await _eStudentDbContext.Students.FindAsync(ID);
                if (student == null)
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.DataInValid;
                    returnData.ReturnMsg = "Không tìm thấy sinh viên để xóa";
                    return returnData;
                }

                _eStudentDbContext.Students.Remove(student);
                var result = await _eStudentDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Success;
                    returnData.ReturnMsg = "Xóa sinh viên thành công";
                }
                else
                {
                    returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Failure;
                    returnData.ReturnMsg = "Không có bản ghi nào được xóa";
                }
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = (int)EShop.Common.Enum_ReturnCode.Failure;
                returnData.ReturnMsg = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn";
            }

            return returnData;
        }


    }
}
