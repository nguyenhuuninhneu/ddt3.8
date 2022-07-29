using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class ThirdTerrorKingFirst : ABrain
    {
        private int m_attackTurn = 0;

        private List<PhysicalObj> starDamageGlobal = new List<PhysicalObj>();

        private SimpleBoss friendBoss;

        private int npcID = 3306;

        private int fearId = 3310;

        private int lockId = 3311;

        private int m_maxTotemCount = 3;

        private int m_maxNpcCount = 2;

        private int BloodRecover = 20000;

        #region NPC Said
        private static string[] AllAttackChat = new string[] {
            "Trận động đất, bản thân mình! ! <br/> bạn vui lòng Ay giúp đỡ",
            "Hạ vũ khí xuống!",
            "Xem nếu bạn có thể đủ khả năng, một số ít!！"
        };

        private static string[] CallChat = new string[]{
            "Vệ binh! <br/> bảo vệ! ! ",
            "Dũng sĩ bộ lạc đâu thể hiện đi!",
            "Đệ tử của ta đâu mau ra đây!!!",
            "Lính đâu ra giết bọn chúng."
        };

        private static string[] FearCallChat = new string[]{
             "Có cảm thấy bủn rủn không?",
             "Ngươi không đủ sức để hạ ta đâu.",
             "Thấy mệt mỏi chưa?"
        };

        private static string[] LockCallChat = new string[]{
           "Sao mà bắn ta được?",
            "Giỏi thì thử bắn nữa đi."

        };

        private static string[] ShootCallChat = new string[]{
           "Sao mà bắn ta được?",
            "Giỏi thì thử bắn nữa đi."

        };

        #endregion

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
            if (friendBoss == null || friendBoss.IsLiving == false)
            {
                List<Living> listLiving = Game.FindAllTurnBossLiving();
                foreach (Living lv in listLiving)
                {
                    if (lv != Body)
                    {
                        friendBoss = (lv as SimpleBoss);
                        return;
                    }
                }
            }
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;
        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            Body.Direction = Game.FindlivingbyDir(Body);
            bool result = false;
            int maxdis = 0;
            foreach (Player player in Game.GetAllFightPlayers())
            {
                if (player.IsLiving && player.X > Body.X - 300 && player.X < Body.X + 300)
                {
                    int dis = (int)Body.Distance(player.X, player.Y);
                    if (dis > maxdis)
                    {
                        maxdis = dis;
                    }
                    result = true;
                }
            }

            if (result)
            {
                KillAttack(Body.X - 300, Body.X + 300);
                return;
            }

            // kiem tra xem co hoi sinh dc ko
            if (friendBoss != null && friendBoss.IsLiving == false)
            {
                Body.PlayMovie("castA", 800, 0);
                Body.Say("Em trai ơi đừng đầu hàng.<br/>Anh sẽ giúp em hồi sinh!!!", 0, 1000);
                ((PVEGame)Game).SendObjectFocus(friendBoss, 1, 3000, 1000);
                Body.CallFuction(new LivingCallBack(ReviveFriendBoss), 5000);
                return;
            }

            if (friendBoss != null && friendBoss.Blood <= (friendBoss.NpcInfo.Blood / 8))
            {
                Body.PlayMovie("castA", 800, 0);
                Body.Say("Các ngươi làm sao đủ khả năng đánh bại em ta!!!", 0, 1000);
                ((PVEGame)Game).SendObjectFocus(friendBoss, 1, 3500, 5000);
                Body.CallFuction(new LivingCallBack(RecoverFriendBlood), 4000);
                return;
            }

            JumpBeat();
        }

        public void RecoverFriendBlood()
        {
            friendBoss.AddBlood(BloodRecover);
        }

        public void ReviveFriendBoss()
        {
            SimpleBoss newBoss = null;
            LivingConfig configLiving = ((PVEGame)Game).BaseLivingConfig();
            configLiving.ReduceBloodStart = 2;

            Game.RemoveLiving(friendBoss.Id);

            newBoss = ((PVEGame)Game).CreateBoss(friendBoss.NpcInfo.ID, friendBoss.X, friendBoss.Y, friendBoss.Direction, 1, "raise", configLiving);
            newBoss.SetRelateDemagemRect(newBoss.NpcInfo.X, newBoss.NpcInfo.Y, newBoss.NpcInfo.Width, newBoss.NpcInfo.Height);
            newBoss.Say("Ta đã hồi sinh!!! Các ngươi phải chết.", 0, 3000);
            friendBoss = newBoss;
        }

        private void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 100f;
            Body.PlayMovie("call", 1000, 0);
            Body.RangeAttacking(fx, tx, "cry", 4000, null);
        }

        private void JumpBeat()
        {
            int rand = Game.Random.Next(1, 100);
            Body.PlayMovie("walk", 1000, 0);
            if (Body.Y >= 310 && Body.Y <= 420)
            {
                // đang ở vị trí 1 => nhảy xuống vt 2
                if (rand <= 50)
                    Body.FallFromTo(Body.X, 483, "", 1600, 0, 30, new LivingCallBack(GoInBeat));
                else
                    Body.FallFromTo(Body.X, 623, "", 1600, 0, 25, new LivingCallBack(GoInBeat));
            }
            else if (Body.Y >= 450 && Body.Y <= 540)
            {
                // đang ở vị trí 2 => nhảy xuống vt3
                if (rand <= 50)
                {
                    Body.FallFromTo(Body.X, 623, "", 1600, 0, 25, new LivingCallBack(GoInBeat));
                }
                else
                {
                    Body.JumpTo(Body.X, 360, "", 1600, 0, 25, new LivingCallBack(GoInBeat));
                }
            }
            else if (Body.Y >= 580 && Body.Y <= 668)
            {
                // đang ở vị trí 3 => nhảy lên vt1
                if (rand <= 50)
                {
                    Body.JumpTo(Body.X, 360, "", 1600, 0, 30, new LivingCallBack(GoInBeat));
                }
                else
                {
                    Body.JumpTo(Body.X, 483, "", 1600, 0, 25, new LivingCallBack(GoInBeat));
                }
            }

        }

        private void GoInBeat()
        {
            Body.PlayMovie("stand", 0, 0);
            // hành dộng tiếp theo
            switch (m_attackTurn)
            {
                case 0:
                    //turn đầu => đặt vật tổ mệt mỏi
                    if (!IsFearBuffLiving())
                        CreateFearBuff();
                    else
                        ShootBeat();
                    m_attackTurn++;
                    break;

                case 1:
                    //gọi đệ tự
                    if (!IsChildNPCLiving())
                        CallbBoss();
                    else
                        ShootBeat();
                    m_attackTurn++;
                    break;
                case 2:
                    //bắn
                    ShootBeat();
                    m_attackTurn++;
                    break;
                case 3:
                    //đặt vt khóa
                    if (!IsLockBuffLiving())
                        CreateLockBuff();
                    else
                        ShootBeat();

                    m_attackTurn = 0;
                    break;
            }

        }

        private bool IsFearBuffLiving()
        {
            SimpleNpc[] npcs = Game.FindAllNpc();
            foreach (SimpleNpc npc in npcs)
            {
                if (npc.IsLiving == true && npc.NpcInfo.ID == fearId)
                    return true;
            }
            return false;
        }

        private bool IsChildNPCLiving()
        {
            SimpleNpc[] npcs = Game.FindAllNpc();
            foreach (SimpleNpc npc in npcs)
            {
                if (npc.IsLiving == true && npc.NpcInfo.ID == npcID)
                    return true;
            }
            return false;
        }

        private bool IsLockBuffLiving()
        {
            SimpleNpc[] npcs = Game.FindAllNpc();
            foreach (SimpleNpc npc in npcs)
            {
                if (npc.IsLiving == true && npc.NpcInfo.ID == lockId)
                    return true;
            }
            return false;
        }

        private void CreateFearBuff()
        {
            int index = Game.Random.Next(0, FearCallChat.Length);
            Body.Say(FearCallChat[index], 0, 2000);
            Body.PlayMovie("call", 2000, 0);
            Body.CallFuction(new LivingCallBack(CreateFearNPC), 5000);
        }

        public void CreateFearNPC()
        {
            //x: 560 - 1090
            //((SimpleBoss)Body).CreateChild(fearId, 1079, 631, 0, 3, -1);
            for (int i = 0; i < m_maxTotemCount; i++)
            {
                int randX = Game.Random.Next(560, 1090);
                LivingConfig config = ((PVEGame)Game).BaseLivingConfig();
                config.CanCountKill = false;
                ((PVEGame)Game).CreateNpc(fearId, randX, 631, 0, -1, config);
            }
        }

        private void CreateLockBuff()
        {
            int index = Game.Random.Next(0, LockCallChat.Length);
            Body.Say(LockCallChat[index], 0, 2000);
            Body.PlayMovie("call", 2000, 0);
            Body.CallFuction(new LivingCallBack(CreateLockNPC), 5000);
        }

        public void CreateLockNPC()
        {
            for (int i = 0; i < m_maxTotemCount; i++)
            {
                int randX = Game.Random.Next(560, 1090);
                LivingConfig config = ((PVEGame)Game).BaseLivingConfig();
                config.CanCountKill = false;
                ((PVEGame)Game).CreateNpc(lockId, randX, 631, 0, -1, config);
            }
        }

        private void CallbBoss()
        {
            int index = Game.Random.Next(0, CallChat.Length);
            Body.Say(CallChat[index], 0, 2300);
            Body.PlayMovie("castA", 2500, 0);
            Body.CallFuction(new LivingCallBack(CreateChild), 5000);
        }

        public void CreateChild()
        {
            //((SimpleBoss)Body).CreateChild(npcID, 217, 242, 0, 3, 1);
            for (int i = 0; i < m_maxNpcCount; i++)
            {
                LivingConfig config = ((PVEGame)Game).BaseLivingConfig();
                config.CanCountKill = false;
                ((PVEGame)Game).CreateNpc(npcID, 217, 242, 0, 1, config);
            }
        }

        private void ShootBeat()
        {
            Player target = Game.FindRandomPlayer();
            Body.PlayMovie("stand", 0, 0);
            Body.CurrentDamagePlus = 1.8f;

            if (target != null)
            {

                if (Body.ShootPoint(target.X, target.Y, 54, 1000, 10000, 3, 2f, 2300))
                {
                    Body.PlayMovie("beatA", 1500, 0);
                }
            }
        }


        private SimpleBoss m_living;
        private void RemovePhysicalObj()
        {
            m_living = friendBoss;
            ((PVEGame)Game).SendObjectFocus(m_living, 1, 0, 0);
            PhysicalObj[] objs = Game.FindPhysicalObjByName("end");
            Game.RemovePhysicalObj(objs[0], false);
        }
        private void CreateEffectEnd()
        {
            PhysicalObj m_effect;
            m_effect = ((PVEGame)Game).CreatePhysicalObj(m_living.X, m_living.Y, "end", "game.crazyTank.view.Focus", "", 1, 0);
            m_living.CallFuction(new LivingCallBack(RemovePhysicalObj), 2000);
        }

        public override void OnDie()
        {
            List<Living> bossLivings = Game.FindAllTurnBossLiving();

            if (bossLivings.Count <= 1)
            {
                ((PVEGame)Game).TotalKillCount = 1;
            }
            if (bossLivings.Count <= 0)
            {
                ((PVEGame)Game).TotalKillCount = 2;
                m_living = Body as SimpleBoss;
                ((PVEGame)Game).SendObjectFocus(m_living, 1, 5000, 0);
                m_living.CallFuction(new LivingCallBack(CreateEffectEnd), 6000);
                m_living.CallFuction(new LivingCallBack(CreateEffectEnd), 10000);
            }
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
    }
}