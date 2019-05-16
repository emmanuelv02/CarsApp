using CarsFunctionApp.Entities;
using FluentValidation;

namespace CarsFunctionApp.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Year).InclusiveBetween(1970, 2020);
        }
    }
}