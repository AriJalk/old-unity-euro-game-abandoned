namespace EDBG.Rules
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
        Blue=Colors.Blue,
        Green=Colors.Green,
        Red=Colors.Red
    }

    public enum EntityNames
    {
        Map,
        Player,
        Automata,
        Neutral,
        Disc,
        Tile,
        SourceTile,
        TargetTile,
        SourceLocation,
        TargetLocation,
        ActionToken,
        ActionCard,
        DiscStack,
        Cube
    }

    public enum Ownership
    {
        Player = EntityNames.Player,
        Automata= EntityNames.Automata,
        Neutral= EntityNames.Neutral
    }

    public enum Colors
    {
        Neutral,
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        White
    }
}