using System.Collections.Generic;
using OpenTK.Input;

namespace OsuTK
{
    public interface IStateBasedGame
    {
        Dictionary<GameState, GameStateObject> GameStateObjects { get; }
        GameState CurrentGameState { get; }
        GameStateObject CurrentGameStateObject { get; }
        GameStateObject GetGameStateObject(GameState state);
        void SetGameState(GameState state);

        void SetMouseBasedViewport(bool follow);
    }
}
