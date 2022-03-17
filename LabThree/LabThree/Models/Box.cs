namespace LabThree;

public sealed class Box {
	// przechowuje w metrach
	private double _a;
	private double _b;
	private double _c;

	public double A {
		get { return Math.Round(_a, 3); }
		set { _a = value; }
	}

	public double B {
		get { return Math.Round(_b, 3); }
		set { _b = value; }
	}

	public double C {
		get { return Math.Round(_c, 3); }
		set { _c = value; }
	}

	public UnitOfMeasure UnitOfMeasure { get; set; }

	public Box(double a = 0.1d, double b = 0.1, double c = 0.1, UnitOfMeasure unitOfMeasure = UnitOfMeasure.Meter) {
		if (a < 0 || b < 0 || c < 0 || a > 10 || b > 10 || c > 10) {
			throw new ArgumentOutOfRangeException();
		}

		UnitOfMeasure = unitOfMeasure;
		A = MeasureConverter.ConvertToMeters(a, UnitOfMeasure);
		B = MeasureConverter.ConvertToMeters(b, UnitOfMeasure);
		C = MeasureConverter.ConvertToMeters(c, UnitOfMeasure);
	}

	public override string ToString() {
		return $"{A} {UnitOfMeasure} × {B} {UnitOfMeasure} × {C} {UnitOfMeasure}";
	}

	public string ToString(string format) {
		switch (format) {
			case "m": {
				
			}
		}
	}
	
}