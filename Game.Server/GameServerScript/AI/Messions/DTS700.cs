using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.Messions
{
	public class DTS700 : AMissionControl
	{
		private List<SimpleNpc> redNpc = new List<SimpleNpc>();
		private List<SimpleNpc> blueNpc = new List<SimpleNpc>();
		private int direction;
		private int m_chimMoBu = 10011;
		private int m_heoRung = 10002;

		public override int CalculateScoreGrade(int score)
		{
			base.CalculateScoreGrade(score);
			if (score > 930)
			{
				return 3;
			}
			if (score > 850)
			{
				return 2;
			}
			if (score > 775)
			{
				return 1;
			}
			return 0;
		}

		public override void OnPrepareNewSession()
		{
			base.OnPrepareNewSession();
			int[] npcIds = new int[2]
			{
				m_chimMoBu,
				m_heoRung
			};
			base.Game.LoadResources(npcIds);
			base.Game.LoadNpcGameOverResources(npcIds);
			base.Game.SetMap(1184);
		}

		public override void OnStartGame()
		{
			base.OnStartGame();
			method_0();
		}

		public override void OnNewTurnStarted()
		{
			if (base.Game.GetLivedLivings().Count <= 0)
			{
				base.Game.PveGameDelay = 0;
				method_0();
			}
		}

		private void method_0()
		{
			if (base.Game.MissionInfo.TotalCount - base.Game.TotalKillCount <= 0)
			{
				return;
			}
			int num2 = 4;
			if (4 > base.Game.MissionInfo.TotalCount - base.Game.TotalKillCount)
			{
				num2 = base.Game.MissionInfo.TotalCount - base.Game.TotalKillCount;
			}
			for (int i = 0; i < num2; i++)
			{
				if (i == 0)
				{
					blueNpc.Add(base.Game.CreateNpc(m_heoRung, 49, 810, 1, -1, base.Game.BaseLivingConfig()));
					continue;
				}
				int x = base.Game.Random.Next(78, 1691);
				int y = base.Game.Random.Next(74, 600);
				LivingConfig livingConfig = base.Game.BaseLivingConfig();
				livingConfig.IsFly = true;
				redNpc.Add(base.Game.CreateNpc(m_chimMoBu, x, y, 1, -1, livingConfig));
			}
			direction++;
			base.Game.PveGameDelay = 0;
		}

		public override void OnBeginNewTurn()
		{
			base.OnBeginNewTurn();
		}

		public override bool CanGameOver()
		{
			if (base.Game.GetAllLivingPlayers().Count < 2)
			{
				return true;
			}
			if (base.Game.TotalKillCount < base.Game.MissionInfo.TotalCount && base.Game.TurnIndex <= base.Game.MissionInfo.TotalTurn - 1)
			{
				return false;
			}
			return true;
		}

		public override int UpdateUIData()
		{
			base.UpdateUIData();
			return base.Game.TotalKillCount;
		}

		public override void OnGameOver()
		{
			base.OnGameOver();
			if (base.Game.TotalKillCount >= base.Game.MissionInfo.TotalCount)
			{
				base.Game.IsWin = true;
			}
			else
			{
				base.Game.IsWin = false;
			}
		}
	}
}
