using Four.Constants.Enums;

namespace Four.Objects.Deck;

public class Card {

    private static readonly Dictionary<CardValue, int> _miltonPointsDictionary = new Dictionary<CardValue, int>() {
        { CardValue.Ace, 4 },
        { CardValue.King, 3 },
        { CardValue.Queen, 2 },
        { CardValue.Jack, 1 }
    };


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


    public static int CalculateMiltonPoints(CardValue value) {
        if (_miltonPointsDictionary.ContainsKey(value)) {
            return _miltonPointsDictionary[value];
        }
        return 0;
    }


}
