﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.entidades
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<ApprovalRule> ApprovalRules { get; set; }
        public ICollection<ProjectProposal> ProjectProposals { get; set; }


    }
}
