using System.Threading.Tasks;
using Totem.App.Web;

namespace Outermind.Web
{
  /// <summary>
  /// The web server of the Outermind area
  /// </summary>
  public static class Program
  {
    static Task Main() =>
      WebApp.Run<OutermindArea>();
  }
}