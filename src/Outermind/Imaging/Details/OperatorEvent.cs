using System;
using System.Collections.Generic;
using Totem;

namespace Quantum.Imaging.Details
{
  public class OperatorEvent
  {
    public Id Operator;
    public string Action { get; set; } = ""; // e.g., "start scan", "open", "process start", "SUCCESS"
    public DateTime Timestamp { get; set; }
    public string Source { get; set; } = ""; // QS / QP
    public string Machine { get; set; } = "";
    public string Path { get; set; }        // For "open"/"close" or when a path appears in details
    public List<string> Details { get; set; } = new();
  }
}