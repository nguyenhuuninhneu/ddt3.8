using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class SixTerrorSecondNpc : ABrain
    {
        private bool m_startTurn = true;

        private const int m_cauthangphai = 1610;

        private const int m_cauthangtrai = 600;

        private int KhoangCach_Du = 0;

        private int m_khoangCachBu = 0;

        private int[] m_diemden = { 675, 755, 835, 915, 995, 1075, 1155, 1235, 1315, 1395 };

        private bool InPos(int y1, int y2)
        {
            return y1 <= Body.Y && Body.Y <= y2;
        }
        private int GetKhoangCach()
        {
            double tile = ((Body.Blood * 1.0) / (Body.MaxBlood * 1.0)) * 100.0;
            int khoangcach_full = 1050;
            if (tile < 10)
            {
                khoangcach_full = Game.Random.Next(500, 550);
            }
            else if (tile < 30)
            {
                khoangcach_full = Game.Random.Next(550, 600);
            }
            else if (tile < 50)
            {
                khoangcach_full = Game.Random.Next(650, 700);
            }
            else if (tile < 70)
            {
                khoangcach_full = Game.Random.Next(700, 750);
            }
            else if (tile < 90)
            {
                khoangcach_full = Game.Random.Next(800, 850);
            }

            return khoangcach_full - m_khoangCachBu * 20;
        }
        public override void OnCreated()
        {
            base.OnCreated();
        }
        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();

        }
        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (Body.Config.IsComplete)
                return;
            ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "focus", "focuscl");
            ((PVEGame)Game).SendLivingActionMapping(Body, "fly", "walk");

            if (base.Body.Blood == 1)
            {
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "standB");
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "cry", "cryB");
                base.Body.PlayMovie("standB", 0, 0);
            }
            else
            {
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "stand");
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "cry", "cry");
                base.Body.PlayMovie("stand", 0, 0);
            }
        }
        public override void OnStartAttacking()
        {
            if (m_startTurn)
            {
                BeginStart();
                m_startTurn = false;
            }
            else
            {
                KhoangCach_Du = 0;
                if (Body.Blood == 1)
                {
                    return;
                }
                if (InPos(980, 1000) || InPos(675, 732))
                {
                    Moveto1();
                }
                else if (InPos(820, 890) || InPos(525, 600))
                {
                    Moveto2();
                }
                else if (InPos(380, 415))
                {
                    Moveto3();
                }
            }
        }

        public override void OnBeforeTakedDamage(Living source, ref int damageAmount, ref int criticalAmount)
        {
            base.OnBeforeTakedDamage(source, ref damageAmount, ref criticalAmount);
            if (Body.Blood == 1)
            {
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "standB");
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "cry", "cryB");
            }
            else
            {
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "stand");
                ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "cry", "cry");
            }

        }
        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
        private void BeginStart()//turn 1
        {
            Body.CallFuction(SetHide, Body.Properties1 * 1000);
            Body.MoveTo(595, Body.Y, "walk", 7, "", Body.Properties1 * 1000, new LivingCallBack(ClaimUp), 3500);
        }
        private void ClaimUp()//treo len
        {
            //((PVEGame)Game).SendFreeFocus(1100, 700, 1, 0, 10000);
            Body.MoveTo(Body.X, Body.Y - 100, "flyup", 7, "", 0, new LivingCallBack(SetPosition), 1000);
        }
        private void SetPosition()
        {
            Body.MoveTo((int)Body.Properties3, Body.Y, "flyLR", 7, "", 1000, SetState, 4000);
        }
        private void SetState()
        {
            Body.PlayMovie("stand", 0, 0);
        }
        private void SetHide()
        {
            Body.IsHide = false;
        }
        private void Moveto3()
        {
            int khoangcach_full = GetKhoangCach();
            int khoangcach = khoangcach_full + Body.X;
            if (khoangcach >= m_cauthangphai)
            {
                khoangcach = m_cauthangphai;
                KhoangCach_Du = (khoangcach_full + Body.X) - m_cauthangphai;
                KhoangCach_Du = (KhoangCach_Du <= 0) ? 0 : KhoangCach_Du;
                m_khoangCachBu++;
            }
            int thoigian = Game.GetDelayDistance(Body.X, khoangcach, 7) + 1000; //Body.GetTimeDistance(m_cauthangphai - Body.X, 7) + 1000;
            Body.MoveTo(khoangcach, Body.Y, "flyLR", 7, "", 100, new LivingCallBack(Moveto3A), thoigian);
        }
        private void Moveto3A()
        {
            if (Math.Abs(Body.X - m_cauthangphai) <= 2)
            {
                int thoigian = Game.GetDelayDistance(Body.Y, Body.Y - 120, 7) + 1000; //Body.GetTimeDistanceY(Body.Y - 120, 7);
                Body.MoveTo(Body.X, Body.Y - 120, "flyup", 7, "", 100, new LivingCallBack(WIN), thoigian);
            }
        }
        private void WIN()
        {
            ((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "happy");
            int thoigian = Game.GetDelayDistance(Body.X, m_diemden[Game.Top], 7) + 1000;//Body.GetTimeDistance(m_diemden[Game.Top], 7) + 2000;
            Body.MoveTo(m_diemden[Game.Top], Body.Y, "fly", 100, "", 7);
            Body.SetRelateDemagemRect(0, 0, 0, 0);
            //Body.Config.IsTurn = false;
            //Body.Config.IsComplete = true;
            //Body.PlayMovie("happy", thoigian, 0);
            Game.Top++;
            Game.StopLiving = true;
            //
            if (((SimpleNpc)Body).NpcInfo.ID == 6321)
                ((PVEGame)Game).TotalKillCount++;
            else
            {
                ((PVEGame)Game).Param2++;
            }

        }

        private void Moveto2()
        {
            int khoangcach_full = GetKhoangCach();
            int khoangcach = Body.X - khoangcach_full;
            if (khoangcach <= m_cauthangtrai)
            {
                khoangcach = m_cauthangtrai;
                KhoangCach_Du = khoangcach_full + m_cauthangtrai - Body.X;
                KhoangCach_Du = (KhoangCach_Du <= 0) ? 0 : KhoangCach_Du;
                m_khoangCachBu++;
            }
            int thoigian = Game.GetDelayDistance(Body.X, khoangcach, 7) + 1000;//Body.GetTimeDistance(Body.X - m_cauthangtrai, 7) + 1000;
            Body.MoveTo(khoangcach, Body.Y, "flyLR", 7, "", 100, new LivingCallBack(Moveto2A), thoigian);
        }
        private void Moveto2A()
        {
            if (Math.Abs(Body.X - m_cauthangtrai) <= 2)
            {
                int thoigian = Game.GetDelayDistance(Body.Y, Body.Y - 150, 7) + 1000; //Body.GetTimeDistanceY(Body.Y - 150, 7) + 1000;
                Body.MoveTo(Body.X, Body.Y - 150, "flyup", 7, "", 100, new LivingCallBack(Moveto2B), thoigian);
            }
        }
        private void Moveto2B()
        {
            if (Math.Abs(Body.X - m_cauthangtrai) <= 2)
            {
                Body.Direction = 1;
                if (KhoangCach_Du == 0)
                {
                    Body.MoveTo(Body.X + Game.Random.Next(40, 70), Body.Y, "flyLR", 100, "", 7);
                }
                else
                {
                    Body.MoveTo(Body.X + this.KhoangCach_Du, Body.Y, "flyLR", 100, "", 7);
                }
            }
        }



        private void Moveto1()
        {
            int khoangcach_full = GetKhoangCach();
            int khoangcach = khoangcach_full + Body.X;
            if (khoangcach >= m_cauthangphai)
            {
                khoangcach = m_cauthangphai;
                KhoangCach_Du = (khoangcach_full + Body.X) - m_cauthangphai;
                KhoangCach_Du = (KhoangCach_Du <= 0) ? 0 : KhoangCach_Du;
                m_khoangCachBu++;
            }
            int thoigian = Game.GetDelayDistance(Body.X, khoangcach, 7) + 1000;//Body.GetTimeDistance(m_cauthangphai - Body.X, 7) + 1000;
            Body.MoveTo(khoangcach, Body.Y, "flyLR", 7, "", 100, new LivingCallBack(Moveto1A), thoigian);
        }
        private void Moveto1A()
        {
            if (Math.Abs(Body.X - m_cauthangphai) <= 2)
            {
                int thoigian = Game.GetDelayDistance(Body.Y, Body.Y - 150, 7) + 1000;//Body.GetTimeDistanceY(Body.Y - 150, 7) + 1000;
                Body.MoveTo(Body.X, Body.Y - 150, "flyup", 7, "", 100, new LivingCallBack(Moveto1B), thoigian);
            }
        }
        private void Moveto1B()
        {
            if (Math.Abs(Body.X - m_cauthangphai) <= 2)
            {
                Body.Direction = -1;
                if (KhoangCach_Du == 0)
                {
                    Body.MoveTo(Body.X - Game.Random.Next(40, 70), Body.Y, "flyLR", 100, "", 7);
                }
                else
                {
                    Body.MoveTo(Body.X - this.KhoangCach_Du, Body.Y, "flyLR", 100, "", 7);
                }
            }
        }

        private void NULL()
        {

        }
    }
}
