﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class LeaveRequestCreateViewModel: IValidatableObject
    {
        [Required]
        //[DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateOnly EndDate { get; set; }

        [DisplayName("Desired Leave Type")]
        public int LeaveTypeId { get; set; }

        [StringLength(250)]
        [DisplayName("Additional Information")]
        public string? RequestComments { get; set; }

        public SelectList? LeaveTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult("The start date must be before the end date.", new[] { nameof(StartDate), nameof(EndDate) });
            }
        }
    }
}