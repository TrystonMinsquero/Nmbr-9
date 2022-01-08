using System.Collections.Generic;
using _Scripts;

public class Deck
{
    private List<Card> _cards;

    public Deck(Card[] availablePieces, uint numOccurences = 2)
    {
        _cards = new List<Card>();
        int idCounter = 1;
        
        for(int i = 0; i < numOccurences; i++)
            foreach (Card pieceType in availablePieces)
            {
                _cards.Add(pieceType);
            }
        _cards.Shuffle();

    }

    public Card DrawCard()
    {
        if (_cards.Count <= 0)
            return null;
        
        Card card = _cards[_cards.Count - 1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }
    
}
