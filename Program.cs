using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
			var enUs = CultureInfo.GetCultureInfo("en-US");
			const string inputFolderKey = "-input-folder";
			const string outputFolderKey = "-output-folder";
			const string activityNamesFileKey = "-activity-names-file";

			var outputFolder = Path.Join(Environment.CurrentDirectory, "out");
			string inputFolder;
			Dictionary<string,string> activityNames;
			
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

			if (options.ContainsKey(activityNamesFileKey))
			{
				//activityNames = options[activityNamesFileKey];
				// For instance:
				// 21213123123;Activity One
				// 21213126262;Activity Two
				// 21213534523;Activity Three
				// 21213123745;Activity Four
				var names = File.ReadLines(options[activityNamesFileKey]);
				activityNames = names.Select(x => x.Split(';')).ToDictionary(col => col[0], col => col[1]);
			}
			else
			{
				activityNames = new Dictionary<string, string>();
			}
			
			var activitiesFile = Path.Join(outputFolder, "activities.csv");
			var lapsFile = Path.Join(outputFolder, "laps.csv");
			var tracksFile = Path.Join(outputFolder, "tracks.csv");
			var acts = 0;

			var sw = new Stopwatch();
			if (!Directory.Exists(outputFolder))
			{
				Directory.CreateDirectory(outputFolder);
			}
			File.WriteAllText(activitiesFile, "ActivityId;ActivityNumber;ActivityName;ActivitySport;ActivityCreatorName;ActivityCreatorProductID");
			File.WriteAllText(lapsFile, "ActivityId;LapNumber;AverageHeartRateBpm;Calories;DistanceMeters;AvgRunCadence;AvgSpeed;MaxRunCadence;Intensity;MaximumHeartRateBpm;MaximumSpeed;StartTime;TotalTimeSeconds;TriggerMethod");
			File.WriteAllText(tracksFile, "ActivityId;LapNumber;TrackNumber;AltitudeMeters;DistanceMeters;RunCadence;Speed;HeartRateBpm;LatitudeDegrees;LongitudeDegrees;Time");

			sw.Start();
			Console.WriteLine("Starting conversion . . .");
			foreach (var file in Directory.GetFiles(inputFolder, "*.tcx"))
			{
				var fileName = file.Split(Path.DirectorySeparatorChar).Last();
				var xml = File.ReadAllText(file);
				var trainingDb = xml.ParseXml<TrainingCenterDatabase>();
				var a = trainingDb.Activities.Activity;
				acts++;

				var activityName = activityNames.ContainsKey(fileName) ? activityNames[fileName].Replace(";", ":") : $"Activity {acts}";

				File.AppendAllText(activitiesFile, $"\n{a.Id};{acts};{activityName};{a.Sport};{a.Creator?.Name};{a.Creator?.ProductID}");

				for (var i = 0; i < a.Lap.Length; i++)
				{
					var lap = a.Lap[i];
					var tmp1 = new StringBuilder();
					tmp1.Append($"\n{a.Id}");
					tmp1.Append($";{i + 1}");
					tmp1.Append($";{lap.AverageHeartRateBpm?.Value.ToString(enUs)}");
					tmp1.Append($";{lap.Calories.ToString(enUs)}");
					tmp1.Append($";{lap.DistanceMeters.ToString(enUs)}");
					tmp1.Append($";{lap.Extensions?.LX?.AvgRunCadence.ToString(enUs)}");
					tmp1.Append($";{lap.Extensions?.LX?.AvgSpeed.ToString(enUs)}");
					tmp1.Append($";{lap.Extensions?.LX?.MaxRunCadence.ToString(enUs)}");
					tmp1.Append($";{lap.Intensity}");
					tmp1.Append($";{lap.MaximumHeartRateBpm?.Value.ToString(enUs)}");
					tmp1.Append($";{lap.MaximumSpeed.ToString(enUs)}");
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
						tmp2.Append($";{track.AltitudeMeters.ToString(enUs)}");
						tmp2.Append($";{track.DistanceMeters.ToString(enUs)}");
						tmp2.Append($";{track.Extensions?.TPX?.RunCadence.ToString(enUs)}");
						tmp2.Append($";{track.Extensions?.TPX?.Speed.ToString(enUs)}");
						tmp2.Append($";{track.HeartRateBpm?.Value.ToString(enUs)}");
						tmp2.Append($";{track.Position?.LatitudeDegrees.ToString(enUs)}");
						tmp2.Append($";{track.Position?.LongitudeDegrees.ToString(enUs)}");
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
			var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document });
			return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
		}
	}
}
