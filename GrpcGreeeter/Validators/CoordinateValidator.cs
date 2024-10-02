using FluentValidation;

namespace GrpcGreeeter.Validators;

public class CoordinateValidator : AbstractValidator<AddCoordinatesRequest>
{
    public CoordinateValidator()
    {
        RuleFor(x => x.A["X"]).GreaterThanOrEqualTo(0);
        RuleFor(x => x.A["Y"]).GreaterThanOrEqualTo(0);
        RuleFor(x => x.B["X"]).GreaterThanOrEqualTo(0);
        RuleFor(x => x.B["Y"]).GreaterThanOrEqualTo(0);
    }
}