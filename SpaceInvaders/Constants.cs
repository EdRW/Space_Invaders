using System;

namespace SpaceInvaders
{
    class Constants
    {
        //Screen Size
        public static readonly int screenWidth = 896;
        public static readonly int screenHeight = 1024;

        // Texture files
        public static readonly String fontAsset = "Consolas36pt.tga";
        public static readonly String invaderAsset = "aliens14x14.tga";
        public static readonly String uninitializedAsset = "HotPink.tga";
        
        // Image Rectangles
        public static readonly Azul.Rect shieldBrickRect = new Azul.Rect(420.0f, 700.0f, 14.0f, 14.0f);
        
        public static readonly Azul.Rect smallInvaderRect1 = new Azul.Rect(616.0f, 28.0f, 112.0f, 112.0f);
        public static readonly Azul.Rect smallInvaderRect2 = new Azul.Rect(616.0f, 182.0f, 112.0f, 112.0f);
        public static readonly Azul.Rect mediumInvaderRect1 = new Azul.Rect(322.0f, 28.0f, 154.0f, 112.0f);
        public static readonly Azul.Rect mediumInvaderRect2 = new Azul.Rect(322.0f, 182.0f, 154.0f, 112.0f);
        public static readonly Azul.Rect largeInvaderRect1 = new Azul.Rect(56.0f, 28.0f, 168.0f, 112.0f);
        public static readonly Azul.Rect largeInvaderRect2 = new Azul.Rect(56.0f, 182.0f, 168.0f, 112.0f);
        public static readonly Azul.Rect UFORect = new Azul.Rect(84.0f, 504.0f, 224.0f, 98.0f);       

        public static readonly Azul.Rect invaderDeathRect1 = new Azul.Rect(406.0f, 490.0f, 112.0f, 112.0f);
        public static readonly Azul.Rect invaderDeathRect2 = new Azul.Rect(574.0f, 490.0f, 182.0f, 112.0f);
        public static readonly Azul.Rect UFODeathRect = new Azul.Rect(42.0f, 644.0f, 294.0f, 112.0f);

        public static readonly Azul.Rect shipRect = new Azul.Rect(56.0f, 336.0f, 182.0f, 112.0f);
        public static readonly Azul.Rect shipDeathRect1 = new Azul.Rect(308.0f, 336.0f, 210.0f, 112.0f);
        public static readonly Azul.Rect shipDeathRect2 = new Azul.Rect(560.0f, 336.0f, 224.0f, 112.0f);

        public static readonly Azul.Rect missileRect = new Azul.Rect(420.0f, 700.0f, 14.0f, 56.0f);

        public static readonly Azul.Rect bombPlainRect = new Azul.Rect(378.0f, 798.0f, 14.0f, 98.0f);

        public static readonly Azul.Rect bombZigZagRect1 = new Azul.Rect(490.0f, 644.0f, 42.0f, 98.0f);
        public static readonly Azul.Rect bombZigZagRect2 = new Azul.Rect(574.0f, 644.0f, 42.0f, 98.0f);
        public static readonly Azul.Rect bombZigZagRect3 = new Azul.Rect(658.0f, 644.0f, 42.0f, 98.0f);
        public static readonly Azul.Rect bombZigZagRect4 = new Azul.Rect(742.0f, 644.0f, 42.0f, 98.0f);

        public static readonly Azul.Rect bombDaggerRect1 = new Azul.Rect(28.0f, 798.0f, 42.0f, 84.0f);
        public static readonly Azul.Rect bombDaggerRect2 = new Azul.Rect(112.0f, 798.0f, 42.0f, 84.0f);
        public static readonly Azul.Rect bombDaggerRect3 = new Azul.Rect(196.0f, 798.0f, 42.0f, 84.0f);
        public static readonly Azul.Rect bombDaggerRect4 = new Azul.Rect(280.0f, 798.0f, 42.0f, 84.0f);

        public static readonly Azul.Rect bombRollingRect1 = new Azul.Rect(448.0f, 798.0f, 42.0f, 98.0f);
        public static readonly Azul.Rect bombRollingRect2 = new Azul.Rect(532.0f, 798.0f, 42.0f, 98.0f);
        public static readonly Azul.Rect bombRollingRect3 = new Azul.Rect(364.0f, 798.0f, 42.0f, 98.0f);

        public static readonly Azul.Rect bombDeathRect = new Azul.Rect(700.0f, 798.0f, 84.0f, 112.0f);

        public static readonly Azul.Rect uninitializedRect = new Azul.Rect(0.0f, 0.0f, 128.0f, 128.0f);


        // Sprite Screen Sizes. I just divided the Image heights and widths by 4
        

        public static readonly float shieldBrickWidth = 4.0f;
        public static readonly float shieldBrickHeight = 4.0f;

        public static readonly float shipWidth = 45.5f;
        public static readonly float shipHeight = 28;

        public static readonly float missileWidth = 7.0f;
        public static readonly float missileHeight = 28;

        public static readonly float alienDivider = 3.75f;

