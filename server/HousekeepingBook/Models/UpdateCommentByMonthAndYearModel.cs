namespace HousekeepingBook.Models
{
    public class UpdateCommentByMonthAndYearModel
    {
        public int Month { get; set; }
        public string Year { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}
