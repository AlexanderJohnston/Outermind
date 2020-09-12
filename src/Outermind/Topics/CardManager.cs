using System;
using System.Collections.Generic;
using System.Text;
using Totem.Timeline;

namespace Outermind.Topics
{
  public class CardManager : Topic
  {
    void When(CreateCard c)
    {
      Then(new CardCreated(c.Card));
    }
    void When(MoveCard c)
    {
      Then(new CardMoved(c.Card));
    }
    void When(ResizeCard c)
    {
      Then(new CardResized(c.Card));
    }
  }
}
