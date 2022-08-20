using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Effects;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
    public class ThirteenNormalBrynBoss : ABrain
    {
        private int xcpsogudosP;

        private int int_0;

        private SimpleBoss simpleBoss_0;

        protected Player m_targer;

        private List<PhysicalObj> list_0;

        private int int_1;

        private int aRvsorGcwe2;

        private int int_2;

        public ThirteenNormalBrynBoss()
        {
			list_0 = new List<PhysicalObj>();
			int_1 = 13104;
			aRvsorGcwe2 = 30;
			int_2 = 5000;
        }

        private void GnqsoQakMuS()
        {
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				if (allLivingPlayer.IsLiving)
				{
					allLivingPlayer.SpeedMultX(20);
					if (base.Body.FindDirection(allLivingPlayer) != 1)
					{
						allLivingPlayer.StartSpeedMult(allLivingPlayer.X + base.Game.Random.Next(400, 600), allLivingPlayer.Y, 0);
					}
					else
					{
						allLivingPlayer.StartSpeedMult(allLivingPlayer.X - base.Game.Random.Next(400, 600), allLivingPlayer.Y, 0);
					}
				}
				base.Body.BeatDirect(allLivingPlayer, "", 1500, 1, 1);
			}
			base.Body.CallFuction(method_9, 5000);
        }

        private void method_0()
        {
			simpleBoss_0.Delay = base.Game.GetHighDelayTurn() + 1;
			m_targer = base.Game.FindRandomPlayer();
			if (m_targer != null && m_targer.IsLiving)
			{
				base.Body.ChangeDirection(m_targer, 1000);
				if (m_targer.Y <= 620)
				{
					base.Body.CurrentDamagePlus = 15f;
					base.Body.Say("Ngươi dám leo lên trên đó à? Chết đi!!!", 0, 2000);
					base.Body.PlayMovie("beatC", 3000, 0);
					base.Body.BeatDirect(m_targer, "", 4000, 3, 1);
				}
				else if (base.Body.ShootPoint(m_targer.X, m_targer.Y, 55, 1000, 10000, 1, 1.5f, 2900))
				{
					base.Body.PlayMovie("beatB", 2000, 3000);
				}
			}
			if (int_0 == 0)
			{
				base.Body.CallFuction(method_0, 4000);
			}
			int_0++;
        }

        private void method_1()
        {
			simpleBoss_0.Delay = base.Game.GetHighDelayTurn() + 1;
			if (base.Body.Properties1 != 1)
			{
				base.Body.CurrentDamagePlus = 2.3f;
				base.Body.Say("Đậu xanh rau muống dám phá nghi lễ của ta.", 1, 2000);
				base.Body.PlayMovie("beatC", 3000, 0);
				base.Body.RangeAttacking(base.Body.X - 10000, base.Body.X + 10000, "", 4000, directDamage: false);
			}
			else
			{
				base.Body.CurrentDamagePlus = 10f;
				base.Body.Say("Hãy xem sức mạnh của tà thần đây. Đây mới chính là sức mạnh thực sự của ta.", 1, 1000);
				(base.Game as PVEGame).SendPlayersPicture(base.Body, 29, state: true);
				(base.Game as PVEGame).SendPlayersPicture(base.Body, 30, state: true);
				base.Body.CallFuction(method_2, 2000);
				base.Body.Properties1 = 2;
			}
        }

        private void method_10(List<Player> MfjIurVzYCQ2CYCIlIq)
        {
			base.Body.CurrentDamagePlus = 1000f;
			base.Body.Say("Chán sống hay sao mà đứng gần ta?", 1, 1000);
			base.Body.PlayMovie("beatC", 2000, 0);
			foreach (Player item in MfjIurVzYCQ2CYCIlIq)
			{
				base.Body.BeatDirect(item, "", 3500, 1, 1);
			}
        }

        private void method_2()
        {
			list_0.Add((base.Game as PVEGame).Createlayer(base.Body.X, base.Body.Y, "", "asset.game.ten.up", "", 1, 0));
			base.Body.PlayMovie("beatC", 2000, 5000);
			base.Body.CallFuction(method_3, 3000);
			base.Body.RangeAttacking(base.Body.X - 10000, base.Body.X + 10000, "", 4000, directDamage: false);
        }

        private void method_3()
        {
			base.Body.AddBlood(int_2);
        }

        private void method_4()
        {
			base.Body.Say("Hãy mau thực hiện nghi thức cúng tế, sức mạnh tà thần sẽ thuộc về ta!", 1000, 3000);
			base.Body.Properties1 = 0;
			simpleBoss_0.Delay = base.Game.GetLowDelayTurn() - 1;
        }

        private void method_5()
        {
			simpleBoss_0.Delay = base.Game.GetHighDelayTurn() + 1;
			base.Body.PlayMovie("callA", 1000, 0);
			m_targer = base.Game.FindRandomPlayer();
			if (m_targer != null)
			{
				(base.Game as PVEGame).SendObjectFocus(m_targer, 1, 2000, 0);
			}
			base.Body.CallFuction(method_6, 2800);
        }

        private void method_6()
        {
			base.Body.CurrentDamagePlus = 1.8f;
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				switch (base.Game.Random.Next(0, 3))
				{
				case 0:
					list_0.Add((base.Game as PVEGame).Createlayer(allLivingPlayer.X, allLivingPlayer.Y, "", "asset.game.ten.jiaodu", "", 1, 0));
					allLivingPlayer.AddEffect(new LockDirectionEffect(2), 1500);
					break;
				case 1:
					list_0.Add((base.Game as PVEGame).Createlayer(allLivingPlayer.X, allLivingPlayer.Y, "", "asset.game.ten.pilao", "", 1, 0));
					allLivingPlayer.AddEffect(new ReduceStrengthEffect(2, aRvsorGcwe2), 1500);
					break;
				default:
					list_0.Add((base.Game as PVEGame).Createlayer(allLivingPlayer.X, allLivingPlayer.Y, "", "asset.game.ten.pilao", "", 1, 0));
					base.Body.BeatDirect(allLivingPlayer, "", 1500, 1, 1);
					break;
				}
				base.Body.BeatDirect(allLivingPlayer, "", 1500, 1, 1);
			}
        }

        private void method_7()
        {
			m_targer = base.Game.FindRandomPlayer();
			if (m_targer != null)
			{
				(base.Game as PVEGame).SendObjectFocus(m_targer, 1, 2000, 0);
			}
			base.Body.PlayMovie("callA", 1000, 0);
			base.Body.CallFuction(method_8, 3000);
        }

        private void method_8()
        {
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				list_0.Add((base.Game as PVEGame).Createlayer(allLivingPlayer.X, allLivingPlayer.Y, "", "asset.game.ten.baozha", "", 1, 0));
			}
			base.Body.CallFuction(GnqsoQakMuS, 1200);
        }

        private void method_9()
        {
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				allLivingPlayer.SpeedMultX(3);
			}
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			m_body.CurrentDamagePlus = 1f;
			m_body.CurrentShootMinus = 1f;
			if (list_0 != null && list_0.Count > 0)
			{
				foreach (PhysicalObj item in list_0)
				{
					base.Game.RemovePhysicalObj(item, sendToClient: true);
				}
				list_0.Clear();
			}
			if (base.Body.Properties1 == 2)
			{
				(base.Game as PVEGame).SendPlayersPicture(base.Body, 29, state: false);
				(base.Game as PVEGame).SendPlayersPicture(base.Body, 30, state: false);
				base.Body.Properties1 = 0;
			}
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
			int_0 = 0;
        }

        public override void OnCreated()
        {
			base.OnCreated();
			base.Body.Properties1 = 0;
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
				if (allLivingPlayer.IsLiving && base.Body.X - 150 <= allLivingPlayer.X && base.Body.X + 150 >= allLivingPlayer.X && allLivingPlayer.Y >= 780)
				{
					list.Add(allLivingPlayer);
				}
			}
			if (list.Count > 0)
			{
				method_10(list);
				return;
			}
			switch (xcpsogudosP)
			{
			case 0:
				method_7();
				break;
			case 1:
				method_5();
				break;
			case 2:
				method_4();
				break;
			case 3:
				method_1();
				break;
			case 4:
				method_0();
				xcpsogudosP = -1;
				break;
			}
			xcpsogudosP++;
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }
    }
}
