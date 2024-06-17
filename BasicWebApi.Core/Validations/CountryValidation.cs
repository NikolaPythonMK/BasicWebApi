using BasicWebApi.Domain.DTO;
using FluentValidation;

namespace BasicWebApi.Core.Validations
{
    public class CountryValidation : AbstractValidator<CountryDTO>
    {
        public CountryValidation()
        {
            RuleFor(s => s.CountryName)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
