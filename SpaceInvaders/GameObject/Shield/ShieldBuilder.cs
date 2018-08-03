using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBuilder
    {
        public static ShieldZone CreateShieldZone(float xOrigin, float yOrigin, float xDeltaBetween, int numShields =  4)
        {
            ShieldFactory pZoneSF = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes);
            ShieldZone pShieldZone = (ShieldZone)pZoneSF.Create(ShieldFactory.Type.Zone, 0, 0);

            for(int i = 0; i < numShields; i++)
            {
                CreateShield(xOrigin + (i * xDeltaBetween), yOrigin, pShieldZone);
            }

            return pShieldZone;
        }

        public static Shield CreateShield(float xOrigin, float yOrigin, GameObjectComposite pGOComposite = null)
        {
            // 21 x 18
            ShieldFactory pShieldSF = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pGOComposite);
            Shield pShield = (Shield)pShieldSF.Create(ShieldFactory.Type.Shield, 0, 0);

            ShieldFactory pColumnSF = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShield);

            ShieldColumn pShieldColumn0 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn1 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn2 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn3 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn4 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn5 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn6 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            // Columns 7 through 13 are produced in a loop below.
            ShieldColumn pShieldColumn14 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn15 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn16 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn17 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn18 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn19 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
            ShieldColumn pShieldColumn20 = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);

            // make shield columns 0 and 20
            for (int i = 0; i < 12; i++)
            {          
                ShieldFactory pShieldBrickFactory = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn0);
                ShieldBrick shieldBrick = (ShieldBrick)pShieldBrickFactory.Create(ShieldFactory.Type.Brick, xOrigin, yOrigin + i * Constants.shieldBrickHeight);
              
                ShieldFactory pShieldBrickFactory20 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn20);
                pShieldBrickFactory20.Create(ShieldFactory.Type.Brick, xOrigin + 20 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);
            }

            // make shield columns 1 and 19
            for (int i = 0; i < 13; i++)
            {
                ShieldFactory pShieldBrickFactory = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn1);
                ShieldBrick shieldBrick = (ShieldBrick)pShieldBrickFactory.Create(ShieldFactory.Type.Brick, xOrigin + 1 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);

                ShieldFactory pShieldBrickFactory19 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn19);
                pShieldBrickFactory19.Create(ShieldFactory.Type.Brick, xOrigin + 19 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);
            }

            // make shield columns 2 and 18
            for (int i = 0; i < 14; i++)
            {
                ShieldFactory pShieldBrickFactory = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn2);
                ShieldBrick shieldBrick = (ShieldBrick)pShieldBrickFactory.Create(ShieldFactory.Type.Brick, xOrigin + 2 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);

                ShieldFactory pShieldBrickFactory18 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn18);
                pShieldBrickFactory18.Create(ShieldFactory.Type.Brick, xOrigin + 18 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);
            }

            // make shield columns 3 and 17
            for (int i = 0; i < 15; i++)
            {
                ShieldFactory pShieldBrickFactory3 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn3);
                pShieldBrickFactory3.Create(ShieldFactory.Type.Brick, xOrigin + 3 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);

                ShieldFactory pShieldBrickFactory17 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn17);
                pShieldBrickFactory17.Create(ShieldFactory.Type.Brick, xOrigin + 17 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);
            }

            // make shield columns 4 and 16
            for (int i = 0; i < 16; i++)
            {
                ShieldFactory pShieldBrickFactory4 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn4);
                pShieldBrickFactory4.Create(ShieldFactory.Type.Brick, xOrigin + 4 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);
                
                ShieldFactory pShieldBrickFactory16 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn16);
                pShieldBrickFactory16.Create(ShieldFactory.Type.Brick, xOrigin + 16 * Constants.shieldBrickHeight, yOrigin + i * Constants.shieldBrickHeight);
            }

            // make shield columns 5 and 15
            for (int i = 0; i < 14; i++)
            {
                ShieldFactory pShieldBrickFactor5 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn5);
                pShieldBrickFactor5.Create(ShieldFactory.Type.Brick, xOrigin + 5 * Constants.shieldBrickHeight, yOrigin + 2 * Constants.shieldBrickHeight + i * Constants.shieldBrickHeight);
                
                ShieldFactory pShieldBrickFactor15 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn15);
                pShieldBrickFactor15.Create(ShieldFactory.Type.Brick, xOrigin + 15 * Constants.shieldBrickHeight, yOrigin + 2 * Constants.shieldBrickHeight + i * Constants.shieldBrickHeight);
            }

            // make shield  columns 6 and 14
            for (int i = 0; i < 13; i++)
            {
                ShieldFactory pShieldBrickFactory6 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn6);
                pShieldBrickFactory6.Create(ShieldFactory.Type.Brick, xOrigin + 6 * Constants.shieldBrickHeight, yOrigin + 3 * Constants.shieldBrickHeight + i * Constants.shieldBrickHeight);
                
                ShieldFactory pShieldBrickFactory14 = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn14);
                pShieldBrickFactory14.Create(ShieldFactory.Type.Brick, xOrigin + 14 * Constants.shieldBrickHeight, yOrigin + 3 * Constants.shieldBrickHeight + i * Constants.shieldBrickHeight);
            }

            // make shield  columns 7 thru 13
            for (int i = 0; i < 7; i++)
            {
                float x = xOrigin + 7 * Constants.shieldBrickHeight + i * Constants.shieldBrickHeight;
                ShieldColumn pShieldColumn = (ShieldColumn)pColumnSF.Create(ShieldFactory.Type.Column, 0, 0);
                ShieldFactory pShieldBrickFactory = new ShieldFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pShieldColumn);
                for (int j = 0; j < 12; j++)
                {
                    float y = yOrigin + 4 * Constants.shieldBrickHeight + j * Constants.shieldBrickHeight;
                    pShieldBrickFactory.Create(ShieldFactory.Type.Brick, x, y);
                }
            }

            return pShield;
        }
    }
}
