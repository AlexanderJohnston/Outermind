using System;
using System.Collections.Generic;
using Quantum.Imaging.Details;
using Totem;

namespace Quantum.Imaging
{

  public class MicroformScan : Scan
  {
    public Dictionary<string, string> Settings { get; set; } = new();
    public List<OperatorEvent> OperatorEvents { get; set; } = new();

    // QPF resolution
    public string QpfPath { get; set; }

    // QP lifecycle
    public DateTime QpOpenedAt { get; set; }
    public DateTime QpClosedAt { get; set; }
    public DateTime QpStartedAt { get; set; }
    public DateTime QpStoppedAt { get; set; }
    public bool QpSuccess { get; set; }
    public int QpExpectedFrames { get; set; }   // e.g., from "process start" details
    public int QpProcessedFrames { get; set; }  // e.g., from "process stop" or details
    public Id QpUser { get; set; }
    public string QpMachine { get; set; }

    // QS (scanner) lifecycle
    public DateTime? QsStartedAt { get; set; }   // "start scan"
    public DateTime? QsStoppedAt { get; set; }   // "stop scan"
    public DateTime? QsResetAt { get; set; }     // "reset"
  }
}