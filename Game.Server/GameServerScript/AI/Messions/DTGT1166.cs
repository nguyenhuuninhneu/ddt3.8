
using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.Messions
{
    public class DTGT1166 : AMissionControl
    {
        private SimpleBoss m_grace;
        private SimpleNpc m_captain;
        private List<SimpleNpc> m_chicken = new List<SimpleNpc>();
        private PhysicalObj m_front;
        private int IsSay = 0;
        private int graceID = 6123;
        private int redID = 6121;
        private int blueID = 6122;
        private int captainID = 6114;
        private static string[] KillChat = new string[]{
           "Gửi cho bạn trở về nhà!",

           "Một mình, bạn có ảo tưởng có thể đánh bại tôi?"
        };

        private static string[] ShootedChat = new string[]{
            " Đau ah! Đau ...",

            "Quốc vương vạn tuế ..."
        };
        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 900)
            {
                return 3;
            }
            else if (score > 825)
            {
                return 2;
            }
            else if (score > 725)
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
            Game.AddLoadingFile(1, "bombs/86.swf", "tank.resource.bombs.Bomb86");
            Game.AddLoadingFile(2, "image/game/living/Living190.swf", "game.living.Living190");
            Game.AddLoadingFile(2, "image/game/effect/6/danti.swf", "asset.game.six.danti");
            Game.AddLoadingFile(2, "image/game/effect/6/cpdian.swf", "asset.game.six.cpdian");
            Game.AddLoadingFile(2, "image/game/effect/6/qunti.swf", "asset.game.six.qunti");
            Game.AddLoadingFile(2, "image/game/effect/6/qunjia.swf", "asset.game.six.qunjia");
            int[] resources = { graceID, redID, blueID, captainID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.SetMap(1166);
        }

        public override void OnPrepareStartGame()
        {
            base.OnPrepareStartGame();
            m_front = Game.Createlayer(1098, 1080, "hide", "game.living.Living190", "stand", 1, 0);
            CreateGraceAndCaptain();
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            Game.Param2 = 0;
            Game.SendLivingActionMapping(m_captain, "stand", "standC");
            m_captain.Say("Rồi, bây giờ thì đồng đội chúng ta phải gắng sức vượt qua đội xanh.", 0, 1000, 5000);
            m_captain.Say("Xuất phát nào!", 0, 5000);
            m_captain.PlayMovie("go", 5500, 0);
            CreateMember();

        }

        private void CreateGraceAndCaptain()
        {
            //Grace
            LivingConfig config = Game.BaseLivingConfig();
            config.CanTakeDamage = false;
            config.KeepLife = true;
            m_grace = Game.CreateBoss(graceID, 1950, 1080, -1, 1, "", config);
            m_grace.AddDelay(1000);
            LivingConfig config2 = Game.BaseLivingConfig();
            config2.CanTakeDamage = false;
            config2.IsTurn = false;
            config.KeepLife = true;
            m_captain = Game.CreateNpc(captainID, 494, 1080, 1, -1, "standC", config2);
            m_captain.BlockTurn = true;
        }
        private void CreateMember()
        {
            LivingConfig redTeam = Game.BaseLivingConfig();
            redTeam.IsHelper = true;
            redTeam.IsFly = true;
            redTeam.IsComplete = false;
            redTeam.KeepLife = true;

            LivingConfig blueTeam = Game.BaseLivingConfig();
            blueTeam.IsFly = true;
            blueTeam.IsComplete = false;
            blueTeam.KeepLife = true;

            //posX
            //int[] m_diemden = { 675, 755, 835, 915, 995, 1075, 1155, 1235, 1315, 1395 };
            int[] m_diemden = { 1315, 1155, 995, 835, 675, 1395, 1235, 1075, 915, 755 };
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)//red
                {
                    m_chicken.Add(Game.CreateNpc(redID, 90, 1080, 0, 1, redTeam));
                }
                else
                {
                    m_chicken.Add(Game.CreateNpc(blueID, 90, 1080, 0, 1, blueTeam));
                }
                m_chicken[i].Properties1 = i;//stt
                m_chicken[i].Properties3 = m_diemden[i];
                m_chicken[i].IsHide = true;
            }
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            IsSay = 0;
        }
        private void Ending()
        {
            Game.CanEndGame = true;
        }
        private void EndGame()
        {
            Game.SendFreeFocus(1100, 200, 0, 0, 2500);
            Game.SendFreeFocus(m_grace.X, m_grace.Y, 0, 3000, 6000);
            m_grace.PlayMovie((Game.TotalKillCount >= 5) ? "die" : "happy", 3000, 2500);
            m_grace.CallFuction(Ending, 3000);
        }
        public override bool CanGameOver()
        {
            base.CanGameOver();
            foreach (SimpleNpc npc in Game.GetNPCLivingWithID(blueID))
            {
                if (npc.Y < 285)
                    npc.Config.IsComplete = true;
            }
            foreach (SimpleNpc npc in Game.GetNPCLivingWithID(redID))
            {
                if (npc.Y < 285)
                    npc.Config.IsComplete = true;
            }
            if (Game.TotalKillCount >= 5)
            {
                Game.TotalKillCount = 5;
                Game.Param2 = 0;
                return true;
            }
            if (Game.Param2 > 5)
                Game.Param2 = 5;
            if (Game.Param2 >= 5)
            {
                Game.TotalKillCount = 0;
                return true;
            }
            return false;

        }

        public override int UpdateUIData()
        {
            return Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();

            if (Game.TotalKillCount >= 5)
            {
                Game.IsWin = true;
            }
            if (Game.Param2 >= 5)
            {
                Game.IsWin = false;
            }
        }
        public override void OnPrepareGameOver()
        {
            base.OnPrepareGameOver();
            EndGame();
        }
    }
}
