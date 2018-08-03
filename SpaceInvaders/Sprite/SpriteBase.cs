using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        public float speedX;
        public float speedY;

        // If you remove a SpriteBase initiated by gameObject... its hard to get the spriteBatchNode
        // so have a back pointer to it
        private SpriteBaseNode pBackSBNode;

        public SpriteBaseNode GetSBNode()
        {
            Debug.Assert(this.pBackSBNode != null);
            return this.pBackSBNode;
        }
        public void SetSBNode(SpriteBaseNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pBackSBNode = pSpriteBatchNode;
        }

        public SpriteBase()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }

        abstract public void Update();
        abstract public void Render();
    }
}
