namespace LeaveManagementSystem.Web.Models.Periods
{
    public class PeriodCreateViewModel
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Period name must be between 4 and 150 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateOnly StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateOnly EndDate { get; set; }
    }
}
