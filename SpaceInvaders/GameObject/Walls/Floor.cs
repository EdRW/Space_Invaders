using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Floor : GameObjectLeaf
    {
        public Floor(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, boxSpriteName)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitFloor(this);
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

    }
}