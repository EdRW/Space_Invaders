using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO : InvaderCategory
    {
        public UFO(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY)
            : base(name, spriteName, boxSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.speedX = 2;
            this.value = 300;
        }

        public override void Update()
        {
            base.Move();
            base.Update();
        }


        public override void VisitWallLeft(WallLeft pWallLeft)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(this, pWallLeft);
            pColPair.NotifyListeners();
        }

        public override void VisitWallRight(WallRight pWallRight)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(this, pWallRight);
            pColPair.NotifyListeners();
        }


    }
}
