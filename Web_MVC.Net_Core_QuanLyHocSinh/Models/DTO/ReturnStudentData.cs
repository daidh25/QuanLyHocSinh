namespace Web_MVC.Net_Core_QuanLyHocSinh.Models.DTO
{
    public class ReturnStudentData
    {
        public int ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
    }
    public class StudentAddReturnData : ReturnStudentData
    {
        public Student student { get; set; }
    }
    public class StudentUpdateReturnData: ReturnStudentData
    {
        public Student student { get; set;}
    }
    public class StudentDeleteReturnData : ReturnStudentData
    {
        public int ID { get; set; }
    }
}
