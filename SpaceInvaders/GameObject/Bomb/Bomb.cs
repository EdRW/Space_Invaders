using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : GameObjectLeaf
    {
        public InvaderCategory pInvaderWhoDroppedMe;

        public Bomb(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY)
            : base(name, spriteName, boxSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.speedY = -5.0f;
            this.pInvaderWhoDroppedMe = null;
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public override void Update()
        {
            base.Move();
            base.Update();
        }

        public override void Remove()
        {
            this.pInvaderWhoDroppedMe = null;
            base.Remove();
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitBomb(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set missile as ObjA
            // Always set Bomb as ObjB
            pColPair.SetCollision(pMissile, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShip(Ship pShip)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set Bomb as ObjB
            pColPair.SetCollision(pShip, this);
            pColPair.NotifyListeners();
        }

        public override void VisitFloor(Floor pFloor)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set Bomb as ObjB
            pColPair.SetCollision(pFloor, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShieldZone(ShieldZone pShieldZone)
        {
            //Debug.WriteLine("in Bomb, visit from ShieldZone");
            GameObject pGameObj = (GameObject)pShieldZone.GetFirstChild();
            // Always set Bomb as ObjB
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShield(Shield pShield)
        {
            //Debug.WriteLine("in Bomb, visit from shield");
            GameObject pGameObj = (GameObject)pShield.GetFirstChild();
            // Always set Bomb as ObjB
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            //Debug.WriteLine("in Bomb, visit from shieldColumn");
            GameObject pGameObj = (GameObject)pShieldColumn.GetFirstChild();
            // Always set Bomb as ObjB
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            //Debug.WriteLine("in Bomb   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set Bomb as ObjB
            pColPair.SetCollision(this, pShieldBrick);
            pColPair.NotifyListeners();
        }
    }
}
