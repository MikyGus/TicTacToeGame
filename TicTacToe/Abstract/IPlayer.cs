using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Abstract;
internal interface IPlayer
{
    SpriteComponent SpriteComponent { get; }
    string Name { get; }
    Vector2 CurrentMarkerPosition { get; }
}
