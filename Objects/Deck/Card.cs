using Four.Constants.Enums;

namespace Four.Objects.Deck;

public class Card {

    public CardColor Color { get; set; }

    public CardValue Value { get; set; }

    public int MiltonPoints { get; private set; }


    public Card(CardColor color, CardValue value) {
        if (color == CardColor.NoTrump) {
            throw new Exception("Card cannot have no trump color.");
        }

        Color = color;
        Value = value;
        MiltonPoints = CalculateMiltonPoints(Value);
    }


    public override string ToString() {
        return ValueToString[Value] + ColorToChar[Color];
    }


    private static readonly Dictionary<CardValue, int> _miltonPointsDictionary = new Dictionary<CardValue, int>() {
        { CardValue.Ace, 4 },
        { CardValue.King, 3 },
        { CardValue.Queen, 2 },
        { CardValue.Jack, 1 }
    };


    public static readonly Dictionary<CardValue, string> ValueToString = new Dictionary<CardValue, string>() {
        { CardValue.Ace, "A" },
        { CardValue.King, "K" },
        { CardValue.Queen, "Q" },
        { CardValue.Jack, "J" },
        { CardValue.Ten, "10" },
        { CardValue.Nine, "9" },
        { CardValue.Eight, "8" },
        { CardValue.Seven, "7" },
        { CardValue.Six, "6" },
        { CardValue.Five, "5" },
        { CardValue.Four, "4" },
        { CardValue.Three, "3" },
        { CardValue.Two, "2" },
    };


    public static readonly Dictionary<CardColor, char> ColorToChar = new Dictionary<CardColor, char>() {
        { CardColor.Spade, '♠' },
        { CardColor.Heart, '♥' },
        { CardColor.Diamond, '♦' },
        { CardColor.Club, '♣' },
    };


    public static int CalculateMiltonPoints(CardValue value) {
        if (_miltonPointsDictionary.ContainsKey(value)) {
            return _miltonPointsDictionary[value];
        }
        return 0;
    }
}
