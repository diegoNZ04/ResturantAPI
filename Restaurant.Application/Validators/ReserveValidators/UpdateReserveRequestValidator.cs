using FluentValidation;
using Restaurant.Application.DTOs.Requests.ReservesRequests;

namespace Restaurant.Application.Validators.ReserveValidators
{
    public class UpdateReserveRequestValidator : AbstractValidator<UpdateReserveRequest>
    {
        public UpdateReserveRequestValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty().WithMessage("Table number is required.")
                .GreaterThan(0).WithMessage("Table number must be greater than 0.");

            RuleFor(x => x.PeopleNumber)
                .NotEmpty().WithMessage("People number is required.")
                .GreaterThan(0).WithMessage("People number must be greater than 0.");

            RuleFor(x => x.ReserveDate)
                .NotEmpty().WithMessage("Reserve date is required.")
                .Must(BeInTheFuture).WithMessage("Reserve date must be in the future.");
        }

        private bool BeInTheFuture(DateTime date)
        {
            var localTime = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTime);
            return date > now;
        }
    }
}