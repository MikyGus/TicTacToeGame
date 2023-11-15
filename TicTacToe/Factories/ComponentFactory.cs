using TicTacToe.Abstract;
using TicTacToe.Models.Components;

namespace TicTacToe.Factories;
internal static class ComponentFactory
{
    public static SpriteComponent SpriteComponent(IPlayer player) => new()
    {
        Sprite = player.Sprite,
        SpriteColor = player.SpriteColor,
        Parent = null,
    };

    public static PlayerComponent PlayerComponent(IPlayer player) => new()
    {
        Player = player,
        Parent = null,
    };

    public static MaySetMarkerComponent MaySetMarkerComponent() => new()
    {
        BackgroundColor = ConsoleColor.DarkGreen,
        MaySetInCell = true,
        Parent = null,
    };

}
