using Four.Constants.Enums;
using Four.Objects;
using Four.Objects.Deck;

namespace Four.Tests;

public static class DealGenerator {

    public static Game CreateGame() {
        var deck = new Card[52];
        for (int i = 0; i < 52; ++i) {
            int color = i / 13;
            int value = i % 13;
            deck[i] = new Card((CardColor)color, (CardValue)value);
        }

        var rng = new Random();
        for (int i = deck.Length - 1; i > 0; i--) {
            int j = rng.Next(i + 1);
            var temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }

        var game = new Game();
        PlayerSide[] sides = [PlayerSide.North, PlayerSide.East, PlayerSide.South, PlayerSide.West];

        for (int i = 0; i < sides.Length; i++) {
            var side = sides[i];
            game.AssignHand(side, deck.Skip(i * 13).Take(13).ToArray());
        }

        return game;
    }
}
