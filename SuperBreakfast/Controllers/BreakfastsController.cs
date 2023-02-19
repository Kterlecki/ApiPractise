using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using SuperBreakfast.Contracts.Breakfast;
using SuperBreakfast.Controllers;
using SuperBreakfast.Models;
using SuperBreakfast.ServiceErrors;
using SuperBreakfast.Services.Breakfasts;

namespace SuperBreakfasts.Controllers;


public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {

        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

        if(requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }
        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        if (createBreakfastResult.IsError)
        {
            return Problem(createBreakfastResult.Errors);
        }
        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors)
        );

    }


    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(mapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);
        
        if(requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }
        
        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertedBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
        return upsertedBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedBreakfastResult = _breakfastService.DeleteBreakFast(id);

        return deletedBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }


    private static BreakfastResponse mapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
                    breakfast.Id,
                    breakfast.Name,
                    breakfast.Description,
                    breakfast.StartDateTime,
                    breakfast.EndDateTime,
                    breakfast.LastModifiedDateTime,
                    breakfast.Savory,
                    breakfast.Sweet);
    }
    private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: mapBreakfastResponse(breakfast));
    }
}