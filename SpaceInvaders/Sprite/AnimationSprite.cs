using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AnimationSprite : Command
    {
        protected Sprite pSprite;
        protected SLink poFirstImage;
        protected SLink pCurrentImage;

        public AnimationSprite(Sprite.Name name)
        {
            this.pSprite = SpriteManager.Find(name);
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrentImage = null;
            this.poFirstImage = null;
        }

        public virtual void Attach(Image.Name imageName)
        {
            // Get the image
            Image pImage = ImageManager.Find(imageName);
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            SLink.AddToFront(ref this.poFirstImage, pImageHolder);

            // Set the first one to this image
            this.pCurrentImage = pImageHolder;
        }

        public override void Execute(float deltaTime)
        {
            // advance to next image 
            ImageHolder pImageHolder = (ImageHolder)this.pCurrentImage.pNext;

            // if at end of list, set to first
            if (pImageHolder == null)
            {
                pImageHolder = (ImageHolder)poFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrentImage = pImageHolder;

            // change image
            this.pSprite.SwapImage(pImageHolder.pImage);

            // Add itself back to timer
            TimerManager.Add(this.name, this, deltaTime);
        }

        public void PrintReport()
        {
            Debug.WriteLine("[Animation Sprite: " + pSprite.name + " ]");
            Debug.Write("Image List: ");
            SLink.PrintList(poFirstImage);
            Debug.WriteLine("\n");
        }
    }
}
