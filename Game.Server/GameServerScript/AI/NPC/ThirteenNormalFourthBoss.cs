using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;

namespace GameServerScript.AI.NPC
{
    public class ThirteenNormalFourthBoss : ABrain
    {
        private int m_attackTurn = 0;

        private int m_lastBloodFriend = 0;

        private SimpleBoss friendBoss;

        protected Player m_targer;

        private List<PhysicalObj> phyObjects = new List<PhysicalObj>();

        private PhysicalObj phyLineDead;

        private int m_friendBoss = 13109;

        private string[] Say_OpenLineDead = { "Bọn mi còn nhớ ta không?", "Còn nhớ ta không? Ta trở về từ cõi chết đây!", "Lâu ngày không gặp, nhớ ta chứ?" };

        private string[] Say_DamageSingle = { "Liệu có đỡ nổi không đây?", "Nếm thử cái này đi!", "Trước kia ở Vương Quốc Gà, các ngươi đã đánh bại bọn ta. Giờ thì đừng hòng nữa!", "Ngươi sẽ là mục tiêu của ta", "Liệu ngươi đỡ được không đây?" };

        private string[] Say_DamageGlobal = { "Lũ khốn các ngươi phải chết hết", "Đừng mơ thắng được bọn ta", "Ngươi nghĩ ngươi đánh bại được ta à?", "Ta sẽ giết tất cả bọn khốn các ngươi" };

        private string[] Say_Angry = { "Ta chịu hết nổi rồi", "Ta nóng rồi đây!", "Ta chết các ngươi cũng phải chết!", "Đùa đủ rồi. Xem đây!" };

        // 644
        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
            if (friendBoss == null)
            {
                friendBoss = Game.FindBossWithID(m_friendBoss);
            }

            //if (friendBoss.IsLiving || m_lastBloodFriend == 0)
            //    m_lastBloodFriend = friendBoss.Blood;
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;

            if (phyObjects != null && phyObjects.Count > 0)
            {
                foreach (PhysicalObj phy in phyObjects)
                {
                    Game.RemovePhysicalObj(phy, true);
                }
                phyObjects.Clear();
            }
        }

        public override void OnCreated()
        {
            base.OnCreated();
            Body.Properties1 = 0;
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();

            switch (m_attackTurn)
            {
                case 0:
                    CreateLineDead();
                    break;

                case 1:
                    DamageDeadLine();
                    break;

                case 2:
                    SingleAttack();
                    break;

                case 3:
                    GlobalAttack();
                    break;

                case 4:
                    ChangeFireStatus();
                    break;

                case 5:
                    if ((int)Body.Properties1 == 0 && (int)friendBoss.Properties1 == 0)
                        CreateLineDead();
                    else
                        WishDead();

                    m_attackTurn = 0;
                    break;
            }
            m_attackTurn++;
        }

        private void EffectFriendDead()
        {
            int bloodReduce = m_lastBloodFriend + (m_lastBloodFriend / 100 * 10);
            if (bloodReduce >= Body.Blood)
            {
                Body.Say("Ta sẽ không tha thứ cho các ngươi đâu", 0, 1000);
                Body.PlayMovie("die", 2000, 0);
                Body.Die(5000);
            }
            else
            {
                Body.AddBlood(-bloodReduce, 1);
                Body.Say("Các ngươi nghĩ như vậy là đủ hạ gục ta à?", 0, 1000);
                Body.PlayMovie("angry", 1000, 0);
                Body.PlayMovie("beatC", 3000, 5000);
                Body.CallFuction(DeadAllPlayers, 6000);
            }
        }

        private void WishDead()
        {
            if ((int)Body.Properties1 == 1)
            {
                // bomb in current
                Body.Say("Hãy vĩnh biệt cuộc đời này đi lũ cờ hó.", 0, 2000);
                Body.PlayMovie("beatC", 2000, 5000);
                Body.CallFuction(DeadAllPlayers, 5500);
                Body.Die(5000);
            }
            else
            {
                // bomb in friend boss
                (Game as PVEGame).SendObjectFocus(friendBoss, 1, 0, 0);
                friendBoss.Say("Hãy vĩnh biệt cuộc đời này đi lũ cờ hó.", 0, 2000);
                friendBoss.PlayMovie("beatC", 2000, 5000);
                friendBoss.CallFuction(DeadAllPlayers, 5500);
                friendBoss.Die(5000);
            }

        }

