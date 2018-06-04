using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Types;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.Validator
{
    
    public interface IPaymentRequestValidator
    {
        ValidationResult IsValid(MakePaymentRequest request);
    }
}

