using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class BLTTAH1126 : AMissionControl
    {
		private PhysicalObj m_kingMoive;

		private PhysicalObj m_kingFront;

		private SimpleBoss m_king = null;

		private SimpleBoss m_secondKing = null;

		private int IsSay = 0;

		private int m_kill = 0;

		private int m_state = 3316;

		private int turn = 0;

		private int firstBossID = 3316;

		private int secondBossID = 3317;

		private int npcID = 3303;

		private int npcID3 = 3318;

		private int npcID2 = 3312;

		private int npcID1 = 3313;

		private int direction;



		private static string[] KillPlayerChat = new string[]{
			"Đùa với các ngươi chán quá!",

			"Trình chỉ tới vậy sao?",
			"Ta mới sử dụng 1 phần công lực thôi đó."
		};

		private static string[] AngryChat = new string[]{
			"Dám chọc giận ta à?",
			"Ta né, ta né!!!",
			"Đồ khốn nạn. Dám đánh ta!!"
		};

		public override int CalculateScoreGrade(int score)
        {
			base.CalculateScoreGrade(score);
			if (score > 1150)
			{
				return 3;
			}
			if (score > 925)
			{
				return 2;
			}
			if (score > 700)
			{
				return 1;
			}
			return 0;
        }

        public override void OnPrepareNewSession()
        {
			base.OnPrepareNewSession();
			base.Game.AddLoadingFile(1, "bombs/55.swf", "tank.resource.bombs.Bomb55");
			base.Game.AddLoadingFile(1, "bombs/54.swf", "tank.resource.bombs.Bomb54");
			base.Game.AddLoadingFile(1, "bombs/53.swf", "tank.resource.bombs.Bomb53");
			base.Game.AddLoadingFile(2, "image/map/1126/object/1126object.swf", "game.assetmap.Flame");
			base.Game.AddLoadingFile(2, "image/map/1076/objects/1076mapasset.swf", "com.mapobject.asset.wordtip75");
			base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
			base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.ClanLeaderAsset");
			int[] npcIds = new int[6]
			{
				firstBossID,
				secondBossID,
				npcID,
				npcID2,
				npcID1,
				npcID3
			};
			base.Game.LoadResources(npcIds);
			int[] npcIds2 = new int[1]
			{
				firstBossID
			};
			base.Game.LoadNpcGameOverResources(npcIds2);
			base.Game.SetMap(1126);
        }

        public override void OnStartGame()
        {
			base.OnStartGame();
			m_kingMoive = base.Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 1);
			m_kingFront = base.Game.Createlayer(700, 355, "font", "game.asset.living.ClanLeaderAsset", "out", 1, 1);
			m_king = base.Game.CreateBoss(m_state, 800, 400, -1, 1, "");
			m_king.FallFrom(800, 400, "fall", 0, 2, 1200, null);
			m_king.SetRelateDemagemRect(-42, -187, 75, 187);
			m_king.Say("Đến đây thôi, dám ngăn cản nghi lễ của ta, không muốn sống à!", 0, 2000);
			m_kingMoive.PlayMovie("in", 7000, 0);
			m_kingFront.PlayMovie("in", 7000, 0);
			m_kingMoive.PlayMovie("out", 13000, 0);
			m_kingFront.PlayMovie("out", 13400, 0);
			turn = base.Game.TurnIndex;
			base.Game.BossCardCount = 1;
        }

        public override void OnNewTurnStarted()
        {
			base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			if (base.Game.TurnIndex > turn + 1)
			{
				if (m_kingMoive != null)
				{
					base.Game.RemovePhysicalObj(m_kingMoive, sendToClient: true);
					m_kingMoive = null;
				}
				if (m_kingFront != null)
				{
					base.Game.RemovePhysicalObj(m_kingFront, sendToClient: true);
					m_kingFront = null;
				}
			}
			IsSay = 0;
        }

        public override bool CanGameOver()
        {
			base.CanGameOver();
			if (!m_king.IsLiving && m_state == firstBossID)
			{
				m_state = secondBossID;
			}
			if (!m_king.IsLiving && m_secondKing == null)
			{
				base.Game.ClearAllChild();
			}
			if (m_state == secondBossID && m_secondKing == null)
			{
				m_secondKing = base.Game.CreateBoss(m_state, m_king.X, m_king.Y, m_king.Direction, 1, "born");
				base.Game.RemoveLiving(m_king.Id);
				m_secondKing.SetRelateDemagemRect(m_secondKing.NpcInfo.X, m_secondKing.NpcInfo.Y, m_secondKing.NpcInfo.Width, m_secondKing.NpcInfo.Height);
				turn = base.Game.TurnIndex;
			}
			if (m_state == secondBossID && m_secondKing != null && !m_secondKing.IsLiving)
			{
				direction = m_secondKing.Direction;
				m_kill++;
				return true;
			}
			return false;
        }

        public override int UpdateUIData()
        {
			base.UpdateUIData();
			return m_kill;
        }

        public override void OnGameOver()
        {
			base.OnGameOver();
			if (m_state == secondBossID && !m_secondKing.IsLiving)
			{
				base.Game.IsWin = true;
			}
			else
			{
				base.Game.IsWin = false;
			}
        }

        public override void DoOther()
        {
			base.DoOther();
			if (m_king != null && m_king.IsLiving)
			{
				int num = base.Game.Random.Next(0, KillPlayerChat.Length);
				m_king.Say(KillPlayerChat[num], 0, 0);
			}
        }

        public override void OnShooted()
        {
			if (IsSay == 0 && m_king.IsLiving)
			{
				int num = base.Game.Random.Next(0, AngryChat.Length);
				m_king.Say(AngryChat[num], 0, 1000);
				IsSay = 1;
			}
        }
    }
}