        private void DeadAllPlayers()
        {
            foreach (Player p in Game.GetAllLivingPlayers())
            {
                p.SyncAtTime = true;
                p.Die();
            }
        }

        private void ChangeFireStatus()
        {
            Body.Properties1 = 1;

            int rand = Game.Random.Next(Say_Angry.Length);

            Body.Say(Say_Angry[rand], 0, 2000);
            Body.PlayMovie("angry", 2000, 0);
            Body.Config.CanTakeDamage = false;


            // set toa do
            Body.SetRectBomb((Body as SimpleBoss).NpcInfo.X, (Body as SimpleBoss).NpcInfo.Y, (Body as SimpleBoss).NpcInfo.Width, (Body as SimpleBoss).NpcInfo.Height);
            Body.SetRelateDemagemRect(-20, -210, 40, 20);

        }

        private void SingleAttack()
        {
            int rand = Game.Random.Next(Say_DamageSingle.Length);

            Body.Say(Say_DamageSingle[rand], 0, 2000);
            Body.PlayMovie("cast", 2000, 0);

            m_targer = Game.FindRandomPlayer();

            if (m_targer != null)
            {
                (Game as PVEGame).SendObjectFocus(m_targer, 1, 3000, 0);
                Body.CallFuction(EffectSingle, 3200);
                Body.BeatDirect(m_targer, "", 3000, 1, 1);
            }
        }

        private void EffectSingle()
        {
            phyObjects.Add((Game as PVEGame).Createlayer(m_targer.X, m_targer.Y, "", "asset.game.ten.danbao", "", 1, 0));
        }

        private void EffectGlobal()
        {
            foreach (Player p in Game.GetAllLivingPlayers())
            {
                phyObjects.Add((Game as PVEGame).Createlayer(p.X, p.Y, "", "asset.game.ten.qunbao", "", 1, 0));
            }

        }

        private void GlobalAttack()
        {
            Body.CurrentDamagePlus = 1.5f;
            int rand = Game.Random.Next(Say_DamageGlobal.Length);

            Body.Say(Say_DamageGlobal[rand], 0, 2000);
            Body.PlayMovie("cast", 2000, 0);

            m_targer = Game.FindRandomPlayer();

            if (m_targer != null)
            {
                (Game as PVEGame).SendObjectFocus(m_targer, 1, 3000, 0);
                Body.CallFuction(EffectGlobal, 3200);
                Body.RangeAttacking(Body.X - 10000, Body.Y + 10000, "cry", 4000, false);
            }
        }

        private void DamageDeadLine()
        {
            Body.CurrentDamagePlus = 10f;
            Body.PlayMovie("beatD", 2000, 2000);
            Body.CallFuction(RemoveDeadLine, 2800);
            Body.RangeAttacking(Body.X, 1380, "cry", 3500, true);
        }

        private void RemoveDeadLine()
        {
            Game.RemovePhysicalObj(phyLineDead, true);
            phyLineDead = null;
        }

        private void CreateLineDead()
        {
            int rand = Game.Random.Next(Say_OpenLineDead.Length);

            Body.Say(Say_OpenLineDead[rand], 0, 2000);

            Body.PlayMovie("beatA", 2100, 4000);

            Body.CallFuction(CreateLineDeadObject, 3500);
        }

        private void CreateLineDeadObject()
        {
            phyLineDead = (Game as PVEGame).Createlayer(783, 1020, "deadline", "asset.game.ten.tedabiaoji", "", 1, 0);
        }

        public override void OnDie()
        {
            base.OnDie();
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        public override void OnAfterTakedFrozen()
        {
            base.OnAfterTakedFrozen();

            if ((int)Body.Properties1 == 1)
            {
                Body.Properties1 = 0;
                Body.PlayMovie("stand", 100, 2000);
                Body.SetRelateDemagemRect((Body as SimpleBoss).NpcInfo.X, (Body as SimpleBoss).NpcInfo.Y, (Body as SimpleBoss).NpcInfo.Width, (Body as SimpleBoss).NpcInfo.Height);
                Body.Config.CanTakeDamage = true;
            }
        }

    }
}