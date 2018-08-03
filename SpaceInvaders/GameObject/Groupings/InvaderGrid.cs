using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InvaderGrid : GameObjectComposite
    {
        private GridState state;
        public readonly float roStaticSpeedX = 10.0f;
        public readonly float roStaticSpeedY = -30.0f;
        public readonly float speedUpMultiplier = 0.97f;


        public InvaderGrid(GameObject.Name name, BoxSprite.Name boxName)
            : base(name, boxName)
        {
            this.speedX = 10;
        }

        public override void Move()
        {
            this.state.Move(this);
            base.Move();
        }

        public override void Remove()
        {
            GameManager.LevelUp();
            base.Remove();
        }

        public void SetState(InvaderGridManager.State inState)
        {
            this.state = InvaderGridManager.GetState(inState);
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitInvaderGrid(this);
        }

        public override void VisitWallLeft(WallLeft pWallLeft)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set Grid as ObjB
            pColPair.SetCollision(pWallLeft, this);
            pColPair.NotifyListeners();
        }

        public override void VisitWallRight(WallRight pWallRight)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set Grid as ObjB
            pColPair.SetCollision(pWallRight, this);
            pColPair.NotifyListeners();
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("in Grid , visit from Missile");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pMissile);
        }

        public override void VisitShip(Ship pShip)
        {
            //Debug.WriteLine("in Grid , visit from Ship");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pShip);
        }

        public override void VisitShieldZone(ShieldZone pShieldZone)
        {
            //Debug.WriteLine("in Grid , visit from ShieldZone");
            GameObject pGameObj = (GameObject)pShieldZone.GetFirstChild();
            
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShield(Shield pShield)
        {
            //Debug.WriteLine("in Grid, visit from shield");
            GameObject pGameObj = (GameObject)pShield.GetFirstChild();
            
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            //Debug.WriteLine("in Grid, visit from shieldColumn");
            GameObject pGameObj = (GameObject)pShieldColumn.GetFirstChild();
            
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            //Debug.WriteLine("in Grid, visit from ShieldBrick");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pShieldBrick);
        }
    }
}