using System.Collections.Generic;
using Totem;
using Totem.Timeline;

namespace Quantum.Commands
{
  public class RecordSettings : Command
  {
    public Id Roll;
    public Dictionary<string, string> Settings;

    public RecordSettings(Id rollId, Dictionary<string, string> settings)
    {
      Roll = rollId;
      Settings = settings;
    }
  }

  public class SettingsUpdated : Event
  {
    public Id Roll;
    public Dictionary<string, string> Settings;

    public SettingsUpdated(Id rollId, Dictionary<string, string> settings)
    {
      Roll = rollId;
      Settings = settings;
    }
  }
}