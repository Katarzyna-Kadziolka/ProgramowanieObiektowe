namespace LabThree; 

public static class MeasureConverter {
	public static double ConvertToMeters(double value, UnitOfMeasure unitOfMeasure) {
		switch (unitOfMeasure) {
			case UnitOfMeasure.Unknown:
				throw new ArgumentException("Unit of measure cannot be unknown");
			case UnitOfMeasure.Milimeter:
				return value / 1000; 
			case UnitOfMeasure.Centimeter:
				return value / 100;
			case UnitOfMeasure.Meter:
				return value;
			default:
				throw new ArgumentOutOfRangeException(nameof(unitOfMeasure), unitOfMeasure, null);
		}
	}
	public static double ConvertToCentimeters(double value, UnitOfMeasure unitOfMeasure) {
		switch (unitOfMeasure) {
			case UnitOfMeasure.Unknown:
				throw new ArgumentException("Unit of measure cannot be unknown");
			case UnitOfMeasure.Milimeter:
				return value / 10; 
			case UnitOfMeasure.Centimeter:
				return value;
			case UnitOfMeasure.Meter:
				return value * 100;
			default:
				throw new ArgumentOutOfRangeException(nameof(unitOfMeasure), unitOfMeasure, null);
		}
	}
	public static double ConvertToMilimeters(double value, UnitOfMeasure unitOfMeasure) {
		switch (unitOfMeasure) {
			case UnitOfMeasure.Unknown:
				throw new ArgumentException("Unit of measure cannot be unknown");
			case UnitOfMeasure.Milimeter:
				return value; 
			case UnitOfMeasure.Centimeter:
				return value * 10;
			case UnitOfMeasure.Meter:
				return value * 1000;
			default:
				throw new ArgumentOutOfRangeException(nameof(unitOfMeasure), unitOfMeasure, null);
		}
	}
}