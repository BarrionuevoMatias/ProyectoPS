using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.entidades
{
    public class ProjectProposal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Area { get; set; }
        public Area AreaNavigation { get; set; }
        public int type { get; set; }
        public ProjectType TypeNavigation { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int Status { get; set; }
        public ApprovalStatus StatusNavigation { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreatedBy { get; set; }
        public User CreatedByNavigation { get; set; }

        public ICollection<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
    }
}
