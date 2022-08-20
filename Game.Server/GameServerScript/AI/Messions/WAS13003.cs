using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class WAS13003 : AMissionControl
    {
        private int bossId = 13007; // minortaur

        private int npcHelpId = 13006;

        private int npcId = 13010; // hop hinh cu

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private SimpleBoss boss = null;

        private SimpleBoss bossHelp = null;

        private SimpleBoss m_tempBoss = null;

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 600)
            {
                return 3;
            }
            else if (score > 520)
            {
                return 2;
            }
            else if (score > 450)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] resources = { bossId, npcHelpId, npcId };

            Game.AddLoadingFile(2, "image/game/effect/10/chengtuo.swf", "asset.game.ten.chengtuo"); // ta 1k
            Game.AddLoadingFile(2, "image/game/effect/10/laotie.swf", "asset.game.ten.laotie"); // fire
            Game.AddLoadingFile(2, "image/game/effect/10/laotie.swf", "asset.game.ten.laotie2"); // fire
            Game.AddLoadingFile(2, "image/game/effect/5/lanhuo.swf", "asset.game.4.lanhuo"); // effect move player
            Game.AddLoadingFile(2, "image/game/effect/5/heip.swf", "asset.game.4.heip"); //global attacking

            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.dadangAsset");

            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.SetMap(1216);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();

            m_moive = Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_front = Game.Createlayer(1200, 700, "front", "game.asset.living.dadangAsset", "out", 1, 0);

            LivingConfig config = Game.BaseLivingConfig();
            config.KeepLife = true;
            config.IsShowBloodBar = true;
            boss = Game.CreateBoss(bossId, 1683, 1012, -1, 1, "", config);
            boss.SetRelateDemagemRect(boss.NpcInfo.X, boss.NpcInfo.Y, boss.NpcInfo.Width, boss.NpcInfo.Height);

            Game.SendObjectFocus(boss, 0, 0, 0);

            boss.CallFuction(new LivingCallBack(CreateBossHelp), 2000);
        }

        private void CreateBossHelp()
        {
            LivingConfig config = Game.BaseLivingConfig();
            config.CanTakeDamage = false;
            config.IsFly = true;

            bossHelp = Game.CreateBoss(npcHelpId, 1683, 762, -1, 1, "", config);
            bossHelp.Delay = 1;

            Game.SendObjectFocus(bossHelp, 0, 0, 0);

            m_moive.PlayMovie("in", 3000, 0);
            m_front.PlayMovie("in", 3200, 0);
            m_moive.PlayMovie("out", 6000, 0);
            m_front.PlayMovie("out", 6200, 0);

        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();

        }
        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (m_moive != null && m_front != null)
            {
                Game.RemovePhysicalObj(m_moive, true);
                Game.RemovePhysicalObj(m_front, true);

                m_moive = null;
                m_front = null;
            }
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();

            //if (boss != null && boss.IsLiving == false)
            if (boss != null && boss.Blood == 1)
            {
                return true;
            }
            if (Game.GetAllLivingPlayers().Count <= 0)
            {
                return true;
            }
            if (Game.TotalTurn > 200)
            {
                return true;
            }

            return false;
        }

        public override void OnPrepareGameOver()
        {
            base.OnPrepareGameOver();
            if (boss.Blood == 1)
            {
                if (boss.X > Game.Map.Info.DeadWidth / 2)
                {
                    boss.Direction = -1;
                    bossHelp.MoveTo(boss.X - 615, boss.Y - 50, "fly", 4000, "", 17, null);
                    bossHelp.ChangeDirection(1, 7000);
                }
                else
                {
                    boss.Direction = 1;
                    bossHelp.MoveTo(boss.X + 615, boss.Y - 50, "fly", 4000, "", 17, null);
                    bossHelp.ChangeDirection(-1, 7000);
                }
                boss.CallFuction(ClearAllBoss, 7000);
                return;
            }
            if (Game.GetAllLivingPlayers().Count <= 0)
            {
                Game.CanEndGame = true;
            }
        }

        private void ClearAllBoss()
        {
            boss.Say("Đều là do ngươi quấy rối, hãy đi khỏi đây mau!", 1, 3000);
            bossHelp.Say("Thật là ngốc, ngốc quá đi!", 1, 0);

            boss.PlayMovie("die", 3000, 0);
            boss.Die(3000);
            bossHelp.Die(3000);

            boss.CallFuction(ClearDiedBoss, 3000);
            boss.CallFuction(SetEnding, 4000);
        }

        private void ClearDiedBoss()
        {
            Game.RemoveLiving(bossHelp.Id);
        }

        private void SetEnding()
        {
            Game.CanEndGame = true;
        }

        public override int UpdateUIData()
        {
            return Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();

            if (boss.IsLiving == false)
            {
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }
        }
    }
}