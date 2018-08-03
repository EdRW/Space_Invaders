using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MediumInvader : InvaderCategory
    {
        public MediumInvader(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName, float posX, float posY)
            : base(name, spriteName, boxSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.value = 20;
        }

        public override void Update()
        {
            base.Update();
        }

    }
}
