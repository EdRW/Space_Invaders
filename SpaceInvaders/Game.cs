using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        public override void Initialize()
        {
            this.SetWindowName("ED Codes Space Invaders");
            this.SetWidthHeight(Constants.screenWidth, Constants.screenHeight);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Create all the managers!
            //---------------------------------------------------------------------------------------------------------
            
            TextureManager.Create();
            ImageManager.Create();
            SpriteManager.Create();
            SoundManager.Create();
            BoxSpriteManager.Create();
            SpriteBatchManager.Create();
            TimerManager.Create();
            ProxySpriteManager.Create();
            ProxyBoxSpriteManager.Create();
            GameObjectManager.Create();
            ColPairManager.Create();
            GlyphManager.Create();
            FontManager.Create();

            //---------------------------------------------------------------------------------------------------------
            // Add the Sound Assets
            //---------------------------------------------------------------------------------------------------------
            SoundManager.Add(Sound.Name.InvaderMarch1, 0.05f);
            SoundManager.Add(Sound.Name.InvaderMarch2, 0.05f);
            SoundManager.Add(Sound.Name.InvaderMarch3, 0.05f);
            SoundManager.Add(Sound.Name.InvaderMarch4, 0.05f);

            SoundManager.Add(Sound.Name.Invaderkilled, 0.025f);

            SoundManager.Add(Sound.Name.Shoot, 0.025f);

            SoundManager.Add(Sound.Name.UFOHighPitch);
            SoundManager.Add(Sound.Name.UFOLowPitch, 0.01f);

            SoundManager.Add(Sound.Name.Uninitialized);

            //---------------------------------------------------------------------------------------------------------
            // Add the Texture and all the Images
            //---------------------------------------------------------------------------------------------------------
            TextureManager.Add(Texture.Name.Invader);
            Texture pTexture = TextureManager.Add(Texture.Name.Consolas36pt);

            FontManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            ImageManager.Add(Image.Name.ShieldBrick);
            ImageManager.Add(Image.Name.Ship);
            ImageManager.Add(Image.Name.Missile);
            ImageManager.Add(Image.Name.SmallInvader1);
            ImageManager.Add(Image.Name.SmallInvader2);
            ImageManager.Add(Image.Name.MediumInvader1);
            ImageManager.Add(Image.Name.MediumInvader2);
            ImageManager.Add(Image.Name.LargeInvader1);
            ImageManager.Add(Image.Name.LargeInvader2);
            ImageManager.Add(Image.Name.UFO);

            ImageManager.Add(Image.Name.BombPlain);

            ImageManager.Add(Image.Name.BombZigZag1);
            ImageManager.Add(Image.Name.BombZigZag2);
            ImageManager.Add(Image.Name.BombZigZag3);
            ImageManager.Add(Image.Name.BombZigZag4);

            ImageManager.Add(Image.Name.BombDagger1);
            ImageManager.Add(Image.Name.BombDagger2);
            ImageManager.Add(Image.Name.BombDagger3);
            ImageManager.Add(Image.Name.BombDagger4);

            ImageManager.Add(Image.Name.BombRolling1);
            ImageManager.Add(Image.Name.BombRolling2);
            ImageManager.Add(Image.Name.BombRolling3);

            ImageManager.Add(Image.Name.InvaderDeath1);
            ImageManager.Add(Image.Name.InvaderDeath2);
            ImageManager.Add(Image.Name.UFODeath);
            ImageManager.Add(Image.Name.ShipDeath1);
            ImageManager.Add(Image.Name.ShipDeath2);
            ImageManager.Add(Image.Name.BombDeath);

            //---------------------------------------------------------------------------------------------------------
            // Add all the Sprites and BoxSprites (x,y pos not needed since its determined by factory upon object creation)
            //---------------------------------------------------------------------------------------------------------
            SpriteManager.Add(Sprite.Name.ShieldBrick, Image.Name.ShieldBrick, 0, 0, Constants.shieldBrickWidth, Constants.shieldBrickHeight, new Azul.Color(0.0f, 0.5f, 0.0f));
            SpriteManager.Add(Sprite.Name.Ship, Image.Name.Ship, 0, 0, Constants.shipWidth, Constants.shipHeight);
            SpriteManager.Add(Sprite.Name.Missile, Image.Name.Missile, 0, 0, Constants.missileWidth, Constants.missileHeight);
            SpriteManager.Add(Sprite.Name.SmallInvader, Image.Name.SmallInvader1, 0, 0, Constants.smallInvaderWidth, Constants.smallInvaderHeight, new Azul.Color(0.5f, 0.0f, 0.5f));
            SpriteManager.Add(Sprite.Name.MediumInvader, Image.Name.MediumInvader1, 0, 0, Constants.mediumInvaderWidth, Constants.mediumInvaderHeight, new Azul.Color(0.3f, 0.0f, 0.75f));
            SpriteManager.Add(Sprite.Name.LargeInvader, Image.Name.LargeInvader1, 0, 0, Constants.largeInvaderWidth, Constants.largeInvaderHeight, new Azul.Color(0.25f, 0.0f, 1.0f));
            SpriteManager.Add(Sprite.Name.UFO, Image.Name.UFO, 0, 0, Constants.UFOWidth, Constants.UFOHeight, new Azul.Color(0.75f, 0.2f, 0.2f));

            SpriteManager.Add(Sprite.Name.BombPlain, Image.Name.BombPlain, 0, 0, Constants.bombPlainWidth, Constants.bombPlainHeight);
            SpriteManager.Add(Sprite.Name.BombZigZag, Image.Name.BombZigZag1, 0, 0, Constants.bombZigZagWidth, Constants.bombZigZagHeight);
            SpriteManager.Add(Sprite.Name.BombDagger, Image.Name.BombDagger1, 0, 0, Constants.bombDaggerWidth, Constants.bombDaggerHeight);
            SpriteManager.Add(Sprite.Name.BombRolling, Image.Name.BombRolling1, 0, 0, Constants.bombRollingWidth1, Constants.bombRollingHeight1);

            SpriteManager.Add(Sprite.Name.InvaderDeath, Image.Name.InvaderDeath1, -50, -50, Constants.largeInvaderWidth, Constants.largeInvaderHeight, new Azul.Color(0.5f, 0.5f, 0.1f));
            SpriteManager.Add(Sprite.Name.BombDeath, Image.Name.BombDeath, -50, -50, Constants.bombDaggerWidth, Constants.bombDaggerHeight, new Azul.Color(0.75f, 0.1f, 0.1f));

            // Add BoxSprites (size of box gets determined by GameObject's ColObject)
            BoxSpriteManager.Add(BoxSprite.Name.ShipBox, new Azul.Color(0.25f, 1.0f, 0.5f));
            BoxSpriteManager.Add(BoxSprite.Name.MissileBox, new Azul.Color(0.25f, 1.0f, 0.5f));
            BoxSpriteManager.Add(BoxSprite.Name.SmallInvaderBox, new Azul.Color(1.0f, 0.0f, 0.0f));
            BoxSpriteManager.Add(BoxSprite.Name.MediumInvaderBox, new Azul.Color(1.0f, 0.0f, 0.0f));
            BoxSpriteManager.Add(BoxSprite.Name.LargeInvaderBox, new Azul.Color(1.0f, 0.0f, 0.0f));
            BoxSpriteManager.Add(BoxSprite.Name.InvaderColumnBox, new Azul.Color(0.0f, 1.0f, 0.0f));
            BoxSpriteManager.Add(BoxSprite.Name.InvaderGridBox, new Azul.Color(0.25f, 0.25f, 1.0f));
            BoxSpriteManager.Add(BoxSprite.Name.UFOBox, new Azul.Color(1.0f, 0.0f, 1.0f));
            BoxSpriteManager.Add(BoxSprite.Name.WallBox, new Azul.Color(1.0f, 1.0f, 0.0f));
            BoxSpriteManager.Add(BoxSprite.Name.GameSpaceBox, new Azul.Color(1.0f, 1.0f, 1.0f));
            BoxSpriteManager.Add(BoxSprite.Name.ShieldBrickBox, new Azul.Color(0.0f, 0.0f, 0.0f));
            BoxSpriteManager.Add(BoxSprite.Name.ShieldColumnBox, new Azul.Color(0.25f, 0.25f, 0.75f));
            BoxSpriteManager.Add(BoxSprite.Name.ShieldBox, new Azul.Color(0.25f, 0.75f, 0.25f));
            BoxSpriteManager.Add(BoxSprite.Name.ShieldZoneBox, new Azul.Color(0.25f, 0.25f, 1.0f));
            BoxSpriteManager.Add(BoxSprite.Name.BombBox, new Azul.Color(0.25f, 0.25f, 1.0f));

            // Create batches for group processing and rendering
            SpriteBatchManager.Add(SpriteBatch.Name.Texts);
            SpriteBatchManager.Add(SpriteBatch.Name.Boxes);
            SpriteBatchManager.Add(SpriteBatch.Name.Sprites);
            SpriteBatch pSBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pSBatch.enabled = false;

            pSBatch = SpriteBatchManager.Add(SpriteBatch.Name.Deaths);
            pSBatch.Attach(SpriteManager.Find(Sprite.Name.BombDeath));
            pSBatch.Attach(SpriteManager.Find(Sprite.Name.InvaderDeath));

            //---------------------------------------------------------------------------------------------------------
            // Create Header Fonts
            //---------------------------------------------------------------------------------------------------------

            Font pScoreHeader1 = FontManager.Add(Font.Name.ScoreHeader1, SpriteBatch.Name.Texts, " SCORE<1> ", Glyph.Name.Consolas36pt, 100, 1000);
            Font pScoreHeader2 = FontManager.Add(Font.Name.ScoreHeader2, SpriteBatch.Name.Texts, " SCORE<2> ", Glyph.Name.Consolas36pt, 650, 1000);
            Font HighScoreHeader = FontManager.Add(Font.Name.HighScoreHeader, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 400, 1000);
            FontManager.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, pScoreHeader1.pFontSprite.x + 30, 960);
            FontManager.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, pScoreHeader2.pFontSprite.x + 30, 960);
            FontManager.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, HighScoreHeader.pFontSprite.x + 30, 960);

            Font pLives = FontManager.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "0", Glyph.Name.Consolas36pt, 50, 100);
            Font pCredits = FontManager.Add(Font.Name.Credits, SpriteBatch.Name.Texts, "CREDITS  00", Glyph.Name.Consolas36pt, 650, 100);

            GameManager.Create(this);
            GameManager.SetGameState(GameManager.State.AttractScreen);
            GameManager.Initialize();

            // Setup toggle boxes input observer
            InputSubject pInputSubject = InputManager.GetBSubject();
            pInputSubject.Attach(new ToggleSpriteBoxObserver());
        }

        public override void Update()
        {
            GameManager.Update();
        }

        public override void Draw()
        {
            GameManager.Draw();
        }

        public override void UnLoadContent()
        {

        }
    }
}

