using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerArtifact
    {
        public Name name;
        public int lives;
        public int score;
        public int level;
        public InvaderGrid pGrid;
        public ShieldZone pShieldZone;

        public TimerMemento pTimerMemento;
        public ManagerMemento pGameObjectMemento;
        public ManagerMemento pColPairMemento;
        public ManagerMemento pSpritesSBNodeMemento;
        public ManagerMemento pSpriteBoxesSBNodeMemento;

        public enum Name
        {
            PlayerOne,
            PlayerTwo
        }

        public PlayerArtifact(Name name)
        {
            this.name = name;
            this.lives = 3;
            this.score = 0;
            this.level = 1;
            this.pGrid = null;
            this.pShieldZone = null;

            this.pTimerMemento = new TimerMemento();
            this.pGameObjectMemento = new ManagerMemento();
            this.pColPairMemento = new ManagerMemento();
            this.pSpritesSBNodeMemento = new ManagerMemento();
            this.pSpriteBoxesSBNodeMemento = new ManagerMemento();
        }

        public void Reset()
        {
            this.lives = 3;
            this.score = 0;
            this.pGrid = null;
            this.pShieldZone = null;
        }

        public void ArchiveManagerStates(float currTime)
        {
            TimerManager.PushToMemento(this.pTimerMemento, currTime);
            GameObjectManager.PushToMemento(this.pGameObjectMemento);
            ColPairManager.PushToMemento(this.pColPairMemento);

            SpriteBatch pSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            pSpriteBatch.GetSBNodeManager().PushToMemento(this.pSpritesSBNodeMemento);
            pSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pSpriteBatch.GetSBNodeManager().PushToMemento(this.pSpriteBoxesSBNodeMemento);
        }

        public void RestoreManagerStates(float currTime)
        {
            TimerManager.PullFromMemento(this.pTimerMemento, currTime);
            GameObjectManager.PullFromMemento(this.pGameObjectMemento);
            ColPairManager.PullFromMemento(this.pColPairMemento);

            SpriteBatch pSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            pSpriteBatch.GetSBNodeManager().PullFromMemento(this.pSpritesSBNodeMemento);
            pSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pSpriteBatch.GetSBNodeManager().PullFromMemento(this.pSpriteBoxesSBNodeMemento);
        }
    }
}
