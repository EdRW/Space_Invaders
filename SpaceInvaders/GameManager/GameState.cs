using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameState
    {
        public abstract void Handle(GameManager pGameManager);

        // Should be called at some point after enter a new state
        public abstract void Initialize(GameManager pGameManager);

        // Should be called at some point before leaving state
        public abstract void CleanUp(GameManager pGameManager);

        // Should be called during the Update part of gameloop
        public abstract void Update(GameManager pGameManager);

        // Should be called during the Draw part of gameloop
        public abstract void Draw(GameManager pGameManager);
    }
}
