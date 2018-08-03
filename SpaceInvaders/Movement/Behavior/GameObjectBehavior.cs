using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // Change name to behavior and 
    public class GameObjectBehavior : Command
    {
        private GameObject pGameObject;

        private SLink poFirstMotion;
        private SLink pCurrentMotion;

        public GameObjectBehavior(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObject = pGameObject;
        }

        public void Attach(Motion motion)
        {
            Debug.Assert(motion != null);

            // Create a new holder
            MotionHolder pMotionHolder = new MotionHolder(motion);
            Debug.Assert(pMotionHolder != null);

            // Attach it to the Motion Composite ( Push to front )
            SLink.AddToFront(ref this.poFirstMotion, pMotionHolder);

            // Set the first one to this motion
            this.pCurrentMotion = pMotionHolder;
        }

        public override void Execute(float deltaTime)
        {
            Debug.Assert(this.pGameObject != null);

            //--------------------------------------
            MotionHolder pMotionHolder = (MotionHolder)this.pCurrentMotion;
            Debug.Assert(pMotionHolder != null);

            if (pMotionHolder.pMotion.motionAdvance == true)
            {
                //set advance flag back to false
                pMotionHolder.pMotion.motionAdvance = false;
                // advance to next motion 
                pMotionHolder = (MotionHolder)pMotionHolder.pNext;

                // if at end of list, set to first
                if (pMotionHolder == null)
                {
                    pMotionHolder = (MotionHolder)poFirstMotion;
                }
            }

            // squirrel away for next timer event
            this.pCurrentMotion = pMotionHolder;

            // computer x and y deltas using composition position info
            Motion motion = pMotionHolder.pMotion;
            motion.ApplyMotion(pGameObject.poColObj.poColRect.x, pGameObject.poColObj.poColRect.y, pGameObject.poColObj.poColRect.width, pGameObject.poColObj.poColRect.height);

            // Update Composition Speeds
            pGameObject.speedX = motion.deltaX;
            pGameObject.speedY = motion.deltaY;

            // Move the Composition using the updated speeds
            pGameObject.Move(); // no longer used.

            // Add itself back to timer
            TimerManager.Add(this.name, this, deltaTime);
        }

        public void PrintReport()
        {
            Debug.WriteLine("[Composite Motion: ");
            Debug.Write("Motion List: ");
            SLink.PrintList(poFirstMotion);
            Debug.WriteLine(" ]\n");
        }
    }
}
