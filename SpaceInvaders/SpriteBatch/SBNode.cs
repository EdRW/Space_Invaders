using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBaseNode_Link : DLink
    {

    }

    public class SpriteBaseNode : SpriteBaseNode_Link
    {
        public SpriteBase pSpriteBase;

        private SpriteBaseNodeManager pBackSBNodeMan;

        public SpriteBaseNode()
        {
            this.pSpriteBase = null;
            this.pBackSBNodeMan = null;
        }

        public void Set(SpriteBase pNode, SpriteBaseNodeManager pSBNodeMan)
        {
            // associate it
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            this.pSpriteBase.SetSBNode(this);

            Debug.Assert(pSBNodeMan != null);
            this.pBackSBNodeMan = pSBNodeMan;
        }

        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }

        public SpriteBaseNodeManager GetSBNodeMan()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan;
        }

        public override void Wash()
        {
            this.pSpriteBase = null;
        }

        public override string ToString()
        {
            return "SBNode:" + this.pSpriteBase;
        }
    }
}
