using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
    public class ThirteenSimpleAntBoss : ABrain
    {
        private int int_0;

        private SimpleBoss simpleBoss_0;

        protected Player m_targer;

        private List<PhysicalObj> list_0;

        private int int_1;

        private string[] string_0;

        private string[] UlosOgpdKaT;

        public ThirteenSimpleAntBoss()
        {
			list_0 = new List<PhysicalObj>();
			int_1 = 13002;
			string_0 = new string[4]
			{
				"Hãy liếm thử mũi tên này của ta",
				"Xem có thốn không nhé",
				"Ta sẽ hạ sát hết lũ các ngươi",
				"Các ngươi nghĩ đủ trình hạ ta?."
			};
			UlosOgpdKaT = new string[3]
			{
				"Băng trâm tiễn.",
				"Liếm thử băng trâm tiễn của ta",
				"Để ta giúp ngươi về trời nhanh."
			};
        }

        private void method_0()
        {
			base.Body.CurrentDamagePlus = 1.3f;
			m_targer = base.Game.FindRandomPlayer();
			if (base.Body.ShootPoint(m_targer.X, m_targer.Y, 51, 1000, 10000, 1, 3f, 2800))
			{
				base.Body.PlayMovie("beatD", 1000, 3000);
			}
        }

        private void method_1()
        {
			int num = base.Game.Random.Next(0, UlosOgpdKaT.Length);
			base.Body.Say(UlosOgpdKaT[num], 0, 1000, 0);
			Player[] array = base.Game.FindRandomPlayer(2);
			if (array.Length != 0)
			{
				if (base.Body.ShootPoint(array[0].X - 20, array[0].Y, 99, 1000, 10000, 1, 3f, 3000))
				{
					base.Body.PlayMovie("beatD", 1500, 3000);
				}
				if (array.Length == 2 && base.Body.ShootPoint(array[1].X - 20, array[1].Y, 99, 1000, 10000, 1, 3f, 5000))
				{
					base.Body.PlayMovie("beatD", 3500, 3000);
				}
			}
        }

        private void method_2()
        {
			base.Body.CurrentDamagePlus = 1.5f;
			int num = base.Game.Random.Next(0, string_0.Length);
			base.Body.Say(string_0[num], 0, 1000, 6000);
			base.Body.PlayMovie("beatC", 1500, 0);
			m_targer = base.Game.FindRandomPlayer();
			(base.Game as PVEGame).SendObjectFocus(m_targer, 0, 3000, 0);
			base.Body.CallFuction(method_3, 4000);
        }

        private void method_3()
        {
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				list_0.Add(((PVEGame)base.Game).Createlayer(allLivingPlayer.X, allLivingPlayer.Y, "", "asset.game.ten.jianyu", "", 1, 1));
				base.Body.BeatDirect(allLivingPlayer, "", 2000, 3, 1);
			}
        }

        private void method_4(List<Player> WeGjyHCVui6nVDiS4xh)
        {
			base.Body.CurrentDamagePlus = 1000f;
			base.Body.PlayMovie("beatB", 1000, 0);
			foreach (Player item in WeGjyHCVui6nVDiS4xh)
			{
				base.Body.BeatDirect(item, "", 2500, 1, 1);
			}
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			m_body.CurrentDamagePlus = 1f;
			m_body.CurrentShootMinus = 1f;
			if (list_0.Count <= 0)
			{
				return;
			}
			foreach (PhysicalObj item in list_0)
			{
				base.Game.RemovePhysicalObj(item, sendToClient: true);
			}
			list_0.Clear();
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
				method_4(list);
				return;
			}
			switch (int_0)
			{
			case 0:
				method_2();
				break;
			case 1:
				method_1();
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
