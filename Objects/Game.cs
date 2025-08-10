using Four.Constants.Enums;
using Four.Objects.Bidding;
using Four.Objects.Deck;
using System.Text;

namespace Four.Objects;

public class Game {

    public Dictionary<PlayerSide, Hand?> Players { get; private set; }

    public Bidding.Bidding Bidding { get; private set; }

    public Game() : this(PlayerSide.North) { }

    public Game(PlayerSide openingSide) {
        Bidding = new Bidding.Bidding(openingSide);
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


    /// <summary>
    /// Dodaje odzywkę do licytacji.
    /// </summary>
    /// <param name="input"></param>
    /// <returns>False - licytacja się zakończyła, True - ciągle trwa.</returns>
    public bool NextBid(string input) {
        return Bidding.Bid(Bid.Read(input));
    }


    public PlayerSide ActivePlayer => Bidding.ActivePlayer;


    public override string ToString() {
        var stringBuilder = new StringBuilder();
        foreach (var player in Players) {
            if (player.Value == null) {
                continue;
            }
            stringBuilder.AppendLine(player.Key.ToString());
            stringBuilder.AppendLine(player.Value.ToString());
        }
        return stringBuilder.ToString();
    }
}
