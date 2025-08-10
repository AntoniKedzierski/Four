using Four.Constants.Enums;
using System.Text;

namespace Four.Objects.Bidding;

public class Bidding {

    public int RoundNumber { get; private set; }

    public List<Bid> Bids { get; private set; }

    public PlayerSide OpeningSide { get; private set; }

    public PlayerSide ActivePlayer { get; private set; }

    public Bidding(PlayerSide openingSide) {
        Bids = [];
        RoundNumber = 0;
        OpeningSide = ActivePlayer = openingSide;
    }


    public bool Bid(Bid bid) {
        Bids.Add(bid);

        if (CheckEnding()) {
            return false;
        }

        ActivePlayer = (PlayerSide)(((int)ActivePlayer + 1) % 4);
        if (ActivePlayer == OpeningSide) {
            RoundNumber++;
        }

        return true;
    }


    public Bid[] GetPlayerBids(PlayerSide player) {
        /*  0      1     2      3
         *  north  east  south  west
         *                      pas
         *  pas    1pi   1BA    2pi
         *  pas    4pi   x      pas
         *  pas    pas
         */
        var offset = (4 + (int)player - (int)OpeningSide) % 4;
        var bids = new List<Bid>();

        for (int i = offset; i < Bids.Count; i += 4) {
            bids.Add(Bids[i]);
        }

        return bids.ToArray();
    }


    public bool CheckEnding() {
        if (Bids.Count > 3) {
            return Bids.TakeLast(3).All(e => e.Type == BidType.Pass);
        }
        return false;
    }


    public override string ToString() {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("N    E    S    W    ");

        for (int i = 0; i < Bids.Count / 4 + 2; ++i) {
            for (int j = 0; j < 4; ++j) {
                // Pierwszy wiersz, nieodzywający się gracze.
                if (j < (int)OpeningSide && i == 0) {
                    stringBuilder.Append(new string(' ', 5));
                    continue;
                }
                var bidNumber = 4 * i + j - (int)OpeningSide;
                if (bidNumber >= Bids.Count) {
                    break;
                }
                stringBuilder.Append(Bids[bidNumber] + " ");
            }
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }

}
