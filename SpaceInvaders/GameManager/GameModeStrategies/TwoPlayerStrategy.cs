using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TwoPlayerStrategy : GameModeStrategy
    {
        public override void EndPlayerTurn(GameManager pGameManager)
        {
            //Debug.WriteLine("RESETTING SHIP!");
            ShipManager.ResetShip();

            SoundManager.StopAllSounds();

            Font pScoreHeader1 = FontManager.Find(Font.Name.ScoreHeader1);
            Font pScoreHeader2 = FontManager.Find(Font.Name.ScoreHeader2);

            pGameManager.pActivePlayer.lives--;
            //Debug.WriteLine("Number of lives remaining is {0}", pGameManager.pActivePlayer.lives);
            
            if (pGameManager.pActivePlayer.name == PlayerArtifact.Name.PlayerOne && pGameManager.poPlayer2.lives > 0)
            {
                pScoreHeader1.UpdateMessage(" SCORE<1> ");
                pScoreHeader2.UpdateMessage("*SCORE<2>*");
                LivesManager.DisplayLives(pGameManager.poPlayer2.lives);
                
                //Debug.WriteLine("SWAPPING TO PLAYER 2!");

                //Debug.WriteLine("Storing P1 Managers to Manager Mementos");
                pGameManager.poPlayer1.ArchiveManagerStates(pGameManager.pGame.GetTime());

                //Debug.WriteLine("Restoring P2 Managers from Manager Mementos");
                pGameManager.poPlayer2.RestoreManagerStates(pGameManager.pGame.GetTime());

                pGameManager.SetActivePlayer(PlayerArtifact.Name.PlayerTwo);

            }
            else if (pGameManager.pActivePlayer.name == PlayerArtifact.Name.PlayerTwo && pGameManager.poPlayer1.lives > 0)
            {
                pScoreHeader1.UpdateMessage("*SCORE<1>*");
                pScoreHeader2.UpdateMessage(" SCORE<2> ");
                LivesManager.DisplayLives(pGameManager.poPlayer1.lives);

                //Debug.WriteLine("SWAPPING TO PLAYER 1!");

                //Debug.WriteLine("Storing P2 Managers to Manager Mementos");
                pGameManager.poPlayer2.ArchiveManagerStates(pGameManager.pGame.GetTime());

                //Debug.WriteLine("Restoring P1 Managers from Manager Mementos");
                pGameManager.poPlayer1.RestoreManagerStates(pGameManager.pGame.GetTime());

                pGameManager.SetActivePlayer(PlayerArtifact.Name.PlayerOne);

            }
            else
            {
                LivesManager.DisplayLives(pGameManager.poPlayer1.lives);
                Debug.WriteLine("GAME OVER. YOU LOSE!");
                GameManager.CleanUp();
            }
        }

        public override void InitializeLevel(GameManager pGameManager)
        {
            Font pCredits = FontManager.Find(Font.Name.Credits);
            pCredits.UpdateMessage("CREDITS  02");

            PlayerOneInit(pGameManager);
            PlayerTwoInit(pGameManager);
        }

        public override void ReinitializeLevel(GameManager pGameManager)
        {
            SoundManager.StopAllSounds();

            TimerManager.PurgeAllNodes();
            GameObjectManager.PurgeAllNodes();
            SpriteBatch pSBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pSBatch.GetSBNodeManager().PurgeAllNodes();
            pSBatch = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            pSBatch.GetSBNodeManager().PurgeAllNodes();
            ColPairManager.PurgeAllNodes();
            DelayedObjectManager.PurgeAll();

            if (pGameManager.pActivePlayer.name == PlayerArtifact.Name.PlayerOne)
            {
                PlayerOneInit(pGameManager);
                pGameManager.poPlayer1.RestoreManagerStates(pGameManager.pGame.GetTime());
                pGameManager.SetActivePlayer(PlayerArtifact.Name.PlayerOne);
            }
            else
            {
                PlayerTwoInit(pGameManager);
                pGameManager.poPlayer2.RestoreManagerStates(pGameManager.pGame.GetTime());
                pGameManager.SetActivePlayer(PlayerArtifact.Name.PlayerTwo);
            }
        }
    }
}

