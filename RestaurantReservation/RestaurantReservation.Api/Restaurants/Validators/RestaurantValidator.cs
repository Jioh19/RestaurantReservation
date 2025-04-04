using FluentValidation;
using RestaurantReservation.Api.Contracts.Restaurants.Models;

namespace RestaurantReservation.Api.Restaurants.Validators;

public class RestaurantValidator : AbstractValidator<RestaurantRequest>
{
    public RestaurantValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Restaurant name is required and cannot be longer than 50 characters");
        
        RuleFor(r => r.Address)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Restaurant address is required and cannot be longer than 100 characters");
        
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .WithMessage("Restaurant phone number is required");
        
        RuleFor(r => r.OpeningHours)
            .NotEmpty()
            .WithMessage("Restaurant opening hours are required");
    }
}