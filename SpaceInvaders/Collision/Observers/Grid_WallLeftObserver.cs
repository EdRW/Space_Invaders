using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Grid_WallLeftObserver : ColObserver
    {
        public Grid_WallLeftObserver()
        {

        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("Grid_Observer: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
            // Grid will always be pObjB
            GameObject pGameObjA = pColSubject.pObjA;
            GameObject pGameObjB = pColSubject.pObjB;
            Debug.Assert(pGameObjA != null);
            Debug.Assert(pGameObjB != null);

            InvaderGrid pGrid = null;
            if (pGameObjA.name == GameObject.Name.InvaderGrid)
            {
                pGrid = (InvaderGrid)pGameObjA;
            }
            else if (pGameObjB.name == GameObject.Name.InvaderGrid)
            {
                pGrid = (InvaderGrid)pGameObjB;
            }
            else { Debug.Assert(false, "Neither of the objects are InvaderGrid"); }

            pGrid.SetState(InvaderGridManager.State.CollidingLeftWall);
        }

        public override string ToString()
        {
            return "[ Grid_WallLeftObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }

    }
}
