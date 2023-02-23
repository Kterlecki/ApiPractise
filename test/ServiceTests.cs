using System.Threading;
using Xunit;
using SuperBreakfast.Services.Breakfasts;
using Moq;
using SuperBreakfast.Models;
using System;
using System.Collections.Generic;
using ErrorOr;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace SuperBreakfast.tests;

[Collection("Sequential")]
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

    [Fact]
    public void BreakfastCreate_GivenCorrectParamaters_ReturnsBreakfast()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();
    // Act
        var result = breakfastService.CreateBreakfast(breakfast);

        // Assert
        Assert.IsType<Created>(result.Value);
        Assert.IsType<ErrorOr<Created>>(result);
        Assert.Equal(1, breakfastService.GetDictionaryCount());
    }


    [Fact]
    public void BreakfastDelete_GivenCorrectParamaters_ReturnsNotFound()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();

    // Act
        var result = breakfastService.CreateBreakfast(breakfast);
        var initialCount = breakfastService.GetDictionaryCount();
        var deleteResult = breakfastService.DeleteBreakFast(breakfast.Id);
        var postDeletionCount = breakfastService.GetDictionaryCount();
        breakfastService.ClearDictionary();
        // Assert
        Assert.IsType<Deleted>(deleteResult.Value);
        Assert.Equal(1, initialCount);
        Assert.Equal(0, postDeletionCount);
    }

    [Fact]
    public void BreakfastDelete_GivenIncorrectParamaters_ReturnsValue()
    {
       var breakfastService = new BreakfastService();
       var breakfast = CreateBreakfast();
       var newGuid = Guid.NewGuid();

    // Act
        var result = breakfastService.CreateBreakfast(breakfast);
        var deleteResult = breakfastService.DeleteBreakFast(newGuid);
        var count = breakfastService.GetDictionaryCount();
        breakfastService.ClearDictionary();

        // Assert
        Assert.IsType<Deleted>(deleteResult.Value);
        Assert.Equal(1, count);
    }

}