using BasicWebApi.Domain.DTO;
using FluentValidation;

namespace BasicWebApi.Core.Validations
{
    public class ContactValidation : AbstractValidator<ContactDTO>
    {
        public ContactValidation()
        {
            RuleFor(s => s.ContactName)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
