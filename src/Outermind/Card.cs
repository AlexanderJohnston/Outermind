using Totem;

namespace Outermind
{
  public class Card
  {
    public Id Id;
    public int Column;
    public int Columns;
    public int Row;
    public int Rows;
    public int Elevation;

    public void Update (Card updated)
    {
      Column = updated.Column;
      Columns = updated.Columns;
      Row = updated.Row;
      Rows = updated.Rows;
      Elevation = updated.Elevation;
    }

    public override bool Equals(object obj)
    {
      return obj is Card card && Id.Equals(card.Id);
    }

    public override int GetHashCode()
    {
      var hashCode = -239686454;
      hashCode = hashCode * -1521134295 + Id.GetHashCode();
      return hashCode;
    }
  }
}