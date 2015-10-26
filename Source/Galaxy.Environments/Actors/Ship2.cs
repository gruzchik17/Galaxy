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
  public class Ship1 : Ship
  {
    public Ship1(ILevelInfo info):base(info)
    {
      Width = 30;
      Height = 30;
      ActorType = ActorType.Enemy;
    }
      public override void Load()
      {
          Load(@"Assets\ship1.png");
          if (m_flyTimer == null)
          {
              m_flyTimer = new Stopwatch();
              m_flyTimer.Start();
          }
      }
  }
}
