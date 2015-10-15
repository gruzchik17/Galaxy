#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
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
        int positionX1= 100 + i * (ship1.Width + 50);
        ship1.Position = new Point(positionX1, positionY1);

        
        Actors.Add(ship);
        Actors.Add(ship1);


      }

      // Player
      Player = new PlayerShip(this);
      int playerPositionX = Size.Width / 2 - Player.Width / 2;
      int playerPositionY = Size.Height - Player.Height - 50;
      Player.Position = new Point(playerPositionX, playerPositionY);
      Actors.Add(Player);

    //Bullets
      BullShot.Start();
    }

    #endregion

    #region Overrides

    private void BulletShot()
    {
        if (BullShot.ElapsedMilliseconds < 1000)
            return;
        Random rnd = new Random();
        int qq = rnd.Next(10);
        var enbul = new EnemyBullet(this);
        enbul.Position = new Point(Actors[qq].Position.X, Actors[qq].Position.Y+10);
        enbul.Load(); 
        
        Actors.Add(enbul);
        BullShot.Restart();
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

      if(m_frameCount % 10 != 0) return;
     
      Bullet bullet = new Bullet(this)
      {
        Position = Player.Position
      };

      bullet.Load();
      Actors.Add(bullet);
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

      //has no enemy
      if (Actors.All(actor => actor.ActorType != ActorType.Enemy))
        Success = true;
    }

    #endregion
  }
}
