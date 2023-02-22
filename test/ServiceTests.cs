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


    [Fact]
    public void BreakfastDelete_GivenCorrectParamaters_ReturnsNotFound()
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
    var createResult = breakfastService.CreateBreakfast(breakfast.Value);
    var postCreationCount = breakfastService.GetDictionaryCount();
    var deleteResult = breakfastService.DeleteBreakFast(breakfast.Value.Id);
    //var postDeletionCount = breakfastService.GetDictionaryCount();
    // Assert
    Assert.IsType<Deleted>(deleteResult.Value);

    Assert.Equal(0, initialCount);
    Assert.Equal(1, postCreationCount);
    Assert.Equal(0, breakfastService.GetDictionaryCount());
    }

    [Fact]
    public void BreakfastDelete_GivenIncorrectParamaters_ReturnsValue()
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
    var createResult = breakfastService.CreateBreakfast(breakfast.Value);
    var postCreationCount = breakfastService.GetDictionaryCount();
    var deleteResult = breakfastService.DeleteBreakFast(Guid.NewGuid());

    // Assert
    Assert.IsType<Deleted>(deleteResult.Value);

    Assert.Equal(0, initialCount);
    Assert.Equal(1, postCreationCount);
    Assert.Equal(1, breakfastService.GetDictionaryCount());
    }

}