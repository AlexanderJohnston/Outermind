using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem;
using Totem.Timeline;

namespace Outermind.Queries
{
  public class StackQuery : Query
  {
    public Dictionary<Id, Card> Stack = new Dictionary<Id, Card>();

    void Given(CardCreated e)
    {
      Stack.Add(e.Card.Id, e.Card);
    }

    void Given(CardMoved e)
    {
      var selected = Matching(e.Card);
      selected.Update(e.Card);
      Refresh(selected);
    }

    void Given(CardResized e)
    {
      var selected = Matching(e.Card);
      selected.Update(e.Card);
      Refresh(selected);
    }

    void Given(CardRemoved e)
    {
      var selected = Matching(e.Card);
      Stack.Remove(selected.Id);
    }

    private Card Matching(Card existingCard) => Stack[existingCard.Id];
    private void Refresh(Card updatedCard) => Stack[updatedCard.Id] = updatedCard;
  }
}
