using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    public static int numberOfRow = 2;
    public static int cardsPerRow = 13;
    public static CardColor Color {
        get {
            if (!isColorSet) {
                _color = (CardColor)Random.Range(0, 2);
                isColorSet = true;
            }
            return _color;
        }
    }

    private static bool isColorSet = false;
    private static CardColor _color;
    private static int seed = int.MinValue;

    public static CardSuit GetSuit (int rowIndex) {
        if (numberOfRow == 2) {
            if (seed < 0) {
                seed = Random.Range(0, 2);
            }

            if (rowIndex == 0) {
                return seed == 0 ? CardSuit.Diamonds : CardSuit.Clubs;
            } else {
                return seed == 0 ? CardSuit.Hearts : CardSuit.Spades;
            }
        } else {
            return (CardSuit)(rowIndex % numberOfRow);
        }
    }
}
