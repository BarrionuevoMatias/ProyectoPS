using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ApprovalStepDto
    {
        public long Id { get; set; }
        public int StepOrder { get; set; }
        public string ApproverRole { get; set; }
        public string Status { get; set; }
        public string ApproverUser { get; set; }
        public string DecisionDate { get; set; }
        public string Observations { get; set; }
    }
}
