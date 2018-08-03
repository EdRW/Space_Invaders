using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBatch_Link : DLink
    {

    }

    public class SpriteBatch : SpriteBatch_Link
    {
        public SpriteBatch.Name name;
        private SpriteBaseNodeManager poSBNodeMan;
        public bool enabled; 

        public enum Name
        {
            Texts,
            Sprites,
            Deaths,
            Boxes,
            Uninitialized
        }

        public SpriteBatch()
        {
            this.name = SpriteBatch.Name.Uninitialized;
            this.poSBNodeMan = new SpriteBaseNodeManager();
            Debug.Assert(this.poSBNodeMan != null);
            this.enabled = true;
        }

        public void Set(SpriteBatch.Name name, int numNodes = 3, int growthSize = 1)
        {
            this.name = name;
            this.poSBNodeMan.Set(name, numNodes, growthSize);
        }
        
        public SpriteBaseNode Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);
            Debug.Assert(this.poSBNodeMan != null);

            SpriteBaseNode pSBNode = this.poSBNodeMan.Attach(pNode);
            Debug.Assert(pSBNode != null);

            // set Back pointer
            this.poSBNodeMan.SetSpriteBatch(this);

            return pSBNode;
        }

        public void SetName(SpriteBatch.Name inName)
        {
            this.name = inName;
        }

        public SpriteBatch.Name GetName()
        {
            return this.name;
        }

        public SpriteBaseNodeManager GetSBNodeManager()
        {
            return this.poSBNodeMan;
        }

        public override void Wash()
        {
        }

        public override string ToString()
        {
            //poSBNodeMan.PrintReport();
            return "[ " + name + " (" + this.GetHashCode() + ") ]";
        }
    }
}
