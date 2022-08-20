using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Effects;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
    public class ThirteenTerrorBrotherNpc : ABrain
    {
        private int int_0;

        private SimpleBoss simpleBoss_0;

        protected Player m_targer;

        private PhysicalObj physicalObj_0;

        private int int_1;

        private int int_2;

        private int int_3;

        public ThirteenTerrorBrotherNpc()
        {
			int_1 = 13305;
			int_2 = 400;
			int_3 = 150;
        }

        private void method_0()
        {
			base.Body.Properties1 = 0;
			base.Body.Say("Tà thần vĩ đại, hãy ban sức mạnh của ngài cho chúng tôi!", 1, 1000);
			base.Body.PlayMovie("walk", 2000, 0);
			base.Body.BoltMove(1660, 485, 2500);
			List<Player> list = new List<Player>();
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				if (allLivingPlayer.IsLiving && allLivingPlayer.X >= 1003 && allLivingPlayer.X <= 1300 && allLivingPlayer.Y <= 620)
				{
					list.Add(allLivingPlayer);
				}
			}
			if (list.Count <= 0)
			{
				base.Body.PlayMovie("beatB", 4000, 0);
				base.Body.CallFuction(method_1, 11000);
				simpleBoss_0.Properties1 = 1;
				base.Body.PlayMovie("walk", 12000, 0);
				base.Body.BoltMove(1604, 594, 12500);
			}
			else
			{
				base.Body.CurrentDamagePlus = 2f;
				base.Body.PlayMovie("castA", 4000, 0);
				physicalObj_0.PlayMovie("beatA", 5000, 0);
				foreach (Player allLivingPlayer2 in base.Game.GetAllLivingPlayers())
				{
					if (allLivingPlayer2.IsLiving && list.Contains(allLivingPlayer2))
					{
						base.Body.BeatDirect(allLivingPlayer2, "", 7500, 1, 1);
					}
					allLivingPlayer2.AddEffect(new AddDamageTurnEffect(int_2, 2), 7000);
					allLivingPlayer2.AddEffect(new AddGuardTurnEffect(int_3, 2), 7000);
				}
				(base.Game as PVEGame).SendObjectFocus(simpleBoss_0, 0, 9000, 0);
				simpleBoss_0.Say("Có chuyện gì vậy? Cúng tế bị sai cmnr.", 0, 10500, 2000);
				base.Body.PlayMovie("walk", 8000, 0);
				base.Body.BoltMove(1604, 594, 8500);
				base.Body.CallFuction(method_1, 13000);
			}
			simpleBoss_0.Delay = base.Game.GetLowDelayTurn() - 1;
			(base.Body as SimpleBoss).Delay = base.Game.GetHighDelayTurn() + 1;
        }

        private void method_1()
        {
			base.Game.RemovePhysicalObj(physicalObj_0, sendToClient: true);
        }

        private void method_2()
        {
			base.Body.Properties1 = 1;
			base.Body.Say("Lễ cúng tế sắp bắt đầu! Chúng ta sẽ có sức mạnh của tà thần! Hãy cố lên….", 1, 1000);
			base.Body.PlayMovie("call", 1100, 4000);
			base.Body.CallFuction(method_3, 3000);
        }

        private void method_3()
        {
			physicalObj_0 = (base.Game as PVEGame).Createlayer(1150, 572, "front", "asset.game.ten.jitan", "1", 1, 0);
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
			base.Body.Properties1 = 0;
        }

        public override void OnDie()
        {
			base.OnDie();
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			if (base.Body.Properties1 == 0)
			{
				method_2();
			}
			else
			{
				method_0();
			}
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }
    }
}
