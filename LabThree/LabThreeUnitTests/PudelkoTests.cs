using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using FluentAssertions;
using LabThree;
using NUnit.Framework;

namespace LabThreeUnitTests;

public class PudelkoTests {
    [SetUp]
    public void Setup() {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
    }

    private void AssertPudelko(Pudelko p, decimal expectedA, decimal expectedB, decimal expectedC) {
        Assert.AreEqual(expectedA, p.A);
        Assert.AreEqual(expectedB, p.B);
        Assert.AreEqual(expectedC, p.C);
    }

    private static decimal defaultSize = 0.1m; // w metrach

    #region Constructors

    [Test]
    public void Constructor_Default() {
        Pudelko p = new Pudelko();

        Assert.AreEqual(defaultSize, p.A);
        Assert.AreEqual(defaultSize, p.B);
        Assert.AreEqual(defaultSize, p.C);
    }

    [Test]
    [TestCase(1.0, 2.543, 3.1,
        1.0, 2.543, 3.1)]
    [TestCase(1.0001, 2.54387, 3.1005,
        1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
    public void Constructor_3params_DefaultMeters(decimal a, decimal b, decimal c,
        decimal expectedA, decimal expectedB, decimal expectedC) {
        Pudelko p = new Pudelko(a, b, c);
        var bbb = p.B;

        AssertPudelko(p, expectedA, expectedB, expectedC);
    }

    [Test]
    [TestCase(1.0, 2.543, 3.1,
        1.0, 2.543, 3.1)]
    [TestCase(1.0001, 2.54387, 3.1005,
        1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
    public void Constructor_3params_InMeters(decimal a, decimal b, decimal c,
        decimal expectedA, decimal expectedB, decimal expectedC) {
        Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.Meter);

        AssertPudelko(p, expectedA, expectedB, expectedC);
    }

    [Test]
    [TestCase(100.0, 25.5, 3.1,
        1.0, 0.255, 0.031)]
    [TestCase(100.0, 25.58, 3.13,
        1.0, 0.255, 0.031)] // dla centymertów liczy się tylko 1 miejsce po przecinku
    public void Constructor_3params_InCentimeters(decimal a, decimal b, decimal c,
        decimal expectedA, decimal expectedB, decimal expectedC) {
        Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.Centimeter);

        AssertPudelko(p, expectedA, expectedB, expectedC);
    }

    [Test]
    [TestCase(100, 255, 3,
        0.1, 0.255, 0.003)]
    [TestCase(100.0, 25.58, 3.13,
        0.1, 0.025, 0.003)] // dla milimetrów nie liczą się miejsca po przecinku
    public void Constructor_3params_InMilimeters(decimal a, decimal b, decimal c,
        decimal expectedA, decimal expectedB, decimal expectedC) {
        Pudelko p = new Pudelko(unit: UnitOfMeasure.Milimeter, a: a, b: b, c: c);

        AssertPudelko(p, expectedA, expectedB, expectedC);
    }


    // ----

    [Test]
    [TestCase(1.0, 2.5, 1.0, 2.5)]
    [TestCase(1.001, 2.599, 1.001, 2.599)]
    [TestCase(1.0019, 2.5999, 1.001, 2.599)]
    public void Constructor_2params_DefaultMeters(decimal a, decimal b, decimal expectedA, decimal expectedB) {
        Pudelko p = new Pudelko(a, b);

        AssertPudelko(p, expectedA, expectedB, expectedC: 0.1m);
    }

    [Test]
    [TestCase(1.0, 2.5, 1.0, 2.5)]
    [TestCase(1.001, 2.599, 1.001, 2.599)]
    [TestCase(1.0019, 2.5999, 1.001, 2.599)]
    public void Constructor_2params_InMeters(decimal a, decimal b, decimal expectedA, decimal expectedB) {
        Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.Meter);

        AssertPudelko(p, expectedA, expectedB, expectedC: 0.1m);
    }

    [Test]
    [TestCase(11.0, 2.5, 0.11, 0.025)]
    [TestCase(100.1, 2.599, 1.001, 0.025)]
    [TestCase(2.0019, 0.25999, 0.02, 0.002)]
    public void Constructor_2params_InCentimeters(decimal a, decimal b, decimal expectedA, decimal expectedB) {
        Pudelko p = new Pudelko(unit: UnitOfMeasure.Centimeter, a: a, b: b);

        AssertPudelko(p, expectedA, expectedB, expectedC: 0.1m);
    }

    [Test]
    [TestCase(11, 2.0, 0.011, 0.002)]
    [TestCase(100.1, 2599, 0.1, 2.599)]
    [TestCase(200.19, 2.5999, 0.2, 0.002)]
    public void Constructor_2params_InMilimeters(decimal a, decimal b, decimal expectedA, decimal expectedB) {
        Pudelko p = new Pudelko(unit: UnitOfMeasure.Milimeter, a: a, b: b);

        AssertPudelko(p, expectedA, expectedB, expectedC: 0.1m);
    }

    [Test]
    [TestCase(2.5)]
    public void Constructor_1param_DefaultMeters(decimal a) {
        Pudelko p = new Pudelko(a);

        Assert.AreEqual(a, p.A);
        Assert.AreEqual(0.1, p.B);
        Assert.AreEqual(0.1, p.C);
    }

    [Test]
    [TestCase(2.5)]
    public void Constructor_1param_InMeters(decimal a) {
        Pudelko p = new Pudelko(a);

        Assert.AreEqual(a, p.A);
        Assert.AreEqual(0.1, p.B);
        Assert.AreEqual(0.1, p.C);
    }

    [Test]
    [TestCase(11.0, 0.11)]
    [TestCase(100.1, 1.001)]
    [TestCase(2.0019, 0.02)]
    public void Constructor_1param_InCentimeters(decimal a, decimal expectedA) {
        Pudelko p = new Pudelko(unit: UnitOfMeasure.Centimeter, a: a);

        AssertPudelko(p, expectedA, expectedB: 0.1m, expectedC: 0.1m);
    }

    [Test]
    [TestCase(11, 0.011)]
    [TestCase(100.1, 0.1)]
    [TestCase(200.19, 0.2)]
    public void Constructor_1param_InMilimeters(decimal a, decimal expectedA) {
        Pudelko p = new Pudelko(unit: UnitOfMeasure.Milimeter, a: a);

        AssertPudelko(p, expectedA, expectedB: 0.1m, expectedC: 0.1m);
    }

    public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]> {
        new object[] { -1.0m, 2.5m, 3.1m },
        new object[] { 1.0m, -2.5m, 3.1m },
        new object[] { 1.0m, 2.5m, -3.1m },
        new object[] { -1.0m, -2.5m, 3.1m },
        new object[] { -1.0m, 2.5m, -3.1m },
        new object[] { 1.0m, -2.5m, -3.1m },
        new object[] { -1.0m, -2.5m, -3.1m },
        new object[] { 0m, 2.5m, 3.1m },
        new object[] { 1.0m, 0m, 3.1m },
        new object[] { 1.0m, 2.5m, 0m },
        new object[] { 1.0m, 0m, 0m },
        new object[] { 0m, 2.5m, 0m },
        new object[] { 0m, 0m, 3.1m },
        new object[] { 0m, 0m, 0m },
        new object[] { 10.1m, 2.5m, 3.1m },
        new object[] { 10m, 10.1m, 3.1m },
        new object[] { 10m, 10m, 10.1m },
        new object[] { 10.1m, 10.1m, 3.1m },
        new object[] { 10.1m, 10m, 10.1m },
        new object[] { 10m, 10.1m, 10.1m },
        new object[] { 10.1m, 10.1m, 10.1m }
    };

