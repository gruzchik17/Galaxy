#region using

using System.Drawing;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;

#endregion

namespace Galaxy.Environments.Actors
{
    public class EnemyBullet : BaseActor
    {
        #region Constant

        private const int Speed = 10;

        #endregion

        #region Constructors

        public EnemyBullet(ILevelInfo info)
            : base(info)
        {
            Width = 6;
            Height = 12;
            ActorType = ActorType.Enemy;
        }

        #endregion

        #region Overrides

        public override void Load()
        {
            Load(@"Assets\bullet.png");
        }

        public override void Update()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        #endregion
    }
}
