using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObjectNode_Link : DLink
    {
    }

    public class GameObjectNode : GameObjectNode_Link
    {
        public GameObject pGameObj;

        public GameObjectNode()
            : base()
        {
            this.pGameObj = null;
        }

        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObj = pGameObject;
        }
        public GameObject.Name GetName()
        {
            return this.pGameObj.name;
        }

        public override void Wash()
        {
            this.pGameObj = null;
        }

        public override string ToString()
        {
            String objName = (pGameObj == null) ? "null" : pGameObj.name.ToString();
            return "[ GameObjectNode : " + objName + " (" + this.GetHashCode() + ") ]";
        }
    }
}
