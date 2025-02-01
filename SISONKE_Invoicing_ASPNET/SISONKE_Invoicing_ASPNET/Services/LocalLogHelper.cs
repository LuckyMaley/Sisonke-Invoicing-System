using log4net;
using System.Runtime.CompilerServices;

namespace SISONKE_Invoicing_ASPNET.Services
{
	public static class LocalLogHelper
	{
		public static ILog GetLogger([CallerFilePath] string filename = "")
		{
			return LogManager.GetLogger(filename);
		}
	}
}
