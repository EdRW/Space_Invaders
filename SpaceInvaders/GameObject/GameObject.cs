using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : ColVisitor
    {
        public GameObject.Name name;

        public float x;
        public float y;
        public ProxySprite pProxySprite;
        public float speedX;
        public float speedY;
        public ColObject poColObj;
        public bool bMarkForDeath;

        // TODO Move these into their Individual Factories
        public enum Type
        {
            SmallInvader,
            MediumInvader,
            LargeInvader,
            UFO,
            InvaderColumn,
            InvaderGrid,

            GameSpace,
            WallRight,
            WallLeft,
            Ceiling,
            Floor
        }

        public enum Name
        {
            Ship,
            Missle,

            SmallInvader,
            MediumInvader,
            LargeInvader,
            UFO,
            InvaderColumn,
            InvaderGrid,

            GameSpace,
            WallRight,
            WallLeft,
            Ceiling,
            Floor,

            Bomb,

            ShieldBrick,
            ShieldColumn,
            Shield,
            ShieldZone,

            Null_Object,
            Uninitialized
        }

        
        protected GameObject(GameObject.Name name)
        {
            this.name = name;
            this.x = 0.0f;
            this.y = 0.0f;
            
            this.pProxySprite = new ProxySprite(Sprite.Name.Uninitialized); //creates an uninit proxy
            Debug.Assert(this.pProxySprite != null);
            
           
            this.speedX = 0;
            this.speedY = 0;
            this.pProxySprite = new ProxySprite(Sprite.Name.Uninitialized);
            Debug.Assert(this.pProxySprite != null);

            this.poColObj = new ColObject(this.pProxySprite, BoxSprite.Name.Uninitialized);
            Debug.Assert(this.poColObj != null);
        }

        protected GameObject(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName)
        {
            this.name = name;
            this.x = 0.0f;
            this.y = 0.0f;
            this.speedX = 0;
            this.speedY = 0;

            this.pProxySprite = new ProxySprite(spriteName);
            Debug.Assert(this.pProxySprite != null);

            this.poColObj = new ColObject(this.pProxySprite, boxSpriteName);
            Debug.Assert(this.poColObj != null);
        }

        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
            Debug.Assert(this.poColObj.pColSprite != null);
            this.poColObj.pColSprite.Update();
        }

        // Usually Called by TimedMover
        public virtual void Move()
        {
            this.x += this.speedX;
            this.y += this.speedY;

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
        }

        public virtual void Remove()
        {
            // Grab a reference to the object's parent
            // We may need to remove it as well if it has no children after this is removed
            GameObject pParent = (GameObject)this.GetParent();

            // Remove from SpriteBatch
            // Find the SBNode
            Debug.Assert(this.pProxySprite != null);
            SpriteBaseNode pSBNode = this.pProxySprite.GetSBNode();

            // Remove it from the manager
            Debug.Assert(pSBNode != null);
            SpriteBatchManager.Remove(pSBNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSBNode = this.poColObj.pColSprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchManager.Remove(pSBNode);

            // Remove from GameObjectMan
            GameObjectManager.Remove(this);

            // check to see if the parent has any more children
            if (pParent != null && pParent.GetFirstChild() == null)
            {
                // We just removed the last of the parent's children 
                // so it is time to remove the parent as well
                pParent.Remove();
            }

            // TODO Add to ghost manager
        }

        public ColObject GetColObject()
        {
            Debug.Assert(this.poColObj != null);
            return this.poColObj;
        }

        public GameObject.Name GetName()
        {
            return this.name;
        }

        public override string ToString()
        {
            return String.Format("({0}), rect: x = {1}, y = {2}, dx = {3}, dy = {4}, box.x = {5}, box.y = {6}\n", this.GetHashCode(), this.poColObj.poColRect.x, this.poColObj.poColRect.y, this.poColObj.poColRect.width, this.poColObj.poColRect.height, this.poColObj.pColSprite.x, this.poColObj.pColSprite.y);
        }
    }
}
