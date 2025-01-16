namespace DA.Video.WebAPI.Exceptions
{
	/// <ChangeLog>
	/// <Create Datum="16.01.2025" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Will be thrown if an object cannot be found
	/// </summary>
	public class ObjectNotFoundException(string entityName, object? id) : Exception
	{
		public override string ToString()
		{
			return $"Entity {entityName} with ID {id} not found.";
		}
	}
}