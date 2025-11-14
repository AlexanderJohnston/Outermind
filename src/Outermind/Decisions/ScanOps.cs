using System;
using System.Collections.Generic;
using System.Text;
using Totem;
using Totem.Timeline;

namespace Quantum.Commands
{
  public class StartScan : Command
  {
    public Id Roll;
    public Id Operator;
    public DateTime TimeStamp;

    public StartScan(Id operatorId, Id rollId, DateTime timeStamp)
    {
      Roll = rollId;
      Operator = operatorId;
      TimeStamp = timeStamp;
    }
  }

  public class ScanStarted : Event
  {
    public Id Roll;
    public Id Operator;
    public DateTime TimeStamp;

    public ScanStarted(Id operatorId, Id rollId, DateTime timeStamp)
    {
      Roll = rollId;
      Operator = operatorId;
      TimeStamp = timeStamp;
    }
  }

  public class FinishScan : Command
  {
    public Id Roll;
    public DateTime TimeStamp;

    public FinishScan(Id rollId, DateTime timeStamp)
    {
      Roll = rollId;
      TimeStamp = timeStamp;
    }
  }

  public class ScanFinished : Event
  {
    public Id Roll;
    public DateTime TimeStamp;

    public ScanFinished(Id rollId, DateTime timeStamp)
    {
      Roll = rollId;
      TimeStamp = timeStamp;
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

  public class DeleteScan : Command
  {
    public Id Roll;

    public DeleteScan(Id rollId)
    {
      Roll = rollId;
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
}
