using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pBoxSpriteBatch;
        InvaderGrid pInvaderGrid;
        WallManager pWallMan;

        public enum Type
        {
            Plain,
            ZigZag,
            Dagger,
            Rolling
        }

        public BombFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName, InvaderGrid pInvaderGrid, WallManager pWallMan)
        {
            Debug.Assert(pInvaderGrid != null);
            this.pInvaderGrid = pInvaderGrid;

            Debug.Assert(pWallMan != null);
            this.pWallMan = pWallMan;

            this.pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchManager.Find(boxSpriteBatchName);
            Debug.Assert(this.pBoxSpriteBatch != null);
        }

        public Bomb Create(Type type)
        {
            InvaderCategory pInvader = InvaderGridManager.GetRandomBombDropper(this.pInvaderGrid);
            Bomb pBomb = null;
            if (pInvader != null)
            {
                float posX = pInvader.x;
                float posY = pInvader.y;

                switch (type)
                {
                    case Type.Plain:
                        pBomb = new Bomb(GameObject.Name.Bomb, Sprite.Name.BombPlain, BoxSprite.Name.BombBox, posX, posY);
                        break;

                    case Type.ZigZag:
                        pBomb = new Bomb(GameObject.Name.Bomb, Sprite.Name.BombZigZag, BoxSprite.Name.BombBox, posX, posY);
                        break;

                    case Type.Dagger:
                        pBomb = new Bomb(GameObject.Name.Bomb, Sprite.Name.BombDagger, BoxSprite.Name.BombBox, posX, posY);
                        break;

                    case Type.Rolling:
                        pBomb = new Bomb(GameObject.Name.Bomb, Sprite.Name.BombRolling, BoxSprite.Name.BombBox, posX, posY);
                        break;
                    default:
                        // something is wrong
                        Debug.Assert(false, "Bomb type not supported by this factory");
                        break;
                }

                // set pointer back to invader who dropped the bomb
                pBomb.pInvaderWhoDroppedMe = pInvader;
                pBomb.pInvaderWhoDroppedMe.canLaunchBomb = false; //block this invader from dropping another bomb until curren bomb dies

                // add it to the gameObjectManager
                Debug.Assert(pBomb != null);
                GameObjectManager.Attach(pBomb);

                // Attached to Batches           
                this.pBoxSpriteBatch.Attach(pBomb.poColObj.pColSprite);
                this.pSpriteBatch.Attach(pBomb.pProxySprite);

                // Add Collision Pairs and Observers
                ColPair pMissile_BombColPair = ColPairManager.Add(ColPair.Name.Missile_Bomb, pBomb, ShipManager.GetMissile());
                Debug.Assert(pMissile_BombColPair != null);

                ColPair pBomb_FloorColPair = ColPairManager.Add(ColPair.Name.Bomb_Floor, pBomb, pWallMan.GetFloor());
                Debug.Assert(pBomb_FloorColPair != null);

                ColPair pBomb_ShipColPair = ColPairManager.Add(ColPair.Name.Bomb_Ship, pBomb, ShipManager.GetShip());
                Debug.Assert(pBomb_ShipColPair != null);

                // Observers for Bomb vs Missile
                pMissile_BombColPair.Attach(new RemoveCollisionPairObserver(pMissile_BombColPair));
                pMissile_BombColPair.Attach(new RemoveCollisionPairObserver(pBomb_FloorColPair));
                pMissile_BombColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShipColPair));
                pMissile_BombColPair.Attach(new ShipMissileReadyObserver());
                pMissile_BombColPair.Attach(new ShipRemoveMissileObserver());
                pMissile_BombColPair.Attach(new RemoveBombObserver());

                //// Observers for Bomb vs Floor
                pBomb_FloorColPair.Attach(new RemoveCollisionPairObserver(pBomb_FloorColPair));
                pBomb_FloorColPair.Attach(new RemoveCollisionPairObserver(pMissile_BombColPair));
                pBomb_FloorColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShipColPair));
                pBomb_FloorColPair.Attach(new RemoveBombObserver());

                // Observers for Bomb vs Ship
                pBomb_ShipColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShipColPair));
                pBomb_ShipColPair.Attach(new RemoveCollisionPairObserver(pBomb_FloorColPair));
                pBomb_ShipColPair.Attach(new RemoveCollisionPairObserver(pMissile_BombColPair));
                pBomb_ShipColPair.Attach(new RemoveShipObserver());
                pBomb_ShipColPair.Attach(new RemoveBombObserver());


                GameObject pShieldZone = GameObjectManager.UnsafeFind(GameObject.Name.ShieldZone);
                if (pShieldZone != null)
                {
                    // Add Collision pair for Bomb vs Shields
                    ColPair pBomb_ShieldColPair = ColPairManager.Add(ColPair.Name.Bomb_Shield, pBomb, pShieldZone);
                    Debug.Assert(pBomb_ShieldColPair != null);

                    // Added observers to previous shield pairs to remove the bomb vs shield col pair
                    pMissile_BombColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShieldColPair));
                    pBomb_FloorColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShieldColPair));
                    pBomb_ShipColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShieldColPair));

                    // Observers for Bomb vs Shield
                    pBomb_ShieldColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShieldColPair));
                    pBomb_ShieldColPair.Attach(new RemoveCollisionPairObserver(pBomb_FloorColPair));
                    pBomb_ShieldColPair.Attach(new RemoveCollisionPairObserver(pMissile_BombColPair));
                    pBomb_ShieldColPair.Attach(new RemoveCollisionPairObserver(pBomb_ShipColPair));
                    pBomb_ShieldColPair.Attach(new RemoveBombObserver());
                    pBomb_ShieldColPair.Attach(new RemoveShieldBrickObserver());
                }
            }

            return pBomb;
        }
    }
}
