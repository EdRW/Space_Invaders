using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class OneTimeAnimation : AnimationSprite
    {

        public OneTimeAnimation(Sprite.Name name, float xPos, float yPos)
            : base(name)
        {
            this.pSprite.x = xPos;
            this.pSprite.y = yPos;
            this.pSprite.Update();
            //SpriteBatch pSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.Sprites);
            //pSpriteBatch.Attach(this.pSprite);
        }


        public override void Execute(float deltaTime)
        {
            // advance to next image 
            ImageHolder pImageHolder = (ImageHolder)this.pCurrentImage.pNext;
            
            if (pImageHolder != null)
            {
                // squirrel away for next timer event
                this.pCurrentImage = pImageHolder;

                // change image
                this.pSprite.SwapImage(pImageHolder.pImage);

                // Add itself back to timer
                TimerManager.Add(this.name, this, deltaTime);
            }
            else
            {
                // if at end of list dont add back to timer
                //this.pSprite.GetSBNode().GetSBNodeMan().Remove(this.pSprite.GetSBNode());
                this.pSprite.x = -50;
                this.pSprite.y = -50;
                this.pSprite.Update();
                this.pSprite = null;
                
            }
        }
    }
}