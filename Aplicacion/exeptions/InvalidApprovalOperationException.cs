using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.exeptions
{
    class InvalidApprovalOperationException : Exception
    {
        public InvalidApprovalOperationException(string message) : base(message)
        {
        }
    }
}
