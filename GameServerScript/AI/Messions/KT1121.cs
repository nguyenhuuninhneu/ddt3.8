using Bussiness;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class KT1121 : AMissionControl
    {
        private SimpleBoss boss;

        private int npcID = 2104;

        private int bossID = 2103;

        private int IsSay = 0;

        private int kill;

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private static string[] KillPlayerChat = {
                "Ah, mặt tôi .....",
                "Ặc, giáp trụ xinh đẹp của mình đã bị trầy rồi.....",
                "Ui za, đau quá !"
        };

        private static string[] AngryChat = {
                "Ah, của tôi hết đừng lấy！"
        };

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 1750)
            {
                return 3;
            }
            if (score > 1675)
            {
                return 2;
            }
            if (score > 1600)
            {
                return 1;
            }
            return 0;
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] resources = { bossID, npcID };
            int[] gameOverResources = { bossID };
            base.Game.LoadResources(resources);
            base.Game.LoadNpcGameOverResources(gameOverResources);
            base.Game.AddLoadingFile(1, "bombs/51.swf", "tank.resource.bombs.Bomb51");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.AntQueenAsset");
            base.Game.SetMap(1121);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_moive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_front = base.Game.Createlayer(1131, 150, "font", "game.asset.living.AntQueenAsset", "out", 1, 1);
            boss = base.Game.CreateBoss(bossID, 1316, 444, -1, 1, "");
            boss.SetRelateDemagemRect(-42, -200, 84, 194);
            boss.Say(LanguageMgr.GetTranslation("Hộp là của tôi, con tôi, miễn là tôi có thể thấy là của tôi!"), 0, 200, 0);
            m_moive.PlayMovie("in", 6000, 0);
            m_front.PlayMovie("in", 6100, 0);
            m_moive.PlayMovie("out", 10000, 1000);
            m_front.PlayMovie("out", 9900, 0);
        }

        public override void OnNewTurnStarted()
        {
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (base.Game.TurnIndex > 1)
            {
                if (m_moive != null)
                {
                    base.Game.RemovePhysicalObj(m_moive, sendToClient: true);
                    m_moive = null;
                }
                if (m_front != null)
                {
                    base.Game.RemovePhysicalObj(m_front, sendToClient: true);
                    m_front = null;
                }
            }
        }

        public override bool CanGameOver()
        {
            if (boss != null && !boss.IsLiving)
            {
                kill++;
                return true;
            }
            return false;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (!boss.IsLiving)
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
            int index = base.Game.Random.Next(0, KillPlayerChat.Length);
            boss.Say(KillPlayerChat[index], 0, 0);
        }

        public override void OnShooted()
        {
            if (boss.IsLiving && IsSay == 0)
            {
                int index = base.Game.Random.Next(0, AngryChat.Length);
                boss.Say(AngryChat[index], 0, 1500);
                IsSay = 1;
            }
        }
    }
}
