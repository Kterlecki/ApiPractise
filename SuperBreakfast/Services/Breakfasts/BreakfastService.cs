using SuperBreakfast.Models;

namespace SuperBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    public void CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);
    }

    public Breakfast GetBreakfast(Guid id)
    {
        return _breakfasts[id];
        //return _breakfasts.Values.FirstOrDefault(b => b.Id == id) ?? throw new KeyNotFoundException("Breakfast with Id provided, not found");
    }
}