using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipManager
    {
        private static ShipManager pInstance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;

        // Motion State References
        private FreeMotionState pFreeMotionState;
        private LeftMotionOnlyState pLeftMotionOnlyState;
        private RightMotionOnlyState pRightMotionOnlyState;
        private MotionLockedState pMotionLockedState;

        // Shoot State References
        private MissleReadyState pMissileReadyState;
        private MissleFlyingState pMissileFlyingState;
        private MissileLockedState pMissileLockedState;



        public enum MotionState
        {
            Free,
            LeftOnly,
            RightOnly,
            Locked
        }

        public enum ShootState
        {
            MissileReady,
            MissileFlying,
            Locked
        }

        private ShipManager()
        {
            // Store the states
            this.pFreeMotionState = new FreeMotionState();
            this.pLeftMotionOnlyState = new LeftMotionOnlyState();
            this.pRightMotionOnlyState = new RightMotionOnlyState();
            this.pMotionLockedState = new MotionLockedState();

            this.pMissileReadyState = new MissleReadyState();
            this.pMissileFlyingState = new MissleFlyingState();
            this.pMissileLockedState = new MissileLockedState();

            // set active
            this.pShip = new Ship(GameObject.Name.Ship, Sprite.Name.Ship, BoxSprite.Name.ShipBox, Constants.shipXPos, Constants.shipYPos);
            Debug.Assert(this.pShip != null);

            this.pMissile = new Missile(GameObject.Name.Missle, Sprite.Name.Missile, BoxSprite.Name.MissileBox, 0, 0);
            Debug.Assert(this.pMissile != null);
            this.pMissile.GetColObject().enabled = false;
        }

        public static void Create()
        {
            // make sure its the first time
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ShipManager();
            }

            Debug.Assert(pInstance != null);

            // Stuff to initialize after the instance was created
            // ActivateShip();
            pInstance.pShip.SetMotionState(ShipManager.MotionState.Free);
            pInstance.pShip.SetShootState(ShipManager.ShootState.MissileReady);
        }

        public static void Purge()
        {
            pInstance = null;
        }

        public static Ship ActivateShip()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetPos(Constants.shipXPos, Constants.shipYPos);

            // Attach to Game Object Manager
            GameObjectManager.Attach(pShipMan.pShip);

            // Attach the sprite to the correct sprite batch
            SpriteBatch pInvaders = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            pInvaders.Attach(pShipMan.pShip.pProxySprite);

            // Attach the spriteBox to the correct sprite batch
            SpriteBatch pBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pBoxes.Attach(pShipMan.pShip.poColObj.pColSprite);

            return pShipMan.pShip;
        }

        // TODO Add a method to reset set pos that takes xy coords

        public static Missile ActivateMissile()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pMissile.SetPos(pShipMan.pShip.x, pShipMan.pShip.y + 20);

            // Attach to Game Object Manager
            GameObjectManager.Attach(pShipMan.pMissile);

            // Attach the sprite to the correct sprite batch
            SpriteBatch pInvaders = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            pInvaders.Attach(pShipMan.pMissile.pProxySprite);

            // Attach the spriteBox to the correct sprite batch
            SpriteBatch pBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pBoxes.Attach(pShipMan.pMissile.poColObj.pColSprite);

            pShipMan.pMissile.GetColObject().enabled = true;
            pShipMan.pMissile.bMarkForDeath = false;

            return pShipMan.pMissile;
        }

        public static void DeactivateMissile()
        {
            SpriteBatch pSB_Sprites = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            //pSB_Sprites.GetSBNodeManager().PrintReport();
            //pSB_Boxes.GetSBNodeManager().PrintReport();
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);
            pShipMan.pMissile.GetColObject().enabled = false;
            pShipMan.pMissile.Remove();
            //Debug.WriteLine("Missile has been deactivated");


            //pSB_Sprites.GetSBNodeManager().PrintReport();
            //pSB_Boxes.GetSBNodeManager().PrintReport();
        }

        public static void ResetShip()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);
            pShipMan.pShip.SetPos(Constants.shipXPos, Constants.shipYPos);
            pInstance.pShip.SetMotionState(ShipManager.MotionState.Free);
            pShipMan.pShip.bMarkForDeath = false;

            if (pShipMan.pMissile.GetColObject().enabled == true)
            {
                ShipManager.DeactivateMissile();
                pShipMan.pShip.SetShootState(ShipManager.ShootState.MissileReady);
            }
        }

        public static Ship GetShip()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static Missile GetMissile()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static ShipMotionState GetMotionState(MotionState state)
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            ShipMotionState pShipMotionState = null;

            switch (state)
            {
                case ShipManager.MotionState.Free:
                    pShipMotionState = pShipMan.pFreeMotionState;
                    break;
                case ShipManager.MotionState.LeftOnly:
                    pShipMotionState = pShipMan.pLeftMotionOnlyState;
                    break;
                case ShipManager.MotionState.RightOnly:
                    pShipMotionState = pShipMan.pRightMotionOnlyState;
                    break;
                case ShipManager.MotionState.Locked:
                    pShipMotionState = pShipMan.pMotionLockedState;
                    break;
            }

            return pShipMotionState;
        }

        public static ShipShootState GetShootState(ShootState state)
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            ShipShootState pShipShootState = null;

            switch (state)
            {
                case ShipManager.ShootState.MissileReady:
                    pShipShootState = pShipMan.pMissileReadyState;
                    break;
                case ShipManager.ShootState.MissileFlying:
                    pShipShootState = pShipMan.pMissileFlyingState;
                    break;
                case ShipManager.ShootState.Locked:
                    pShipShootState = pShipMan.pMissileLockedState;
                    break;
            }

            return pShipShootState;
        }

        private static ShipManager PrivInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
