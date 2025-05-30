namespace LeaveManagementSystem.Web.Data
{
    public class LeaveRequestStatus: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}