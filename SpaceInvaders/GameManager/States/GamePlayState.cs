using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GamePlayState : GameState
    {
        public override void Handle(GameManager pGameManager)
        {
            GameManager.SetGameState(GameManager.State.GameOver);
            GameManager.Initialize();
        }

        // Should be called at some point after enter a new state
        public override void Initialize(GameManager pGameManager)
        {
            //Debug.WriteLine("Initializing the GamePlay State in {0}", pGameManager.gameMode);

            Font pCredits = FontManager.Find(Font.Name.Credits);
            Font pScoreHeader1 = FontManager.Find(Font.Name.ScoreHeader1);
            Font pScoreHeader2 = FontManager.Find(Font.Name.ScoreHeader2);

            pScoreHeader1.UpdateMessage("*SCORE<1>*");
            pScoreHeader2.UpdateMessage(" SCORE<2> ");
            LivesManager.DisplayLives(3);

            //---------------------------------------------------------------------------------------------------------
            // Create the player ship and missile
            //---------------------------------------------------------------------------------------------------------
            ShipManager.Create();

            pGameManager.SetActivePlayer(PlayerArtifact.Name.PlayerOne);
            pGameManager.pActiveGameModeStrategy.InitializeLevel(pGameManager);
            pGameManager.poPlayer1.RestoreManagerStates(pGameManager.pGame.GetTime());

            //---------------------------------------------------------------------------------------------------------
            // Add Keyboard Input Observers
            //---------------------------------------------------------------------------------------------------------
            InputSubject pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());            
        }

        // Should be called at some point before leaving state
        public override void CleanUp(GameManager pGameManager)
        {
            GameManager.PushHighScoreToFont();

            ShipManager.Purge();

            TimerManager.PurgeAllNodes();
            GameObjectManager.PurgeAllNodes();

            SpriteBatch pSBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pSBatch.GetSBNodeManager().PurgeAllNodes();
            pSBatch = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            pSBatch.GetSBNodeManager().PurgeAllNodes();

            ColPairManager.PurgeAllNodes();

            DelayedObjectManager.PurgeAll();

            InputSubject pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.PurgeAll();

            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.PurgeAll();

            pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.PurgeAll();


            Handle(pGameManager);
        }

        // Should be called during the Update part of gameloop
        public override void Update(GameManager pGameManager)
        {
            // Debug.WriteLine("Tick" + this.GetTime());
            InputManager.Update();

            // Sprite animations and motions being handled in TimerManger
            TimerManager.Update(pGameManager.pGame.GetTime());

            // Pushes updates from Gameobjects to proxysprites
            GameObjectManager.Update();

            ColPairManager.Process();

            DelayedObjectManager.Process();

            pGameManager.HandleEndOfPlayerTurn();
        }

        // Should be called during the Draw part of gameloop
        public override void Draw(GameManager pGameManager)
        {
            SpriteBatchManager.Draw();
        }
    }
}