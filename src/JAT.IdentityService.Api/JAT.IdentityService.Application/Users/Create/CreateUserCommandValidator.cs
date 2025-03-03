using FluentValidation;
using JAT.IdentityService.Domain.Constants;

namespace JAT.IdentityService.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        AddRuleForUsername();
        AddRuleForEmail();
        AddRuleForPasswordHash();
    }

    private void AddRuleForPasswordHash()
    {
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is required")
            .MaximumLength(MaxLengths.User.PasswordHash)
            .WithMessage($"Password must be less than {MaxLengths.User.PasswordHash} characters");
    }

    private void AddRuleForUsername()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MaximumLength(MaxLengths.User.Username)
            .WithMessage($"Username must be less than {MaxLengths.User.Username} characters");
    }

    private void AddRuleForEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid")
            .MaximumLength(MaxLengths.User.Email)
            .WithMessage($"Email must be less than {MaxLengths.User.Email} characters");
    }
}
