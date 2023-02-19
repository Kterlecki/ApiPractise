using ErrorOr;
using SuperBreakfast.Contracts.Breakfast;
using SuperBreakfast.ServiceErrors;

namespace SuperBreakfast.Models;

public class Breakfast
{
    public const int MinNameLenght = 3;
    public const int MaxNameLenght = 50;
    public const int MinDescriptionLenght = 50;
    public const int MaxDescriptionLenght = 150;
    
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)

    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savory,
        List<string> sweet,
        Guid? id =null)
    {
        List<Error> errors = new();
        if(name.Length is < MinNameLenght or >MaxNameLenght)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }
        if(description.Length is < MinDescriptionLenght or >MaxDescriptionLenght)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }
        if( errors.Count > 0)
        {
            return errors;
        }
        
        return new Breakfast(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            savory,
            sweet);
    }

    public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );
    }
    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet,
            id
        );
    }

}

