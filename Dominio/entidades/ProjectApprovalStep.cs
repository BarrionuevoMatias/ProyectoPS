using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.entidades
{
    public class ProjectApprovalStep
    {
        public long Id { get; set; }
        public int ProjectProposalId { get; set; }
        public ProjectProposal ProjectProposal { get; set; }
        
        public int StepOrder { get; set; }
        public int ApproverRoleId { get; set; }
        public ApproverRole ApproverRole { get; set; }
        public int Status { get; set; }
        public ApprovalStatus StatusNavigation { get; set; }
        public int? ApproverUserId { get; set; }
        public User ApproverUser { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string? Observations { get; set; }

    }
}
