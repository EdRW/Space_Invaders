using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameOverState : GameState
    {
        public override void Handle(GameManager pGameManager)
        {
            GameManager.SetGameState(GameManager.State.AttractScreen);
            GameManager.Initialize();
        }

        // Should be called at some point after enter a new state
        public override void Initialize(GameManager pGameManager)
        {
            FontManager.Add(Font.Name.GameOver, SpriteBatch.Name.Texts, "GAME  OVER", Glyph.Name.Consolas36pt, 350, 800);
            FontManager.Add(Font.Name.PressSpace, SpriteBatch.Name.Texts, "<PRESS SPACE TO CONTINUE>", Glyph.Name.Consolas36pt, 200, 200);

            Font pCredits = FontManager.Find(Font.Name.Credits);
            pCredits.UpdateMessage("CREDITS  00");

            Font pScoreHeader1 = FontManager.Find(Font.Name.ScoreHeader1);
            Font pScoreHeader2 = FontManager.Find(Font.Name.ScoreHeader2);

            pScoreHeader1.UpdateMessage(" SCORE<1> ");
            pScoreHeader2.UpdateMessage(" SCORE<2> ");


            InputSubject pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new AdvanceGameStateObserver());
        }

        // Should be called at some point before leaving state
        public override void CleanUp(GameManager pGameManager)
        {
            pGameManager.poPlayer1.Reset();
            pGameManager.poPlayer2.Reset();
            GameManager.PushPlayerScoresToFonts();

            FontManager.Remove(FontManager.Find(Font.Name.PressSpace));
            FontManager.Remove(FontManager.Find(Font.Name.GameOver));

            InputSubject pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.PurgeAll();

            Handle(pGameManager);
        }

        // Should be called during the Update part of gameloop
        public override void Update(GameManager pGameManager)
        {
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