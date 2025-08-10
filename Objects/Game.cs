using Four.Constants.Enums;
using Four.Objects.Deck;
using System.Text;

namespace Four.Objects;

public class Game {

    public Dictionary<PlayerSide, Hand?> Players { get; private set; }

    public Game() {
        Players = new Dictionary<PlayerSide, Hand?>() {
            { PlayerSide.North, null },
            { PlayerSide.East, null },
            { PlayerSide.South, null },
            { PlayerSide.West, null }
        };
    }

    public void AssignHand(PlayerSide side, Card[] cards) {
        Players[side] = new Hand(cards);
    }


    public override string ToString() {
        var stringBuilder = new StringBuilder();
        foreach (var player in Players) {
            if (player.Value == null) {
                continue;
            }
            stringBuilder.AppendLine(player.Key.ToString());
            stringBuilder.AppendLine($"NT PC: { player.Value.MiltonPointsNoTrump }");
            stringBuilder.AppendLine($"PC: { player.Value.MiltonPointsColor }");
            stringBuilder.AppendLine(player.Value.ToString());
        }
        return stringBuilder.ToString();
    }
}
