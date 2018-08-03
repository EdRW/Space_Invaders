using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class ProxySprite_Link : SpriteBase
    {

    }
    public class ProxySprite : ProxySprite_Link
    {
        public ProxySprite.Name name;
        public Sprite pSprite;

        public enum Name
        {
            Proxy,
            Uninitialized
        }

        public ProxySprite()
            : base()
        {
            this.name = ProxySprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pSprite = null;
        }

        public ProxySprite(Sprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pSprite = SpriteManager.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public void Set(Sprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pSprite = SpriteManager.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.PrivPushToReal();
            this.pSprite.Update();
        }

        public override void Render()
        {
            // move the values over to Real GameSprite
            this.PrivPushToReal();

            // update and draw real sprite 
            // Seems redundant - Real Sprite might be stale
            this.pSprite.Update();
            this.pSprite.Render();
        }

        public void SetName(Name inName)
        {
            this.name = inName;
        }

        public Name GetName()
        {
            return this.name;
        }

        public override void Wash()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.name = Name.Uninitialized;
            this.pSprite = null;
        }

        private void PrivPushToReal()
        {
            // push the data from proxy to Real GameSprite
            Debug.Assert(this.pSprite != null);

            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
        }

        public override string ToString()
        {
            String spriteName = (pSprite == null) ? "null" : pSprite.name.ToString();
            return "[ " + name + " (" + this.GetHashCode() + ") x = " + x + " y = " + y + " Sprite: " + spriteName + " ]";
        }
    }
}
