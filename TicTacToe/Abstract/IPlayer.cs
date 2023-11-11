using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Abstract;
internal interface IPlayer
{
    string Name { get; }
    char Sprite { get; init; }
    ConsoleColor SpriteColor { get; init; }
    Vector2 CurrentMarkerPosition { get; set; }
}
