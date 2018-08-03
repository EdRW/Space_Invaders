using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ToggleSpriteBoxObserver : InputObserver
    {
        protected override void derivedUpdate(InputSubject pInputSubject)
        {
            SpriteBatch pSBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pSBatch.enabled = !pSBatch.enabled;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override void Wash()
        {
            throw new NotImplementedException();
        }
    }
}
