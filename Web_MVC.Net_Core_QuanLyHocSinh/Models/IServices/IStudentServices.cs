using Web_MVC.Net_Core_QuanLyHocSinh.Models.DTO;

namespace Web_MVC.Net_Core_QuanLyHocSinh.Models.Services
{
    public interface IStudentServices
    {
        
        Task <StudentAddReturnData> Add_Student(Student student);
        
        Task <StudentUpdateReturnData>Edit_Student(Student student);
        Task <StudentDeleteReturnData>Delete_Student(int ID);
    }
}
