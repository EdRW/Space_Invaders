using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldZone : GameObjectComposite
    {
        public ShieldZone(GameObject.Name name, BoxSprite.Name boxName)
            : base(name, boxName)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(ColVisitor other)
        {
            //Debug.WriteLine("ShieldZone Accepts");
            other.VisitShieldZone(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("in ShieldZone, visit from Missle");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pMissile);
        }

        public override void VisitBomb(Bomb pBomb)
        {
            //Debug.WriteLine("in ShieldZone, visit from pBomb");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pBomb);
        }

        public override void VisitInvaderGrid(InvaderGrid pGrid)
        {
            //Debug.WriteLine("in ShieldZone, visit from InvaderGrid");
            GameObject pGameObj = (GameObject)pGrid.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderColumn(InvaderColumn pColumn)
        {
            //Debug.WriteLine("in ShieldZone, visit from InvaderColumn");
            GameObject pGameObj = (GameObject)pColumn.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderCategory(InvaderCategory pInvader)
        {
            //Debug.WriteLine("in ShieldZone, visit from InvaderCategory");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pInvader);
        }
    }
}