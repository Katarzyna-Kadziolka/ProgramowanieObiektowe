using System.Runtime.CompilerServices;

namespace LabThree;

public sealed class Pudelko : IEquatable<Pudelko> {
    // przechowuje w metrach
    private decimal _a;
    private decimal _b;
    private decimal _c;

    public decimal A {
        get { return Math.Round(_a, 3); }
        set { _a = value; }
    }

    public decimal B {
        get { return Math.Round(_b, 3); }
        set { _b = value; }
    }

    public decimal C {
        get { return Math.Round(_c, 3); }
        set { _c = value; }
    }

    public UnitOfMeasure UnitOfMeasure { get; set; }

    public decimal Objetosc {
        get { return Math.Round(_a * _b * _c, 9); }
    }
    public decimal Pole {
        get { return Math.Round(_a * _b * 2 + _a * _c * 2 + _b * _c * 2, 6); }
    }

    public Pudelko(decimal a = 0.1m, decimal b = 0.1m, decimal c = 0.1m, UnitOfMeasure unitOfMeasure = UnitOfMeasure.Meter) {
        if (a < 0 || b < 0 || c < 0 || a > 10 || b > 10 || c > 10) {
            throw new ArgumentOutOfRangeException();
        }
        UnitOfMeasure = unitOfMeasure;
        A = MeasureConverter.ConvertToMeters(a, UnitOfMeasure);
        B = MeasureConverter.ConvertToMeters(b, UnitOfMeasure);
        C = MeasureConverter.ConvertToMeters(c, UnitOfMeasure);
    }

    public override bool Equals(object? obj) {
        if( obj == null ) return false;
        if( ReferenceEquals(this, obj)) return true;
        if ( !(obj is Pudelko) ) return false;
        var box = (Pudelko)obj;
        var parameters = new List<decimal> { A, B, C };
        var parametersCompared = new List<decimal> { box.A, box.B, box.C };
        return parameters.All(parametersCompared.Contains) && parameters.Count == parametersCompared.Count;
    }

    public bool Equals(Pudelko? box) {
        return Equals((object?) box);
    }

    public override int GetHashCode() {
        return HashCode.Combine(A, B, C);
    }
    public static bool operator == (Pudelko? leftBox, Pudelko? rightBox) {
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

    public static bool operator != (Pudelko? leftBox, Pudelko? rightBox) => !(leftBox == rightBox);

    public override string ToString() {
        return $"{A} m × {B} m × {C} m";
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