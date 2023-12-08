using Microsoft.Extensions.Configuration;

namespace DL
{
	public class Conexion
	{
		private static readonly IConfiguration _configuration;
		static Conexion()
		{
			var builder = new ConfigurationBuilder()
		   .SetBasePath(Directory.GetCurrentDirectory())
		   .AddJsonFile("appsettings.json");

			_configuration = builder.Build();
		}

		public static string Get()
		{
			return _configuration.GetConnectionString("SqlServer");
		}
	}
}
