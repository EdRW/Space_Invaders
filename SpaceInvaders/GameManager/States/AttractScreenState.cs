using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AttractScreenState : GameState
    {
        public override void Handle(GameManager pGameManager)
        {
            GameManager.SetGameState(GameManager.State.SelectScreen);
            GameManager.Initialize();
        }

        // Should be called at some point after enter a new state
        public override void Initialize(GameManager pGameManager)
        {
            //Debug.WriteLine("Initializing the Attract Screen");
            //---------------------------------------------------------------------------------------------------------
            // Create Header Fonts
            //---------------------------------------------------------------------------------------------------------

            FontManager.Add(Font.Name.Play, SpriteBatch.Name.Texts, "PLAY", Glyph.Name.Consolas36pt, 400, 800);
            FontManager.Add(Font.Name.SpaceInvader, SpriteBatch.Name.Texts, "SPACE    INVADERS", Glyph.Name.Consolas36pt, 300, 700);
            FontManager.Add(Font.Name.ScoringTable, SpriteBatch.Name.Texts, "*SCORING ADVANCE TABLE*", Glyph.Name.Consolas36pt, 220, 600);
            FontManager.Add(Font.Name.ScoreMystery, SpriteBatch.Name.Texts, "= ? Mystery", Glyph.Name.Consolas36pt, 400, 550);
            FontManager.Add(Font.Name.ScoreSmInvader, SpriteBatch.Name.Texts, "= 30 POINTS", Glyph.Name.Consolas36pt, 400, 500);
            FontManager.Add(Font.Name.ScoreMedInvader, SpriteBatch.Name.Texts, "= 20 POINTS", Glyph.Name.Consolas36pt, 400, 450);
            FontManager.Add(Font.Name.ScoreLgInvader, SpriteBatch.Name.Texts, "= 10 POINTS", Glyph.Name.Consolas36pt, 400, 400);
            FontManager.Add(Font.Name.PressSpace, SpriteBatch.Name.Texts, "<PRESS SPACE TO CONTINUE>", Glyph.Name.Consolas36pt, 200, 200);

            InvaderFactory rootLevelIF = new InvaderFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes);
            UFO pUFO = (UFO)rootLevelIF.ActiveCreate(GameObject.Type.UFO, 350, 550);
            pUFO.speedX = 0;
            rootLevelIF.ActiveCreate(GameObject.Type.SmallInvader, 350, 500);
            rootLevelIF.ActiveCreate(GameObject.Type.MediumInvader, 350, 450);
            rootLevelIF.ActiveCreate(GameObject.Type.LargeInvader, 350, 400);

            InputSubject pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new AdvanceGameStateObserver());
        }

        // Should be called at some point before leaving state
        public override void CleanUp(GameManager pGameManager)
        {
            //Debug.WriteLine("Cleaning up Attract Screen and Preparing to leave");

            // Clear text before leaving select screen
            FontManager.Remove(FontManager.Find(Font.Name.Play));
            FontManager.Remove(FontManager.Find(Font.Name.SpaceInvader));
            FontManager.Remove(FontManager.Find(Font.Name.ScoringTable));
            FontManager.Remove(FontManager.Find(Font.Name.ScoreMystery));
            FontManager.Remove(FontManager.Find(Font.Name.ScoreSmInvader));
            FontManager.Remove(FontManager.Find(Font.Name.ScoreMedInvader));
            FontManager.Remove(FontManager.Find(Font.Name.ScoreLgInvader));
            FontManager.Remove(FontManager.Find(Font.Name.PressSpace));

            GameObject pGameObj = GameObjectManager.Find(GameObject.Name.UFO);
            pGameObj.Remove();
            pGameObj = GameObjectManager.Find(GameObject.Name.SmallInvader);
            pGameObj.Remove();
            pGameObj = GameObjectManager.Find(GameObject.Name.MediumInvader);
            pGameObj.Remove();
            pGameObj = GameObjectManager.Find(GameObject.Name.LargeInvader);
            pGameObj.Remove();

            InputSubject pInputSubject = InputManager.GetSpaceSubject();
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
