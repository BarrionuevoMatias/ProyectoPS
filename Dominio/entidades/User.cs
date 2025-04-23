using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.entidades
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public ApproverRole ApproverRole { get; set; }

        public ICollection<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
        public ICollection<ProjectProposal> ProjectProposals { get; set; }
    }
}
