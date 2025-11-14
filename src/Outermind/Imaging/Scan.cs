using System;
using Totem;

namespace Quantum.Imaging
{
  public class Scan
  {
    public Id Id;
    public Id Operator;
    public string Path;
    public int ImageCount;
    public DateTime Started;
    public DateTime Stopped;

    public override bool Equals(object obj)
    {
      return obj is Scan scan && Id.Equals(scan.Id);
    }

    public override int GetHashCode()
    {
      var hashCode = -239686454;
      hashCode = hashCode * -1521134295 + Id.GetHashCode();
      return hashCode;
    }
  }
}