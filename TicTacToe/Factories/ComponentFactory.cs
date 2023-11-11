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
        Owner = player
    };
}
