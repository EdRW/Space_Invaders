using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameManager
    {
        private static GameManager pInstance = null;

        private GameState poAttractScreenState;
        private GameState poSelectScreenState;
        private GameState poGamePlayState;
        private GameState poGamerOverState;

        private GameModeStrategy poOnePlayerStrategy;
        private GameModeStrategy poTwoPlayerStrategy;

        public PlayerArtifact poPlayer1;
        public PlayerArtifact poPlayer2;

        public Azul.Game pGame;
        public GameState pGameState;
        public GameModeStrategy pActiveGameModeStrategy;
        public PlayerArtifact pActivePlayer;

        public int highScore;
        public Mode gameMode;

        public bool levelUpFlag;

        public enum Mode
        {
            OnePlayerMode,
            TwoPlayerMode
        }

        public enum State
        {
            AttractScreen,
            SelectScreen,
            GamePlay,
            GameOver
        }

        private GameManager()
        {
            this.poAttractScreenState = new AttractScreenState();
            this.poSelectScreenState = new SelectScreenState();
            this.poGamePlayState = new GamePlayState();
            this.poGamerOverState = new GameOverState();

            this.poOnePlayerStrategy = new OnePlayerStrategy();
            this.poTwoPlayerStrategy = new TwoPlayerStrategy();

            this.poPlayer1 = new PlayerArtifact(PlayerArtifact.Name.PlayerOne);
            this.poPlayer2 = new PlayerArtifact(PlayerArtifact.Name.PlayerTwo);

            this.pGame = null;
            this.pGameState = null;
            this.pActiveGameModeStrategy = null;
            this.pActivePlayer = null;

            this.highScore = 0;
            this.gameMode = Mode.OnePlayerMode;
            this.levelUpFlag = false;
        }


        public static void Create(Azul.Game pGame)
        {
            // make sure its the first time
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameManager();
            }

            Debug.Assert(pInstance != null);
            pInstance.pGame = pGame;

            PushPlayerScoresToFonts();
        }

        // Should be called at some point after enter a new state
        public static void Initialize()
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);
            pMan.pGameState.Initialize(pMan);
        }

        // Should be called at some point before leaving state
        public static void CleanUp()
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);
            pMan.pGameState.CleanUp(pMan);
        }

        // Should be called during the Update part of gameloop
        public static void Update()
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);
            pMan.pGameState.Update(pMan);
        }

        // Should be called during the Draw part of gameloop
        public static void Draw()
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);
            pMan.pGameState.Draw(pMan);
        }

        public void SetActivePlayer(PlayerArtifact.Name name)
        {
            //GameManager pMan = GameManager.PrivInstance();
            //Debug.Assert(pMan != null);

            if (name == PlayerArtifact.Name.PlayerOne)
            {
                Debug.Assert(this.poPlayer1 != null);
                this.pActivePlayer = this.poPlayer1;;
            }
            else
            {
                Debug.Assert(this.poPlayer2 != null);
                this.pActivePlayer = this.poPlayer2;
            }
        }

        public static void SetActiveGameMode(Mode mode)
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);

            if (mode == Mode.OnePlayerMode)
            {
                Debug.Assert(pMan.poOnePlayerStrategy != null);
                pMan.pActiveGameModeStrategy = pMan.poOnePlayerStrategy;
            }
            else
            {
                Debug.Assert(pMan.poTwoPlayerStrategy != null);
                pMan.pActiveGameModeStrategy = pMan.poTwoPlayerStrategy;
            }

            pMan.gameMode = mode;
        }

        public static void AwardPoints(int points)
        {
            Debug.Assert(points > 0);

            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);
            

            pMan.pActivePlayer.score += points;
            if (pMan.pActivePlayer.score > pMan.highScore) pMan.highScore = pMan.pActivePlayer.score;

            PushPlayerScoresToFonts();
        }

        public static void SetGameState(GameManager.State state)
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);

            switch (state)
            {
                case GameManager.State.AttractScreen:
                    Debug.Assert(pMan.poSelectScreenState != null);
                    pMan.pGameState = pMan.poAttractScreenState;
                    //Debug.WriteLine("Game has been set to AttractScreen State");
                    break;
                case GameManager.State.SelectScreen:
                    Debug.Assert(pMan.poSelectScreenState != null);
                    pMan.pGameState = pMan.poSelectScreenState;
                    //Debug.WriteLine("Game has been set to SelectScreen State");
                    break;
                case GameManager.State.GamePlay:
                    Debug.Assert(pMan.poGamePlayState != null);
                    pMan.pGameState = pMan.poGamePlayState;
                    //Debug.WriteLine("Game has been set to GamePlay State");
                    break;
                case GameManager.State.GameOver:
                    Debug.Assert(pMan.poGamerOverState != null);
                    pMan.pGameState = pMan.poGamerOverState;
                    //Debug.WriteLine("Game has been set to GameOver State");
                    break;
            }
        }

        public static void PushPlayerScoresToFonts()
        {
            Font pScore1 = FontManager.Find(Font.Name.Score1);
            Font pScore2 = FontManager.Find(Font.Name.Score2);

            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);

            pScore1.UpdateMessage(pMan.poPlayer1.score.ToString("D4"));
            pScore2.UpdateMessage(pMan.poPlayer2.score.ToString("D4"));
        }
        public static void PushHighScoreToFont()
        {
            Font pHighScore = FontManager.Find(Font.Name.HighScore);

            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);

            pHighScore.UpdateMessage(pMan.highScore.ToString("D4"));
        }

        public void HandleEndOfPlayerTurn()
        {
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);

            if (ShipManager.GetShip().bMarkForDeath == true)
            {
                pMan.pActiveGameModeStrategy.EndPlayerTurn(this);
                JanitorDutyHack();
            }
            else if (pMan.levelUpFlag == true)
            {
                pMan.levelUpFlag = false;
                pMan.pActiveGameModeStrategy.ReinitializeLevel(this);
                JanitorDutyHack();
            }
        }

        public static void LevelUp()
        {
            Debug.WriteLine("YAY! LEVEL UP!");
            GameManager pMan = GameManager.PrivInstance();
            Debug.Assert(pMan != null);

            pMan.levelUpFlag = true;
            pMan.pActivePlayer.level++;
        }


        private static void JanitorDutyHack()
        {
            Sprite pSprite = SpriteManager.Find(Sprite.Name.InvaderDeath);
            pSprite.x = -50;
            pSprite.y = -50;
            pSprite.Update();
            pSprite = SpriteManager.Find(Sprite.Name.BombDeath);
            pSprite.x = -50;
            pSprite.y = -50;
            pSprite.Update();
        }
        private static GameManager PrivInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
