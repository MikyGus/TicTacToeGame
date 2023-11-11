using TicTacToe.Abstract;

namespace TicTacToe.Models;
internal class Player : IPlayer
{
    public string Name { get; init; }
    public char Sprite { get; init; }
    public ConsoleColor SpriteColor { get; init; }
    public Vector2 MarkerPosition { get; set; } = new(0, 0);

    public Player(string name, char sprite, ConsoleColor color)
    {
        Name = name;
        Sprite = sprite;
        SpriteColor = color;
    }
}