    [Test]
    [TestCaseSource(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
    public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(decimal a, decimal b, decimal c) {
        Action act = () => new Pudelko(a, b, c);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCaseSource(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
    public void Constructor_3params_InMeters_ArgumentOutOfRangeException(decimal a, decimal b, decimal c) {
        Action act = () => new Pudelko(a, b, c, unit: UnitOfMeasure.Meter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1, 1, 1)]
    [TestCase(1, -1, 1)]
    [TestCase(1, 1, -1)]
    [TestCase(-1, -1, 1)]
    [TestCase(-1, 1, -1)]
    [TestCase(1, -1, -1)]
    [TestCase(-1, -1, -1)]
    [TestCase(0, 1, 1)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 0)]
    [TestCase(0, 0, 1)]
    [TestCase(0, 1, 0)]
    [TestCase(1, 0, 0)]
    [TestCase(0, 0, 0)]
    [TestCase(0.01, 0.1, 1)]
    [TestCase(0.1, 0.01, 1)]
    [TestCase(0.1, 0.1, 0.01)]
    [TestCase(1001, 1, 1)]
    [TestCase(1, 1001, 1)]
    [TestCase(1, 1, 1001)]
    [TestCase(1001, 1, 1001)]
    [TestCase(1, 1001, 1001)]
    [TestCase(1001, 1001, 1)]
    [TestCase(1001, 1001, 1001)]
    public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(decimal a, decimal b, decimal c) {
        Action act = () => new Pudelko(a, b, c, unit: UnitOfMeasure.Centimeter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }


    [Test]
    [TestCase(-1, 1, 1)]
    [TestCase(1, -1, 1)]
    [TestCase(1, 1, -1)]
    [TestCase(-1, -1, 1)]
    [TestCase(-1, 1, -1)]
    [TestCase(1, -1, -1)]
    [TestCase(-1, -1, -1)]
    [TestCase(0, 1, 1)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 0)]
    [TestCase(0, 0, 1)]
    [TestCase(0, 1, 0)]
    [TestCase(1, 0, 0)]
    [TestCase(0, 0, 0)]
    [TestCase(0.1, 1, 1)]
    [TestCase(1, 0.1, 1)]
    [TestCase(1, 1, 0.1)]
    [TestCase(10001, 1, 1)]
    [TestCase(1, 10001, 1)]
    [TestCase(1, 1, 10001)]
    [TestCase(10001, 10001, 1)]
    [TestCase(10001, 1, 10001)]
    [TestCase(1, 10001, 10001)]
    [TestCase(10001, 10001, 10001)]
    public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(decimal a, decimal b, decimal c) {
        Action act = () => new Pudelko(a, b, c, unit: UnitOfMeasure.Milimeter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]> {
        new object[] { -1.0m, 2.5m },
        new object[] { 1.0m, -2.5m },
        new object[] { -1.0m, -2.5m },
        new object[] { 0m, 2.5m },
        new object[] { 1.0m, 0m },
        new object[] { 0m, 0m },
        new object[] { 10.1m, 10m },
        new object[] { 10m, 10.1m },
        new object[] { 10.1m, 10.1m }
    };

    [Test]
    [TestCaseSource(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
    public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(decimal a, decimal b) {
        Action act = () => new Pudelko(a, b);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCaseSource(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
    public void Constructor_2params_InMeters_ArgumentOutOfRangeException(decimal a, decimal b) {
        Action act = () => new Pudelko(a, b, unit: UnitOfMeasure.Meter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(-1, -1)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(0, 0)]
    [TestCase(0.01, 1)]
    [TestCase(1, 0.01)]
    [TestCase(0.01, 0.01)]
    [TestCase(1001, 1)]
    [TestCase(1, 1001)]
    [TestCase(1001, 1001)]
    public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(decimal a, decimal b) {
        Action act = () => new Pudelko(a, b, unit: UnitOfMeasure.Centimeter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(-1, -1)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(0, 0)]
    [TestCase(0.1, 1)]
    [TestCase(1, 0.1)]
    [TestCase(0.1, 0.1)]
    [TestCase(10001, 1)]
    [TestCase(1, 10001)]
    [TestCase(10001, 10001)]
    public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(decimal a, decimal b) {
        Action act = () => new Pudelko(a, b, unit: UnitOfMeasure.Milimeter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }


    [Test]
    [TestCase(-1.0)]
    [TestCase(0)]
    [TestCase(10.1)]
    public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(decimal a) {
        Action act = () => new Pudelko(a);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1.0)]
    [TestCase(0)]
    [TestCase(10.1)]
    public void Constructor_1param_InMeters_ArgumentOutOfRangeException(decimal a) {
        Action act = () => new Pudelko(a, unit: UnitOfMeasure.Meter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1.0)]
    [TestCase(0)]
    [TestCase(0.01)]
    [TestCase(1001)]
    public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(decimal a) {
        Action act = () => new Pudelko(a, unit: UnitOfMeasure.Centimeter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(0.1)]
    [TestCase(10001)]
    public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(decimal a) {
        Action act = () => new Pudelko(a, unit: UnitOfMeasure.Milimeter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    #endregion

    #region ToString tests

    [Test]
    public void ToString_Default_Culture_EN() {
        var p = new Pudelko(2.5m, 9.321m);
        string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

        Assert.AreEqual(expectedStringEN, p.ToString());
    }

    [Test]
    [TestCase("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
    [TestCase("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
    [TestCase("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
    public void ToString_Formattable_Culture_EN(string format, decimal a, decimal b, decimal c,
        string expectedStringRepresentation) {
        var p = new Pudelko(a, b, c, unit: UnitOfMeasure.Meter);
        Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
    }

    [Test]
    public void ToString_DefaultUnitMeasure_FormatException() {
        var p = new Pudelko(2.5m, 9.321m, 0.1m, UnitOfMeasure.Meter);
        Action act = () => p.ToString(null);
        act.Should().Throw<FormatException>();
    }

    [Test]
    public void ToString_Formattable_WrongFormat_FormatException() {
        var p = new Pudelko(1);
        Action act = () => p.ToString("wrong code");
        act.Should().Throw<FormatException>();
    }

    #endregion

    #region Conversions

    [Test]
    public void ExplicitConversion_ToDoubleArray_AsMeters() {
        var p = new Pudelko(1, 2.1m, 3.231m);
        double[] tab = (double[])p;
        Assert.AreEqual(3, tab.Length);
        Assert.AreEqual(p.A, tab[0]);
        Assert.AreEqual(p.B, tab[1]);
        Assert.AreEqual(p.C, tab[2]);
    }

    [Test]
    public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters() {
        var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
        Pudelko p = (a, b, c);
        Assert.AreEqual((int)(p.A * 1000), a);
        Assert.AreEqual((int)(p.B * 1000), b);
        Assert.AreEqual((int)(p.C * 1000), c);
    }

    #endregion

    #region Indexer, enumeration

    [Test]
    public void Indexer_ReadFrom() {
        var p = new Pudelko(1, 2.1m, 3.231m);
        Assert.AreEqual(p.A, p[0]);
        Assert.AreEqual(p.B, p[1]);
        Assert.AreEqual(p.C, p[2]);
    }

    [Test]
    public void ForEach_Test() {
        var p = new Pudelko(1, 2.1m, 3.231m);
        var tab = new[] { p.A, p.B, p.C };
        int i = 0;
        foreach (var x in p) {
            Assert.AreEqual(x, tab[i]);
            i++;
        }
    }

    #endregion

    #region Parsing

    [Test]
    [TestCase("2.500 m × 9.321 m × 0.100 m", 2.500, 9.321, 0.100, UnitOfMeasure.Meter)]
    [TestCase("250.0 cm × 932.1 cm × 10.0 cm", 2.500, 9.321, 0.100, UnitOfMeasure.Centimeter)]
    [TestCase("2500 mm × 9321 mm × 100 mm", 2.500, 9.321, 0.100, UnitOfMeasure.Milimeter)]
    public void Parse_CorrectString_ShouldReturnPudelko(string text, decimal expectedA, decimal expectedB, decimal expectedC, UnitOfMeasure expectedUnit) {
        // Arrange
        // Act
        var box = Pudelko.Parse(text);
        // Assert
        box.A.Should().Be(expectedA);
        box.B.Should().Be(expectedB);
        box.C.Should().Be(expectedC);
        box.Unit.Should().Be(expectedUnit);
    }
    [Test]
    [TestCase("2.500 m × 9.321 m")]
    [TestCase("250.0 cm × 932.1 cm")]
    [TestCase("2500 mm × 9321 mm")]
    [TestCase("2.500 m")]
    [TestCase("250.0 cm")]
    [TestCase("2500 mm")]
    [TestCase("")]
    [TestCase("abababa")]
    public void Parse_OneParameterLess_ShouldThrowFormatException(string text) {
        // Arrange
        // Act
        var act = () => Pudelko.Parse(text);
        // Assert
        act.Should().Throw<FormatException>().WithMessage("Do not find any matches");
    }

    #endregion

    #region Pole, Objętość

    

    #endregion
}