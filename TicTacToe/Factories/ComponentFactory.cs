using TicTacToe.Abstract;
using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Factories;
internal static class ComponentFactory
{
    public static SpriteComponent SpriteComponent(IPlayer player) => new()
    {
        Sprite = player.Sprite,
        SpriteColor = player.SpriteColor,
        Parent = null,
        //Owner = player
    };

    public static PlayerComponent PlayerComponent(IPlayer player) => new()
    {
        Player = player,
    };
}
