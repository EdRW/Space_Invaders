using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InvaderFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pBoxSpriteBatch;
        private GameObjectComposite pGOComposite;

        // TODO Move Types out of gameobject and into here

        public InvaderFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName, GameObjectComposite pGOComposite = null)
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

        // Creates invader but does not add it to Batches or gameobject manager
        public GameObject PassiveCreate(GameObject.Type type, float posX, float posY)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case GameObject.Type.SmallInvader:
                    pGameObj = new SmallInvader(GameObject.Name.SmallInvader, Sprite.Name.SmallInvader, BoxSprite.Name.SmallInvaderBox, posX, posY);
                    break;

                case GameObject.Type.MediumInvader:
                    pGameObj = new MediumInvader(GameObject.Name.MediumInvader, Sprite.Name.MediumInvader, BoxSprite.Name.MediumInvaderBox, posX, posY);
                    break;

                case GameObject.Type.LargeInvader:
                    pGameObj = new LargeInvader(GameObject.Name.LargeInvader, Sprite.Name.LargeInvader, BoxSprite.Name.LargeInvaderBox, posX, posY);           
                    break;

                case GameObject.Type.UFO:
                    pGameObj = new UFO(GameObject.Name.UFO, Sprite.Name.UFO, BoxSprite.Name.UFOBox, posX, posY);
                    break;

                case GameObject.Type.InvaderColumn:
                    pGameObj = new InvaderColumn(GameObject.Name.InvaderColumn, BoxSprite.Name.InvaderColumnBox);
                    break;

                case GameObject.Type.InvaderGrid:
                    pGameObj = new InvaderGrid(GameObject.Name.InvaderGrid, BoxSprite.Name.InvaderGridBox);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false, "GameObject type not supported by this factory"); 
                    break;
            }

            if(pGOComposite != null)
            {
                // add to grouping
                pGOComposite.Add(pGameObj);
            }

            return pGameObj;
        }

        // Creates invader And adds it to Batches or gameobject manager
        public GameObject ActiveCreate(GameObject.Type type, float posX, float posY)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case GameObject.Type.SmallInvader:
                    pGameObj = new SmallInvader(GameObject.Name.SmallInvader, Sprite.Name.SmallInvader, BoxSprite.Name.SmallInvaderBox, posX, posY);
                    break;

                case GameObject.Type.MediumInvader:
                    pGameObj = new MediumInvader(GameObject.Name.MediumInvader, Sprite.Name.MediumInvader, BoxSprite.Name.MediumInvaderBox, posX, posY);
                    break;

                case GameObject.Type.LargeInvader:
                    pGameObj = new LargeInvader(GameObject.Name.LargeInvader, Sprite.Name.LargeInvader, BoxSprite.Name.LargeInvaderBox, posX, posY);
                    break;

                case GameObject.Type.UFO:
                    pGameObj = new UFO(GameObject.Name.UFO, Sprite.Name.UFO, BoxSprite.Name.UFOBox, posX, posY);
                    break;

                case GameObject.Type.InvaderColumn:
                    pGameObj = new InvaderColumn(GameObject.Name.InvaderColumn, BoxSprite.Name.InvaderColumnBox);
                    break;

                case GameObject.Type.InvaderGrid:
                    pGameObj = new InvaderGrid(GameObject.Name.InvaderGrid, BoxSprite.Name.InvaderGridBox);
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
