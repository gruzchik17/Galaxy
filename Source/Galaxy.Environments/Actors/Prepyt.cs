#region using

using System;
using System.Diagnostics;
using System.Windows;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

#endregion

namespace Galaxy.Environments.Actors
{
    public class Prepyt : DethAnimationActor
    {
       #region Constant

    private const int MaxSpeed = 1;
    private const long StartFlyMs = 2000;

    #endregion

    #region Private fields

    protected bool m_flying;
    protected Stopwatch m_flyTimer;

    #endregion

    #region Constructors

    public Prepyt(ILevelInfo info):base(info)
    {
      Width = 50;
      Height = 30;
      ActorType = ActorType.Enemy;
    }

    #endregion

    #region Overrides

    public override void Update()
    {
      base.Update();

      if (!IsAlive)
        return;

      if (!m_flying)
      {
        if (m_flyTimer.ElapsedMilliseconds <= StartFlyMs) return;

        m_flyTimer.Stop();
        m_flyTimer = null;
        h_changePosition();
        m_flying = true;
      }
      else
      {
        h_changePosition();
      }
    }

    #endregion

    #region Overrides

    public override void Load()
    {
      Load(@"Assets\Prepyt.png");
      if (m_flyTimer == null)
      {
        m_flyTimer = new Stopwatch();
        m_flyTimer.Start();
      }
    }

    #endregion

    #region Private methods

    
    private void h_changePosition()
    {
        int speed = 0;

        Position = new Point(Position.X + speed, Position.Y);
    }

    #endregion
    }
}
