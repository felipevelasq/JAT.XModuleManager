using FluentValidation;
using JAT.IdentityService.Domain.Constants;

namespace JAT.IdentityService.Application.Authentication.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        AddRuleForUsername();
        AddRuleForPasswordHash();
    }

    private void AddRuleForPasswordHash()
    {
        RuleFor(x => x.Password)
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
}
