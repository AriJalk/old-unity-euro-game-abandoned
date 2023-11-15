namespace EDBG.GameLogic.Rules
{
    public enum TileTypes
    {
        Mountain = TileColors.Red,
        Desert = TileColors.Yellow,
        Forest = TileColors.Green,
        City = TileColors.White,
        Snowy,
        Default,
    }

    public enum LocationTypes
    {
        Blue=PieceColors.Blue,
        Green=PieceColors.Green,
        Red=PieceColors.Red,
    }


    public enum Ownership
    {
        HumanPlayer,
        BotPlayer,
        Neutral,
    }

    public enum PieceColors
    {
        Neutral,
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        White,
    }

    public enum DiscColors
    {
        White,
        Black,
        Blue,
        Red,
        Green,
        Yellow,
    }
    
    public enum TileColors
    {
        White,
        Black,
        Red,
        Green,
        Blue,
        Brown,
        Yellow,

    }

    public enum TokenColors
    {
        Red,
        Green,
        Blue
    }

    public enum CorporationActions
    {
        AddExpansionPoints,
        AddMarketPoints,
        MoveDisc,
        Research,
    }
}