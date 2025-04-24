using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    class ProposalDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public string Type { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateAt { get; set; }
        public List<ApprovalStepDto> Steps { get; set; }
    }
}
