using DA.Video.Model;

namespace DA.Video.EFCore.Setup.Cons
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("database will be dropped!!");
			Console.WriteLine("Press any key or CTRL+C");
			Console.ReadKey();
			InsertData();
		}

		static void InsertData()
		{
			using VideoContext ctx = new("Server=localhost;Database=DinnerPlanner_dev;") ;
			ctx.Database.EnsureDeleted();
			ctx.Database.EnsureCreated();
		}
	}
}
