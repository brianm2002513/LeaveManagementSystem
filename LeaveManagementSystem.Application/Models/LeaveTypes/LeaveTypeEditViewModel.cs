﻿namespace LeaveManagementSystem.Application.Models.LeaveTypes
{
    public class LeaveTypeEditViewModel : BaseModel
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Leave name must be between 4 and 150 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(1, 90)]
        [Display(Name = "Maximum allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}
