using System;
using System.Collections.Generic;
using System.Text;
using Totem;
using Totem.Timeline;

namespace Quantum.Decisions
{
  public class OperatorReport : Command
  {
    public Id Roll;
    public Id Operator;
    public string Notes;

    public OperatorReport(Id rollId, Id operatorId, string notes)
    {
      Roll = rollId;
      Operator = operatorId;
      Notes = notes;
    }
  }

  public class ScanCommented : Event
  {
    public Id Roll;
    public Id Operator;
    public string Notes;

    public ScanCommented(Id rollId, Id operatorId, string notes)
    {
      Roll = rollId;
      Operator = operatorId;
      Notes = notes;
    }
  }
}
