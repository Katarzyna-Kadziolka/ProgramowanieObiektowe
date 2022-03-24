using System;
using FluentAssertions;
using LabThree;
using NUnit.Framework;

namespace LabThreeUnitTests; 

public class MeasureConverterTests {
    [Test]
    public void ConvertToMeters_UnitOfMeasureDefaultValue_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 100;
        // Act
        var result = MeasureConverter.ConvertToMeters(value);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMeters_Meter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 100;
        // Act
        var result = MeasureConverter.ConvertToMeters(value, UnitOfMeasure.Meter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMeters_Centimeter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 1;
        // Act
        var result = MeasureConverter.ConvertToMeters(value, UnitOfMeasure.Centimeter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMeters_Milimeter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 0.1m;
        // Act
        var result = MeasureConverter.ConvertToMeters(value, UnitOfMeasure.Milimeter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMeters_Unknown_ShouldThrowArgumentException() {
        // Arrange
        var value = 100;
        // Act
        Action act = () =>  MeasureConverter.ConvertToMeters(value, UnitOfMeasure.Unknown);
        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Unit of measure cannot be unknown");
    }
    [Test]
    public void ConvertToCentimeters_UnitOfMeasureDefaultValue_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 10000;
        // Act
        var result = MeasureConverter.ConvertToCentimeters(value);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToCentimeters_Meter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 10000;
        // Act
        var result = MeasureConverter.ConvertToCentimeters(value, UnitOfMeasure.Meter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToCentimeters_Centimeter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 100;
        // Act
        var result = MeasureConverter.ConvertToCentimeters(value, UnitOfMeasure.Centimeter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToCentimeters_Milimeter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 10;
        // Act
        var result = MeasureConverter.ConvertToCentimeters(value, UnitOfMeasure.Milimeter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToCentimeters_Unknown_ShouldThrowArgumentException() {
        // Arrange
        var value = 100;
        // Act
        Action act = () =>  MeasureConverter.ConvertToCentimeters(value, UnitOfMeasure.Unknown);
        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Unit of measure cannot be unknown");
    }
    ////
    [Test]
    public void ConvertToMilimeters_UnitOfMeasureDefaultValue_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 100000;
        // Act
        var result = MeasureConverter.ConvertToMilimeters(value);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMilimeters_Meter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 100000;
        // Act
        var result = MeasureConverter.ConvertToMilimeters(value, UnitOfMeasure.Meter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMilimeters_Centimeter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 1000;
        // Act
        var result = MeasureConverter.ConvertToMilimeters(value, UnitOfMeasure.Centimeter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMilimeters_Milimeter_ShouldReturnDecimal() {
        // Arrange
        var value = 100;
        var expectedResult = 100;
        // Act
        var result = MeasureConverter.ConvertToMilimeters(value, UnitOfMeasure.Milimeter);
        // Assert
        result.Should().Be(expectedResult);
    }
    [Test]
    public void ConvertToMilimeters_Unknown_ShouldThrowArgumentException() {
        // Arrange
        var value = 100;
        // Act
        Action act = () =>  MeasureConverter.ConvertToMilimeters(value, UnitOfMeasure.Unknown);
        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Unit of measure cannot be unknown");
    }
}