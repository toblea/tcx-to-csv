using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TcxToCsv
{
	class Program
	{
		static int Main(string[] args)
		{
			const string inputFolderKey = "-input-folder";
			const string outputFolderKey = "-output-folder";

			var outputFolder = Path.Join(Environment.CurrentDirectory, "out");
			string inputFolder;

			var options = (args ?? Array.Empty<string>())
				.Select(a => a.Split('='))
				.ToDictionary(a => a[0], b => b[1]);

			if (options.ContainsKey(inputFolderKey))
			{
				inputFolder = options[inputFolderKey];
			}
			else
			{
				var c = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("ERROR: You need to specify an path where to find the TCX files.");
				Console.ForegroundColor = c;
				Console.WriteLine("For example:");
				Console.WriteLine($"\t{Environment.CommandLine} {inputFolderKey} \"C:\\Temp\\MyGarminFiles\"");
				return 1;
			}
			if (options.ContainsKey(outputFolderKey))
			{
				outputFolder = options[outputFolderKey];
			}
			else
			{
				var c = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"WARNING: Writing output to: {outputFolder}");
				Console.ForegroundColor = c;
			}
			
			var activitiesFile = Path.Join(outputFolder, "activities.csv");
			var lapsFile = Path.Join(outputFolder, "laps.csv");
			var tracksFile = Path.Join(outputFolder, "tracks.csv");

			var sw = new Stopwatch();
			if (!Directory.Exists(outputFolder))
			{
				Directory.CreateDirectory(outputFolder);
			}
			File.WriteAllText(activitiesFile, "ActivityId;ActivitySport;ActivityCreatorName;ActivityCreatorProductID");
			File.WriteAllText(lapsFile, "ActivityId;LapNumber;AverageHeartRateBpm;Calories;DistanceMeters;AvgRunCadence;AvgSpeed;MaxRunCadence;Intensity;MaximumHeartRateBpm;MaximumSpeed;StartTime;TotalTimeSeconds;TriggerMethod");
			File.WriteAllText(tracksFile, "ActivityId;LapNumber;TrackNumber;AltitudeMeters;DistanceMeters;RunCadence;Speed;HeartRateBpm;LatitudeDegrees;LongitudeDegrees;Time");

			sw.Start();
			Console.WriteLine("Starting conversion . . .");
			foreach (var file in Directory.GetFiles(inputFolder, "*.tcx"))
			{
				var xml = File.ReadAllText(file);
				var trainingDb = xml.ParseXml<TrainingCenterDatabase>();
				var a = trainingDb.Activities.Activity;

				File.AppendAllText(activitiesFile, $"\n{a.Id};{a.Sport};{a.Creator?.Name};{a.Creator?.ProductID}");

				for (var i = 0; i < a.Lap.Length; i++)
				{
					var lap = a.Lap[i];
					var tmp1 = new StringBuilder();
					tmp1.Append($"\n{a.Id};");
					tmp1.Append($";{i + 1}");
					tmp1.Append($";{lap.AverageHeartRateBpm?.Value}");
					tmp1.Append($";{lap.Calories}");
					tmp1.Append($";{lap.DistanceMeters}");
					tmp1.Append($";{lap.Extensions?.LX?.AvgRunCadence}");
					tmp1.Append($";{lap.Extensions?.LX?.AvgSpeed}");
					tmp1.Append($";{lap.Extensions?.LX?.MaxRunCadence}");
					tmp1.Append($";{lap.Intensity}");
					tmp1.Append($";{lap.MaximumHeartRateBpm?.Value}");
					tmp1.Append($";{lap.MaximumSpeed}");
					tmp1.Append($";{lap.StartTime:u}");
					tmp1.Append($";{lap.TotalTimeSeconds}");
					tmp1.Append($";{lap.TriggerMethod}");

					File.AppendAllText(lapsFile, tmp1.ToString());

					if (lap.Track == null || lap.Track.Length < 1) continue;

					for (var j = 0; j < lap.Track.Length; j++)
					{
						var track = lap.Track[j];
						var tmp2 = new StringBuilder();
						tmp2.Append($"\n{a.Id}");
						tmp2.Append($";{i + 1}");
						tmp2.Append($";{j + 1}");
						tmp2.Append($";{track.AltitudeMeters}");
						tmp2.Append($";{track.DistanceMeters}");
						tmp2.Append($";{track.Extensions?.TPX?.RunCadence}");
						tmp2.Append($";{track.Extensions?.TPX?.Speed}");
						tmp2.Append($";{track.HeartRateBpm?.Value}");
						tmp2.Append($";{track.Position?.LatitudeDegrees}");
						tmp2.Append($";{track.Position?.LongitudeDegrees}");
						tmp2.Append($";{track.Time:u}");

						File.AppendAllText(tracksFile, tmp2.ToString());
					}
				}
			}
			sw.Stop();
			Console.WriteLine($"Finished in {sw.Elapsed:g}");

			return 0;
		}

	}
	internal static class ParseHelpers
	{
		public static Stream ToStream(this string @this)
		{
			@this = @this.Replace("xsi:type=\"Device_t\"", "");
			@this = @this.Replace("xsi:type=\"Application_t\"", "");

			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(@this);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
		
		public static T ParseXml<T>(this string @this) where T : class
		{
			var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
			return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
		}
	}
}
