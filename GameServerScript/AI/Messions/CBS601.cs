using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class CBS601 : AMissionControl
    {
        private int m_Anna = 11101;

        private int m_bachTuocCon = 11102;

        private int m_trauNuoc = 11105;

        private int m_nhaThamHiem = 8104;

        private int m_ball = 36;

        private Player m_deTu;

        private Player m_suPhu;

        private SimpleBoss boss;

        private SimpleNpc npc;

        //private SimpleNpc simpleNpc_1;

        private int TotalCount;

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
			int[] npcIds = new int[4]
			{
				m_Anna,
				m_bachTuocCon,
				m_trauNuoc,
				m_nhaThamHiem
			};
			base.Game.LoadResources(npcIds);
			base.Game.LoadNpcGameOverResources(npcIds);
			base.Game.AddLoadingFile(1, $"bombs/{m_ball}.swf", $"tank.resource.bombs.Bomb{m_ball}");
			base.Game.SetMap(1189);
        }

        public override void OnNewTurnStarted()
        {
			base.OnNewTurnStarted();
			if (npc != null && !npc.IsLiving)
			{
				m_suPhu.BlockTurn = false;
				m_suPhu.BoltMove(649, 469, 100);
				npc = null;
			}
			if (!m_suPhu.BlockTurn)
			{
				if (m_deTu != null)
				{
					m_deTu.SetBall(m_ball);
				}
				if (m_suPhu != null)
				{
					m_suPhu.SetBall(m_ball);
				}
			}
        }

        public override void OnPrepareNewGame()
        {
			base.OnPrepareNewGame();
			CheckGradePlayer();
			m_deTu.BoltMove(214, 850, 0);
			m_suPhu.BoltMove(649, 452, 0);
			m_suPhu.BlockTurn = true;
        }

        public override void OnStartGame()
        {
			base.OnStartGame();
			LivingConfig livingConfig = base.Game.BaseLivingConfig();
			livingConfig.BallCanDamage = m_ball;
			livingConfig.IsFly = true;
			boss = base.Game.CreateBoss(m_Anna, 1179, 681, -1, 1, "born", livingConfig);
			LivingConfig livingConfig2 = base.Game.BaseLivingConfig();
			livingConfig2.IsFly = true;
			livingConfig2.IsTurn = false;
			npc = base.Game.CreateNpc(m_trauNuoc, 646, 628, 1, -1, livingConfig2);
			LivingConfig livingConfig3 = base.Game.BaseLivingConfig();
			livingConfig3.IsFly = true;
			livingConfig3.CanTakeDamage = false;
			livingConfig3.IsTurn = false;
			//simpleNpc_1 = base.Game.CreateNpc(int_2, 1282, 720, 1, -1, livingConfig3);
			//base.Game.SendHideBlood(simpleNpc_1, 0);
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
				TotalCount = 1;
				return true;
			}
			return false;
        }

        public override int UpdateUIData()
        {
			base.UpdateUIData();
			return TotalCount;
        }

        public override void OnGameOver()
        {
			base.OnGameOver();
			if (TotalCount >= base.Game.MissionInfo.TotalCount && base.Game.GetLivedLivings().Count == 0)
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
