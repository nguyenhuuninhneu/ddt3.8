using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.Messions
{
    public class DCR5104 : AMissionControl
    {
        private List<PhysicalObj> phyObjects = new List<PhysicalObj>();

        private SimpleBoss m_boss = null;

        private SimpleBoss m_npcHelper = null;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private int m_kill = 0;

        private int bossId = 5131;

        private int npcId = 5132;

        private int npcHelperId1 = 5133; // nha tham hiem

        private int npcHelperId2 = 5134; // pha le

        private int m_map = 1154;

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
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.hongpaoxiaoemoAsset");
            base.Game.AddLoadingFile(2, "image/game/effect/5/cuipao.swf", "asset.game.4.cuipao");
            base.Game.AddLoadingFile(2, "image/game/effect/5/guang.swf", "asset.game.4.guang");
            base.Game.AddLoadingFile(2, "image/game/effect/5/da.swf", "asset.game.4.da");
            base.Game.AddLoadingFile(2, "image/game/effect/5/mubiao.swf", "asset.game.4.mubiao");
            int[] resources = { bossId, npcHelperId1, npcHelperId2, npcId };
            base.Game.LoadResources(resources);
            int[] gameOverResources = { bossId };
            base.Game.LoadNpcGameOverResources(gameOverResources);
            base.Game.SetMap(m_map);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_kingMoive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_kingFront = base.Game.Createlayer(1291, 257, "top", "game.asset.living.xieyanjulongAsset", "out", 1, 1);
            LivingConfig livingConfig = base.Game.BaseLivingConfig();
            livingConfig.IsFly = true;
            m_boss = base.Game.CreateBoss(bossId, 1700, 480, -1, 1, "", livingConfig);
            m_boss.SetRect(-180, -90, 300, 100);
            m_boss.SetRelateDemagemRect(-60, -200, 116, 100);
            base.Game.SendHideBlood(m_boss, 0);
            m_boss.CallFuction(EffectCuiPao, 3300);
            m_boss.CallFuction(MoveAllPlayer, 3300);
            m_boss.CallFuction(SetDefaultSpeedMult, 6000);
            m_kingMoive.PlayMovie("in", 7000, 0);
            m_kingFront.PlayMovie("in", 7000, 0);
            m_kingMoive.PlayMovie("out", 10000, 0);
            m_kingFront.PlayMovie("out", 10000, 0);
            m_boss.CallFuction(CreateHelperNpc, 11000);
        }

        private void CreateHelperNpc()
        {
            LivingConfig config = base.Game.BaseLivingConfig();
            config.IsFly = true;
            config.CanTakeDamage = false;
            config.IsHelper = true;
            m_npcHelper = base.Game.CreateBoss(npcHelperId1, 190, 250, 1, 2, "", config);
            m_npcHelper.Delay++;
            base.Game.SendHideBlood(m_npcHelper, 0);
            m_npcHelper.Say("Đừng sợ. Đã có ta ở đây.", 0, 3000, 2000);
        }

        private void EffectCuiPao()
        {
            foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
            {
                phyObjects.Add(base.Game.CreatePhysicalObj(0, 0, "top", "asset.game.4.cuipao", "", 1, 1, allLivingPlayer.Id + 1));
            }
        }

        private void MoveAllPlayer()
        {
            foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
            {
                allLivingPlayer.SpeedMultX(18);
                allLivingPlayer.StartSpeedMult(750 + base.Game.Random.Next(0, 50), allLivingPlayer.Y, 0);
            }
        }

        private void SetDefaultSpeedMult()
        {
            foreach (Player allLivingPlayer in base.Game.GetAllLivingPlayers())
            {
                allLivingPlayer.SpeedMultX(3);
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
            foreach (PhysicalObj item in phyObjects)
            {
                base.Game.RemovePhysicalObj(item, sendToClient: true);
            }
            phyObjects = new List<PhysicalObj>();
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
