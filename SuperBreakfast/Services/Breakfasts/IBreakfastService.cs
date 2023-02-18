using SuperBreakfast.Models;

namespace SuperBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    public void CreateBreakfast(Breakfast breakfast);
    void DeleteBreakFast(Guid id);
    Breakfast GetBreakfast(Guid id);
    void UpsertBreakfast(Breakfast breakfast);
}