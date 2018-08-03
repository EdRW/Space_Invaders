using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObjectComposite : GameObject
    {
        public DLink poHeadChild;
        public int numChildren;

        public GameObjectComposite(GameObject.Name name, BoxSprite.Name boxSpriteName)
            : base(name, Sprite.Name.NullObject, boxSpriteName) //TODO the null object here could screw me later
        {
            this.poHeadChild = null;
            this.numChildren = 0;
        }

        override public void Add(Component pComponent)
        {
            GameObject pGameObject = (GameObject)pComponent;

            Debug.Assert(pGameObject != null);
            DLink.AddToFront(ref this.poHeadChild, pGameObject);
            pGameObject.pParent = this;

            this.numChildren++;
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.Remove(ref this.poHeadChild, pComponent);
            this.numChildren--;
        }

        public override Component GetFirstChild()
        {
            return (Component)this.poHeadChild;
        }

        public override Component GetLastChild()
        {
            DLink pNode = this.poHeadChild;
            DLink pTmp = null;
            while (pNode != null)
            {
                pTmp = pNode;
                pNode = pNode.GetNext();
            }

            return (Component)pTmp;
        }

        public override void Print()
        {
            Debug.Write(this.ToString());
        }

        public override void Update()
        {
            BaseUpdateBoundingBox();
            base.Update();
        }

        public int GetNumChildren()
        {
            return this.numChildren;
        }

        public Component GetChild(int index)
        {
            Debug.Assert(index <= this.GetNumChildren(), "There is no child at the current index");
            
            Component pTmp = this.GetFirstChild();
            Component pNode = null;
            for (int i = 0; i < (index + 1); i++)
            {
                pNode = pTmp;
                pTmp = (Component)pTmp.GetNext();
            }
            Debug.Assert(pNode != null);
            return pNode;
        }

        public override void Wash()
        {
            Debug.Assert(false);

            while (poHeadChild != null)
            {
                DLink pNode = DLink.RemoveFront(ref poHeadChild);
                pNode.Wash();
            }
            base.Wash();
        }

        public override string ToString()
        {
            String returnVal = "[GameObjectComposite: " + base.ToString();

            DLink pNode = this.poHeadChild;

            while (pNode != null)
            {
                returnVal += pNode.ToString();
                returnVal += ", ";
                pNode = pNode.GetNext();
            }

            return returnVal + " ]\n";
        }


        protected void BaseUpdateBoundingBox()
        {
            // point to ColTotal
            ColRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            GameObject pNode = (GameObject)this.GetFirstChild();

            if (pNode != null)
            {
                // Initialized the union to the first block
                ColTotal.Set(pNode.poColObj.poColRect);

                // loop through sliblings
                while (pNode != null)
                {
                    ColTotal.Union(pNode.poColObj.poColRect);

                    // go to next sibling
                    pNode = (GameObject)pNode.GetNextSibling();
                }

                //this.poColObj.poColRect.Set(201, 201, 201, 201);
                this.x = this.poColObj.poColRect.x;
                this.y = this.poColObj.poColRect.y;

                //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
            }
        }

    }
}
