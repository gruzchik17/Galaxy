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
    public class Prepyt2 : DethAnimationActor
    {
        #region Constructors

        public Prepyt2(ILevelInfo info)
            : base(info)
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
        }

        #endregion

        #region Overrides

        public override void Load()
        {
            Load(@"Assets\Prepyt2.png");
        }

        #endregion
        public override bool IsAlive
        {
            get
            {
                return base.IsAlive;
            }
            set
            {
                base.IsAlive = true;
                CanDrop = false;
            }
        }
    }
}