        public static readonly float smallInvaderWidth = smallInvaderRect1.width / alienDivider; //28.0f;
        public static readonly float smallInvaderHeight = smallInvaderRect1.height / alienDivider; //28.0f;
        public static readonly float mediumInvaderWidth = mediumInvaderRect1.width / alienDivider; //38.5f;
        public static readonly float mediumInvaderHeight = mediumInvaderRect1.height / alienDivider; //28.0f;
        public static readonly float largeInvaderWidth = largeInvaderRect1.width / alienDivider; // 42.0f;
        public static readonly float largeInvaderHeight = largeInvaderRect1.height / alienDivider; //28.0f;
        public static readonly float UFOWidth = UFORect.width / alienDivider; // 56.0f;
        public static readonly float UFOHeight = UFORect.height / alienDivider; //24.5f;
        public static readonly float uninitializedWidth = 32.0f;
        public static readonly float uninitializedHeight = 32.0f;

        public static readonly float bombDivider = 4.0f;
        public static readonly float bombPlainWidth = bombPlainRect.width / bombDivider;
        public static readonly float bombPlainHeight = bombPlainRect.height / bombDivider;
        public static readonly float bombZigZagWidth = bombZigZagRect1.width / bombDivider;
        public static readonly float bombZigZagHeight = bombZigZagRect1.height / bombDivider;
        public static readonly float bombDaggerWidth = bombDaggerRect1.width / bombDivider;
        public static readonly float bombDaggerHeight = bombDaggerRect1.height / bombDivider;
        public static readonly float bombRollingWidth1 = bombRollingRect1.width / bombDivider;
        public static readonly float bombRollingHeight1 = bombRollingRect1.height / bombDivider;
        public static readonly float bombRollingWidth2 = bombRollingRect1.width / bombDivider;
        public static readonly float bombRollingHeight2 = bombRollingRect1.height / bombDivider;

        // Walls and Game Space Dimensions
        public static readonly float gameSpaceUpperLimit = 950.0f;
        public static readonly float gameSpaceLowerLimit = 50.0f;
        public static readonly float gameSpaceLeftLimit = 28.0f;
        public static readonly float gameSpaceRightLimit = 868.0f;
        public static readonly float wallCeilingThickness = 40.0f;

        public static readonly float leftWallXPos = gameSpaceLeftLimit + (wallCeilingThickness * 0.5f);
        public static readonly float leftWallYPos = gameSpaceLowerLimit + ((gameSpaceUpperLimit - gameSpaceLowerLimit) * 0.5f);
        public static readonly float leftWallWidth = wallCeilingThickness;
        public static readonly float leftWallHeight = gameSpaceUpperLimit - gameSpaceLowerLimit - 2.0f * wallCeilingThickness;

        public static readonly float rightWallXPos = gameSpaceRightLimit - (wallCeilingThickness * 0.5f);
        public static readonly float rightWallYPos = gameSpaceLowerLimit + ((gameSpaceUpperLimit - gameSpaceLowerLimit) * 0.5f);
        public static readonly float rightWallWidth = wallCeilingThickness;
        public static readonly float rightWallHeight = gameSpaceUpperLimit - gameSpaceLowerLimit - 2.0f * wallCeilingThickness;

        public static readonly float ceilingXPos = gameSpaceLeftLimit + ((gameSpaceRightLimit - gameSpaceLeftLimit) * 0.5f);
        public static readonly float ceilingYPos = gameSpaceUpperLimit - (wallCeilingThickness * 0.5f);
        public static readonly float ceilingWidth = gameSpaceRightLimit - gameSpaceLeftLimit - 2.0f * wallCeilingThickness;
        public static readonly float ceilingHeight = wallCeilingThickness;

        public static readonly float floorXPos = gameSpaceLeftLimit + ((gameSpaceRightLimit - gameSpaceLeftLimit) * 0.5f);
        public static readonly float floorYPos = gameSpaceLowerLimit + (wallCeilingThickness * 0.5f);
        public static readonly float floorWidth = gameSpaceRightLimit - gameSpaceLeftLimit - 2.0f * wallCeilingThickness;
        public static readonly float floorHeight = wallCeilingThickness;

        // Sprite Spawn Positions
        public static readonly float shipXPos = screenWidth/2.0f;
        public static readonly float shipYPos = 175;

        public static readonly float gridColumnDelta = 55.0f;
        public static readonly float gridXOrigin = 150.0f;
        public static readonly float smallInvaderYPos = 800.0f;
        public static readonly float MediumInvaderYPos1 = 750.0f;
        public static readonly float MediumInvaderYPos2 = 700.0f;
        public static readonly float LargeInvaderYPos1 = 650.0f;
        public static readonly float LargeInvaderYPos2 = 600.0f;

        public static readonly float UFOPosXPos = leftWallXPos + leftWallWidth + 100.0f;
        public static readonly float UFONegXPos = rightWallXPos - 100.0f;
        public static readonly float UFOYPos = 850.0f;


        // Sound Files
        public static readonly String soundInvaderMarch1 = "fastinvader4.wav";
        public static readonly String soundInvaderMarch2 = "fastinvader1.wav";
        public static readonly String soundInvaderMarch3 = "fastinvader2.wav";
        public static readonly String soundInvaderMarch4 = "fastinvader3.wav";

        public static readonly String soundShoot = "shoot.wav";

        public static readonly String soundInvaderkilled = "invaderkilled.wav";

        public static readonly String soundUFOHighPitch = "ufo_highpitch.wav";
        public static readonly String soundUFOLowPitch = "ufo_lowpitch.wav";

        public static readonly String soundUninitialized = "explosion.wav";
    }
}
