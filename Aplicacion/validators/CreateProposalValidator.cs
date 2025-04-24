using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Aplicacion.commands.Proposals;

namespace Aplicacion.validators
{
    class CreateProposalValidator : AbstractValidator<CreateProposalCommand>
    {
        public CreateProposalValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.EstimatedAmount).GreaterThan(0);
            RuleFor(x => x.EstimatedDuration).GreaterThan(0);
        }
    }
}
