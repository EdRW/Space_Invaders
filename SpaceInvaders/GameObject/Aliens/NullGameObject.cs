using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NullGameObject : GameObjectLeaf
    {
        public NullGameObject()
            : base(GameObject.Name.Null_Object)
        {

        }

        public override void Update()
        {
            // do nothing - its a null object
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitNullGameObject(this);
        }
    }
}
