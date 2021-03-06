using UnityEngine;
using System.Collections;

public enum CardColor {
    Red,
    Blue,
}

public enum CardSuit {
    Clubs = 0,
    Diamonds = 1,
    Spades = 2,
    Hearts = 3,
}

public enum CardName {
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
}

[System.Serializable]
public class CardSettings {

    [SerializeField]
    private CardColor _color;
    [SerializeField]
    private CardSuit _suit;
    [SerializeField]
    private CardName _name;

    public CardColor Color {
        get { return _color; }
    }
    public CardSuit Suit {
        get { return _suit; }
    }
    public CardName Name { 
        get { return _name; }
    }

    public CardSettings (CardColor color, CardSuit suit, CardName name) {
        _color = color;
        _suit = suit;
        _name = name;
    }

    public override bool Equals(object obj) {
        return obj is CardSettings settings &&
               _color == settings.Color &&
               _suit == settings.Suit &&
               _name == settings.Name;
    }

    public override int GetHashCode() {
        var hashCode = -986649535;
        hashCode = hashCode * -1521134295 + _color.GetHashCode();
        hashCode = hashCode * -1521134295 + _suit.GetHashCode();
        hashCode = hashCode * -1521134295 + _name.GetHashCode();
        return hashCode;
    }
}
