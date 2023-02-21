using Xunit;
using SuperBreakfast.Services.Breakfasts;
using Moq;
using SuperBreakfast.Models;
using System;
using System.Collections.Generic;
using ErrorOr;

namespace SuperBreakfast.tests;

public class ServiceTests
{
    [Fact]
    public void BreakfastCreate_GivenCorrectParamaters_ReturnsBreakfast()
    {
        
       var breakfastService = new BreakfastService();
       var initialCount = breakfastService.GetDictionaryCount();
       var breakfast = Breakfast.Create(
        "Its now dinner time",
        "This breakfast contains nutrients, lots and lost of nutrients",
        new DateTime(2023, 02, 20, 8, 0, 0),
        new DateTime(2023, 02, 20, 10, 0, 0),
        new List<string> { "Oatmeal", "Avocado Toast", "Omelette", "Salad" },
        new List<string> { "Pancakes", "Waffles" },
        Guid.NewGuid()
    );
    
    // Act
    var result = breakfastService.CreateBreakfast(breakfast.Value);

    // Assert
    Assert.IsType<Created>(result.Value);
    Assert.IsType<ErrorOr<Created>>(result);

    Assert.Equal(0, initialCount);
    Assert.Equal(1, breakfastService.GetDictionaryCount());
    }
}