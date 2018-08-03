using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ForwardIterator : Iterator
    {
        public ForwardIterator(Component pStart)
        {
            Debug.Assert(pStart != null);

            this.pCurr = pStart;
            this.pRoot = pStart;
        }

        public override Component First()
        {
            Debug.Assert(this.pRoot != null);
            Component pNode = this.pRoot;

            Debug.Assert(pNode != null);
            this.pCurr = pNode;

            return this.pCurr;
        }

        public override Component Current()
        {
            Debug.Assert(this.pCurr != null);
            return this.pCurr;
        }

        public override Component Next()
        {
            Debug.Assert(this.pCurr != null);

            Component pChild = this.pCurr.GetFirstChild();
            Component pSibling = this.pCurr.GetNextSibling();
            Component pParent = this.pCurr.GetParent();

            Component pNext = null;

            if(pChild != null)
            {
                // Current Component is a Composite so the next node will be its first child
                pNext = pChild;
            }
            else
            {
                // Current Component is either a Composite with no children or it is a Leaf
                if (pSibling != null)
                {
                    pNext = pSibling;
                }
                else
                {
                    // Current Component has no siblings move up to parent and check for a sibling
                    while (pParent != null)
                    {
                        pNext = pParent.GetNextSibling();
                        if(pNext != null)
                        {
                            // Parent's Sibling is a valid next node
                            break;
                        }
                        else
                        {
                            // parent has no siblings, checked the parent's parent
                            pParent = pParent.GetParent();
                        }
                    }
                }
            }
            this.pCurr = pNext;

            return pCurr;
        }

        public override bool IsDone()
        {
            return (this.pCurr == null);
        }
    }
}
