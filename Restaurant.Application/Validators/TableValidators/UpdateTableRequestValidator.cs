using FluentValidation;
using Restaurant.Application.DTOs.Requests.TablesRequests;

namespace Restaurant.Application.Validators.TableValidators;

public class UpdateTableRequestValidator : AbstractValidator<UpdateTableRequest>
{
    public UpdateTableRequestValidator()
    {
        RuleFor(x => x.TableNumber)
            .NotEmpty().WithMessage("Table number is required.")
            .GreaterThan(0).WithMessage("Table number must be greater than 0.");

        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("Capacity is required.")
            .GreaterThan(0).WithMessage("Capacity must be greater than 0.");
    }
}
