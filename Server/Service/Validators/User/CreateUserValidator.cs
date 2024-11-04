using FluentValidation;
using Service.Transfermodels.Request;

namespace Service.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        //Validation for username
        RuleFor(u => u.Username)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Username can max be 50 characters");
        
        //Validation for email
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Must be a valid email");

        //Validation for password
        RuleFor(u => u.Passwordhash)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Must be at least 8 characters")
            .Matches("[A-Z]").WithMessage("Must have one uppercase letter")
            .Matches("[a-z]").WithMessage("Must have one lowercase letter")
            .Matches("[0-9]").WithMessage("Must contain one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("must contain at least on special character");
    }
}