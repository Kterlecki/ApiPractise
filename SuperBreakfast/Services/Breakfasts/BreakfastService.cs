using ErrorOr;
using SuperBreakfast.Models;
using SuperBreakfast.ServiceErrors;

namespace SuperBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{

    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<Updated> UpsertBreakfast(Breakfast breakfast)
    {
        _breakfasts[breakfast.Id] = breakfast;

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteBreakFast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }
}