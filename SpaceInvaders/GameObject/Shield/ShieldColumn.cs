using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldColumn : GameObjectComposite
    {
        public ShieldColumn(GameObject.Name name, BoxSprite.Name boxName)
            : base(name, boxName)
        {

        }
        public override void Accept(ColVisitor other)
        {
            //Debug.WriteLine("ShieldColumn Accepts");
            other.VisitShieldColumn(this);
        }

        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("in ShieldColumn, visit from Missle");
            GameObject pGameObj = (GameObject)this.GetLastChild();
            ColPair.RevCollide(pGameObj, pMissile);
        }

        public override void VisitBomb(Bomb pBomb)
        {
            //Debug.WriteLine("in ShieldColumn, visit from pBomb");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pBomb);
        }

        public override void VisitInvaderGrid(InvaderGrid pGrid)
        {
            //Debug.WriteLine("in ShieldColumn, visit from InvaderGrid");
            GameObject pGameObj = (GameObject)pGrid.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderColumn(InvaderColumn pColumn)
        {
            //Debug.WriteLine("in ShieldColumn, visit from InvaderColumn");
            GameObject pGameObj = (GameObject)pColumn.GetFirstChild();
            ColPair.FwdCollide(this, pGameObj);
        }

        public override void VisitInvaderCategory(InvaderCategory pInvader)
        {
            //Debug.WriteLine("in ShieldColumn, visit from InvaderCategory");
            GameObject pGameObj = (GameObject)this.GetFirstChild();
            ColPair.FwdCollide(pGameObj, pInvader);
        }
    }
}