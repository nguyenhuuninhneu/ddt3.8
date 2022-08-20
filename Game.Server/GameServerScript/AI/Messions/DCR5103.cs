using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class DCR5103 : AMissionControl
    {
        private SimpleBoss m_boss = null;

        private SimpleBoss m_tempBoss = null;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private int m_kill = 0;

        private int bossId = 5121;

        private int npcId1 = 5122;

        private int npcId2 = 5123;

        private int npcId3 = 5124;

        private int npcId4 = 5104;

        private int m_map = 1153;

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
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.hongpaoxiaoemoAsset");
            base.Game.AddLoadingFile(2, "image/game/effect/5/heip.swf", "asset.game.4.heip");
            base.Game.AddLoadingFile(2, "image/game/effect/5/tang.swf", "asset.game.4.tang");
            base.Game.AddLoadingFile(2, "image/game/effect/5/lanhuo.swf", "asset.game.4.lanhuo");
            int[] resources = { bossId, npcId1, npcId2, npcId3, npcId4 };
            base.Game.LoadResources(resources);
            int[] gameOverResources = { bossId };
            base.Game.LoadNpcGameOverResources(gameOverResources);
            base.Game.SetMap(m_map);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_kingMoive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_kingFront = base.Game.Createlayer(850, 258, "front", "game.asset.living.hongpaoxiaoemoAsset", "out", 1, 1);
            LivingConfig livingConfig = base.Game.BaseLivingConfig();
            livingConfig.IsFly = true;
            m_boss = base.Game.CreateBoss(bossId, 1000, 500, 1, 3, "", livingConfig);
            m_boss.SetRelateDemagemRect(m_boss.NpcInfo.X, m_boss.NpcInfo.Y, m_boss.NpcInfo.Width, m_boss.NpcInfo.Height);
            m_boss.Say("Ta đợi lâu lắm rồi!", 0, 1000);
            m_kingMoive.PlayMovie("in", 4000, 0);
            m_kingFront.PlayMovie("in", 4000, 0);
            m_kingMoive.PlayMovie("out", 7000, 0);
            m_kingFront.PlayMovie("out", 7200, 0);
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
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

        public override bool CanGameOver()
        {
            base.CanGameOver();
            if (m_boss != null && !m_boss.IsLiving)
            {
                m_kill++;
                return true;
            }
            if (base.Game.TurnIndex > 200)
            {
                return true;
            }
            return false;
        }

        private void CreateEffectEndGame()
        {
            m_tempBoss.ChangeDirection(1, 500);
            m_tempBoss.Say("Thôi ta éo đùa nữa.", 0, 1000);
            m_tempBoss.PlayMovie("out", 1000, 0);
            m_tempBoss.CallFuction(CreateNpcEndGame, 4000);
        }

        private void CreateNpcEndGame()
        {
            SimpleNpc simpleNpc = base.Game.CreateNpc(npcId4, 179, 552, 1, 1, "standC", base.Game.BaseLivingConfig());
            simpleNpc.PlayMovie("cool", 1000, 0);
            simpleNpc.Say("Thôi hãy rời khỏi đây mau. Chúa rồng đã sống lại rồi.", 0, 4000, 2000);
            base.Game.RemoveLiving(m_tempBoss.Id);
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return m_kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (m_boss != null && !m_boss.IsLiving)
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
        }

        public override void OnShooted()
        {
            base.OnShooted();
        }

        public override void OnDied()
        {
            base.OnDied();
            if (!m_boss.IsLiving)
            {
                LivingConfig livingConfig = base.Game.BaseLivingConfig();
                livingConfig.IsFly = true;
                livingConfig.CanTakeDamage = false;
                m_tempBoss = base.Game.CreateBoss(bossId, m_boss.X, m_boss.Y, m_boss.Direction, 1, "", livingConfig);
                base.Game.RemoveLiving(m_boss.Id);
                base.Game.SendHideBlood(m_tempBoss, 0);
                m_tempBoss.MoveTo(1000, 485, "fly", base.Game.GetWaitTimerLeft(), CreateEffectEndGame, 10);
            }
        }
    }
}
