using System;
using System.Collections.Generic;
using Moq;
using SuperBreakfast.Contracts.Breakfast;
using SuperBreakfast.Models;
using SuperBreakfast.Services.Breakfasts;
using SuperBreakfasts.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace SuperBreakfast.tests;

public class ControllerTests
{
    [Fact]
    public void ControllerCreateBreakfast_GivenCorrectParameters_ReturnsBreakfast()
    {
        var name = "Its now dinner time";
        var description = "This breakfast contains nutrients, lots and lost of nutrients";
        var startDateTime = DateTime.Now;
        var endDateTime = DateTime.Now.AddHours(1);
        var savory = new List<string> { "Bacon", "Sausage" };
        var sweet = new List<string> { "Maple syrup", "Whipped cream" };

        var request = new CreateBreakfastRequest(
            Name: name,
            Description: description,
            StartDateTime: startDateTime,
            EndDateTime: endDateTime,
            Savory: savory,
            Sweet: sweet
        );
        var breakfastService = new Mock<IBreakfastService>();
        var breakfastController = new BreakfastsController(breakfastService.Object);

        var result = breakfastController.CreateBreakfast(request);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var actualBreakfast = Assert.IsType<BreakfastResponse>(createdAtActionResult.Value);
        Assert.Equal(request.Name, actualBreakfast.Name);
        Assert.Equal(request.Description, actualBreakfast.Description);
        Assert.Equal(request.StartDateTime, actualBreakfast.StartDateTime);
        Assert.Equal(request.EndDateTime, actualBreakfast.EndDateTime);
        Assert.Equal(request.Savory, actualBreakfast.Savory);
        Assert.Equal(request.Sweet, actualBreakfast.Sweet);
    }

    [Fact]
    public void ControllerCreateBreakfast_GivenIncorrectParameters_ReturnsError()
    {
        var name = "It";
        var description = "This breakfast contains nutrients, lots and lost of nutrients";
        var startDateTime = DateTime.Now;
        var endDateTime = DateTime.Now.AddHours(1);
        var savory = new List<string> { "Bacon", "Sausage" };
        var sweet = new List<string> { "Maple syrup", "Whipped cream" };

        var request = new CreateBreakfastRequest(
            Name: name,
            Description: description,
            StartDateTime: startDateTime,
            EndDateTime: endDateTime,
            Savory: savory,
            Sweet: sweet
        );
        var breakfastService = new Mock<IBreakfastService>();
        var breakfastController = new BreakfastsController(breakfastService.Object);

        var result = breakfastController.CreateBreakfast(request);
        
        var createdAtActionResult = Assert.IsType<ObjectResult>(result);
        var actualBreakfast = Assert.IsType<ValidationProblemDetails>(createdAtActionResult.Value);
    }
}