using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColPair_Link : DLink
    {

    }
    public class ColPair : ColPair_Link
    {

        public ColPair.Name name;
        public GameObject treeA;
        public GameObject treeB;
        public ColSubject poSubject;

        public bool bMarkForDeath;

        public enum Name
        {
            Grid_WallLeft,
            Grid_WallRight,
            Grid_ShieldZone,

            Ship_Grid,
            Ship_WallLeft,
            Ship_WallRight,

            Missile_Ceiling,
            Missile_Grid,
            Missile_UFO,
            Missile_Bomb,
            Missile_ShieldZone,

            UFO_WallLeft,
            UFO_WallRight,

            Bomb_Floor,
            Bomb_Shield,
            Bomb_Ship,

            NullObject,
            Not_Initialized
        }

        public ColPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Not_Initialized;

            this.poSubject = new ColSubject(this);
            Debug.Assert(this.poSubject != null);
            this.bMarkForDeath = false;
        }

        ~ColPair()
        {

        }

        public void Set(ColPair.Name colpairName, GameObject pTreeRootA, GameObject pTreeRootB)
        {
            Debug.Assert(pTreeRootA != null);
            Debug.Assert(pTreeRootB != null);

            this.treeA = pTreeRootA;
            this.treeB = pTreeRootB;
            this.name = colpairName;
        }

        public ColPair.Name GetName()
        {
            return this.name;
        }

        public void Process()
        {
            //Debug.Write("({0})", this.GetHashCode().ToString());
            // this is here so that persistable items that are not in groups like the missle and ufo can be ignore when killed
            if (this.treeA.GetColObject().enabled && this.treeB.GetColObject().enabled)
                FwdCollide(this.treeA, this.treeB);
        }

        static public void FwdCollide(GameObject pSafeTreeA, GameObject pSafeTreeB)
        {
            // A vs B
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            while (pNodeA != null)
            {
                // Restart compare
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {
                    // who is being tested?
                    //Debug.WriteLine("ColPair:    test:  {0}({2}), {1}({3})", pNodeA.name, pNodeB.name, pNodeA.GetHashCode(), pNodeB.GetHashCode());

                    // Get rectangles
                    ColRect rectA = pNodeA.GetColObject().poColRect;
                    ColRect rectB = pNodeB.GetColObject().poColRect;

                    // test them
                    if (ColRect.Intersect(rectA, rectB))
                    {
                        // Boom - it works (Visitor in Action)
                        // Debug.WriteLine("Collision Detected!");
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    pNodeB = (GameObject)pNodeB.GetNextSibling();
                }

                pNodeA = (GameObject)pNodeA.GetNextSibling();
            }
        }

        static public void RevCollide(GameObject pSafeTreeA, GameObject pSafeTreeB)
        {
            // A vs B
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            while (pNodeA != null)
            {
                // Restart compare
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {
                    // who is being tested?
                    //Debug.WriteLine("ColPair:    test:  {0}({2}), {1}({3})", pNodeA.name, pNodeB.name, pNodeA.GetHashCode(), pNodeB.GetHashCode());

                    // Get rectangles
                    ColRect rectA = pNodeA.GetColObject().poColRect;
                    ColRect rectB = pNodeB.GetColObject().poColRect;

                    // test them
                    if (ColRect.Intersect(rectA, rectB))
                    {
                        // Boom - it works (Visitor in Action)
                        // Debug.WriteLine("Collision Detected!");
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    pNodeB = (GameObject)pNodeB.GetPrevSibling();
                }

                pNodeA = (GameObject)pNodeA.GetPrevSibling();
            }
        }


        public void SetName(ColPair.Name inName)
        {
            this.name = inName;
        }

        public void Attach(ColObserver observer)
        {
            this.poSubject.Attach(observer);
        }

        public void NotifyListeners()
        {
            this.poSubject.Notify();
        }

        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            // GameObject pAlien = AlienCategory.GetAlien(objA, objB);
            this.poSubject.pObjA = pObjA;
            this.poSubject.pObjB = pObjB;
        }

        public override void Wash()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Not_Initialized;
            this.poSubject.PurgeAll();
            this.bMarkForDeath = false;
        }

        public override string ToString()
        {
            return "[ ColPair: " + name + " (" + this.GetHashCode() + ") ]";
        }

        public void Print()
        {
            Debug.WriteLine(this);
            Debug.WriteLine("Tree A: " + this.treeA.name);
            Debug.WriteLine("Tree B: " + this.treeB.name);
            Debug.WriteLine("Observers:");
            this.poSubject.Print();

        }
    }
}
