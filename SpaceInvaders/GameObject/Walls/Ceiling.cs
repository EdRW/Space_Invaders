using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Ceiling : GameObjectLeaf
    {
        public Ceiling(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, boxSpriteName)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitCeiling(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            // Always set missile as ObjA
            pColPair.SetCollision(pMissile, this);
            pColPair.NotifyListeners();
        }
    }
}
