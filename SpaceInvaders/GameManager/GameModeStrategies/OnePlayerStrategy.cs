using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class OnePlayerStrategy : GameModeStrategy
    {

        public override void EndPlayerTurn(GameManager pGameManager)
        {
            Debug.Assert(pGameManager.pActivePlayer.name == PlayerArtifact.Name.PlayerOne);
            pGameManager.pActivePlayer.lives--;
            //Debug.WriteLine("Number of lives remaining is {0}", pGameManager.pActivePlayer.lives);
            LivesManager.DisplayLives(pGameManager.poPlayer1.lives);
            if (pGameManager.pActivePlayer.lives > 0)
            {
                //Debug.WriteLine("RESETTING SHIP!");
                ShipManager.ResetShip();
            }
            else
            {
                SoundManager.StopAllSounds();
                //Debug.WriteLine("GAME OVER. YOU LOSE!");
                GameManager.CleanUp();
            }
        }
        public override void InitializeLevel(GameManager pGameManager)
        {
            Font pCredits = FontManager.Find(Font.Name.Credits);
            pCredits.UpdateMessage("CREDITS  01");

            PlayerOneInit(pGameManager);
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

            PlayerOneInit(pGameManager);
            pGameManager.poPlayer1.RestoreManagerStates(pGameManager.pGame.GetTime());
            pGameManager.SetActivePlayer(PlayerArtifact.Name.PlayerOne);
        }
    }
}
