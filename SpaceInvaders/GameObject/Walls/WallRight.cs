using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRight : GameObjectLeaf
    {
        public WallRight(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, boxSpriteName)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;
        }
        public override void Accept(ColVisitor other)
        {
            other.VisitWallRight(this);
        }

        public override void VisitInvaderGrid(InvaderGrid pGrid)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set grid as ObjB
            pColPair.SetCollision(this, pGrid);
            pColPair.NotifyListeners();
        }

        public override void VisitShip(Ship pShip)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set ship as ObjB
            pColPair.SetCollision(this, pShip);
            pColPair.NotifyListeners();
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
