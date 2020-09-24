using System.Threading.Tasks;
using Totem.App.Web;
using Totem.Timeline.Hosting;

namespace Outermind.Web
{
  /// <summary>
  /// The web server of the Outermind area
  /// </summary>
  public static class Program
  {
    static Task Main() =>
      WebApp.Run<OutermindArea>(/*(configuration) => { }*/);
  }
}