using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SelectScreenState : GameState
    {
        public override void Handle(GameManager pGameManager)
        {
            GameManager.SetGameState(GameManager.State.GamePlay);
            GameManager.Initialize();
        }

        // Should be called at some point after enter a new state
        public override void Initialize(GameManager pGameManager)
        {
            //Debug.WriteLine("Initializing the Select Screen");
            //---------------------------------------------------------------------------------------------------------
            // Create Header Fonts
            //---------------------------------------------------------------------------------------------------------

            FontManager.Add(Font.Name.InsertCoin, SpriteBatch.Name.Texts, "INSERT COIN", Glyph.Name.Consolas36pt, 350, 800);
            FontManager.Add(Font.Name.OneOrTwo, SpriteBatch.Name.Texts, "<1 OR 2 PLAYERS>", Glyph.Name.Consolas36pt, 300, 700);
            FontManager.Add(Font.Name.Select1P, SpriteBatch.Name.Texts, "*1 PLAYER    PRESS 1", Glyph.Name.Consolas36pt, 260, 600);
            FontManager.Add(Font.Name.Select2P, SpriteBatch.Name.Texts, "*2 PLAYERS   PRESS 2", Glyph.Name.Consolas36pt, 260, 550);

            InputSubject pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new SetGameModeObserver(GameManager.Mode.OnePlayerMode));
            pInputSubject.Attach(new AdvanceGameStateObserver());

            pInputSubject = InputManager.GetOneSubject();
            pInputSubject.Attach(new SetGameModeObserver(GameManager.Mode.OnePlayerMode));
            pInputSubject.Attach(new AdvanceGameStateObserver());

            pInputSubject = InputManager.GetTwoSubject();
            pInputSubject.Attach(new SetGameModeObserver(GameManager.Mode.TwoPlayerMode));
            pInputSubject.Attach(new AdvanceGameStateObserver());
        }

        // Should be called at some point before leaving state
        public override void CleanUp(GameManager pGameManager)
        {
            //Debug.WriteLine("Cleaning up Select Screen and Preparing to leave");
            // Clear text before leaving select screen
            FontManager.Remove(FontManager.Find(Font.Name.InsertCoin));
            FontManager.Remove(FontManager.Find(Font.Name.OneOrTwo));
            FontManager.Remove(FontManager.Find(Font.Name.Select1P));
            FontManager.Remove(FontManager.Find(Font.Name.Select2P));

            InputSubject pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.PurgeAll();

            pInputSubject = InputManager.GetOneSubject();
            pInputSubject.PurgeAll();

            pInputSubject = InputManager.GetTwoSubject();
            pInputSubject.PurgeAll();

            Handle(pGameManager);
        }

        // Should be called during the Update part of gameloop
        public override void Update(GameManager pGameManager)
        {
            GameObjectManager.Update();
            InputManager.Update();
            DelayedObjectManager.Process();
        }

        // Should be called during the Draw part of gameloop
        public override void Draw(GameManager pGameManager)
        {
            SpriteBatchManager.Draw();
        }
    }
}
