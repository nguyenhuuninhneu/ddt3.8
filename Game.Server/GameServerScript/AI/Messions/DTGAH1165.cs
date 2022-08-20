using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Effects;
using Game.Logic.Phy.Object;
using Game.Logic;
using SqlDataProvider.Data;
using System.Drawing;
using Bussiness.Managers;

namespace GameServerScript.AI.Messions
{
    //aaa
    public class DTGAH1165 : AMissionControl
    {
        private SimpleNpc m_boss;

        private List<PhysicalObj> m_balls = new List<PhysicalObj>();

        private bool m_isDouble = false;

        private int m_doubleCount = 1;

        private int bossID = 6321;

        private int ballID = 6313;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private PhysicalObj m_diem_trai;
        private PhysicalObj hangdonvi_trai;
        private PhysicalObj hangchuc_trai;

        private PhysicalObj m_diem_phai;
        private PhysicalObj hangdonvi_phai;
        private PhysicalObj hangchuc_phai;

        private int turn = 0;

        private int[] birthX = { 450, 550, 650, 750, 850, 950, 1050, 1150, 1250, 455, 555, 655, 755, 855, 955, 1055, 1155, 1255 };//Toa do X

        private int[] birthY = { 184, 259, 335, 420, 504 };//Toa do Y

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
            int[] resources = { bossID, ballID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.AddLoadingFile(2, "image/game/thing/bossborn6.swf", "game.asset.living.GuizeAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/effect/6/ball.swf", "asset.game.six.ball");
            Game.AddLoadingFile(2, "image/game/effect/6/jifenpai.swf", "asset.game.six.fenshu");
            Game.AddLoadingFile(2, "image/game/effect/6/jifenpai.swf", "asset.game.six.shuzi");
            Game.SetMap(1165);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();

            Game.TotalCount = 99;
            Game.TotalTurn = Game.PlayerCount * 20;
            Game.SendMissionInfo();
            LivingConfig config = Game.BaseLivingConfig();
            config.CanTakeDamage = false;
            config.IsTurn = false;
            m_boss = Game.CreateNpc(bossID, 345, 860, 1, 1, "", config);

            m_boss.PlayMovie("standC", 0, 0);
            m_boss.PlayMovie("go", 1000, 0);
            m_boss.Say("Ô ! Ngưới mới à, chắc bạn không biết quy tắc ở đây.", 0, 0);

            m_boss.CallFuction(new LivingCallBack(NextAttack2), 4000);
            m_boss.CallFuction(new LivingCallBack(NextAttack), 5000);
        }

        private void NextAttack2()
        {
            m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 1);

            ((PVEGame)Game).SendGameFocus(900, 500, 1, 0, 1000);

            m_kingMoive.PlayMovie("in", 0, 0);
            m_kingMoive.PlayMovie("out", 5000, 0);

            m_boss.CallFuction(new LivingCallBack(CreatBall), 6000);
        }

        private void NextAttack()
        {
            m_kingFront = Game.Createlayer(900, 450, "font", "game.asset.living.GuizeAsset", "out", 1, 1);
            m_diem_trai = Game.Createlayer(170, 650, "movie", "asset.game.six.fenshu", "Z", 1, 1);
            hangchuc_trai = Game.Createlayer(270, 650, "movie", "asset.game.six.shuzi", "z0", 1, 1);
            hangdonvi_trai = Game.Createlayer(320, 650, "movie", "asset.game.six.shuzi", "z0", 1, 1);

            m_diem_phai = Game.Createlayer(1550, 650, "movie", "asset.game.six.fenshu", "Z", 1, 1);
            hangchuc_phai = Game.Createlayer(1650, 650, "movie", "asset.game.six.shuzi", "z0", 1, 1);
            hangdonvi_phai = Game.Createlayer(1700, 650, "movie", "asset.game.six.shuzi", "z0", 1, 1);
        }

