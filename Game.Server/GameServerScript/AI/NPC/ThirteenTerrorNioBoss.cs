using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
    public class ThirteenTerrorNioBoss : ABrain
    {
        private int int_0;

        private SimpleBoss simpleBoss_0;

        protected Player m_targer;

        private List<PhysicalObj> list_0;

        private int int_1;

        private int int_2;

        private int int_3;

        private string[] string_0;

        public ThirteenTerrorNioBoss()
        {
			list_0 = new List<PhysicalObj>();
			int_1 = 13301;
			int_2 = 30000;
			int_3 = 130000;
			string_0 = new string[4]
			{
				"Phê phê phê. Coi đánh ta kiểu gì?",
				"Ôi thật thần kỳ. Buff máu phê vãi",
				"Buff cho phê, phê như con tê tê..",
				"Đánh bại ta đâu có dễ. Coi ta này."
			};
        }

        private void method_0()
        {
			List<Player> list = new List<Player>();
			if (simpleBoss_0.IsLiving)
			{
				foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
				{
					if (allLivingPlayer.IsFrost)
					{
						list.Add(allLivingPlayer);
					}
				}
			}
			base.Body.PlayMovie("beat", 1000, 0);
			if (list.Count <= 0)
			{
				base.Body.CurrentDamagePlus = 1.5f;
				base.Body.RangeAttacking(base.Body.X - 10000, base.Body.X + 10000, "cry", 3200, directDamage: false);
				return;
			}
			base.Body.CurrentDamagePlus = 1000f;
			foreach (Player item in list)
			{
				base.Body.BeatDirect(item, "", 3200, 1, 1);
			}
        }

        private void method_1()
        {
			base.Body.CurrentDamagePlus = 1.8f;
			m_targer = base.Game.FindRandomPlayer();
			base.Body.ChangeDirection(m_targer, 500);
			if (base.Body.ShootPoint(m_targer.X, m_targer.Y, 61, 1000, 10000, 1, 1.7f, 1900))
			{
				base.Body.PlayMovie("beat2", 1000, 4000);
			}
        }

        private void method_2()
        {
			int num = base.Game.Random.Next(0, string_0.Length);
			base.Body.Say(string_0[num], 0, 1000);
			base.Body.PlayMovie("renew", 1500, 3500);
			base.Body.CallFuction(method_3, 3000);
        }

        private void method_3()
        {
			base.Body.AddBlood(int_2);
			if (simpleBoss_0.IsLiving)
			{
				(base.Game as PVEGame).SendObjectFocus(simpleBoss_0, 0, 1500, 0);
				base.Body.CallFuction(method_4, 3000);
			}
        }

        private void method_4()
        {
			simpleBoss_0.AddBlood(int_3);
        }

        private void method_5(List<Player> i1Q2uxCmW0RByTpGt6F)
        {
			base.Body.CurrentDamagePlus = 1000f;
			foreach (Player item in i1Q2uxCmW0RByTpGt6F)
			{
				base.Body.BeatDirect(item, "", 2000, 1, 1);
			}
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			m_body.CurrentDamagePlus = 1f;
			m_body.CurrentShootMinus = 1f;
        }

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
			if (simpleBoss_0 == null)
			{
				SimpleBoss[] array = ((PVEGame)base.Game).FindLivingTurnBossWithID(int_1);
				if (array.Length != 0)
				{
					simpleBoss_0 = array[0];
				}
			}
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnDie()
        {
			base.OnDie();
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			List<Player> list = new List<Player>();
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				if (allLivingPlayer.IsLiving && allLivingPlayer.X > 1066)
				{
					list.Add(allLivingPlayer);
				}
			}
			if (list.Count > 0)
			{
				method_5(list);
				return;
			}
			switch (int_0)
			{
			case 0:
				method_1();
				break;
			case 1:
				method_2();
				break;
			case 2:
				method_0();
				int_0 = -1;
				break;
			}
			int_0++;
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }
    }
}
