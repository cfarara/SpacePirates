using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpacePirates.Model
{
    /// <summary>
    ///     Manages the entire game.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int LevelOneAmount = 2;
        private const int LevelTwoAmount = 4;
        private const int LevelThreeAmount = 6;
        private const int LevelFourAmount = 8;

        private const int LeftEdgeCanvas = 0;

        private const int TickInterval = 30;
        private const int EnemyShipAnimationInterval = 100;

        private const int EnemyShipsSpeed = 3;
        private const int PlayerBulletSpeed = 15;
        private const int EnemyBulletSpeed = 5;

        private const int EnemyShipGameMovement = 20;
        private const int CenterOffset = 80;

        private const int LevelOneEnemyShipScore = 100;
        private const int LevelTwoEnemyShipScore = 200;
        private const int LevelThreeEnemyShipScore = 300;
        private const int LevelFourEnemyShipScore = 400;

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        /// <summary>
        /// Levels to the Space Invaders Game.
        /// </summary>
        public enum Levels { LevelOne = 1, LevelTwo, LevelThree, LevelFour };

        private ScoreBlock scoreBlock;
        private ScoreBlock levelBlock;
        private GameOverBlock gameOverBlock;

        private readonly Random random;

        private int randomNumber;
        private int score;

        private List<Life> playerLives;
        private List<EnemyShip> allEnemyShips;
        private List<Bullet> playerBullets;
        private List<Bullet> enemyBullets;

        private PlayerShip playerShip;

        private readonly TimeSpan gameTickInterval = new TimeSpan(0, 0, 0, 0, TickInterval);
        private readonly TimeSpan fireEnemyBulletsTickInterval;
        private readonly TimeSpan enemyShipAnimationInterval = new TimeSpan(0, 0, 0, 0, EnemyShipAnimationInterval);

        private DispatcherTimer gameTimer;
        private DispatcherTimer fireEnemyBulletsTimer;
        private DispatcherTimer enemyShipAnimationTimer;

        private int enemyShipsMovement;

        private int enemyShipTickCount;
        private int playerBulletTickCount;
        private int enemyBulletTickCount;
        private int collisionTickCount;
        private int enemyShipAnimationTickCount;

        private Canvas theCanvas;

        /// <summary>
        /// The current game level
        /// </summary>
        public Levels Level { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0 AND backgroundWidth > 0
        /// </summary>
        /// <param name="backgroundHeight">The backgroundHeight of the game play window.</param>
        /// <param name="backgroundWidth">The backgroundWidth of the game play window.</param>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }
            this.theCanvas = new Canvas();
            this.Level = Levels.LevelOne;

            this.random = new Random();

            this.fireEnemyBulletsTickInterval = new TimeSpan(0, 0, 0, 0, 3000);

            this.playerLives = new List<Life>();
            this.allEnemyShips = new List<EnemyShip>();
            this.playerBullets = new List<Bullet>();
            this.enemyBullets = new List<Bullet>();

            this.enemyShipsMovement = EnemyShipsSpeed;
            this.playerBulletTickCount = 0;
            this.enemyBulletTickCount = EnemyBulletSpeed;
            this.collisionTickCount = 0;

            this.score = 0;

            this.createAndStartGameTimer();
            this.createAndStartEnemyBulletFireTimer();
            this.createAndStartEnemyShipAnimationTimer();

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
        }

        #endregion

        #region Methods

        private void createAndStartEnemyShipAnimationTimer()
        {
            this.enemyShipAnimationTimer = new DispatcherTimer();
            this.enemyShipAnimationTimer.Interval = this.enemyShipAnimationInterval;
            this.enemyShipAnimationTimer.Tick += this.animateEnemyShipsOnTick;
            this.enemyShipAnimationTimer.Start();
        }

        private void createAndStartEnemyBulletFireTimer()
        {
            this.fireEnemyBulletsTimer = new DispatcherTimer();
            this.fireEnemyBulletsTimer.Interval = this.fireEnemyBulletsTickInterval;
            this.fireEnemyBulletsTimer.Tick += this.fireEnemyBulletsOnTick;
            this.fireEnemyBulletsTimer.Start();
        }

        private void createAndStartGameTimer()
        {
            this.gameTimer = new DispatcherTimer();
            this.gameTimer.Interval = this.gameTickInterval;
            this.gameTimer.Tick += this.timerOnTick;
            this.gameTimer.Start();
        }

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="background">The background canvas.</param>
        public void InitializeGame(Canvas background)
        {
            this.theCanvas = background ?? throw new ArgumentNullException(nameof(this.theCanvas));
            this.ClearCanvas();
            this.CreateAndPlacePlayerShip();
            this.CreateEnemyShips(this.Level);
            this.PlaceEnemyShipsOnCanvasAsPyramid(this.Level);
            this.createAndPlaceScoreBox(this.theCanvas);
            this.CreateAndPlaceLevelBox();
            this.CreateAndPlacePlayerLives();
        }

        /// <summary>
        /// Resets the Space Invaders Game.
        /// </summary>
        /// <param name="background"></param>
        public void ResetGame(Canvas background)
        {
            this.theCanvas = background ?? throw new ArgumentNullException(nameof(this.theCanvas));
            this.ClearCanvas();
            this.Level = Levels.LevelOne;
            this.playerLives = new List<Life>();
            this.allEnemyShips = new List<EnemyShip>();
            this.playerBullets = new List<Bullet>();
            this.enemyBullets = new List<Bullet>();
            this.CreateAndPlacePlayerShip();
            this.CreateEnemyShips(this.Level);
            this.PlaceEnemyShipsOnCanvasAsPyramid(this.Level);
            this.createAndPlaceScoreBox(this.theCanvas);
            this.CreateAndPlaceLevelBox();
            this.CreateAndPlacePlayerLives();
            this.ResumeTimers();
        }

        /// <summary>
        /// Creates the Level Box label and places it on the canvas.
        /// </summary>
        public void CreateAndPlaceLevelBox()
        {
            this.levelBlock = new ScoreBlock();
            this.levelBlock.SetText("Level: " + (int)this.Level);
            this.theCanvas.Children.Add(this.levelBlock.Sprite);
            this.levelBlock.X = (this.backgroundWidth / 2) - 50;
            this.levelBlock.Y = 0;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            this.theCanvas.Children.Clear();
        }

        /// <summary>
        /// Create the player life hearts and place them on the canvas.
        /// </summary>
        public void CreateAndPlacePlayerLives()
        {
            Life life = null;
            if (this.Level == Levels.LevelOne)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (this.playerLives.Count <= 0)
                    {
                        life = new Life();
                        this.theCanvas.Children.Add(life.Sprite);
                        life.X = 565;
                        life.Y = 0;
                        this.playerLives.Add(life);
                    }
                    else
                    {
                        double nextLifeX = life.X + 25;
                        life = new Life();
                        this.theCanvas.Children.Add(life.Sprite);
                        life.X = nextLifeX;
                        life.Y = 0;
                        this.playerLives.Add(life);
                    }
                }
            }
            else
            {
                foreach (var currLife in this.playerLives)
                {
                    this.theCanvas.Children.Add(currLife.Sprite);
                    
                }
            }
        }

        private void createAndPlaceScoreBox(Canvas background)
        {
            if (background == null)
            {
                throw new ArgumentNullException(nameof(background));
            }
            this.scoreBlock = new ScoreBlock();
            this.scoreBlock.SetText("Score: " + this.score);
            background.Children.Add(this.scoreBlock.Sprite);
            this.scoreBlock.X = 0;
            this.scoreBlock.Y = 0;
        }

        /// <summary>
        /// Creates enemy ships based on the current game level.
        /// </summary>
        /// <param name="background"></param>
        /// <param name="currentLevel"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CreateEnemyShips(Levels currentLevel)
        {
            if (currentLevel == Levels.LevelOne)
            {
                this.createLevelOneEnemyShips();
            }
            else if (currentLevel == Levels.LevelTwo)
            {
                this.createLevelOneEnemyShips();
                this.createLevelTwoEnemyShips();
            }
            else if (currentLevel == Levels.LevelThree)
            {
                this.createLevelOneEnemyShips();
                this.createLevelTwoEnemyShips();
                this.createLevelThreeEnemyShips();
            }
            else if (currentLevel == Levels.LevelFour)
            {
                this.createLevelOneEnemyShips();
                this.createLevelTwoEnemyShips();
                this.createLevelThreeEnemyShips();
                this.createLevelFourenemyShips();
            }
        }

        private void createLevelFourenemyShips()
        {
            for (var i = 0; i < LevelFourAmount; i++)
            {
                EnemyShip levelFourEnemyShip = new LevelFourEnemyShip();
                this.allEnemyShips.Add(levelFourEnemyShip);
            }
        }

        private void createLevelThreeEnemyShips()
        {
            for (var i = 0; i < LevelThreeAmount; i++)
            {
                EnemyShip levelThreeEnemyShip = new LevelThreeEnemyShip();
                this.allEnemyShips.Add(levelThreeEnemyShip);
            }
        }

        private void createLevelTwoEnemyShips()
        {
            for (var i = 0; i < LevelTwoAmount; i++)
            {
                EnemyShip levelTwoEnemyShip = new LevelTwoEnemyShip();
                this.allEnemyShips.Add(levelTwoEnemyShip);
            }
        }

        private void createLevelOneEnemyShips()
        {
            for (var i = 0; i < LevelOneAmount; i++)
            {
                EnemyShip levelOneEnemyShip = new LevelOneEnemyShip();
                this.allEnemyShips.Add(levelOneEnemyShip);
            }
        }

        /// <summary>
        /// Places all enemy ships on the canvas according to level.
        /// </summary>
        /// <param name="background"></param>
        /// <param name="currentLevel"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void PlaceEnemyShipsOnCanvasAsPyramid(Levels currentLevel)
        {
            if (!(this.allEnemyShips.Count <= 0))
            {
                if (currentLevel == Levels.LevelOne)
                {
                    this.placeLevelOneEnemyShips();
                }
                else if (currentLevel == Levels.LevelTwo)
                {
                    this.placeLevelOneEnemyShips();
                    this.placeLevelTwoEnemyShips();

                }
                else if (currentLevel == Levels.LevelThree)
                {
                    this.placeLevelOneEnemyShips();
                    this.placeLevelTwoEnemyShips();
                    this.placeLevelThreeEnemyShips();
                }
                else if (currentLevel == Levels.LevelFour)
                {
                    this.placeLevelOneEnemyShips();
                    this.placeLevelTwoEnemyShips();
                    this.placeLevelThreeEnemyShips();
                    this.placeLevelFourEnemyShips();
                }
            }
        }

        private void placeLevelOneEnemyShips()
        {
            double shipOffset = 0;

            foreach (var levelOneEnemyShip in this.allEnemyShips)
            {
                if (!(levelOneEnemyShip is LevelOneEnemyShip))
                {
                    continue;
                }
                this.theCanvas.Children.Add(levelOneEnemyShip.Sprite);
                levelOneEnemyShip.X = this.backgroundWidth / 2 - CenterOffset + shipOffset;
                levelOneEnemyShip.Y = this.backgroundHeight - 220;
                shipOffset += levelOneEnemyShip.Width + 10;
            }
        }

        private void placeLevelTwoEnemyShips()
        {
            double shipOffset = 0;

            foreach (var levelTwoEnemyShip in this.allEnemyShips)
            {
                if (!(levelTwoEnemyShip is LevelTwoEnemyShip))
                {
                    continue;
                }
                this.theCanvas.Children.Add(levelTwoEnemyShip.Sprite);
                levelTwoEnemyShip.X = this.backgroundWidth / 6.5 + CenterOffset + shipOffset;
                levelTwoEnemyShip.Y = this.backgroundHeight - 280;
                shipOffset += levelTwoEnemyShip.Width + 10;
            }
        }

        private void placeLevelThreeEnemyShips()
        {
            double shipOffset = 0;

            foreach (var levelThreeEnemyShip in this.allEnemyShips)
            {
                if (!(levelThreeEnemyShip is LevelThreeEnemyShip))
                {
                    continue;
                }
                this.theCanvas.Children.Add(levelThreeEnemyShip.Sprite);
                levelThreeEnemyShip.X = this.backgroundWidth / 10.5 + CenterOffset - LevelThreeEnemyShip.LevelThreeEnemyShipPlacementOffset +
                                        shipOffset;
                levelThreeEnemyShip.Y = this.backgroundHeight - 340;
                shipOffset += levelThreeEnemyShip.Width + 10;
            }
        }

        private void placeLevelFourEnemyShips()
        {
            double shipOffset = 0;

            foreach (var levelFourEnemyShip in this.allEnemyShips)
            {
                if (!(levelFourEnemyShip is LevelFourEnemyShip))
                {
                    continue;
                }
                this.theCanvas.Children.Add(levelFourEnemyShip.Sprite);
                levelFourEnemyShip.X = this.backgroundWidth / 10.3 + shipOffset;
                levelFourEnemyShip.Y = this.backgroundHeight - 400;
                shipOffset += levelFourEnemyShip.Width + 10;
            }
        }

        /// <summary>
        /// Creates the player ship then adds it to the canvas.
        /// </summary>
        public void CreateAndPlacePlayerShip()
        {
            this.playerShip = new PlayerShip();
            this.theCanvas.Children.Add(this.playerShip.Sprite);

            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.playerShip.X = this.backgroundWidth / 2 - this.playerShip.Width / 2.0;
            this.playerShip.Y = this.backgroundHeight - this.playerShip.Height - PlayerShip.PlayerShipBottomOffset;
        }

        private void timerOnTick(object sender, object o)
        {
            this.moveEnemyShips();
            this.movePlayerBulletUp();
            this.MoveEnemyBulletsDown();
            this.RemoveEnemyBulletsLeavingCanvas();
            this.RemoveFreePlayerBullets();
            this.checkForCollision();
            this.EndLevel();
        }

        private void fireEnemyBulletsOnTick(object sender, object o)
        {
            this.createEnemyBullet();
        }

        private void animateEnemyShipsOnTick(object sender, object o)
        {
            this.animateEnemyShips();
        }

        private void animateEnemyShips()
        {
            this.enemyShipAnimationTickCount++;

            foreach (var enemyShip in this.allEnemyShips)
            {
                enemyShip.Animate();
            }
        }

        /// <summary>
        /// Resumes all timers for game.
        /// </summary>
        public void ResumeTimers()
        {
            this.gameTimer.Start();
            this.fireEnemyBulletsTimer.Start();
            this.enemyShipAnimationTimer.Start();
        }

        private void stopTimers()
        {
            this.gameTimer.Stop();
            this.fireEnemyBulletsTimer.Stop();
            this.enemyShipAnimationTimer.Stop();
        }

        private void moveEnemyShips()
        {
            this.enemyShipTickCount++;

            foreach (var enemyShip in this.allEnemyShips)
            {
                if (this.enemyShipTickCount > EnemyShipGameMovement)
                {
                    this.enemyShipTickCount = 0;
                    this.enemyShipsMovement = -this.enemyShipsMovement;
                }
                enemyShip.X = enemyShip.X + this.enemyShipsMovement;
            }
        }

        private void movePlayerBulletUp()
        {
            this.playerBulletTickCount++;

            foreach (var playerBullet in this.playerBullets)
            {
                if (playerBullet?.Y <= this.backgroundHeight)
                {
                    playerBullet.Y -= PlayerBulletSpeed;
                }
            }
        }

        /// <summary>
        ///     Fire the player bullet from the playership.
        /// </summary>
        public void FirePlayerBullet()
        {
            if (this.playerShip != null)
            {
                if (!(this.playerBullets.Count >= 3))
                {
                    var playerBullet = new Bullet();

                    this.theCanvas.Children.Add(playerBullet.Sprite);

                    playerBullet.X = this.playerShip.X + PlayerShip.PlayerShipTipXCoordinateOffset;
                    playerBullet.Y = this.playerShip.Y - PlayerShip.PlayerShipTipYCoordinateOffset;
                    this.playerBullets.Add(playerBullet);
                }
            }
        }

        /// <summary>
        ///     Fire a bullet from an enemyship.
        /// </summary>
        public void MoveEnemyBulletsDown()
        {
            this.enemyBulletTickCount++;

            foreach (var enemyBullet in this.enemyBullets)
            {
                if (enemyBullet?.Y <= this.backgroundHeight)
                {
                    enemyBullet.Y += EnemyBulletSpeed;
                }
            }
        }

        /// <summary>
        /// Removes all enemy bullets that have not collided with the player.
        /// @Precondition: enemy bullet must not have collided with the player
        /// @Postcondition: enemy bullets which have not collided with the player will have been removed from the canvas
        /// </summary>
        public void RemoveEnemyBulletsLeavingCanvas()
        {
            foreach (var enemyBullet in this.enemyBullets.ToList())
            {
                if (enemyBullet.Y >= this.backgroundHeight)
                {
                    this.theCanvas.Children.Remove(enemyBullet.Sprite);
                    this.enemyBullets.Remove(enemyBullet);
                }
            }
        }

        /// <summary>
        /// Removes player bullets that have not collided with the enemy.
        /// @Precondition: player bullets must not have collided with the enemy
        /// @Postcondition: player bullets which have not collided with the enemy will have been removed from the canvas
        /// </summary>
        public void RemoveFreePlayerBullets()
        {
            foreach (var playerBullet in this.playerBullets.ToList())
            {
                if (playerBullet.Y <= 0)
                {
                    this.theCanvas.Children.Remove(playerBullet.Sprite);
                    this.playerBullets.Remove(playerBullet);
                }
            }
        }

        /// <summary>
        ///     Fire the player bullet from the playership.
        /// </summary>
        private void createEnemyBullet()
        {
            if (!(this.allEnemyShips.Count <= 0))
            {
                this.randomNumber = this.random.Next(0, this.allEnemyShips.Count - 1);
                var enemyBullet = new Bullet();

                this.enemyBullets.Add(enemyBullet);

                this.theCanvas.Children.Add(enemyBullet.Sprite);

                EnemyShip randomEnemyShip = this.allEnemyShips[this.randomNumber];

                enemyBullet.X = randomEnemyShip.X + LevelThreeEnemyShip.LevelThreeEnemyShipCenterX;
                enemyBullet.Y = randomEnemyShip.Y + LevelThreeEnemyShip.LevelThreeEnemyShipCenterY;
            }
        }

        private void checkForCollision()
        {
            this.collisionTickCount++;

            this.PlayerBulletToEnemyShipCollisionDetection();
            this.EnemyBulletToPlayerShipCollisionDetection();
        }

        /// <summary>
        ///     Manages What happens when a player bullet collides with an enemy ship.
        ///     Precondition: none
        ///     Postcondition: Player bullet and enemy ship are removed once they collide
        /// </summary>
        public void PlayerBulletToEnemyShipCollisionDetection()
        {
            foreach (var enemyShip in this.allEnemyShips.ToList())
            {
                foreach (var playerBullet in this.playerBullets.ToList())
                {
                    if (this.checkPlayerBullletsHitEnemyShips(enemyShip, playerBullet))
                    {
                        this.calculateScore(enemyShip);
                        this.theCanvas.Children.Remove(enemyShip.Sprite);
                        this.theCanvas.Children.Remove(playerBullet.Sprite);
                        this.playerBullets.Remove(playerBullet);
                        this.allEnemyShips.Remove(enemyShip);
                        return;
                    }
                }

            }
        }

        /// <summary>
        /// Ends the current level, all timers stop and player win status is displayed and player ship removed from the canvas when level four is completed.
        /// @Precondition: All Enemies must be destroyed
        /// @Postcondition: Ends the level, or ends the game when level four is completed - displays win status, and removes player ship from game
        /// </summary>
        public void EndLevel()
        {
            if ((this.allEnemyShips.Count == 0) && (this.Level == Levels.LevelOne))
            {
                this.removePlayerBullets();
                this.removeEnemyBullets();
                this.createAndDisplayGameStatus("Level " + (int)this.Level + " Complete!\nPress S for Next Level");
                this.Level = Levels.LevelTwo;

                this.RemovePlayerShipFromGame();
                this.stopTimers();
            }
            else if ((this.allEnemyShips.Count == 0) && (this.Level == Levels.LevelTwo))
            {
                this.removePlayerBullets();
                this.removeEnemyBullets();
                this.createAndDisplayGameStatus("Level " + (int)this.Level + " Complete!\nPress S for Next Level");
                this.Level = Levels.LevelThree;

                this.RemovePlayerShipFromGame();
                this.stopTimers();
            }
            else if ((this.allEnemyShips.Count == 0) && (this.Level == Levels.LevelThree))
            {
                this.removePlayerBullets();
                this.removeEnemyBullets();
                this.createAndDisplayGameStatus("Level " + (int)this.Level + " Complete!\nPress S for Next Level");
                this.Level = Levels.LevelFour;

                this.RemovePlayerShipFromGame();
                this.stopTimers();
            }
            else if ((this.allEnemyShips.Count == 0) && (this.Level == Levels.LevelFour))
            {
                this.removePlayerBullets();
                this.removeEnemyBullets();
                this.createAndDisplayGameStatus("Level " + (int)this.Level + " Complete!\nYOU WON!");
                this.theCanvas.Children.Remove(this.playerShip.Sprite);
                this.stopTimers();
            }
        }

        /// <summary>
        /// Removes the Player Ship from the game.
        /// </summary>
        public void RemovePlayerShipFromGame()
        {
            this.theCanvas.Children.Remove(this.playerShip.Sprite);
            this.playerShip = null;
        }

        /// <summary>
        /// Ends game, all timers stop and player win status is displayed and player ship removed from the canvas.
        /// @Precondition: All Enemies must be destroyed
        /// @Postcondition: Ends the game, displays win status, and removes player ship from game
        /// </summary>
        public void EndGame()
        {
            if (this.allEnemyShips.Count <= 0)
            {
                this.removePlayerBullets();
                this.removeEnemyBullets();
                this.createAndDisplayGameStatus("YOU WON!");
                this.theCanvas.Children.Remove(this.playerShip.Sprite);
                this.stopTimers();
            }
        }

        private void calculateScore(GameObject enemyShip)
        {
            if (enemyShip is LevelOneEnemyShip)
            {
                this.score += LevelOneEnemyShipScore;
                this.scoreBlock.SetText("Score: " + this.score);
            }
            if (enemyShip is LevelTwoEnemyShip)
            {
                this.score += LevelTwoEnemyShipScore;
                this.scoreBlock.SetText("Score: " + this.score);
            }
            if (enemyShip is LevelThreeEnemyShip)
            {
                this.score += LevelThreeEnemyShipScore;
                this.scoreBlock.SetText("Score: " + this.score);
            }
            if (enemyShip is LevelFourEnemyShip)
            {
                this.score += LevelFourEnemyShipScore;
                this.scoreBlock.SetText("Score: " + this.score);
            }
        }

        private bool checkPlayerBullletsHitEnemyShips(GameObject enemyShip, Bullet playerBullet)
        {
            if (this.checkTheUpperLeftCaseOfEnemyShip(enemyShip, playerBullet)
                && this.checkTheUpperRightCaseOfEnemyShip(enemyShip, playerBullet) &&
                this.checkTheBottomLeftCornerOfEnemyShip(enemyShip, playerBullet) &&
                this.checkTheBottomRightCornerOfEnemyShip(enemyShip, playerBullet))
            {
                return true;
            }
            return false;
        }

        private bool checkTheUpperLeftCaseOfEnemyShip(GameObject enemyShip, Bullet playerBullet)
        {
            return (enemyShip.X <= playerBullet?.X) &&
                   (enemyShip.Y <= playerBullet?.Y);
        }

        private bool checkTheUpperRightCaseOfEnemyShip(GameObject enemyShip, Bullet playerBullet)
        {
            return (enemyShip.X + enemyShip.Width >= playerBullet?.X)
                   && (playerBullet?.Y >= enemyShip.Y);
        }

        private bool checkTheBottomLeftCornerOfEnemyShip(GameObject enemyShip, Bullet playerBullet)
        {
            return (enemyShip.X <= playerBullet?.X) &&
                   (playerBullet?.Y <= enemyShip.Y + enemyShip.Height);
        }

        private bool checkTheBottomRightCornerOfEnemyShip(GameObject enemyShip, Bullet playerBullet)
        {
            return (enemyShip.X + enemyShip.Width >= playerBullet?.X) &&
                   (playerBullet?.Y <= enemyShip.Y + enemyShip.Height);
        }

        /// <summary>
        ///     Manages what happens when an enemy bullet collides with the player ship.
        ///     Precondition: none
        ///     Postcondition: Player ship and enemy bullet is removed from the Canvas once they collide
        /// </summary>
        public void EnemyBulletToPlayerShipCollisionDetection()
        {
            foreach (var enemyBullet in this.enemyBullets.ToList())
            {
                if (this.checkTheUpperLeftCaseOfEnemyBullet(enemyBullet)
                    && this.checkTheUpperRightCaseOfEnemyBullet(enemyBullet) &&
                    this.checkTheBottomLeftCornerOfEnemyBullet(enemyBullet) &&
                    this.checkTheBottomRightCornerOfEnemyBullet(enemyBullet))
                {
                    this.theCanvas.Children.Remove(this.playerShip.Sprite);
                    this.removeEnemyBullets();
                    this.theCanvas.Children.Remove(enemyBullet.Sprite);
                    this.removePlayerLife();

                    if (this.playerLives.Count <= 0)
                    {
                        this.createAndDisplayGameStatus("Game Over!");
                        this.stopTimers();
                        return;
                    }
                    this.CreateAndPlacePlayerShip();
                }
            }
        }

        private void removePlayerLife()
        {
            if (this.playerLives.Count != 0)
            {
                var life = this.playerLives.First();
                this.playerLives.Remove(life);
                this.theCanvas.Children.Remove(life.Sprite);
            }
        }

        private void createAndDisplayGameStatus(string text)
        {
            this.gameOverBlock = new GameOverBlock();
            this.gameOverBlock.SetText(text);
            this.theCanvas.Children.Add(this.gameOverBlock.Sprite);
            this.gameOverBlock.X = (this.backgroundWidth / 2) - 61.896;
            this.gameOverBlock.Y = this.backgroundHeight / 2 - 16.896;
        }

        private bool checkTheUpperLeftCaseOfEnemyBullet(GameObject enemyBullet)
        {
            return (this.playerShip.X <= enemyBullet.X) &&
                   (this.playerShip.Y <= enemyBullet.Y);
        }

        private bool checkTheUpperRightCaseOfEnemyBullet(GameObject enemyBullet)
        {
            return (this.playerShip.X + this.playerShip.Width >= enemyBullet.X)
                   && (enemyBullet.Y >= this.playerShip.Y);
        }

        private bool checkTheBottomLeftCornerOfEnemyBullet(GameObject enemyBullet)
        {
            return (this.playerShip.X <= enemyBullet.X) &&
                   (enemyBullet.Y <= this.playerShip.Y + this.playerShip.Height);
        }

        private bool checkTheBottomRightCornerOfEnemyBullet(GameObject enemyBullet)
        {
            return (this.playerShip.X + this.playerShip.Width >= enemyBullet.X) &&
                   (enemyBullet.Y <= this.playerShip.Y + this.playerShip.Height);
        }

        private void removePlayerBullets()
        {
            foreach (var playerBullet in this.playerBullets.ToList())
            {
                this.theCanvas.Children.Remove(playerBullet?.Sprite);
                this.playerBullets.Remove(playerBullet);
            }
        }

        private void removeEnemyBullets()
        {
            foreach (var enemyBullet in this.enemyBullets.ToList())
            {
                this.theCanvas.Children.Remove(enemyBullet?.Sprite);
                this.enemyBullets.Remove(enemyBullet);
            }
        }

        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: the player ship cannot move left pass the game boundary
        ///     Postcondition: The player ship has moved left.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            if (this.playerShip != null)
            {
                if (!(this.playerShip.X <= LeftEdgeCanvas + this.playerShip.SpeedX))
                {
                    this.playerShip.MoveLeft();
                }
            }
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: the player ship cannot move right pass the game boundary
        ///     Postcondition: The player ship has moved right.
        /// </summary>
        public void MovePlayerShipRight()
        {
            if (this.playerShip != null)
            {
                if (!(this.playerShip.X >= this.backgroundWidth - this.playerShip.Width - this.playerShip.SpeedX))
                {
                    this.playerShip.MoveRight();
                }
            }
        }

        #endregion
    }
}