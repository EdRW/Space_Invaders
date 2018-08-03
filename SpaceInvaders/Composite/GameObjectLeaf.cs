using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObjectLeaf : GameObject
    {
        public GameObjectLeaf(GameObject.Name name)
            : base(name)
        {
        }

        public GameObjectLeaf(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName)
            : base(name, spriteName, boxSpriteName)
        {
        }

        public override void Add(Component c)
        {
            Debug.Assert(false);
        }

        public override void Remove(Component c)
        {
            Debug.Assert(false);
        }

       
        public override Component GetFirstChild()
        {
            return null;
        }

        public override Component GetLastChild()
        {
            return null;
        }

        public override void Print()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1}) rect: x = {2}, y = {3}, dx = {4}, dy = {5}", this.GetName(), this.GetHashCode(), this.poColObj.poColRect.x, this.poColObj.poColRect.y, this.poColObj.poColRect.width, this.poColObj.poColRect.height);
        }

        public override string ToString()
        {
            return String.Format(" GameObject Name: {0} ({1})", this.GetName(), this.GetHashCode());
        }

        public override void Wash()
        {
            base.Wash();
        }
    }
}
