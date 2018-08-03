using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColObject
    {
        public ProxyBoxSprite pColSprite;
        public ColRect poColRect;
        public bool enabled;

        public ColObject(ProxySprite pProxySprite, BoxSprite.Name boxSpriteName)
        {
            Debug.Assert(pProxySprite != null);

            Sprite pSprite = pProxySprite.pSprite;
            Debug.Assert(pSprite != null);

            // Origin is in the UPPER RIGHT 
            this.poColRect = new ColRect(pSprite.GetScreenRect());
            Debug.Assert(this.poColRect != null);

            this.pColSprite = ProxyBoxSpriteManager.Add(boxSpriteName);
            Debug.Assert(this.pColSprite != null);

            this.enabled = true;
        }

        public void UpdatePos(float x, float y)
        {
            this.poColRect.x = x;
            this.poColRect.y = y;

            this.pColSprite.SetScreenRect(this.poColRect.x, this.poColRect.y, this.poColRect.width, this.poColRect.height);
            this.pColSprite.Update();
        }
    }
}

