using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class CrosairBoss : ABrain
    {
        private int int_0;

        public int currentCount;

        public int Dander;

        private void akVhgRvqjd8()
        {
			base.Body.RangeAttacking(0, base.Game.Map.Info.ForegroundWidth + 1, "cry", 0, null);
        }

        private void method_0(int int_1, int int_2)
        {
			base.Body.CurrentDamagePlus = 1000f;
			base.Body.PlayMovie("beatA", 1000, 0);
			base.Body.RangeAttacking(int_1, int_2, "cry", 4000, null);
        }

        private void method_1()
        {
			base.Body.CurrentDamagePlus = 1.5f;
			base.Body.PlayMovie("beatA", 1000, 0);
			base.Body.CallFuction(akVhgRvqjd8, 4000);
        }

        private void method_2()
        {
			base.Body.CurrentDamagePlus = 3.1f;
			base.Body.PlayMovie("beatC", 1000, 0);
			base.Body.CallFuction(akVhgRvqjd8, 3500);
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

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnStartAttacking()
        {
			base.Body.Direction = base.Game.FindlivingbyDir(base.Body);
			bool flag = false;
			int num = 0;
			foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
			{
				if (allFightPlayer.IsLiving && allFightPlayer.X > 670)
				{
					int num2 = (int)base.Body.Distance(allFightPlayer.X, allFightPlayer.Y);
					if (num2 > num)
					{
						num = num2;
					}
					flag = true;
				}
			}
			if (flag)
			{
				method_0(0, base.Game.Map.Info.ForegroundWidth + 1);
			}
			else if (int_0 == 0)
			{
				method_1();
				int_0++;
			}
			else if (int_0 != 1)
			{
				method_2();
				int_0 = 0;
			}
			else
			{
				TashgAsMuBN();
				int_0++;
			}
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }

        private void TashgAsMuBN()
        {
			base.Body.PlayMovie("beatB", 1000, 0);
			base.Body.CallFuction(akVhgRvqjd8, 4000);
        }
    }
}
