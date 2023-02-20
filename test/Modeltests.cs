using System.Collections.Generic;
using System;
using Moq;
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
        Assert.NotNull(result);
        Assert.Equal(name, breakfast.Name);
        Assert.Equal(description, breakfast.Description);
        Assert.Equal(startDateTime, breakfast.StartDateTime);
        Assert.Equal(endDateTime, breakfast.EndDateTime);
        Assert.Equal(savory, breakfast.Savory);
        Assert.Equal(sweet, breakfast.Sweet);
        
       
        
    }
}