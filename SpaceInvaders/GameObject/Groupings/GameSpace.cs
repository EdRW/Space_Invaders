using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSpace : GameObjectComposite
    {
        public GameSpace(GameObject.Name name, BoxSprite.Name boxName)
            : base(name, boxName)
        {

        }

        public override void Accept(ColVisitor other)
        {
            other.VisitGameSpace(this);
        }
    }
}
