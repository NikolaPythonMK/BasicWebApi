using BasicWebApi.Domain.DTO;
using FluentValidation;

namespace BasicWebApi.Core.Validations
{
    public class CompanyValidation : AbstractValidator<CompanyDTO>
    {
        public CompanyValidation()
        {
            RuleFor(s => s.CompanyName)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
