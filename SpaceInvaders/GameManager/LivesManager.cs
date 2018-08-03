using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class LivesManager
    {
        private static LivesManager pInstance;

        private int numLives;
        private ProxySprite pShip1;
        private ProxySprite pShip2;
        private ProxySprite pShip3;

        private LivesManager()
        {
            this.numLives = 0;
            this.pShip1 = new ProxySprite(Sprite.Name.Ship);
            this.pShip1.x = 100;
            this.pShip1.y = 100;

            this.pShip2 = new ProxySprite(Sprite.Name.Ship);
            this.pShip2.x = 150;
            this.pShip2.y = 100;

            this.pShip3 = new ProxySprite(Sprite.Name.Ship);
            this.pShip3.x = 200;
            this.pShip3.y = 100;
        }

        public static void DisplayLives(int numLives)
        {
            ClearLives();

            Debug.Assert(numLives >= 0 && numLives <= 3);
            SpriteBatch pSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.Texts);
            Debug.Assert(pSpriteBatch != null);

            LivesManager pMan = PrivGetInstance();
            if (numLives > 0)
            {
                pSpriteBatch.Attach(pMan.pShip1);
            }
            if (numLives > 1)
            {
                pSpriteBatch.Attach(pMan.pShip2);
            }
            if (numLives > 2)
            {
                pSpriteBatch.Attach(pMan.pShip3);
            }

            pMan.numLives = numLives;

            Font pLives = FontManager.Find(Font.Name.Lives);
            pLives.UpdateMessage(numLives.ToString());
        }

        private static void ClearLives()
        {
            LivesManager pMan = PrivGetInstance();

            if (pMan.numLives > 0) 
            {
                SpriteBatchManager.Remove(pMan.pShip1.GetSBNode());
            }
            if (pMan.numLives > 1)
            {
                SpriteBatchManager.Remove(pMan.pShip2.GetSBNode());
            }
            if (pMan.numLives > 2)
            {
                SpriteBatchManager.Remove(pMan.pShip3.GetSBNode());
            }

            pMan.numLives = 0;
        }

        private static LivesManager PrivGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new LivesManager();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
