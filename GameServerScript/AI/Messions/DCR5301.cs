using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class DCR5301 : AMissionControl
    {
        private SimpleBoss m_boss = null;

        private SimpleNpc m_helper = null;

        private SimpleNpc m_npc = null;

        private PhysicalObj m_specialEffect = null;

        private PhysicalObj m_dianEffect = null;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private int m_kill = 0;

        private int bossId = 5301; //quypuchi

        private int npcId1 = 5302; //nole

        private int npcId2 = 5303; //banh rang

        private int helperId = 5304; // nha tham hiem

        private int m_map = 1151;

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
            base.Game.AddLoadingFile(1, "bombs/72.swf", "tank.resource.bombs.Bomb72");
            base.Game.AddLoadingFile(2, "image/game/effect/5/zap.swf", "asset.game.4.zap");
            base.Game.AddLoadingFile(2, "image/game/effect/5/zap2.swf", "asset.game.4.zap2");
            base.Game.AddLoadingFile(2, "image/game/effect/5/dian.swf", "asset.game.4.dian");
            base.Game.AddLoadingFile(2, "image/game/effect/5/minigun.swf", "asset.game.4.minigun");
            base.Game.AddLoadingFile(2, "image/game/effect/5/jinqud.swf", "asset.game.4.jinqud");
            base.Game.AddLoadingFile(2, "image/game/effect/5/xiaopao.swf", "asset.game.4.xiaopao");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.gebulinzhihuiguanAsset");
            int[] resources = { bossId, npcId1, npcId2, helperId };
            base.Game.LoadResources(resources);
            int[] gameOverResources = { bossId };
            base.Game.LoadNpcGameOverResources(gameOverResources);
            base.Game.SetMap(m_map);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_kingMoive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_kingFront = base.Game.Createlayer(1172, 587, "front", "game.asset.living.gebulinzhihuiguanAsset", "out", 1, 1);
            LivingConfig livingConfig = base.Game.BaseLivingConfig();
            livingConfig.IsFly = true;
            m_boss = base.Game.CreateBoss(bossId, 1484, 750, -1, 1, "born", livingConfig);
            base.Game.SendHideBlood(m_boss, 0);
            livingConfig = base.Game.BaseLivingConfig();
            livingConfig.IsTurn = false;
            m_helper = base.Game.CreateNpc(helperId, 1287, 859, 0, 1, livingConfig);
            base.Game.SendHideBlood(m_helper, 0);
            base.Game.SendObjectFocus(m_helper, 1, 700, 0);
            m_helper.Say("Haha, ta thích cái máy này!", 0, 2000);
            m_helper.MoveTo(1388, 867, "walk", 4000, StepAfterWalk);
        }

        private void StepAfterWalk()
        {
            m_specialEffect = base.Game.Createlayer(1470, 822, "", "asset.game.4.jinqud", "", 1, 1);
            m_dianEffect = base.Game.Createlayer(m_helper.X, m_helper.Y, "", "asset.game.4.dian", "", 1, 1);
            m_helper.PlayMovie("outA", 500, 2000);
            m_helper.Die(2500);
            m_boss.PlayMovie("in", 3000, 5000);
            m_kingMoive.PlayMovie("in", 5000, 0);
            m_kingFront.PlayMovie("in", 5200, 0);
            m_kingMoive.PlayMovie("out", 8000, 0);
            m_kingFront.PlayMovie("out", 8200, 0);
            m_boss.CallFuction(CreateProtectNpc, 10000);
        }

        private void CreateProtectNpc()
        {
            LivingConfig config = base.Game.BaseLivingConfig();
            config.IsTurn = false;
            config.CanTakeDamage = false;
            m_npc = base.Game.CreateNpc(npcId1, 187, 370, 1, 1, config);
            base.Game.SendLivingActionMapping(m_npc, "stand", "standA");
            base.Game.SendObjectFocus(m_npc, 1, 700, 0);
            m_npc.PlayMovie("in", 1500, 10000);
            m_npc.PlayMovie("walkA", 9000, 3000);
        }

        private void RemoveDianEffect()
        {
            if (m_dianEffect != null)
            {
                base.Game.RemovePhysicalObj(m_dianEffect, sendToClient: true);
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
            RemoveDianEffect();
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

        private void CallActionEndGame()
        {
            m_helper = base.Game.CreateNpc(helperId, 243, 368, 0, 1);
            base.Game.SendObjectFocus(m_helper, 1, 0, 500);
            m_helper.Say("Đừng có để hắn trốn thoát.", 0, 1000);
            m_helper.Say("Grrr. Máy bị các ngươi làm hư cmnr.", 0, 3000, 2000);
        }

        public override void OnShooted()
        {
            base.OnShooted();
            if (m_boss != null && !m_boss.IsLiving)
            {
                int waitTimerLeft = base.Game.GetWaitTimerLeft();
                base.Game.ClearAllChild();
                m_boss.CallFuction(CallActionEndGame, waitTimerLeft + 3000);
            }
        }

        public override void OnDied()
        {
            base.OnDied();
        }
    }
}
