﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.entidades
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApprovalRule> ApprovalRules { get; set; }
        public ICollection<ProjectProposal> ProjectProposals { get; set; }

    }
}
