using TicTacToe.Abstract;
using TicTacToe.Models.Components;

namespace TicTacToe.Models;
internal class Player : IPlayer
{
    public SpriteComponent SpriteComponent { get; init; }
    public string Name { get; init; }
    public Vector2 CurrentMarkerPosition { get; set; } = new(0, 0);

    public Player(string name, char sprite, ConsoleColor color)
    {
        Name = name;
        SpriteComponent = new()
        {
            Sprite = sprite,
            SpriteColor = color,
            Parent = this
        };
    }
}
