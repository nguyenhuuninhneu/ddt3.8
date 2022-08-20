using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class DTS701 : AMissionControl
    {
        private int m_Franken = 10101;

        private int m_Doi = 10102;

        private int m_nhaThamHiem = 8104;

        private int m_ball1 = 32;

        private int m_ball2 = 35;

        private Player m_deTu;

        private Player m_suPhu;

        private SimpleBoss boss;

        private int Count;

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
				m_Franken,
				m_Doi,
				m_nhaThamHiem
			};
			base.Game.LoadResources(npcIds);
			base.Game.LoadNpcGameOverResources(npcIds);
			base.Game.AddLoadingFile(1, $"bombs/{m_ball1}.swf", $"tank.resource.bombs.Bomb{m_ball1}");
			base.Game.AddLoadingFile(1, $"bombs/{m_ball2}.swf", $"tank.resource.bombs.Bomb{m_ball2}");
			base.Game.SetMap(1188);
        }

        public override void OnNewTurnStarted()
        {
			base.OnNewTurnStarted();
			if (m_deTu != null)
			{
				m_deTu.SetBall(m_ball1);
			}
			if (m_suPhu != null)
			{
				m_suPhu.SetBall(m_ball2);
			}
        }

        public override void OnPrepareNewGame()
        {
			base.OnPrepareNewGame();
			CheckGradePlayer();
			m_deTu.BoltMove(833, 990, 0);
			m_suPhu.BoltMove(388, 851, 0);
        }

        public override void OnStartGame()
        {
			base.OnStartGame();
			LivingConfig livingConfig = base.Game.BaseLivingConfig();
			livingConfig.BallCanDamage = m_ball1;
			livingConfig.IsFly = true;
			boss = base.Game.CreateBoss(m_Franken, 986, 438, -1, 1, "born", livingConfig);
			boss.PlayMovie("stand", 4000, 0);
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
			if (boss != null && !boss.IsLiving)
			{
				Count = 1;
				return true;
			}
			return false;
        }

        public override int UpdateUIData()
        {
			base.UpdateUIData();
			return Count;
        }

        public override void OnGameOver()
        {
			base.OnGameOver();
			if (Count >= base.Game.MissionInfo.TotalCount && base.Game.GetLivedLivings().Count == 0)
			{
				base.Game.IsWin = true;
			}
			else
			{
				base.Game.IsWin = false;
			}
        }

        private void CheckGradePlayer()
        {
			foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				if (allLivingPlayer.PlayerDetail.PlayerCharacter.masterID != 0 && allLivingPlayer.PlayerDetail.PlayerCharacter.Grade < 20)
				{
					m_deTu = allLivingPlayer;
				}
				else
				{
					m_suPhu = allLivingPlayer;
				}
			}
        }
    }
}
