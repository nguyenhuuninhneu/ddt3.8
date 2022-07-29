using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.Messions
{
    public class BLTTK1122 : AMissionControl
    {
		private List<SimpleNpc> SomeNpc = new List<SimpleNpc>();

		private SimpleBoss boss = null;

		private int redTotalCount = 0;

		private int redNpcID = 3201;

		private int blueNpcID = 3204;

		public override int CalculateScoreGrade(int score)
        {
			base.CalculateScoreGrade(score);
			if (score > 600)
			{
				return 3;
			}
			if (score > 520)
			{
				return 2;
			}
			if (score > 450)
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
				blueNpcID,
				redNpcID
			};
			base.Game.AddLoadingFile(1, "bombs/58.swf", "tank.resource.bombs.Bomb58");
			base.Game.LoadResources(npcIds);
			base.Game.LoadNpcGameOverResources(npcIds);
			base.Game.SetMap(1122);
        }

        public override void OnStartGame()
        {
			base.OnStartGame();
			if (base.Game.GetLivedLivings().Count == 0)
			{
				base.Game.PveGameDelay = 0;
			}
			for (int i = 0; i < 4; i++)
			{
				redTotalCount++;
				if (i < 1)
				{
					SomeNpc.Add(base.Game.CreateNpc(redNpcID, 900 + (i + 1) * 100, 444, -1, 1));
				}
				else if (i < 3)
				{
					SomeNpc.Add(base.Game.CreateNpc(redNpcID, 920 + (i + 1) * 100, 444, -1, 1));
				}
				else
				{
					SomeNpc.Add(base.Game.CreateNpc(redNpcID, 1000 + (i + 1) * 100, 444, -1, 1));
				}
			}
			redTotalCount++;
			boss = base.Game.CreateBoss(blueNpcID, 1300, 444, -1, 10, "");
			boss.FallFrom(boss.X, boss.Y, "", 0, 0, 1000, null);
        }

        public override void OnNewTurnStarted()
        {
			base.OnNewTurnStarted();
			if (base.Game.GetLivedLivings().Count == 0)
			{
				base.Game.PveGameDelay = 0;
			}
			if (base.Game.TurnIndex <= 1 || base.Game.GetLivedLivings().Count >= 4)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				if (redTotalCount < 20)
				{
					redTotalCount++;
					if (i < 1)
					{
						SomeNpc.Add(base.Game.CreateNpc(redNpcID, 900 + (i + 1) * 100, 444, -1, 1));
					}
					else if (i < 3)
					{
						SomeNpc.Add(base.Game.CreateNpc(redNpcID, 920 + (i + 1) * 100, 444, -1, 1));
					}
					else
					{
						SomeNpc.Add(base.Game.CreateNpc(redNpcID, 1000 + (i + 1) * 100, 444, -1, 1));
					}
				}
			}
			if (redTotalCount < 20 && !boss.IsLiving)
			{
				redTotalCount++;
				boss = base.Game.CreateBoss(blueNpcID, 1300, 444, -1, 10, "");
				boss.FallFrom(boss.X, boss.Y, "", 0, 0, 1000, null);
			}
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
        }

        public override bool CanGameOver()
        {
			base.CanGameOver();
			if (base.Game.GetLivedLivings().Count == 0 && !boss.IsLiving && base.Game.TotalKillCount == base.Game.MissionInfo.TotalCount)
			{
				base.Game.IsWin = true;
				return true;
			}
			return false;
        }

        public override int UpdateUIData()
        {
			return base.Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
			base.OnGameOver();
			if (base.Game.GetLivedLivings().Count == 0 && !boss.IsLiving && base.Game.TotalKillCount == base.Game.MissionInfo.TotalCount)
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
