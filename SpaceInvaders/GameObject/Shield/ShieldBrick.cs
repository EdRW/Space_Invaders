using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : GameObjectLeaf
    {
        public ShieldBrick(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY)
            : base(name, spriteName, boxSpriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        public override void Accept(ColVisitor other)
        {
            //Debug.WriteLine("ShieldBrick Accepts");
            other.VisitShieldBrick(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("in brick   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set missile as ObjA
            pColPair.SetCollision(pMissile, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb pBomb)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set Bomb as ObjB
            pColPair.SetCollision(this, pBomb);
            pColPair.NotifyListeners();
        }

        public override void VisitInvaderGrid(InvaderGrid pGrid)
        {
            //Debug.WriteLine("in ShieldBrick, visit from InvaderGrid");
            GameObject pGameObj = (GameObject)pGrid.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderColumn(InvaderColumn pColumn)
        {
            //Debug.WriteLine("in ShieldBrick, visit from InvaderColumn");
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