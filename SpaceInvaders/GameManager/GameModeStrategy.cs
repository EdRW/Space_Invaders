using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameModeStrategy
    {
        public abstract void InitializeLevel(GameManager pGameManager);
        public abstract void EndPlayerTurn(GameManager pGameManager);

        public abstract void ReinitializeLevel(GameManager pGameManager);

        protected virtual void PlayerOneInit(GameManager pGameManager)
        {
            float levelSpeed = 0.75f / pGameManager.pActivePlayer.level;

            pGameManager.poPlayer1.pGrid = InvaderGridManager.GenerateGrid(pGameManager.pActivePlayer.level);
            //Debug.WriteLine("Generated Grid ({0}) for P1", pGameManager.poPlayer1.pGrid.GetHashCode());

            pGameManager.poPlayer1.pShieldZone = ShieldBuilder.CreateShieldZone(160, 250, 160, 4);

            //---------------------------------------------------------------------------------------------------------
            // Create GameSpace and Wall Objects
            //---------------------------------------------------------------------------------------------------------
            WallManager pWallMan = new WallManager();

            //---------------------------------------------------------------------------------------------------------
            // Set up Sprite Animations
            //---------------------------------------------------------------------------------------------------------
            // Set up animations for the 3 basic Invaders
            AnimationSprite pSmInvaderAnim = new AnimationSprite(Sprite.Name.SmallInvader);
            pSmInvaderAnim.Attach(Image.Name.SmallInvader1);
            pSmInvaderAnim.Attach(Image.Name.SmallInvader2);

            AnimationSprite pMedInvaderAnim = new AnimationSprite(Sprite.Name.MediumInvader);
            pMedInvaderAnim.Attach(Image.Name.MediumInvader1);
            pMedInvaderAnim.Attach(Image.Name.MediumInvader2);

            AnimationSprite pLgInvaderAnim = new AnimationSprite(Sprite.Name.LargeInvader);
            pLgInvaderAnim.Attach(Image.Name.LargeInvader1);
            pLgInvaderAnim.Attach(Image.Name.LargeInvader2);

            // Add the Invader animations as timed events to the TimerManager
            TimerManager.Add(TimeEvent.Name.SmInvaderMarchAnimation, pSmInvaderAnim, levelSpeed);
            TimerManager.Add(TimeEvent.Name.MedInvaderMarchAnimation, pMedInvaderAnim, levelSpeed);
            TimerManager.Add(TimeEvent.Name.LgInvaderMarchAnimation, pLgInvaderAnim, levelSpeed);

            // Set up bomb animations
            AnimationSprite pBombZigZagAnim = new AnimationSprite(Sprite.Name.BombZigZag);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag1);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag2);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag3);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag4);

            AnimationSprite pBombDaggerAnim = new AnimationSprite(Sprite.Name.BombDagger);
            pBombDaggerAnim.Attach(Image.Name.BombDagger1);
            pBombDaggerAnim.Attach(Image.Name.BombDagger2);
            pBombDaggerAnim.Attach(Image.Name.BombDagger3);
            pBombDaggerAnim.Attach(Image.Name.BombDagger4);

            AnimationSprite pBombRollingAnim = new AnimationSprite(Sprite.Name.BombRolling);
            pBombRollingAnim.Attach(Image.Name.BombRolling1);
            pBombRollingAnim.Attach(Image.Name.BombRolling2);

            // Add the bomb animations as timed events to the TimerManager
            TimerManager.Add(TimeEvent.Name.BombAnimation, pBombZigZagAnim, 0.25f);
            TimerManager.Add(TimeEvent.Name.BombAnimation, pBombDaggerAnim, 0.25f);
            TimerManager.Add(TimeEvent.Name.BombAnimation, pBombRollingAnim, 0.25f);


            //---------------------------------------------------------------------------------------------------------
            // Set up TimedSoundEffects
            //---------------------------------------------------------------------------------------------------------
            TimedSoundEffects pGridMarchingSounds = new TimedSoundEffects();
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch4);
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch3);
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch2);
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch1);

            TimerManager.Add(TimeEvent.Name.GridMarchSoundEffects, pGridMarchingSounds, levelSpeed);

            //---------------------------------------------------------------------------------------------------------
            // Setting up the collisions for the player ship and missile
            //---------------------------------------------------------------------------------------------------------
            Ship pShip = ShipManager.ActivateShip();
            Missile pMissile = ShipManager.GetMissile();

            //---------------------------------------------------------------------------------------------------------
            // Add Collision Pairs and their Observers
            //---------------------------------------------------------------------------------------------------------
            // Applies to both players
            // ship vs left wall
            ColPair pShip_WallLeftColPair = ColPairManager.Add(ColPair.Name.Ship_WallLeft, pShip, pWallMan.GetWallLeft());
            Debug.Assert(pShip_WallLeftColPair != null);
            pShip_WallLeftColPair.Attach(new Ship_WallLeftObserver());
            // ship vs Right wall
            ColPair pShip_WallRightColPair = ColPairManager.Add(ColPair.Name.Ship_WallRight, pShip, pWallMan.GetWallRight());
            Debug.Assert(pShip_WallRightColPair != null);
            pShip_WallRightColPair.Attach(new Ship_WallRightObserver());
            // Missile vs Ceiling
            ColPair pMissile_CeilingColPair = ColPairManager.Add(ColPair.Name.Missile_Ceiling, pMissile, pWallMan.GetCeiling());
            Debug.Assert(pMissile_CeilingColPair != null);
            pMissile_CeilingColPair.Attach(new ShipMissileReadyObserver());
            pMissile_CeilingColPair.Attach(new ShipRemoveMissileObserver());

            //---------------------------------------------------------------------------------------------------------
            // Set up Random UFO Spawns
            //---------------------------------------------------------------------------------------------------------
            RandomlySpawnUFO pRandomUfoEvent = new RandomlySpawnUFO(30.0f, pMissile, pWallMan);
            TimerManager.Add(TimeEvent.Name.RandomlySpawnUFO, pRandomUfoEvent, 10.0f);


            //---------------------------------------------------------------------------------------------------------
            // Create Alien Grid and random bomb drops
            //---------------------------------------------------------------------------------------------------------
            BombFactory pBombFactory = new BombFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pGameManager.poPlayer1.pGrid, pWallMan);
            RandomlySpawnBomb pRandomBombEvent = new RandomlySpawnBomb(2.0f, pBombFactory);
            TimerManager.Add(TimeEvent.Name.RandomBombSpawn, pRandomBombEvent, 2.0f);

            // Applies to players seperately
            // Set up timing for Grid and UFO motion
            TimeBasedMover pGridMover = new TimeBasedMover(pGameManager.poPlayer1.pGrid);
            TimerManager.Add(TimeEvent.Name.TimedGridMover, pGridMover, levelSpeed);

            //---------------------------------------------------------------------------------------------------------
            // Create the Shields
            //---------------------------------------------------------------------------------------------------------

            // Applies to each player seperately
            // grid vs leftwall
            ColPair pGrid_WallLeftColPair = ColPairManager.Add(ColPair.Name.Grid_WallLeft, pGameManager.poPlayer1.pGrid, pWallMan.GetWallLeft());
            Debug.Assert(pGrid_WallLeftColPair != null);
            pGrid_WallLeftColPair.Attach(new Grid_WallLeftObserver());
            // Grid vs rightwall
            ColPair pGrid_WallRightcolPair = ColPairManager.Add(ColPair.Name.Grid_WallRight, pGameManager.poPlayer1.pGrid, pWallMan.GetWallRight());
            Debug.Assert(pGrid_WallRightcolPair != null);
            pGrid_WallRightcolPair.Attach(new Grid_WallRightObserver());
            // Grid vs ship
            ColPair pShip_GridColPair = ColPairManager.Add(ColPair.Name.Ship_Grid, pShip, pGameManager.poPlayer1.pGrid);
            Debug.Assert(pShip_GridColPair != null);
            pShip_GridColPair.Attach(new RemoveShipObserver());
            // Grid vs Missile
            ColPair pMissile_GridColPair = ColPairManager.Add(ColPair.Name.Missile_Grid, pMissile, pGameManager.poPlayer1.pGrid);
            Debug.Assert(pMissile_GridColPair != null);
            pMissile_GridColPair.Attach(new ShipRemoveMissileObserver());
            pMissile_GridColPair.Attach(new ShipMissileReadyObserver());
            pMissile_GridColPair.Attach(new RemoveInvaderObserver());
            pMissile_GridColPair.Attach(new AwardPointsObserver());
            // Grid vs Shields
            ColPair pGrid_ShieldZoneColPair = ColPairManager.Add(ColPair.Name.Grid_ShieldZone, pGameManager.poPlayer1.pGrid, pGameManager.poPlayer1.pShieldZone);
            pGrid_ShieldZoneColPair.Attach(new RemoveShieldBrickObserver());

            // Missile vs Shields
            ColPair pMissile_ShieldZoneColPair = ColPairManager.Add(ColPair.Name.Missile_ShieldZone, pMissile, pGameManager.poPlayer1.pShieldZone);
            pMissile_ShieldZoneColPair.Attach(new ShipMissileReadyObserver());
            pMissile_ShieldZoneColPair.Attach(new ShipRemoveMissileObserver());
            pMissile_ShieldZoneColPair.Attach(new RemoveShieldBrickObserver());


            // STORE Current State of managers into manager mementos
            pGameManager.poPlayer1.ArchiveManagerStates(pGameManager.pGame.GetTime());
        }

        protected virtual void PlayerTwoInit(GameManager pGameManager)
        {
            float levelSpeed = 0.75f / pGameManager.pActivePlayer.level;

            pGameManager.poPlayer2.pGrid = InvaderGridManager.GenerateGrid(pGameManager.pActivePlayer.level);
            //Debug.WriteLine("Generated Grid ({0}) for P2", pGameManager.poPlayer2.pGrid.GetHashCode());

            pGameManager.poPlayer2.pShieldZone = ShieldBuilder.CreateShieldZone(175, 300, 150, 4);

            //---------------------------------------------------------------------------------------------------------
            // Create GameSpace and Wall Objects
            //---------------------------------------------------------------------------------------------------------
            WallManager pWallMan = new WallManager();

            //Debug.WriteLine("WallLeft Consts x: {0}, y:{1}, width:{2}, height:{3}", Constants.leftWallXPos, Constants.leftWallYPos, Constants.leftWallWidth, Constants.leftWallHeight);
            //Debug.WriteLine("WallRight Consts x: {0}, y:{1}, width:{2}, height:{3}", Constants.rightWallXPos, Constants.rightWallYPos, Constants.rightWallWidth, Constants.rightWallHeight);
            //Debug.WriteLine("Ceiling Consts x: {0}, y:{1}, width:{2}, height:{3}", Constants.ceilingXPos, Constants.ceilingYPos, Constants.ceilingWidth, Constants.ceilingHeight);
            //Debug.WriteLine("Floor Consts x: {0}, y:{1}, width:{2}, height:{3}", Constants.floorXPos, Constants.floorYPos, Constants.floorWidth, Constants.floorHeight);

            //---------------------------------------------------------------------------------------------------------
            // Set up Sprite Animations
            //---------------------------------------------------------------------------------------------------------
            // Set up animations for the 3 basic Invaders
            AnimationSprite pSmInvaderAnim = new AnimationSprite(Sprite.Name.SmallInvader);
            pSmInvaderAnim.Attach(Image.Name.SmallInvader1);
            pSmInvaderAnim.Attach(Image.Name.SmallInvader2);

            AnimationSprite pMedInvaderAnim = new AnimationSprite(Sprite.Name.MediumInvader);
            pMedInvaderAnim.Attach(Image.Name.MediumInvader1);
            pMedInvaderAnim.Attach(Image.Name.MediumInvader2);

            AnimationSprite pLgInvaderAnim = new AnimationSprite(Sprite.Name.LargeInvader);
            pLgInvaderAnim.Attach(Image.Name.LargeInvader1);
            pLgInvaderAnim.Attach(Image.Name.LargeInvader2);

            // Add the Invader animations as timed events to the TimerManager
            TimerManager.Add(TimeEvent.Name.SmInvaderMarchAnimation, pSmInvaderAnim, levelSpeed);
            TimerManager.Add(TimeEvent.Name.MedInvaderMarchAnimation, pMedInvaderAnim, levelSpeed);
            TimerManager.Add(TimeEvent.Name.LgInvaderMarchAnimation, pLgInvaderAnim, levelSpeed);

            // Set up bomb animations
            AnimationSprite pBombZigZagAnim = new AnimationSprite(Sprite.Name.BombZigZag);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag1);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag2);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag3);
            pBombZigZagAnim.Attach(Image.Name.BombZigZag4);

            AnimationSprite pBombDaggerAnim = new AnimationSprite(Sprite.Name.BombDagger);
            pBombDaggerAnim.Attach(Image.Name.BombDagger1);
            pBombDaggerAnim.Attach(Image.Name.BombDagger2);
            pBombDaggerAnim.Attach(Image.Name.BombDagger3);
            pBombDaggerAnim.Attach(Image.Name.BombDagger4);

            AnimationSprite pBombRollingAnim = new AnimationSprite(Sprite.Name.BombRolling);
            pBombRollingAnim.Attach(Image.Name.BombRolling1);
            pBombRollingAnim.Attach(Image.Name.BombRolling2);

            // Add the bomb animations as timed events to the TimerManager
            TimerManager.Add(TimeEvent.Name.BombAnimation, pBombZigZagAnim, 0.25f);
            TimerManager.Add(TimeEvent.Name.BombAnimation, pBombDaggerAnim, 0.25f);
            TimerManager.Add(TimeEvent.Name.BombAnimation, pBombRollingAnim, 0.25f);


            //---------------------------------------------------------------------------------------------------------
            // Set up TimedSoundEffects
            //---------------------------------------------------------------------------------------------------------
            TimedSoundEffects pGridMarchingSounds = new TimedSoundEffects();
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch4);
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch3);
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch2);
            pGridMarchingSounds.Attach(Sound.Name.InvaderMarch1);

            TimerManager.Add(TimeEvent.Name.GridMarchSoundEffects, pGridMarchingSounds, levelSpeed);

            //---------------------------------------------------------------------------------------------------------
            // Setting up the collisions for the player ship and missile
            //---------------------------------------------------------------------------------------------------------
            Ship pShip = ShipManager.ActivateShip();
            Missile pMissile = ShipManager.GetMissile();

            //---------------------------------------------------------------------------------------------------------
            // Add Collision Pairs and their Observers
            //---------------------------------------------------------------------------------------------------------
            // Applies to both players
            // ship vs left wall
            ColPair pShip_WallLeftColPair = ColPairManager.Add(ColPair.Name.Ship_WallLeft, pShip, pWallMan.GetWallLeft());
            Debug.Assert(pShip_WallLeftColPair != null);
            pShip_WallLeftColPair.Attach(new Ship_WallLeftObserver());
            // ship vs Right wall
            ColPair pShip_WallRightColPair = ColPairManager.Add(ColPair.Name.Ship_WallRight, pShip, pWallMan.GetWallRight());
            Debug.Assert(pShip_WallRightColPair != null);
            pShip_WallRightColPair.Attach(new Ship_WallRightObserver());
            // Missile vs Ceiling
            ColPair pMissile_CeilingColPair = ColPairManager.Add(ColPair.Name.Missile_Ceiling, pMissile, pWallMan.GetCeiling());
            Debug.Assert(pMissile_CeilingColPair != null);
            pMissile_CeilingColPair.Attach(new ShipMissileReadyObserver());
            pMissile_CeilingColPair.Attach(new ShipRemoveMissileObserver());

            //---------------------------------------------------------------------------------------------------------
            // Set up Random UFO Spawns
            //---------------------------------------------------------------------------------------------------------
            RandomlySpawnUFO pRandomUfoEvent = new RandomlySpawnUFO(30.0f, pMissile, pWallMan);
            TimerManager.Add(TimeEvent.Name.RandomlySpawnUFO, pRandomUfoEvent, 10.0f);


            //---------------------------------------------------------------------------------------------------------
            // Create Alien Grid and random bomb drops
            //---------------------------------------------------------------------------------------------------------
            BombFactory pBombFactory = new BombFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pGameManager.poPlayer2.pGrid, pWallMan);
            RandomlySpawnBomb pRandomBombEvent = new RandomlySpawnBomb(2.0f, pBombFactory);
            TimerManager.Add(TimeEvent.Name.RandomBombSpawn, pRandomBombEvent, 2.0f);

            // Applies to players seperately
            // Set up timing for Grid and UFO motion
            TimeBasedMover pGridMover = new TimeBasedMover(pGameManager.poPlayer2.pGrid);
            TimerManager.Add(TimeEvent.Name.TimedGridMover, pGridMover, levelSpeed);

            //---------------------------------------------------------------------------------------------------------
            // Create the Shields
            //---------------------------------------------------------------------------------------------------------

            // Applies to each player seperately
            // grid vs leftwall
            ColPair pGrid_WallLeftColPair = ColPairManager.Add(ColPair.Name.Grid_WallLeft, pGameManager.poPlayer2.pGrid, pWallMan.GetWallLeft());
            Debug.Assert(pGrid_WallLeftColPair != null);
            pGrid_WallLeftColPair.Attach(new Grid_WallLeftObserver());
            // Grid vs rightwall
            ColPair pGrid_WallRightcolPair = ColPairManager.Add(ColPair.Name.Grid_WallRight, pGameManager.poPlayer2.pGrid, pWallMan.GetWallRight());
            Debug.Assert(pGrid_WallRightcolPair != null);
            pGrid_WallRightcolPair.Attach(new Grid_WallRightObserver());
            // Grid vs ship
            ColPair pShip_GridColPair = ColPairManager.Add(ColPair.Name.Ship_Grid, pShip, pGameManager.poPlayer2.pGrid);
            Debug.Assert(pShip_GridColPair != null);
            pShip_GridColPair.Attach(new RemoveShipObserver());
            // Grid vs Missile
            ColPair pMissile_GridColPair = ColPairManager.Add(ColPair.Name.Missile_Grid, pMissile, pGameManager.poPlayer2.pGrid);
            Debug.Assert(pMissile_GridColPair != null);
            pMissile_GridColPair.Attach(new ShipRemoveMissileObserver());
            pMissile_GridColPair.Attach(new ShipMissileReadyObserver());
            pMissile_GridColPair.Attach(new RemoveInvaderObserver());
            pMissile_GridColPair.Attach(new AwardPointsObserver());
            // Grid vs Shields
            ColPair pGrid_ShieldZoneColPair = ColPairManager.Add(ColPair.Name.Grid_ShieldZone, pGameManager.poPlayer2.pGrid, pGameManager.poPlayer2.pShieldZone);
            pGrid_ShieldZoneColPair.Attach(new RemoveShieldBrickObserver());

            // Missile vs Shields
            ColPair pMissile_ShieldZoneColPair = ColPairManager.Add(ColPair.Name.Missile_ShieldZone, pMissile, pGameManager.poPlayer2.pShieldZone);
            pMissile_ShieldZoneColPair.Attach(new ShipMissileReadyObserver());
            pMissile_ShieldZoneColPair.Attach(new ShipRemoveMissileObserver());
            pMissile_ShieldZoneColPair.Attach(new RemoveShieldBrickObserver());


            // STORE Current State of managers into manager mementos
            pGameManager.poPlayer2.ArchiveManagerStates(pGameManager.pGame.GetTime());
        }
    }
}
