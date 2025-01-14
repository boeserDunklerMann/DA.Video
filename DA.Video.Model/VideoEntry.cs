using System.Text.Json;

namespace DA.Video.Model
{
	/// <ChangeLog>
	/// <Create Datum="14.01.2025" Entwickler="DA" />
	/// <Change Datum="14.01.2025" Entwickler="DA">class moved to Model-library</Change>
	/// </ChangeLog>
	/// <summary>
	/// POCO class for a video
	/// </summary>
	public class VideoEntry
	{
		/// <summary>
		/// filename without path
		/// </summary>
		public string ID { get; set; } = "";
		public string Title { get; set; } = "";
		/// <summary>
		/// relative path to preview gif-file
		/// </summary>
		public string PreviewFile { get; set; } = "";
		public List<string> Tags { get; set; } = [];
		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}