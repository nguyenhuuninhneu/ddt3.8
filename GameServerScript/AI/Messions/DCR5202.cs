using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.Messions
{
    public class DCR5202 : AMissionControl
    {
        private SimpleBoss m_boss = null;

        private SimpleNpc m_helper = null;

        private SimpleBoss m_npcRight = null;

        private SimpleBoss m_npcLeft = null;

        private SimpleNpc m_npcBottom = null;

        private SimpleNpc m_npcCenter = null;

        private SimpleNpc m_npc = null;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private List<PhysicalObj> m_targetEffect = new List<PhysicalObj>();

        private PhysicalObj m_wallBlock;

        private int m_kill = 0;

        private int bossId = 5214;

        private int npcId = 5211;

        private int npcLeftId = 5212;

        private int npcRightId = 5213;

        private int npcBottomId = 5216;

        private int npcCenterId = 5217;

        private int npcHelperId = 5204;

        private int m_map = 1152;

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
            base.Game.AddLoadingFile(1, "bombs/56.swf", "tank.resource.bombs.Bomb56");
            base.Game.AddLoadingFile(2, "image/game/effect/5/jinqudan.swf", "asset.game.4.jinqudan");
            base.Game.AddLoadingFile(2, "image/game/effect/5/mubiao.swf", "asset.game.4.mubiao");
            base.Game.AddLoadingFile(2, "image/game/effect/5/zao.swf", "asset.game.4.zao");
            base.Game.AddLoadingFile(2, "image/game/effect/5/xiaopao.swf", "asset.game.4.xiaopao");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.gebulinzhihuiguanAsset");
            int[] resources = { bossId, npcLeftId, npcRightId, npcBottomId, npcCenterId, npcHelperId, npcId };
            base.Game.LoadResources(resources);
            int[] gameOverResources = { bossId };
            base.Game.LoadNpcGameOverResources(gameOverResources);
            base.Game.SetMap(m_map);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            CreateBossAndNpc();
            CreateEffectBegin();
        }

        private void CreateBossAndNpc()
        {
            m_kingMoive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_kingFront = base.Game.Createlayer(1300, 413, "front", "game.asset.living.gebulinzhihuiguanAsset", "out", 1, 1);
            m_boss = base.Game.CreateBoss(bossId, 1478, 596, -1, 1, "born", base.Game.BaseLivingConfig());
            m_boss.SetRelateDemagemRect(m_boss.NpcInfo.X, m_boss.NpcInfo.Y, m_boss.NpcInfo.Width, m_boss.NpcInfo.Height);
            m_boss.Config.CanTakeDamage = false;
            base.Game.SendHideBlood(m_boss, 0);
            m_wallBlock = base.Game.Createlayer(m_boss.X, m_boss.Y, "", "asset.game.4.zao", "stand", 1, 1);
            m_npcLeft = base.Game.CreateBoss(npcLeftId, 1323, 663, -1, 1, "", base.Game.BaseLivingConfig());
            m_npcLeft.SetRelateDemagemRect(m_npcLeft.NpcInfo.X, m_npcLeft.NpcInfo.Y, m_npcLeft.NpcInfo.Width, m_npcLeft.NpcInfo.Height);
            m_npcLeft.Config.IsTurn = false;
            m_npcRight = base.Game.CreateBoss(npcRightId, 1664, 532, -1, 1, "", base.Game.BaseLivingConfig());
            m_npcRight.SetRelateDemagemRect(m_npcRight.NpcInfo.X, m_npcRight.NpcInfo.Y, m_npcRight.NpcInfo.Width, m_npcRight.NpcInfo.Height);
            m_npcRight.Config.IsTurn = false;
            m_npcBottom = base.Game.CreateNpc(npcBottomId, 1360, 840, 1, -1, base.Game.BaseLivingConfig());
            m_npcBottom.Config.IsTurn = false;
            m_npcBottom.OnSmallMap(state: false);
            base.Game.SendHideBlood(m_npcBottom, 0);
            m_npcCenter = base.Game.CreateNpc(npcCenterId, 1546, 650, 1, -1, base.Game.BaseLivingConfig());
            m_npcCenter.Config.IsTurn = false;
            m_npcCenter.OnSmallMap(state: false);
            base.Game.SendHideBlood(m_npcCenter, 0);
            m_npc = base.Game.CreateNpc(npcId, 503, 831, 1, 1, base.Game.BaseLivingConfig());
            m_npc.Config.IsTurn = true;
            m_npc.Config.CanTakeDamage = false;
            m_npc.OnSmallMap(state: false);
            base.Game.SendHideBlood(m_npc, 0);
        }

        private void CreateEffectBegin()
        {
            m_helper = base.Game.CreateNpc(npcHelperId, 1022, 828, 0, 1, "standB", null);
            base.Game.SendObjectFocus(m_helper, 1, 1000, 0);
            m_helper.Say("Ê, hãy nếm thử sức mạnh này!", 0, 2000);
            m_helper.PlayMovie("beatA", 2000, 0);
            base.Game.SendObjectFocus(m_npcBottom, 1, 4000, 0);
            m_npcBottom.PlayMovie("beatA", 5000, 0);
            m_helper.PlayMovie("outB", 7000, 0);
            m_helper.Die(10000);
            base.Game.SendObjectFocus(m_boss, 1, 10000, 0);
            m_boss.PlayMovie("beatC", 10500, 0);
            m_kingMoive.PlayMovie("in", 11000, 0);
            m_kingFront.PlayMovie("in", 11000, 0);
            m_kingMoive.PlayMovie("out", 16000, 6000);
            m_kingFront.PlayMovie("out", 16000, 6000);
            m_boss.CallFuction(CreateTargetEffect, 17000);
        }

        private void CreateTargetEffect()
        {
            m_targetEffect.Add(base.Game.Createlayer(m_boss.X, m_boss.Y, "", "asset.game.4.mubiao", "", 1, 1));
            m_targetEffect.Add(base.Game.Createlayer(m_npcLeft.X, m_npcLeft.Y, "", "asset.game.4.mubiao", "", 1, 1));
            m_targetEffect.Add(base.Game.Createlayer(m_npcRight.X, m_npcRight.Y, "", "asset.game.4.mubiao", "", 1, 1));
        }

        private void RemoveShield()
        {
            if (m_wallBlock != null)
            {
                base.Game.RemovePhysicalObj(m_wallBlock, sendToClient: true);
                m_wallBlock = null;
            }
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
            if (m_targetEffect.Count <= 0)
            {
                return;
            }
            foreach (PhysicalObj item in m_targetEffect)
            {
                base.Game.RemovePhysicalObj(item, sendToClient: true);
            }
            m_targetEffect = new List<PhysicalObj>();
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
            if (!m_npcLeft.IsLiving && !m_npcRight.IsLiving && m_wallBlock != null)
            {
                base.Game.SendObjectFocus(m_wallBlock, 1, 1000, 0);
                m_wallBlock.PlayMovie("die", 2000, 2000);
                m_boss.Config.CanTakeDamage = true;
                m_boss.CallFuction(RemoveShield, 5000);
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
        }
    }
}
