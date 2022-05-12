using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Unicode;

class Program
{
	public static void Main() {
		var testCase = Console.ReadLine();
		var input = Console.ReadLine();
		if (testCase != null && input != null) {
			var times = input.Split(",");
			var timesInTimesSpan = times.Select(a => StringToTImeSpan(a)).ToArray();
			var timesOfSingleLoop = ToTimesOfSingleLoop(timesInTimesSpan);
			switch( testCase )
			{
				case "test1" :
					Console.WriteLine( times.Length );
					break;
				case "test2" :
					Console.WriteLine(string.Join(' ', timesOfSingleLoop));
					break;
				case "test3" :
					var theFastestTime = timesOfSingleLoop.Min();
					var indexesOfTheFastestLap = timesOfSingleLoop
					                             .Where(a => a == theFastestTime)
					                             .Select(a => Array.IndexOf(timesOfSingleLoop, a) + 1);
					var indexes = string.Join(' ', indexesOfTheFastestLap);
					Console.WriteLine($"00:{theFastestTime.Seconds} {indexes}");
					break;
				case "test4" :
					var theSlowestTime = timesOfSingleLoop.Max();
					var indexesOfTheSlowestLap = timesOfSingleLoop
					                             .Where(a => a == theSlowestTime)
						.Select(a => Array.IndexOf(timesOfSingleLoop, a) + 1);
					var slowIndexes = string.Join(' ', indexesOfTheSlowestLap);
					Console.WriteLine($"00:{theSlowestTime.Seconds} {slowIndexes}");
					break;
				case "test5" :
					var average = TimeSpan.FromSeconds(timesOfSingleLoop.Select(s => s.TotalSeconds).Average());
					var rounded = new TimeSpan(0, average.Minutes,
						TimeSpan.FromSeconds(Math.Ceiling(average.TotalSeconds)).Seconds);
					Console.WriteLine($"00:{rounded.Seconds}");
					break;
			}
		}
	}

	public static TimeSpan StringToTImeSpan(string text) {
		return new TimeSpan(0, Convert.ToInt32(text.Split(":")[0]), Convert.ToInt32(text.Split(":")[1]));
	}

	public static TimeSpan[] ToTimesOfSingleLoop(TimeSpan[] times) {
		var result = times
		                       .Where(a => Array.IndexOf(times, a) != 0)
		                       .Select(a => a - times[Array.IndexOf(times, a) - 1]).ToList();
		result.Insert(0, times[0]);
		return result.ToArray();
	}
}