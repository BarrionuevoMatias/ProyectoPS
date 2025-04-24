using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class PendingStepDto
    {
        public long StepId { get; set; }
        public int ProposalId { get; set; }
        public string ProposalTitle { get; set; }
        public int StepOrder { get; set; }
        public string Area { get; set; }
        public string ProjectType { get; set; }
        public decimal Amount { get; set; }
    }
}
