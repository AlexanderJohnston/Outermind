using System.Threading.Tasks;
using Totem.App.Service;

namespace Outermind.Service
{
  /// <summary>
  /// The timeline server of the Outermind area
  /// </summary>
  public static class Program
  {
    static Task Main() =>
      ServiceApp.Run<OutermindArea>();
  }
}