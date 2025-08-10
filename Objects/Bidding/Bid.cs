using Four.Constants.Enums;
using Four.Objects.Deck;
using System.Text.RegularExpressions;

namespace Four.Objects.Bidding;

public class Bid {

    public BidType Type { get; private set; }

    public CardColor? Color { get; private set; }

    public int? Value { get; private set; }

    public Bid(BidType type) {
        Type = type;
        Color = null;
        Value = null;
    }

    public Bid(CardColor color, int value) {
        Type = BidType.Value;
        Color = color;
        Value = value;
    }


    public override string ToString() {
        return Type switch {
            BidType.Pass => "pass",
            BidType.Double => "x   ",
            BidType.DoubleDouble => "xx  ",
            BidType.Value => $"{Value} " + (Color == CardColor.NoTrump ? "NT" : (Card.ColorToChar[Color ?? CardColor.NoTrump] + " ")),
            _ => throw new Exception("Cannot serialize bid.")
        };
    }


    public static Bid Read(string input) {
        input = input.Trim().ToUpper();
        if (input.Equals("P", StringComparison.CurrentCultureIgnoreCase)) { 
            return new Bid(BidType.Pass);
        }
        if (input.Equals("X", StringComparison.CurrentCultureIgnoreCase)) {
            return new Bid(BidType.Double);
        }
        if (input.Equals("XX", StringComparison.CurrentCultureIgnoreCase)) {
            return new Bid(BidType.DoubleDouble);
        }
        if (Regex.IsMatch(input, @"\d(?:NT|SP|HE|DI|CL)")) {
            var value = int.Parse(input[0].ToString());
            var color = input.Substring(1, 2);
            return color switch { 
                "NT" => new Bid(CardColor.NoTrump, value),
                "SP" => new Bid(CardColor.Spade, value),
                "HE" => new Bid(CardColor.Heart, value),
                "DI" => new Bid(CardColor.Diamond, value),
                "CL" => new Bid(CardColor.Club, value),
                _ => throw new ArgumentException($"Invalid input: {input}")
            };
        }
        throw new ArgumentException($"Invalid input: {input}");
    }

}
