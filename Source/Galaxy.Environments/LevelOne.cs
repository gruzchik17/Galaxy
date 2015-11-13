#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Media;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;

#endregion

namespace Galaxy.Environments
{
    /// <summary>
    ///   The level class for Open Mario.  This will be the first level that the player interacts with.
    /// </summary>   
    public class LevelOne : BaseLevel
    {
        private int m_frameCount;

        #region Constructors
        
        /// <summary>
        ///   Initializes a new instance of the <see cref="LevelOne" /> class.
        /// </summary>
        public LevelOne()
        {
            // Backgrounds
            FileName = @"Assets\LevelOne.png";

            // Enemies
            for (int i = 0; i < 5; i++)
            {
                var ship = new Ship(this);
                int positionY = ship.Height + 50;
                int positionX = 150 + i * (ship.Width + 50);
                ship.Position = new Point(positionX, positionY);

                var ship1 = new Ship1(this);
                int positionY1 = ship.Height + 10;
                int positionX1 = 150 + i * (ship1.Width + 50);
                ship1.Position = new Point(positionX1, positionY1);

                var ship3 = new Ship(this);
                int positionY2 = ship3.Height + 90;
                int positionX2 = 150 + i * (ship3.Width + 50);
                ship3.Position = new Point(positionX2, positionY2);

                var prepyt2 = new Prepyt2(this);
                int positionY4 = prepyt2.Height + 170;
                int positionX4 = i * (prepyt2.Width + 93);
                prepyt2.Position = new Point(positionX4, positionY4);


                Actors.Add(ship);
                Actors.Add(ship1);
                Actors.Add(ship3);
                Actors.Add(prepyt2);
            }
            // Blocks
            for (int i = 0; i < 11; i++)
            {
                var prepyt = new Prepyt(this);
                int positionY3 = prepyt.Height + 130;
                int positionX3 = 50 + i * (prepyt.Width);
                prepyt.Position = new Point(positionX3, positionY3);

                Actors.Add(prepyt);
            }


            // Player
            Player = new PlayerShip(this);
            int playerPositionX = Size.Width / 2 - Player.Width / 2;
            int playerPositionY = Size.Height - Player.Height - 50;
            Player.Position = new Point(playerPositionX, playerPositionY);
            Actors.Add(Player);
            
        }

        #endregion

        #region Overrides

        public void BulletShot()
        {
            if (BullShot.ElapsedMilliseconds < 1000 )
                return;

            var enbul = new EnemyBullet(this);
            var enemyList = Actors.Where((actor) => actor is Ship || actor is Ship1).ToList();
            if (enemyList.Count > 0)
            {

            Random rnd = new Random();
            int qq = rnd.Next(enemyList.Count);

            var target = enemyList[qq].Position;
            enbul.Position = new Point(target.X, target.Y + 10);

            enbul.Load();

            Actors.Add(enbul);
            SoundPlayer lazershot2 = new SoundPlayer(@"Media\Lazer2.wav");
            lazershot2.Play();

            BullShot.Restart();
            }
        }
        private void UpWallpapers()
        {
            if (UpWall.ElapsedMilliseconds < 1000)
                return;

            UpWall.Restart();
        }
        public void h_dispatchKey()
        {
            if (!IsPressed(VirtualKeyStates.Space)) return;

            if (m_frameCount % 10 != 0) return;

            Bullet bullet = new Bullet(this)
            {
                Position = Player.Position
            };

            bullet.Load();
            Actors.Add(bullet);
            SoundPlayer lazershot = new SoundPlayer(@"Media\Lazer1.wav");
            lazershot.Play();
        }
        public override BaseLevel NextLevel()
        {
            return new StartScreen();

        }
        
        private Stopwatch BullShot = new Stopwatch();
        private Stopwatch UpWall = new Stopwatch();

        public override void Update()
        {
            m_frameCount++;
            h_dispatchKey();
            BulletShot();
            base.Update();

            IEnumerable<BaseActor> killedActors = CollisionChecher.GetAllCollisions(Actors);

            foreach (BaseActor killedActor in killedActors)
            {
                if (killedActor.IsAlive) 
                    killedActor.IsAlive = false;
            }

            List<BaseActor> toRemove = Actors.Where(actor => actor.CanDrop).ToList();
            BaseActor[] actors = new BaseActor[toRemove.Count()];
            toRemove.CopyTo(actors);

            foreach (BaseActor actor in actors.Where(actor => actor.CanDrop))
            {
                Actors.Remove(actor);
            }

            if (Player.CanDrop)
                Failed = true;

            //has no enemy Actors.All(actor => actor.ActorType != ActorType.Enemy)
            if (Actors.Where((actor) => actor is Ship || actor is Ship1).ToList().Count == 0)
                Success = true;
        }

        public override void Load()
        {
            base.Load();
            BullShot.Start();
        }

        #endregion
    }
}
