
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb
{
    public class BaseRequest
    {
        public int Id { get; set; }
    }

    public  class BaseRequestValidate: AbstractValidator<BaseRequest>
    {
        public BaseRequestValidate()
        {
            RuleFor(t => t.Id).NotNull().GreaterThanOrEqualTo(1).WithMessage("行程总天数至少为1天");
        }
    }
}
