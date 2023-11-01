namespace EDBG.GameLogic.Rules
{
    public enum TileTypes
    {
        Mountain,
        Desert,
        Forest,
        City,
        Snowy,
        Default
    }

    public enum LocationTypes
    {
        Blue=PieceColors.Blue,
        Green=PieceColors.Green,
        Red=PieceColors.Red
    }

    public enum EntityNames
    {
        Map,
        Player,
        Bot,
        Neutral,
        Disc,
        Tile,
        SourceTile,
        TargetTile,
        ActionToken,
        ActionCard,
        DiscStack,
        Cube
    }

    public enum Ownership
    {
        Player = EntityNames.Player,
        Bot= EntityNames.Bot,
        Neutral= EntityNames.Neutral
    }

    public enum PieceColors
    {
        Neutral,
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        White
    }

    public enum TokenColors
    {
        Red,
        Green,
        Blue
    }
}