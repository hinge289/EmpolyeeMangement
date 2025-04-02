namespace EmpolyeeMangement.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmpolyeeId { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int OverTime { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
       
    }
}
