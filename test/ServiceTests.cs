using System.Threading;
using Xunit;
using SuperBreakfast.Services.Breakfasts;
using Moq;
using SuperBreakfast.Models;
using System;
using System.Collections.Generic;
using SuperBreakfast.ServiceErrors;
using ErrorOr;

namespace SuperBreakfast.tests;


public class ServiceTests
{
    private Breakfast CreateBreakfast()
    {
        var breakfast = Breakfast.Create(
            "Its now dinner time",
            "This breakfast contains nutrients, lots and lost of nutrients",
            new DateTime(2023, 02, 20, 8, 0, 0),
            new DateTime(2023, 02, 20, 10, 0, 0),
            new List<string> { "Oatmeal", "Avocado Toast", "Omelette", "Salad" },
            new List<string> { "Pancakes", "Waffles" },
            Guid.NewGuid()
        );
        var result = breakfast.Value;
        return result;
    }

    private void ClearDictionary(Breakfast breakfast, BreakfastService breakfastService)
    {
        breakfastService.DeleteBreakFast(breakfast.Id);
    }

    [Fact]
    public void BreakfastCreate_GivenCorrectParamaters_ReturnsBreakfast()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();
    // Act
        var result = breakfastService.CreateBreakfast(breakfast);
        var dictionaryCount = breakfastService.GetDictionaryCount();
        ClearDictionary(breakfast, breakfastService);

        // Assert
        Assert.IsType<Created>(result.Value);
        Assert.IsType<ErrorOr<Created>>(result);
        Assert.Equal(1, dictionaryCount);
    }


    [Fact]
    public void BreakfastDelete_GivenCorrectParamaters_ReturnsNotFound()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();

    // Act
        var createBreakfast = breakfastService.CreateBreakfast(breakfast);
        var deleteResult = breakfastService.DeleteBreakFast(breakfast.Id);
        var dictionaryCount = breakfastService.GetDictionaryCount();
        ClearDictionary(breakfast, breakfastService);

        // Assert
        Assert.IsType<Deleted>(deleteResult.Value);
        Assert.Equal(0, dictionaryCount);
    }

    [Fact]
    public void BreakfastDelete_GivenIncorrectParamaters_ReturnsValue()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();
       var newGuid = Guid.NewGuid();

    // Act
        var createBreakfast = breakfastService.CreateBreakfast(breakfast);
        var deleteResult = breakfastService.DeleteBreakFast(newGuid);
        var dictionaryCount = breakfastService.GetDictionaryCount();
        ClearDictionary(breakfast, breakfastService);

        // Assert
        Assert.IsType<Deleted>(deleteResult.Value);
        Assert.Equal(1, dictionaryCount);
    }

    [Fact]
    public void BreakfastGet_GivenCorrectParamaters_ReturnsBreakfast()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();

    // Act
        var createBreakfast = breakfastService.CreateBreakfast(breakfast);
        var getBreakfastResult = breakfastService.GetBreakfast(breakfast.Id);
        ClearDictionary(breakfast, breakfastService);

        // Assert
        Assert.IsType<Breakfast>(getBreakfastResult.Value);
        Assert.Equal(getBreakfastResult.Value.Id, breakfast.Id);
    }
    [Fact]
    public void BreakfastGet_GivenIncorrectParamaters_ReturnsNotFound()
    {
       var breakfastService = new BreakfastService();
       var newGuid = Guid.NewGuid();
    // Act
        var getBreakfastResult = breakfastService.GetBreakfast(newGuid);

        // Assert
        Assert.True(getBreakfastResult.IsError);
    }


}