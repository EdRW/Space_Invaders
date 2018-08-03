using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pBoxSpriteBatch;
        private GameObjectComposite pGOComposite;

        public WallFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName, GameObjectComposite pGOComposite = null)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchManager.Find(boxSpriteBatchName);
            Debug.Assert(this.pBoxSpriteBatch != null);

            if (pGOComposite != null)
            {
                this.pGOComposite = pGOComposite;
            }
        }


        public GameObject Create(GameObject.Type type, float posX, float posY, float width, float height)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case GameObject.Type.GameSpace:
                    pGameObj = new GameSpace(GameObject.Name.GameSpace, BoxSprite.Name.GameSpaceBox);
                    break;

                case GameObject.Type.WallRight:
                    pGameObj = new WallRight(GameObject.Name.WallRight, Sprite.Name.NullObject, BoxSprite.Name.WallBox, posX, posY, width, height);
                    break;

                case GameObject.Type.WallLeft:
                    pGameObj = new WallLeft(GameObject.Name.WallLeft, Sprite.Name.NullObject, BoxSprite.Name.WallBox, posX, posY, width, height);
                    break;

                case GameObject.Type.Ceiling:
                    pGameObj = new Ceiling(GameObject.Name.Ceiling, Sprite.Name.NullObject, BoxSprite.Name.WallBox, posX, posY, width, height);
                    break;

                case GameObject.Type.Floor:
                    pGameObj = new Floor(GameObject.Name.Floor, Sprite.Name.NullObject, BoxSprite.Name.WallBox, posX, posY, width, height);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false, "GameObject type not supported by this factory");
                    break;
            }

            // add it to the gameObjectManager
            Debug.Assert(pGameObj != null);
            GameObjectManager.Attach(pGameObj);

            if (pGOComposite != null)
            {
                // add to grid
                pGOComposite.Add(pGameObj);
            }

            // Attached to Batches           
            this.pBoxSpriteBatch.Attach(pGameObj.poColObj.pColSprite);
            this.pSpriteBatch.Attach(pGameObj.pProxySprite);
            return pGameObj;
        }
    }
}
