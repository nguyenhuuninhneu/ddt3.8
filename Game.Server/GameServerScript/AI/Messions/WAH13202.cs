using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class WAH13202 : AMissionControl
    {
        private int bossId = 13205;

        private int npcLeftId = 13203;

        private int npcRightId = 13204;

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private PhysicalObj m_zhuzi;

        private SimpleBoss boss;

        private SimpleNpc npcLeft;

        private SimpleBoss npcRight;

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

        public override bool CanGameOver()
        {
            base.CanGameOver();
            if (boss != null && !boss.IsLiving)
            {
                return true;
            }
            if (base.Game.TotalTurn > 200)
            {
                return true;
            }
            return false;
        }

        private void CreateBossLeft()
        {
            LivingConfig config = base.Game.BaseLivingConfig();
            config.CanTakeDamage = false;
            config.IsFly = true;
            config.IsTurn = false;
            npcLeft = base.Game.CreateNpc(npcLeftId, 701, 594, 2, 1, "", config);
            npcLeft.SetRelateDemagemRect(npcLeft.NpcInfo.X, npcLeft.NpcInfo.Y, npcLeft.NpcInfo.Width, npcLeft.NpcInfo.Height);
            base.Game.SendHideBlood(npcLeft, 0);
            base.Game.SendObjectFocus(npcLeft, 0, 0, 0);
            boss.CallFuction(CreateBossRight, 2000);
        }

        private void CreateBossRight()
        {
            LivingConfig config = base.Game.BaseLivingConfig();
            config.CanTakeDamage = false;
            config.IsFly = true;
            npcRight = base.Game.CreateBoss(npcRightId, 1604, 594, -1, 2, "", config);
            npcRight.SetRelateDemagemRect(npcRight.NpcInfo.X, npcRight.NpcInfo.Y, npcRight.NpcInfo.Width, npcRight.NpcInfo.Height);
            npcRight.Delay = base.Game.GetHighDelayTurn() + 1;
            base.Game.SendHideBlood(npcRight, 0);
            base.Game.SendObjectFocus(npcRight, 0, 0, 0);
            base.Game.SendFreeFocus(1160, 860, 0, 2000, 0);
            m_moive.PlayMovie("in", 4000, 0);
            m_front.PlayMovie("in", 4200, 0);
            m_moive.PlayMovie("out", 7000, 0);
            m_front.PlayMovie("out", 7200, 0);
        }

        private void MovieEndGame()
        {
            base.Game.SendObjectFocus(npcLeft, 1, 3000, 0);
            npcLeft.Say("Có lẽ nào đây là giá phải trả cho nghi thức tà thần?", 0, 4000);
            npcLeft.PlayMovie("die", 6000, 0);
            npcLeft.CallFuction(RemoveLeftNpc, 7700);
            base.Game.SendObjectFocus(npcRight, 1, 8000, 0);
            npcRight.Say("Các ngươi hãy đợi đấy. Ta sẽ còn quay lại..", 1, 9000);
            npcRight.PlayMovie("die", 12000, 3000);
            npcRight.CallFuction(RemoveRightNpc, 13700);
        }

        private void RemoveLeftNpc()
        {
            base.Game.RemoveLivings(npcLeft.Id);
        }

        private void RemoveRightNpc()
        {
            base.Game.RemoveLivings(npcRight.Id);
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (m_moive != null && m_front != null)
            {
                base.Game.RemovePhysicalObj(m_moive, sendToClient: true);
                base.Game.RemovePhysicalObj(m_front, sendToClient: true);
                m_moive = null;
                m_front = null;
            }
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (boss != null && !boss.IsLiving)
            {
                base.Game.IsWin = true;
            }
            else
            {
                base.Game.IsWin = false;
            }
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] resources = { bossId, npcRightId, npcLeftId };
            base.Game.AddLoadingFile(1, "bombs/55.swf", "tank.resource.bombs.Bomb55");
            base.Game.AddLoadingFile(2, "image/game/effect/10/tuteng.swf", "asset.game.ten.baozha");
            base.Game.AddLoadingFile(2, "image/game/effect/10/tuteng.swf", "asset.game.ten.jiaodu");
            base.Game.AddLoadingFile(2, "image/game/effect/10/tuteng.swf", "asset.game.ten.pilao");
            base.Game.AddLoadingFile(2, "image/game/effect/10/jitan.swf", "asset.game.ten.jitan");
            base.Game.AddLoadingFile(2, "image/game/effect/10/gongfang.swf", "asset.game.ten.down");
            base.Game.AddLoadingFile(2, "image/game/effect/10/gongfang.swf", "asset.game.ten.up");
            base.Game.AddLoadingFile(2, "image/game/effect/10/zhuzi.swf", "asset.game.ten.zhuzi");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.ClanLeaderAsset");
            base.Game.LoadResources(resources);
            base.Game.LoadNpcGameOverResources(resources);
            base.Game.SetMap(1215);
        }

        public override void OnShooted()
        {
            base.OnShooted();
            if (boss != null && !boss.IsLiving)
            {
                int delay = base.Game.GetWaitTimerLeft();
                boss.CallFuction(MovieEndGame, delay);
            }
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_zhuzi = base.Game.Createlayer(1150, 1041, "normal", "asset.game.ten.zhuzi", "1", 1, 0);
            m_moive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_front = base.Game.Createlayer(970, 750, "front", "game.asset.living.ClanLeaderAsset", "out", 1, 0);
            boss = base.Game.CreateBoss(bossId, 1290, 1013, -1, 1, "");
            boss.SetRelateDemagemRect(boss.NpcInfo.X, boss.NpcInfo.Y, boss.NpcInfo.Width, boss.NpcInfo.Height);
            base.Game.SendObjectFocus(boss, 0, 0, 0);
            boss.CallFuction(CreateBossLeft, 2000);
        }

        public override int UpdateUIData()
        {
            return base.Game.TotalKillCount;
        }
    }
}
