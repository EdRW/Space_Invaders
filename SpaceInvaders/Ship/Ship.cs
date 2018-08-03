using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Ship : GameObjectLeaf
    {
        private ShipMotionState pMotionState;
        private ShipShootState pShootState;

        public Ship(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY)
            : base(name, spriteName, boxSpriteName)
        {
            this.x = posX;
            this.y = posY;

            this.speedX = 5.0f;
            this.pMotionState = null;
            this.pShootState = null;
        }

        public void MoveRight()
        {
            this.pMotionState.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.pMotionState.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.pShootState.ShootMissile(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public void SetMotionState(ShipManager.MotionState inState)
        {
            this.pMotionState = ShipManager.GetMotionState(inState);
        }

        public void SetShootState(ShipManager.ShootState inState)
        {
            this.pShootState = ShipManager.GetShootState(inState);
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitShip(this);
        }

        public override void VisitWallLeft(WallLeft pWallLeft)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(pWallLeft, this);
            pColPair.NotifyListeners();
        }

        public override void VisitWallRight(WallRight pWallRight)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(pWallRight, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb pBomb)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(pBomb, this);
            pColPair.NotifyListeners();
        }

        public override void VisitInvaderGrid(InvaderGrid pGrid)
        {
            GameObject pGameObj = (GameObject)pGrid.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderColumn(InvaderColumn pColumn)
        {
            GameObject pGameObj = (GameObject)pColumn.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderCategory(InvaderCategory pInvader)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(this, pInvader);
            pColPair.NotifyListeners();
        }
    }
}
