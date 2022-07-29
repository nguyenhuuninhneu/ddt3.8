using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class WAN13104 : AMissionControl
    {

        private int bossId = 13108; // mathia

        private int boss2Id = 13109; // ao anh mathia

        private int npc1Id = 13112;

        private int npc2Id = 13113;

        private int npc3Id = 13114;

        private int[] m_locationX = new int[] { 83, 90, 100, 110, 125, 135, 145, 265, 600, 850, 990, 1400, 1550, 1620, 1760, 1910 };

        private int m_turnMax = 2;

        private int m_turnCurrent = 0;

        private int m_turnReady = -1;

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private SimpleBoss boss = null;

        private SimpleBoss boss2 = null;

        private SimpleNpc m_bombs;

        private int turnCreateBombs = 3;
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
            int[] resources = { bossId, boss2Id, npc1Id, npc2Id, npc3Id };

            Game.AddLoadingFile(2, "image/game/effect/10/danbao.swf", "asset.game.ten.danbao"); // damage alone
            Game.AddLoadingFile(2, "image/game/effect/10/qunbao.swf", "asset.game.ten.qunbao"); // damage global
            Game.AddLoadingFile(2, "image/game/effect/10/tedabiaoji.swf", "asset.game.ten.tedabiaoji"); // vach ke duong

            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.shuangwangAsset");

            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.SetMap(1217);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();

            m_moive = Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_front = Game.Createlayer(810, 750, "front", "game.asset.living.shuangwangAsset", "out", 1, 0);

            LivingConfig config = Game.BaseLivingConfig();
            config.IsShowBloodBar = true;
            config.IsShowBloodBar = true;
            config.FriendlyBoss = new LivingConfig.FriendlyLiving(boss2Id, true);

            boss = Game.CreateBoss(bossId, 83, 1020, 1, 1, "", config);
            boss.SetRelateDemagemRect(boss.NpcInfo.X, boss.NpcInfo.Y, boss.NpcInfo.Width, boss.NpcInfo.Height);

            Game.SendObjectFocus(boss, 0, 0, 0);

            boss.CallFuction(new LivingCallBack(CreateBossHelp), 2000);
        }

        private void CreateBossHelp()
        {
            LivingConfig config = Game.BaseLivingConfig();
            config.IsShowBloodBar = true;
            config.IsShowBloodBar = true;
            config.FriendlyBoss = new LivingConfig.FriendlyLiving(bossId, true);

            boss2 = Game.CreateBoss(boss2Id, 1912, 1020, -1, 1, "", config);
            boss2.SetRelateDemagemRect(boss2.NpcInfo.X, boss2.NpcInfo.Y, boss2.NpcInfo.Width, boss2.NpcInfo.Height);
            boss2.Delay = 1;

            Game.SendObjectFocus(boss2, 0, 0, 0);

            Game.SendFreeFocus(1000, 900, 1, 2000, 0);

            m_moive.PlayMovie("in", 3000, 0);
            m_front.PlayMovie("in", 3200, 0);
            m_moive.PlayMovie("out", 6000, 0);
            m_front.PlayMovie("out", 6200, 0);

        }

        private void ReadyToBomb(SimpleBoss simpleBoss)
        {
            simpleBoss.Say("Tưởng vậy là xong rồi ư? Bọn ta sẽ tiễn các ngươi xuống địa ngục!", 1, 1000);
            simpleBoss.PlayMovie("die", 1900, 0);
            simpleBoss.Die(2800);

            foreach (Player players in Game.GetAllLivingPlayers())
            {
                players.SetSeal(true);
            }
        }

        private void CreateBombs()
        {
            Game.Shuffer<int>(m_locationX);
            for (int i = 0; i < turnCreateBombs; i++)
            {
                LivingConfig config = Game.BaseLivingConfig();
                config.CanCountKill = false;
                int rand = Game.Random.Next(0, 30);

                if (i >= m_locationX.Length)
                {
                    Random randX = new Random();
                    m_bombs = Game.CreateNpc((rand >= 0 && rand <= 10 ? npc1Id : rand >= 11 && rand <= 20 ? npc2Id : npc3Id), m_locationX[randX.Next(0, m_locationX.Length)], 1000, 0, -1);
                }
                else
                {
                    m_bombs = Game.CreateNpc((rand >= 0 && rand <= 10 ? npc1Id : rand >= 11 && rand <= 20 ? npc2Id : npc3Id), m_locationX[i], 1000, 0, -1);
                }
                m_bombs.Properties1 = 1;
            }
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
            if (m_turnReady != -1 && Game.FindAllNpcLiving().Length <= 0)
            {
                m_turnReady++;
                if (m_turnReady > Game.GetAllLivingPlayers().Count)
                {
                    CreateBombs();
                    m_turnCurrent++;
                    m_turnReady = 0;
                }
            }
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
            if (boss != null && !boss.IsLiving && m_turnReady == -1)
            {
                m_turnReady = 0;
                Game.SendFreeFocus(boss2.X, boss2.Y - 150, 0, 0, 0);
                boss2.CallFuction(delegate
                { ReadyToBomb(boss2); }, 1500);
                foreach (PhysicalObj phys in Game.FindPhysicalObjByName("deadline"))
                {
                    Game.RemovePhysicalObj(phys, true);
                }
                Game.GameStateModify = eGameState.Waiting;
            }
            if (boss2 != null && !boss2.IsLiving && m_turnReady == -1)
            {
                m_turnReady = 0;
                Game.SendFreeFocus(boss.X, boss.Y - 150, 0, 0, 0);
                boss.CallFuction(delegate
                { ReadyToBomb(boss); }, 1500);
                foreach (PhysicalObj phys in Game.FindPhysicalObjByName("deadline"))
                {
                    Game.RemovePhysicalObj(phys, true);
                }
                Game.GameStateModify = eGameState.Waiting;
            }
            if (m_turnCurrent >= m_turnMax && Game.FindAllNpcLiving().Length <= 0)
            {
                return true;
            }
            else if (Game.TotalTurn > 200)
            {
                return true;
            }

            return false;
        }

        public override void OnWaitingGameState()
        {
            base.OnWaitingGameState();
            Game.GameStateModify = eGameState.Playing;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();

            if (m_turnCurrent >= m_turnMax && Game.FindAllNpcLiving().Length <= 0)
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