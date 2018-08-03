using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallManager
    {
        private GameSpace pGameSpace;
        private WallLeft pWallLeft;
        private WallRight pWallRight;
        private Ceiling pCeiling;
        private Floor pFloor;

        public WallManager()
        {
            WallFactory gameSpaceFactory = new WallFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes);
            this.pGameSpace = (GameSpace)gameSpaceFactory.Create(GameObject.Type.GameSpace, 0, 0, 0, 0);
            Debug.Assert(this.pGameSpace != null);

            WallFactory wallFactory = new WallFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, this.pGameSpace);
            this.pWallLeft = (WallLeft)wallFactory.Create(GameObject.Type.WallLeft, Constants.leftWallXPos, Constants.leftWallYPos,
                                Constants.leftWallWidth, Constants.leftWallHeight);
            Debug.Assert(this.pWallLeft != null);

            this.pWallRight = (WallRight)wallFactory.Create(GameObject.Type.WallRight, Constants.rightWallXPos, Constants.rightWallYPos,
                                Constants.rightWallWidth, Constants.rightWallHeight);
            Debug.Assert(this.pWallRight != null);

            this.pCeiling = (Ceiling)wallFactory.Create(GameObject.Type.Ceiling, Constants.ceilingXPos, Constants.ceilingYPos,
                                Constants.ceilingWidth, Constants.ceilingHeight);
            Debug.Assert(this.pCeiling != null);

            this.pFloor = (Floor)wallFactory.Create(GameObject.Type.Floor, Constants.floorXPos, Constants.floorYPos,
                                Constants.floorWidth, Constants.floorHeight);
            Debug.Assert(this.pFloor != null);
        }

        public  GameSpace getGameSpace()
        {
            return this.pGameSpace;
        }

        public WallLeft GetWallLeft()
        {
            return this.pWallLeft;
        }

        public WallRight GetWallRight()
        {
            return this.pWallRight;
        }

        public Ceiling GetCeiling()
        {
            return this.pCeiling;
        }

        public Floor GetFloor()
        {
            return this.pFloor;
        }
    }
}
