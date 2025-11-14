using System;
using System.Collections.Generic;
using Totem;
using Totem.Timeline;

namespace Outermind
{




  public class DeleteScan : Command
  {
    public Id Roll;

    public DeleteScan(Id rollId)
    {
      Roll = rollId;
    }
  }

  public class MoveScan : Command
  {
    public Id Roll;
    public Id Operator;
    public string Source;
    public string Destination;

    public MoveScan(Id rollId, Id operatorId, string source, string destination)
    {
      Roll = rollId;
      Operator = operatorId;
      Source = source;
      Destination = destination;
    }
  }

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







  public class ScanDeleted : Command
  {
    public Id Roll;

    public ScanDeleted(Id rollId)
    {
      Roll = rollId;
    }
  }

  public class ScanMoved : Event
  {
    public Id Roll;
    public Id Operator;
    public string Source;
    public string Destination;

    public ScanMoved(Id rollId, Id operatorId, string source, string destination)
    {
      Roll = rollId;
      Operator = operatorId;
      Source = source;
      Destination = destination;
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