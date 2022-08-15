using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class NewTrainingBoss22001 : ABrain
    {
        private int m_attackTurn = 0;

        public int currentCount = 0;

        public int Dander = 0;

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			base.Body.CurrentDamagePlus = 1f;
			base.Body.CurrentShootMinus = 1f;
			base.Body.SetRect(((SimpleBoss)base.Body).NpcInfo.X, ((SimpleBoss)base.Body).NpcInfo.Y, ((SimpleBoss)base.Body).NpcInfo.Width, ((SimpleBoss)base.Body).NpcInfo.Height);
			if (base.Body.Direction == -1)
			{
				base.Body.SetRect(((SimpleBoss)base.Body).NpcInfo.X, ((SimpleBoss)base.Body).NpcInfo.Y, ((SimpleBoss)base.Body).NpcInfo.Width, ((SimpleBoss)base.Body).NpcInfo.Height);
			}
			else
			{
				base.Body.SetRect(-((SimpleBoss)base.Body).NpcInfo.X - ((SimpleBoss)base.Body).NpcInfo.Width, ((SimpleBoss)base.Body).NpcInfo.Y, ((SimpleBoss)base.Body).NpcInfo.Width, ((SimpleBoss)base.Body).NpcInfo.Height);
			}
        }

        public override void OnDiedSay()
        {
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnStartAttacking()
        {
			base.Body.Direction = base.Game.FindlivingbyDir(base.Body);
			if (m_attackTurn < 2)
			{
				NextAttack();
				m_attackTurn++;
			}
			else
			{
				PersonalAttack();
			}
        }

        private void PersonalAttack()
        {
			int num = base.Game.Random.Next(20, 50);
			int[] array = new int[2]
			{
				1,
				-1
			};
			if (num > 30)
			{
				base.Body.MoveTo(base.Body.X + num * array[1], base.Body.Y, "happy", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1000, NextAttack);
			}
			else
			{
				base.Body.MoveTo(base.Body.X + num * array[0], base.Body.Y, "happy", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1000, NextAttack);
			}
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }

        private void NextAttack()
        {
			Player player = base.Game.FindRandomPlayer();
			base.Body.SetRect(0, 0, 0, 0);
			if (player.X > base.Body.Y)
			{
				base.Body.ChangeDirection(1, 500);
			}
			else
			{
				base.Body.ChangeDirection(-1, 500);
			}
			base.Body.CurrentDamagePlus = 1.8f;
			if (player != null)
			{
				int x = base.Game.Random.Next(player.X - 50, player.X + 50);
				if (base.Body.ShootPoint(x, player.Y, ((SimpleBoss)base.Body).NpcInfo.CurrentBallId, 1000, 10000, 1, 1f, 2200))
				{
					base.Body.PlayMovie("beat", 1700, 0);
				}
			}
        }
    }
}
