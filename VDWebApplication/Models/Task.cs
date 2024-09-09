namespace VDWebApplication.Models
{
    public class Tasks
    {
        public string? TaskId { get; set; }
        public string? UserId { get; set; }
        public string? TaskTitle { get; set; }
        public int? TotalJob { get; set; }
        public int? TotalCompletedJob { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
