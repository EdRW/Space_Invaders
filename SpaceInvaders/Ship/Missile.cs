using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : GameObjectLeaf
    {
        public Missile(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY)
            : base(name, spriteName, boxSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.speedY = 10.0f;
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

        public override void Accept(ColVisitor other)
        {
            //Debug.WriteLine("In Missile Accepts {0}({1})", ((GameObject)other).name, other.GetHashCode());
            other.VisitMissile(this);
        }

        public override void VisitCeiling(Ceiling pCeiling)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            
            pColPair.SetCollision(this, pCeiling);
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
            // Always set missile as ObjA
            pColPair.SetCollision(this, pInvader);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb pBomb)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set missile as ObjA
            // Always set Bomb as ObjB
            pColPair.SetCollision(this, pBomb);
            pColPair.NotifyListeners();
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set missile as ObjA
            // Always set Bomb as ObjB
            pColPair.SetCollision(this, pMissile);
            pColPair.NotifyListeners();
        }

        public override void VisitShieldZone(ShieldZone pShieldZone)
        {
            //Debug.WriteLine("in Missile, visit from ShieldZone");
            GameObject pGameObj = (GameObject)pShieldZone.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShield(Shield pShield)
        {
            //Debug.WriteLine("in Missile, visit from shield");
            GameObject pGameObj = (GameObject)pShield.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            //Debug.WriteLine("in Missile, visit from shieldColumn");
            GameObject pGameObj = (GameObject)pShieldColumn.GetLastChild();
            ColPair.RevCollide(this, pGameObj);
        }

        public override void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            //Debug.WriteLine("in Missile   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set missile as ObjA
            pColPair.SetCollision(this, pShieldBrick);
            pColPair.NotifyListeners();
        }
    }
}
