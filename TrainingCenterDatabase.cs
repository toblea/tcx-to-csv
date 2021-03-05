using System;

namespace TcxToCsv
{


	// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2", IsNullable = false)]
	public class TrainingCenterDatabase
	{

		public TrainingCenterDatabaseActivities Activities { get; set; }

		public TrainingCenterDatabaseAuthor Author { get; set; }
	}

	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivities
	{
		public TrainingCenterDatabaseActivitiesActivity Activity { get; set; }
	}

	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivity
	{
		public DateTime Id { get; set; }


		[System.Xml.Serialization.XmlElementAttribute("Lap")]
		public TrainingCenterDatabaseActivitiesActivityLap[] Lap { get; set; }

		public TrainingCenterDatabaseActivitiesActivityCreator Creator { get; set; }

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Sport { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLap
	{



		public decimal TotalTimeSeconds { get; set; }

		public decimal DistanceMeters { get; set; }
		public decimal MaximumSpeed { get; set; }
		public int Calories { get; set; }


		public TrainingCenterDatabaseActivitiesActivityLapAverageHeartRateBpm AverageHeartRateBpm { get; set; }


		public TrainingCenterDatabaseActivitiesActivityLapMaximumHeartRateBpm MaximumHeartRateBpm { get; set; }


		public string Intensity { get; set; }
		public string TriggerMethod { get; set; }


		[System.Xml.Serialization.XmlArrayItemAttribute("Trackpoint", IsNullable = false)]
		public TrainingCenterDatabaseActivitiesActivityLapTrackpoint[] Track { get; set; }


		public TrainingCenterDatabaseActivitiesActivityLapExtensions Extensions { get; set; }


		[System.Xml.Serialization.XmlAttributeAttribute()]
		public DateTime StartTime { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapAverageHeartRateBpm
	{

		public int Value { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapMaximumHeartRateBpm
	{

		public int Value { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapTrackpoint
	{

		public DateTime Time { get; set; }


		public TrainingCenterDatabaseActivitiesActivityLapTrackpointPosition Position { get; set; }


		public decimal AltitudeMeters { get; set; }


		public decimal DistanceMeters { get; set; }


		public TrainingCenterDatabaseActivitiesActivityLapTrackpointHeartRateBpm HeartRateBpm { get; set; }

		public TrainingCenterDatabaseActivitiesActivityLapTrackpointExtensions Extensions { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapTrackpointPosition
	{
		public decimal LatitudeDegrees { get; set; }


		public decimal LongitudeDegrees { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapTrackpointHeartRateBpm
	{

		public int Value { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapTrackpointExtensions
	{

		[System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.garmin.com/xmlschemas/ActivityExtension/v2")]
		public TpxThing TPX { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/ActivityExtension/v2")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.garmin.com/xmlschemas/ActivityExtension/v2", IsNullable = false)]
	public class TpxThing
	{

		public decimal Speed { get; set; }


		public int RunCadence { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityLapExtensions
	{
		[System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.garmin.com/xmlschemas/ActivityExtension/v2")]
		public LXthing LX { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/ActivityExtension/v2")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.garmin.com/xmlschemas/ActivityExtension/v2", IsNullable = false)]
	public class LXthing
	{


		public decimal AvgSpeed { get; set; }


		public int AvgRunCadence { get; set; }


		public int MaxRunCadence { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityCreator
	{
		public string Name { get; set; }
		public uint UnitId { get; set; }

		public ushort ProductID { get; set; }


		public TrainingCenterDatabaseActivitiesActivityCreatorVersion Version { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseActivitiesActivityCreatorVersion
	{

		public int VersionMajor { get; set; }


		public int VersionMinor { get; set; }


		public int BuildMajor { get; set; }


		public int BuildMinor { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseAuthor
	{


		public string Name { get; set; }


		public TrainingCenterDatabaseAuthorBuild Build { get; set; }


		public string LangID { get; set; }


		public string PartNumber { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseAuthorBuild
	{

		public TrainingCenterDatabaseAuthorBuildVersion Version { get; set; }
	}


	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
	public class TrainingCenterDatabaseAuthorBuildVersion
	{

		public int VersionMajor { get; set; }

		public int VersionMinor { get; set; }

		public int BuildMajor { get; set; }

		public int BuildMinor { get; set; }
	}


}
