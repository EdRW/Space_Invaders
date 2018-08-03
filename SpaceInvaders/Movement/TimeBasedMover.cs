using System;
using System.Diagnostics;

namespace SpaceInvaders
{
   public  class TimeBasedMover : Command
    {
        private GameObject pGameObject;

        public TimeBasedMover(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObject = pGameObject;
        }


        public override void Execute(float deltaTime)
        {
            Debug.Assert(this.pGameObject != null);
            ForwardIterator pFwdItor = new ForwardIterator(this.pGameObject);

            Component pNode = pFwdItor.First();
            GameObject pGameObj = (GameObject)pNode;

            // Move the first node before getting speed
            // just in case the move method changes the speed
            pGameObj.Move();

            float groupSpeedX = pGameObj.speedX;
            float groupSpeedY = pGameObj.speedY;

            pGameObj = (GameObject)pFwdItor.Next();
            while (!pFwdItor.IsDone())
            {
                // Apply group speed to every game object in the group.
                pGameObj.speedX = groupSpeedX;
                pGameObj.speedY = groupSpeedY;

                pGameObj.Move();

                pGameObj = (GameObject)pFwdItor.Next();
            }

            // Add itself back to timer
            TimerManager.Add(this.name, this, deltaTime);
        }
    }
}
