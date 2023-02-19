using ErrorOr;

namespace SuperBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be atleast {Models.Breakfast.MinNameLenght} " +
            $"and not greater than {Models.Breakfast.MaxNameLenght} characters long"
        );
        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast Description must be atleast {Models.Breakfast.MinDescriptionLenght} " +
            $"and not greater than {Models.Breakfast.MaxDescriptionLenght} characters long"
        );
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found"
        );

    }
}