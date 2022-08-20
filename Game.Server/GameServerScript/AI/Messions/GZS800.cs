using Bussiness;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.Messions
{
	public class GZS800 : AMissionControl
	{
		private List<SimpleNpc> redNpc;

		private List<SimpleNpc> blueNpc;

		private SimpleBoss boss;

		private int direction;

		private bool isLife;

		private int m_luuDanBupBe = 8001;

		private int m_nguoiLuuDanNong = 8002;

		private int m_nhaThamHiem = 8104;

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
			int[] npcIds = new int[3]
			{
				m_luuDanBupBe,
				m_nguoiLuuDanNong,
				m_nhaThamHiem
			};
			base.Game.LoadResources(npcIds);
			base.Game.LoadNpcGameOverResources(npcIds);
			base.Game.SetMap(1184);
		}

		public override void OnStartGame()
		{
			base.OnStartGame();
			boss = base.Game.CreateBoss(m_nhaThamHiem, 1159, 850, -1, 1, "born");
			boss.CallFuction(method_0, 4000);
		}

		public override void OnNewTurnStarted()
		{
			if (base.Game.GetLivedLivings().Count <= 0)
			{
				base.Game.PveGameDelay = 0;
				method_2();
			}
		}

		private void method_0()
		{
			boss.Say(LanguageMgr.GetTranslation("GameServerScript.AI.Messions.GZS800.Msg1"), 0, 0);
			boss.Say(LanguageMgr.GetTranslation("GameServerScript.AI.Messions.GZS800.Msg2"), 0, 2000);
			boss.Say(LanguageMgr.GetTranslation("GameServerScript.AI.Messions.GZS800.Msg3"), 0, 7000);
			boss.Say(LanguageMgr.GetTranslation("GameServerScript.AI.Messions.GZS800.Msg4"), 0, 11000);
			boss.PlayMovie("out", 14000, 3000);
			boss.CallFuction(method_1, 18000);
		}

		private void method_1()
		{
			base.Game.RemoveLiving(boss, sendToClient: true);
			method_2();
		}

		private void method_2()
		{
			int num = base.Game.MissionInfo.TotalCount - base.Game.TotalKillCount;
			if (num <= 0)
			{
				return;
			}
			int num2 = base.Game.Random.Next(3, 6);
			if (num2 > num)
			{
				num2 = num;
			}
			for (int i = 0; i < num2; i++)
			{
				if (base.Game.Random.Next(100) < 40)
				{
					if (isLife)
					{
						blueNpc.Add(base.Game.CreateNpc(m_nguoiLuuDanNong, 1748 - i * 100, 877, 1, -1, base.Game.BaseLivingConfig()));
					}
					else
					{
						blueNpc.Add(base.Game.CreateNpc(m_nguoiLuuDanNong, 25 + i * 50, 909, 1, 1, base.Game.BaseLivingConfig()));
					}
				}
				else if (isLife)
				{
					redNpc.Add(base.Game.CreateNpc(m_luuDanBupBe, 1700 - i * 100, 877, 1, -1, base.Game.BaseLivingConfig()));
				}
				else
				{
					redNpc.Add(base.Game.CreateNpc(m_luuDanBupBe, 30 + i * 50, 909, 1, 1, base.Game.BaseLivingConfig()));
				}
			}
			isLife = ((!isLife) ? true : false);
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
