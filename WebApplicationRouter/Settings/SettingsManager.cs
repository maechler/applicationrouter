using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Marx.Net
{
	public class SettingsManager
	{
		protected String XmlPath = "xml/settings.xml";

		public Settings Read ()
		{
			var savedSettings = File.ReadAllText (this.XmlPath);
			var xsSubmit = new XmlSerializer (typeof(Marx.Net.Settings));
			var stringReader = new StringReader (savedSettings);

			return (Settings)xsSubmit.Deserialize (stringReader);
		}

		public void Write (Settings newSettings)
		{
			var xsSubmit = new XmlSerializer (typeof(Marx.Net.Settings));
			var sww = new StringWriter ();
			var writer = XmlWriter.Create (sww);

			xsSubmit.Serialize (writer, newSettings);
			File.WriteAllText (this.XmlPath, sww.ToString ());
		}
	}
}

