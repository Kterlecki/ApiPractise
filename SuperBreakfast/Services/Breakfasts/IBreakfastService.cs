using ErrorOr;
using SuperBreakfast.Models;

namespace SuperBreakfast.Services.Breakfasts;

public interface IBreakfastService
{

    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBreakFast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
}