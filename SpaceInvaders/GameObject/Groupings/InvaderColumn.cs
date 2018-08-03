using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InvaderColumn : GameObjectComposite
    {
        public InvaderColumn(GameObject.Name name, BoxSprite.Name boxName)
            : base(name, boxName)
        {

        }
        public override void Accept(ColVisitor other)
        {
            other.VisitInvaderColumn(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pMissile);
        }

        public override void VisitShip(Ship pShip)
        {
            //Debug.WriteLine("in InvaderColumn , visit from Ship");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pShip);
        }

        public override void VisitShieldZone(ShieldZone pShieldZone)
        {
            //Debug.WriteLine("in InvaderColumn , visit from ShieldZone");
            GameObject pGameObj = (GameObject)pShieldZone.GetFirstChild();

            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShield(Shield pShield)
        {
            //Debug.WriteLine("in InvaderColumn, visit from shield");
            GameObject pGameObj = (GameObject)pShield.GetFirstChild();

            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            //Debug.WriteLine("in InvaderColumn, visit from shieldColumn");
            GameObject pGameObj = (GameObject)pShieldColumn.GetFirstChild();

            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            //Debug.WriteLine("in InvaderColumn, visit from ShieldBrick");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pShieldBrick);
        }
    }
}
