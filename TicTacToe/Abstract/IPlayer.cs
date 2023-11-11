using TicTacToe.Models;

namespace TicTacToe.Abstract;
internal interface IPlayer
{
    string Name { get; }
    char Sprite { get; init; }
    ConsoleColor SpriteColor { get; init; }
    Vector2 MarkerPosition { get; set; }
}
