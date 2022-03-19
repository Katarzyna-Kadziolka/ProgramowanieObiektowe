using System.Collections;
using System.Text.RegularExpressions;

namespace LabThree;

public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable<decimal> {
    // przechowuje w metrach
    private decimal _a;
    private decimal _b;
    private decimal _c;
    private decimal[] _parameters;

    public decimal A {
        get => Math.Round(_a, 3);
        init => _a = value;
    }

    public decimal B {
        get => Math.Round(_b, 3);
        init => _b = value;
    }

    public decimal C {
        get => Math.Round(_c, 3);
        init => _c = value;
    }

    public UnitOfMeasure UnitOfMeasure { get; init; }

    public decimal Objetosc => Math.Round(_a * _b * _c, 9);
    public decimal Pole => Math.Round(_a * _b * 2 + _a * _c * 2 + _b * _c * 2, 6);

    public Pudelko(decimal a = 0.1m, decimal b = 0.1m, decimal c = 0.1m,
        UnitOfMeasure unitOfMeasure = UnitOfMeasure.Meter) {
        if (a < 0 || b < 0 || c < 0 || a > 10 || b > 10 || c > 10) {
            throw new ArgumentOutOfRangeException();
        }

        UnitOfMeasure = unitOfMeasure;
        A = MeasureConverter.ConvertToMeters(a, UnitOfMeasure);
        B = MeasureConverter.ConvertToMeters(b, UnitOfMeasure);
        C = MeasureConverter.ConvertToMeters(c, UnitOfMeasure);
        _parameters = new[] { A, B, C };
    }

    public override bool Equals(object? obj) {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (!(obj is Pudelko)) return false;
        var box = (Pudelko)obj;
        var parametersCompared = new[] { box.A, box.B, box.C };
        return _parameters.All(parametersCompared.Contains) && _parameters.Length == parametersCompared.Length;
    }

    public bool Equals(Pudelko? box) {
        return Equals((object?)box);
    }

    public override int GetHashCode() {
        return HashCode.Combine(A, B, C);
    }

    public static bool operator ==(Pudelko? leftBox, Pudelko? rightBox) {
        if (leftBox is null && rightBox is null) {
            return true;
        }

        if (leftBox is null && rightBox is not null) {
            return false;
        }

        if (leftBox is not null && rightBox is null) {
            return false;
        }

        return leftBox.Equals(rightBox);
    }

    public static bool operator !=(Pudelko? leftBox, Pudelko? rightBox) => !(leftBox == rightBox);

    public static Pudelko operator +(Pudelko leftBox, Pudelko rightBox) {
        var leftBoxParameters = new[] { leftBox.A, leftBox.B, leftBox.C }.OrderByDescending(a => a).ToArray();
        var rightBoxParameters = new[] { rightBox.A, rightBox.B, rightBox.C }.OrderByDescending(a => a).ToArray();
        var a = new[] { leftBoxParameters[0], rightBoxParameters[0] }.Max();
        var b = new[] { leftBoxParameters[1], rightBoxParameters[1] }.Max();
        var c = leftBoxParameters[2] + rightBoxParameters[2];
        return new Pudelko(a, b, c);
        // sprobuj przy testach z _parameters
    }

    public static explicit operator double[](Pudelko box) => new[]
        { Convert.ToDouble(box.A), Convert.ToDouble(box.B), Convert.ToDouble(box.C) };

    public static implicit operator Pudelko(ValueTuple<int, int, int> values) =>
        new(values.Item1, values.Item2, values.Item3, UnitOfMeasure.Milimeter);

    public decimal this[int index] => _parameters[index];

    public override string ToString() {
        return $"{A} m × {B} m × {C} m";
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public IEnumerator<decimal> GetEnumerator() {
        foreach (var parameter in _parameters) {
            yield return parameter;
        }
    }

    public static Pudelko Parse(string text) {
        var regex = new Regex(@"([0-9]+\.?[0-9]*) ([m,mm,cm]+)");
        var matches = regex.Matches(text);
        if (matches.Count != 3) {
            throw new FormatException();
        }

        var values = matches.Select(a => decimal.Parse(a.Groups[1].Value)).ToArray();
        if (values.Count() != 3) {
            throw new FormatException();
        }
        var unitOfMeasureString = matches[0].Groups[2].Value;
        var unitOfMeasure = UnitOfMeasure.Unknown;
        switch (unitOfMeasureString) {
            case "m":
                unitOfMeasure = UnitOfMeasure.Meter;
                break;
            case "cm":
                unitOfMeasure = UnitOfMeasure.Centimeter;
                break;
            case "mm":
                unitOfMeasure = UnitOfMeasure.Milimeter;
                break;
        }

        return new Pudelko(values[0], values[1], values[2], unitOfMeasure);
    }

    public string ToString(string format) {
        return format switch {
            "m" => ToString(),
            "cm" => $"{Math.Round(MeasureConverter.ConvertToCentimeters(A), 1)} cm × " +
                    $"{Math.Round(MeasureConverter.ConvertToCentimeters(B), 1)} cm × " +
                    $"{Math.Round(MeasureConverter.ConvertToCentimeters(C), 1)} cm",
            "mm" => $"{Math.Round(MeasureConverter.ConvertToMilimeters(A), 0)} mm × " +
                    $"{Math.Round(MeasureConverter.ConvertToMilimeters(B), 0)} mm × " +
                    $"{Math.Round(MeasureConverter.ConvertToMilimeters(C), 0)} mm",
            _ => throw new FormatException()
        };
    }
}