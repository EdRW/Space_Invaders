using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ImageHolder : SLink
    {
        public Image pImage;

        public ImageHolder(Image image)
            : base()
        {
            this.pImage = image;
        }

        public override string ToString()
        {
            return pImage.ToString();
        }
    }
}
