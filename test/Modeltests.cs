using System.Collections.Generic;
using System;
using SuperBreakfast.Models;
using Xunit;
using ErrorOr;

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
}