using System.Collections.Generic;
using System;
using SuperBreakfast.Models;
using Xunit;
using ErrorOr;
using SuperBreakfast.Contracts.Breakfast;

namespace SuperBreakfast.tests;

public class ModelTests
{
    [Fact]
    public void BreakfastCreate_GivenCorrectParamaters_ReturnsBreakfast()
    {
        var name = "Its now dinner time";
        var description = "This breakfast contains nutrients, lots and lost of nutrients";
        var startDateTime = new DateTime(2023, 02, 20, 8, 0, 0);
        var endDateTime = new DateTime(2023, 02, 20, 10, 0, 0);
        var savory = new List<string> { "Oatmeal", "Avocado Toast", "Omelette", "Salad" };
        var sweet = new List<string> { "Pancakes", "Waffles" };

        var result = Models.Breakfast.Create(name, 
        description,
        startDateTime,
        endDateTime,
        savory,
        sweet,
        Guid.NewGuid()
        );
        var breakfast = result.Value;

        Assert.IsType<ErrorOr<Breakfast>>(result);
        Assert.Equal(name, breakfast.Name);
        Assert.Equal(description, breakfast.Description);
        Assert.Equal(startDateTime, breakfast.StartDateTime);
        Assert.Equal(endDateTime, breakfast.EndDateTime);
        Assert.Equal(savory, breakfast.Savory);
        Assert.Equal(sweet, breakfast.Sweet);
    }

    [Fact]
    public void BreakfastCreate_GivenNameIncorrectParamaters_ReturnsError()
    {
        var name = "It";
        var description = "This breakfast contains nutrients, lots and lost of nutrients";
        var startDateTime = new DateTime(2023, 02, 20, 8, 0, 0);
        var endDateTime = new DateTime(2023, 02, 20, 10, 0, 0);
        var savory = new List<string> { "Oatmeal", "Avocado Toast", "Omelette", "Salad" };
        var sweet = new List<string> { "Pancakes", "Waffles" };

        var result = Models.Breakfast.Create(name, 
        description,
        startDateTime,
        endDateTime,
        savory,
        sweet,
        Guid.NewGuid()
        );

        Assert.True(result.IsError);

    }

    [Fact]
    public void BreakfastCreate_GivenDescriptionIncorrectParamaters_ReturnsError()
    {
        var name = "Its now breakfast time";
        var description = "This breakfast";
        var startDateTime = new DateTime(2023, 02, 20, 8, 0, 0);
        var endDateTime = new DateTime(2023, 02, 20, 10, 0, 0);
        var savory = new List<string> { "Oatmeal", "Avocado Toast", "Omelette", "Salad" };
        var sweet = new List<string> { "Pancakes", "Waffles" };

        var result = Models.Breakfast.Create(name, 
        description,
        startDateTime,
        endDateTime,
        savory,
        sweet,
        Guid.NewGuid()
        );

        Assert.True(result.IsError);

    }


    [Fact]
    public void BreakfastFrom_GivenCorrectValues_ReturnBreakfast()
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

        var result = Models.Breakfast.From(request);

        Assert.IsType<ErrorOr<Breakfast>>(result);
        Assert.Equal(name, result.Value.Name);
        Assert.Equal(description, result.Value.Description);
        Assert.Equal(startDateTime, result.Value.StartDateTime);
        Assert.Equal(endDateTime, result.Value.EndDateTime);
        Assert.Equal(savory, result.Value.Savory);
        Assert.Equal(sweet, result.Value.Sweet);
    }

    [Fact]
    public void BreakfastFrom_GivenCorrectValuesOfIdAndUpsertBreakfastRequest_ReturnBreakfast()
    {
        var name = "Its now dinner time";
        var description = "This breakfast contains nutrients, lots and lost of nutrients";
        var startDateTime = DateTime.Now;
        var endDateTime = DateTime.Now.AddHours(1);
        var savory = new List<string> { "Bacon", "Sausage" };
        var sweet = new List<string> { "Maple syrup", "Whipped cream" };
        var id = Guid.NewGuid();

        var request = new UpsertBreakfastRequest(
            Name: name,
            Description: description,
            StartDateTime: startDateTime,
            EndDateTime: endDateTime,
            Savory: savory,
            Sweet: sweet
        );

        var result = Models.Breakfast.From(id, request);

        Assert.IsType<ErrorOr<Breakfast>>(result);
        Assert.Equal(name, result.Value.Name);
        Assert.Equal(description, result.Value.Description);
        Assert.Equal(startDateTime, result.Value.StartDateTime);
        Assert.Equal(endDateTime, result.Value.EndDateTime);
        Assert.Equal(savory, result.Value.Savory);
        Assert.Equal(sweet, result.Value.Sweet);
        Assert.Equal(id, result.Value.Id);
    }

}