        public List<string> chaysodonvi(int Totalcount)
        {
            string action = "";
            List<string> trave = new List<string>();
            string diem = Totalcount.ToString().Replace("-", "");
            string diemhangdonvi = "0";
            string diemhangchuc = "0";
            if (Totalcount >= 0)
            {
                if (m_diem_trai != null)
                {
                    m_diem_trai.PlayMovie("Z", 0, 900);
                    m_diem_phai.PlayMovie("Z", 0, 900);
                }
            }
            else
            {
                if (m_diem_trai != null)
                {
                    m_diem_trai.PlayMovie("F", 0, 900);
                    m_diem_phai.PlayMovie("F", 0, 900);
                }
            }

            for (int i = 0; i < diem.Length; i++)
            {
                if (diem.Length == 2)
                {
                    diemhangdonvi = diem[1].ToString();
                    diemhangchuc = diem[0].ToString();
                }
                else
                {
                    diemhangdonvi = diem[0].ToString();
                }
            }
            switch (diemhangchuc)
            {
                case "1":
                    action = "z1";
                    break;
                case "2":
                    action = "z2";
                    break;
                case "3":
                    action = "z3";
                    break;
                case "4":
                    action = "z4";
                    break;
                case "5":
                    action = "z5";
                    break;
                case "6":
                    action = "z6";
                    break;
                case "7":
                    action = "z7";
                    break;
                case "8":
                    action = "z8";
                    break;
                case "9":
                    action = "z9";
                    break;
                case "0":
                    action = "z0";
                    break;
            }
            trave.Add(action);
            switch (diemhangdonvi)
            {
                case "1":
                    action = "z1";
                    break;
                case "2":
                    action = "z2";
                    break;
                case "3":
                    action = "z3";
                    break;
                case "4":
                    action = "z4";
                    break;
                case "5":
                    action = "z5";
                    break;
                case "6":
                    action = "z6";
                    break;
                case "7":
                    action = "z7";
                    break;
                case "8":
                    action = "z8";
                    break;
                case "9":
                    action = "z9";
                    break;
                case "0":
                    action = "z0";
                    break;
            }
            trave.Add(action);
            return trave;
        }
        private int solantangbongxanh = 4;
        private void CreatBall()
        {
            string[] actReds = { "s1", "s2", "s3", "s4", "s5", "s1", "s2", "s3", "s4", "s5", "s1", "s2", "s3", "s4", "s5" };
            string[] actBlues = { "s-1", "s-2", "s-3", "s-4", "s-5", "s-1", "s-2", "s-3", "s-4", "s-5", "s-1", "s-2", "s-3", "s-4", "s-5" };
            string actDouble = "double";
            Point[] arrPoint =
            {
                new Point(1001,173),
                new Point(774,203),
                new Point(639,299),
                new Point(545,463),
                new Point(566,661),
                new Point(698,805),
                new Point(858,863),
                new Point(1091,837),
                new Point(1223,719),
                new Point(1307,517),
                new Point(1289,366),
                new Point(1171,241),
                new Point(985,240),
                new Point(717,400),
                new Point(684,602),
                new Point(853,742),
                new Point(1140,629),
                new Point(1145,410),
                new Point(982,438),
                new Point(829,506),
                new Point(962,592)
            };
            Game.Shuffer(arrPoint);
            for (int i = 0; i < arrPoint.Length; i++)
            {
                int actInd = Game.Random.Next(actBlues.Length);
                if (i < solantangbongxanh)
                {
                    m_balls.Add(Game.CreateBall(arrPoint[i].X, arrPoint[i].Y, actBlues[actInd]));
                }
                else
                {
                    int actInd1 = Game.Random.Next(actReds.Length);
                    m_balls.Add(Game.CreateBall(arrPoint[i].X, arrPoint[i].Y, actReds[actInd1]));
                }
            }
            if (m_isDouble && m_doubleCount != 0)
            {
                int rand = Game.Random.Next(0, arrPoint.Length);
                m_balls[rand].PlayMovie(m_balls[rand].ActionMapping[m_balls[rand].CurrentAction], 0, 500);
                m_balls[rand].Die();
                Game.RemovePhysicalObj(m_balls[rand], true);
                m_balls[rand] = Game.CreateBall(arrPoint[rand].X, arrPoint[rand].Y, actDouble);
                m_isDouble = false;
                m_doubleCount--;
            }
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
            Game.ClearBall();
            if (Game.TurnIndex > 2)
            {
                int rand = Game.Random.Next(4, 8);
                solantangbongxanh = rand;

                //
                int randDouble = Game.Random.Next(1, 100);
                if (randDouble > 80)
                {
                    m_isDouble = true;
                }
            }
            CreatBall();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            if (Game.TurnIndex > 1)
            {
                if (m_kingMoive != null)
                {
                    Game.RemovePhysicalObj(m_kingMoive, true);
                    m_kingMoive = null;
                }
                if (m_kingFront != null)
                {
                    Game.RemovePhysicalObj(m_kingFront, true);
                    m_kingFront = null;
                }
            }
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();
            if (Game.TurnIndex > Game.PlayerCount * 10 && Game.TotalKillCount < 99)
            {
                return false;
            }

            if (Game.TotalKillCount >= 99)
            {
                Game.TotalKillCount = 99;
                return true;
            }
            return false;
        }

        public override int UpdateUIData()
        {
            if (Game.TotalKillCount < -99)
            {
                Game.TotalKillCount = -99;
            }
            if (hangdonvi_trai != null) //NHOK
            {
                hangdonvi_trai.PlayMovie(chaysodonvi(Game.TotalKillCount)[1], 0, 1100);
                hangchuc_trai.PlayMovie(chaysodonvi(Game.TotalKillCount)[0], 0, 1100);
            }
            if (hangdonvi_phai != null) //NHOK
            {
                hangdonvi_phai.PlayMovie(chaysodonvi(Game.TotalKillCount)[1], 0, 1100);
                hangchuc_phai.PlayMovie(chaysodonvi(Game.TotalKillCount)[0], 0, 1100);
            }
            return Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (Game.TotalKillCount >= 99)
            {
                Game.IsWin = true;
            }
            else if (Game.TurnIndex > Game.PlayerCount * 10 && Game.TotalKillCount < 99)
            {
                Game.IsWin = false;
            }
        }
        public override void OnCalculatePoint(int point, bool isdouble)
        {
            Game.TotalKillCount += point;
            if (isdouble)
                Game.TotalKillCount *= 2;
            if (Game.TotalKillCount < -99)
            {
                Game.TotalKillCount = -99;
            }
            if (Game.TotalKillCount >= 99)
            {
                Game.TotalKillCount = 99;
            }
        }
    }
}
