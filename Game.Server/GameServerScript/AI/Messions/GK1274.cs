using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class GK1274 : AMissionControl
    {
        private SimpleBoss m_king = null;

        private SimpleBoss m_secondKing = null;

        private int m_kill;

        private int IsSay;

        private int bossID = 1207;

        private int npcID = 1204;

        private int turn;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private static string[] KillChat = {
                "Chỉ được zậy thôi sao ?",
                "Ai ya~đánh đau quá! Ah hahahaha ?",
                "A~cũng được lấm."
        };

        private static string[] ShootedChat = {
                "Tưởng thắng rồi sao ? Chưa kết thúc đâu! Tôi còn quay lại!"
        };

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 900)
            {
                return 3;
            }
            if (score > 825)
            {
                return 2;
            }
            if (score > 725)
            {
                return 1;
            }
            return 0;
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] resources = { npcID, bossID };
            base.Game.LoadResources(resources);
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BombKingAsset");
            base.Game.LoadNpcGameOverResources(resources);
            base.Game.SetMap(1076);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_kingMoive = base.Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_kingFront = base.Game.Createlayer(720, 455, "font", "game.asset.living.boguoKingAsset", "out", 1, 1);
            m_king = base.Game.CreateBoss(bossID, 888, 510, -1, 1, "");
            m_king.FallFrom(888, 510, "fall", 0, 2, 1000);
            m_king.SetRelateDemagemRect(-41, -187, 83, 140);
            m_kingMoive.PlayMovie("in", 9000, 0);
            m_kingFront.PlayMovie("in", 9000, 0);
            m_kingMoive.PlayMovie("out", 13000, 0);
            m_kingFront.PlayMovie("out", 13400, 0);
            m_king.AddDelay(16);
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            IsSay = 0;
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
        }

        public override bool CanGameOver()
        {
            if (!m_king.IsLiving)
            {
                m_kill++;
                return true;
            }
            if (base.Game.TurnIndex > base.Game.MissionInfo.TotalTurn - 1)
            {
                return true;
            }
            return false;
        }

        public override int UpdateUIData()
        {
            return m_kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            bool result = true;
            foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
            {
                if (allFightPlayer.IsLiving)
                {
                    result = false;
                }
            }
            if (!m_king.IsLiving && !result)
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
            int index = base.Game.Random.Next(0, KillChat.Length);
            m_king.Say(KillChat[index], 0, 0);
        }

        public override void OnShooted()
        {
            if (m_king.IsLiving && IsSay == 0)
            {
                int index = base.Game.Random.Next(0, ShootedChat.Length);
                m_king.Say(ShootedChat[index], 0, 1500);
                IsSay = 1;
            }
        }
    }
}
