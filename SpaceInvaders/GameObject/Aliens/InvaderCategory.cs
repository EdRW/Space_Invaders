using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class InvaderCategory : GameObjectLeaf
    {
        // This is a Class exists primarily to reduce the number of visitor methods
        // by making all of the Invaders share the same methods.

        public bool canLaunchBomb; // state is overkill for this
        public int value;

        protected InvaderCategory(GameObject.Name name, Sprite.Name spriteName, BoxSprite.Name boxSpriteName)
            : base(name, spriteName, boxSpriteName)
        {
            this.canLaunchBomb = true;
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitInvaderCategory(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(pMissile, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShip(Ship pShip)
        {
            //Debug.WriteLine("   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(pShip, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShieldZone(ShieldZone pShieldZone)
        {
            //Debug.WriteLine("in InvaderCategory , visit from ShieldZone");
            GameObject pGameObj = (GameObject)pShieldZone.GetFirstChild();

            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShield(Shield pShield)
        {
            //Debug.WriteLine("in InvaderCategory, visit from shield");
            GameObject pGameObj = (GameObject)pShield.GetFirstChild();

            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            //Debug.WriteLine("in InvaderCategory, visit from shieldColumn");
            GameObject pGameObj = (GameObject)pShieldColumn.GetFirstChild();

            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            //Debug.WriteLine("in InvaderCategory   --->DONE<----");
            ColPair pColPair = ColPairManager.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(this, pShieldBrick);
            pColPair.NotifyListeners();
        }
    }
}
