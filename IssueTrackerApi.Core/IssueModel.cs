using System;

namespace IssueTrackerApi.Core
{
    public class IssueModel
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Statuses Status { get; set; }
        public string Number { get; set; }
        public Project Project { get; set; }
        public User CreatedBy { get; set; }
        public User AssignedTo { get; set; }
    }
}
