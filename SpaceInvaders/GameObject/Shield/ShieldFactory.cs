using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pBoxSpriteBatch;
        private GameObjectComposite pGOComposite;

        public enum Type
        {
            Brick,
            Column,
            Shield,
            Zone
        }

        public ShieldFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName, GameObjectComposite pGOComposite = null)
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


        public GameObject Create(Type type, float posX, float posY)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case Type.Brick:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, Sprite.Name.ShieldBrick, BoxSprite.Name.ShieldBrickBox, posX, posY);
                    break;
                case Type.Column:
                    pGameObj = new ShieldColumn(GameObject.Name.ShieldColumn, BoxSprite.Name.ShieldColumnBox);
                    break;

                case Type.Shield:
                    pGameObj = new Shield(GameObject.Name.Shield, BoxSprite.Name.ShieldBox);
                    break;
                case Type.Zone:
                    pGameObj = new ShieldZone(GameObject.Name.ShieldZone, BoxSprite.Name.ShieldZoneBox);
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
                // add to grouping
                pGOComposite.Add(pGameObj);
            }

            // Attached to Batches           
            this.pBoxSpriteBatch.Attach(pGameObj.poColObj.pColSprite);
            this.pSpriteBatch.Attach(pGameObj.pProxySprite);
            return pGameObj;
        }
    }
}
