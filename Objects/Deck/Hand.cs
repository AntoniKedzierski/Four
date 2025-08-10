using Four.Constants.Enums;
using System.Text;

namespace Four.Objects.Deck;

public class Hand {

    public Card[] Cards { get; private set; }

    public int MiltonPointsNoTrump { get; private set; }

    public int MiltonPointsColor { get; private set; }

    public Hand(Card[] cards) {
        Cards = cards;
        MiltonPointsNoTrump = CalculateMiltonPointsInNoTrumpGame();
        MiltonPointsColor = CalculateMiltonPointsInColorGame();
    }


    public int MiltonPointInColor(CardColor color) => Cards.Where(e => e.Color == color).Sum(e => e.MiltonPoints);

    public int NumberOfCardsInColor(CardColor color) => Cards.Where(e => e.Color == color).Count();

    public IEnumerable<Card> this[CardColor color] => Cards.Where(e => e.Color == color);

    public IDictionary<CardColor, int> CardNumberInColor => Cards
        .GroupBy(e => e.Color)
        .ToDictionary(e => e.Key, e => e.Count());


    public IDictionary<CardColor, SuitState> SuitStates {
        get {
            var result = new Dictionary<CardColor, SuitState>();
            foreach (var color in _colors) {
                var numberOfCardsInColor = NumberOfCardsInColor(color);
                result[color] = numberOfCardsInColor switch {
                    0 => SuitState.Void,
                    1 => SuitState.Single,
                    2 => SuitState.Double,
                    _ => SuitState.Many
                };
            }
            return result;
        }
    }


    /// <summary>
    /// Liczy punkty ze wszystkich kolorów, jakby to była gra bez atu.
    /// </summary>
    public int CalculateMiltonPointsInNoTrumpGame() => _colors
        .Select(color => this[color].CalculateMiltonPoints())
        .Sum();


    /// <summary>
    /// Liczy punkty układowe do gry kolorowej.
    /// </summary>
    public int CalculateMiltonPointsInColorGame() {
        var totalPoints = 0;
        foreach (var (color, state) in SuitStates) {
            totalPoints += CalculateMiltonPoints(state, this[color]);
        }
        return totalPoints;
    }


    public override string ToString() {
        var stringBuilder = new StringBuilder();
        foreach (var color in _colors) {
            stringBuilder.Append(Card.ColorToChar[color] + " ");
            foreach (var card in this[color].OrderByDescending(e => e.Value)) {
                stringBuilder.Append(Card.ValueToString[card.Value] + " ");
            }
            stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }


    #region static
    private static readonly CardColor[] _colors = [CardColor.Spade, CardColor.Heart, CardColor.Diamond, CardColor.Club];

    private static readonly Dictionary<SuitState, int> _miltonPointsForState = new Dictionary<SuitState, int>() {
        { SuitState.Void, 3 },
        { SuitState.Single, 2 },
        { SuitState.Double, 1 },
        { SuitState.Many, 0 }
    };


    public static int CalculateMiltonPointsForState(SuitState state) {
        if (_miltonPointsForState.ContainsKey(state)) {
            return _miltonPointsForState[state];
        }
        return 0;
    }


    public static int CalculateMiltonPoints(SuitState state, IEnumerable<Card> cards) {
        switch (state) {
            case SuitState.Void:
                return CalculateMiltonPointsForState(state);
            case SuitState.Single:
                var card = cards.First();
                if (card.Value == CardValue.King || card.Value == CardValue.Queen || card.Value == CardValue.Jack) {
                    return card.MiltonPoints;
                }
                return CalculateMiltonPointsForState(state) + cards.CalculateMiltonPoints();
            case SuitState.Double:
                if (cards.ContainsValue(CardValue.Queen) || cards.ContainsValue(CardValue.Jack)) {
                    return cards.CalculateMiltonPoints();
                }
                return CalculateMiltonPointsForState(state) + cards.CalculateMiltonPoints();
            case SuitState.Many:
                return cards.CalculateMiltonPoints();
            default:
                return 0;
        }
    }

    #endregion
}
