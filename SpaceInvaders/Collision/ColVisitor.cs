using System;
using System.Diagnostics;


namespace SpaceInvaders
{

    abstract public class ColVisitor : Component
    {
        abstract public void Accept(ColVisitor other);

        public virtual void VisitGameSpace(GameSpace pGameSpace)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by GameSpace not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallLeft(WallLeft pWallLeft)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallRight(WallRight pWallRight)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitCeiling(Ceiling pCeiling)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Ceiling not implemented");
            Debug.Assert(false);
        }


        public virtual void VisitFloor(Floor pFloor)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Floor not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitInvaderGrid(InvaderGrid pGrid)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by InvaderGrid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitInvaderColumn(InvaderColumn pColumn)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by InvaderColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitInvaderCategory(InvaderCategory pInvader)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by InvaderCategory not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship pShip)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile pMissile)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb pBomb)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Bomb not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShield(Shield pShield)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Shield not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldZone(ShieldZone pShieldZone)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by ShieldZone not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by ShieldColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by ShieldBrick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitNullGameObject(NullGameObject n)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

    }

}
