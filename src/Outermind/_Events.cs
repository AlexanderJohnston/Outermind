using Totem.Timeline;

namespace Outermind
{
  public class CreateCard : Command
  {
    public readonly Card Card;

    public CreateCard(Card card)
    {
      Card = card;
    }
  }
  public class MoveCard : Command
  {
    public readonly Card Card;

    public MoveCard(Card card)
    {
      Card = card;
    }
  }
  public class ResizeCard : Command
  {
    public readonly Card Card;

    public ResizeCard(Card card)
    {
      Card = card;
    }
  }

  public class CardCreated : Event
  {
    public readonly Card Card;

    public CardCreated(Card card)
    {
      Card = card;
    }
  }
  public class CardMoved : Event
  {
    public readonly Card Card;

    public CardMoved(Card card)
    {
      Card = card;
    }
  }
  public class CardResized : Event
  {
    public readonly Card Card;

    public CardResized(Card card)
    {
      Card = card;
    }
  }
}