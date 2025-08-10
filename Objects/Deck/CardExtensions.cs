using Four.Constants.Enums;

namespace Four.Objects.Deck;

public static class CardExtensions {

    public static bool ContainsValue(this IEnumerable<Card> cards, CardValue value) {
        return cards.Any(e => e.Value == value);
    }


    public static bool ContainsValues(this IEnumerable<Card> cards, CardValue[] values) {
        foreach (var value in values) { 
            if (!cards.ContainsValue(value)) {
                return false;
            }
        }
        return true;
    }


    public static int CalculateMiltonPoints(this IEnumerable<Card> cards) {
        if (cards.Count() == 0) {
            return 0;
        }
        return cards.Sum(e => e.MiltonPoints);
    }


    public static int CalculateMiltonPoints(this IEnumerable<CardValue> cardValues) {
        if (cardValues.Count() == 0) {
            return 0;
        }
        return cardValues.Sum(Card.CalculateMiltonPoints);
    }


    public static Card? HighestCard(this IEnumerable<Card> cards) {
        return cards.OrderBy(e => e.Value).LastOrDefault();
    }
}
