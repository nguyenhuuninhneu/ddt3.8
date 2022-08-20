using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class SixNormalThirdBoss : ABrain
    {
        private Player m_target = null;

        private PhysicalObj m_popcan = null;

        private int m_hiep = 0;

        private int m_attackTurn = 0;

        private int m_buffGuard = 0;

        private int m_buffAttack = 0;

        private Player FindUntargetPlayer()
        {
            List<Player> list = new List<Player>();
            List<Player> finalList = new List<Player>();
            foreach (Player value in Game.GetAllLivingPlayers())
            {
                if (value.IsLiving)
                {
                    list.Add(value);
                }
            }
            finalList = list.OrderBy(p => p.Properties1).ToList();
            if (finalList.Count > 0)
            {
                return finalList[0];
            }
            return null;
        }
        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;

        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            SimpleBoss[] trongtai = Game.FindLivingTurnBossWithID(6132);
            int delay = 0;
            int delay2 = 0;
            if ((int)trongtai[0].Properties2 == 1)
            {
                trongtai[0].Properties2 = 0;
                if (Body.Properties1 == 1)
                {
                    trongtai[0].Say("Sao mặt đỏ thế? Dùng chất kích thích à!", 0, 0);
                    trongtai[0].PlayMovie("beat", 1000, 0);
                    ((PVEGame)Game).SendGameFocus(Body, 3500, 0);
                    Body.Properties1 = 0;
                    Body.IconPicture(eMirariType.Guard, false);
                    Body.IconPicture(eMirariType.Damage, false);
                    Body.Attack = (Body as SimpleBoss).NpcInfo.Attack;
                    Body.Defence = (Body as SimpleBoss).NpcInfo.Defence;
                    Body.CallFuction(CreateEffectQusan, 4000);
                    delay2 = 6000;
                }
            }
            if (trongtai[0].Properties1 == 1)
            {
                delay = 2000 + delay2;
                ((PVEGame)Game).SendGameFocus(trongtai[0], 0, delay2);
                trongtai[0].PlayMovie("beida", delay2 + 500, 2000);
                trongtai[0].Properties1 = 0;
            }
            if (Body.Blood > 1)
            {
                if ((bool)Body.Properties2)
                {
                    Body.CallFuction(CreateEffectGreen, delay);
                }
                else
                {
                    int rand = Game.Random.Next(0, 100);
                    LivingCallBack callback = null;

                    if (rand >= 50)
                    {
                        callback = CreateEffectRed;
                    }
                    else
                    {
                        callback = CreateEffectBlue;
                    }
                    //Body.Direction = Game.FindlivingbyDir(Body);
                    if (m_attackTurn == 0)
                    {
                        MoveToTarget(PunchBeatE, delay, delay + 1000);
                        m_attackTurn++;
                    }
                    else if (m_attackTurn == 1)
                    {
                        Body.CallFuction(callback, delay);
                        MoveToTarget(PunchBeatF, delay + 2800, delay + 3800);
                        m_attackTurn++;
                    }
                    else if (m_attackTurn == 2)
                    {
                        Body.CallFuction(callback, delay);
                        MoveToTarget(PunchBeatA, delay + 2800, delay + 3800);
                        m_attackTurn++;
                    }
                    else if (m_attackTurn == 3)
                    {
                        MoveToTarget(PunchBeatA, delay, delay + 1000);
                        m_attackTurn = 0;
                    }
                }
                SetState();
            }
            else
            {
                if (m_hiep == 0)
                {
                    ((PVEGame)Game).SendFreeFocus(trongtai[0].X, trongtai[0].Y - 150, 0, 0, 0);
                    trongtai[0].Say("Hả? Quyền Vương gục rồi sao?!", 0, 800);
                    trongtai[0].PlayMovie("beat", 1000, 0);
                    trongtai[0].Properties1 = 1;
                    ((PVEGame)Game).SendGameFocus(Body, 2000, 0);
                    Body.PlayMovie("dieA", 3000, 0);
                    Body.CallFuction(ResetBlood, 6400);

                    ((PVEGame)Game).SendGameFocus(trongtai[0], 7500, 0);
                    trongtai[0].PlayMovie("beida", 8000, 2000);
                    trongtai[0].Properties1 = 0;
                    m_hiep = 1;
                }
                else
                {
                    ((PVEGame)Game).SendFreeFocus(trongtai[0].X, trongtai[0].Y - 150, 0, 0, 0);
                    trongtai[0].Say("Hả? Quyền Vương gục rồi sao?!", 0, 800);
                    trongtai[0].PlayMovie("beat", 1000, 0);
                    ((PVEGame)Game).SendGameFocus(Body, 2000, 0);
                    Body.PlayMovie("dieB", 3000, 0);
                    trongtai[0].Say("Ôi! Gục rồi..........", 0, 6500);
                    ((PVEGame)Game).SendFreeFocus(trongtai[0].X, trongtai[0].Y - 150, 0, 6000, 0);
                    trongtai[0].PlayMovie("beat", 6800, 0);
                    Body.Die(6700);
                    m_hiep = 2;
                }
            }
        }
        private void CreateEffectQusan()
        {
            m_popcan = ((PVEGame)Game).Createlayer(Body.X - 150, Body.Y + 120, "blue", "popcan_fla.qusan_8", "", 1, 0);
            Body.CallFuction(RemoveEffect, 1200);
        }
        private void ResetBlood()
        {
            Body.AddBlood(Body.MaxBlood);
            SetState();
        }
        private void CreateEffectBlue()
        {
            m_popcan = ((PVEGame)Game).Createlayer(Body.X, Body.Y, "blue", "asset.game.six.popcan", "blueB", 1, 0);
            //Body.AddEffect(new GuardEffect(99), 2500);
            Body.IconPicture(eMirariType.Guard, true);
            Body.Properties1 = 1;
            m_buffGuard += 1000;
            Body.Defence += (double)m_buffGuard;
            Body.CallFuction(RemoveEffect, 2500);
        }
        private void CreateEffectGreen()
        {
            m_popcan = ((PVEGame)Game).Createlayer(Body.X, Body.Y, "green", "asset.game.six.popcan", "greenB", 1, 0);
            Body.CallFuction(RemoveEffect, 2500);
        }
        private void CreateEffectRed()
        {
            m_popcan = ((PVEGame)Game).Createlayer(Body.X, Body.Y, "red", "asset.game.six.popcan", "redB", 1, 0);
            //Body.AddEffect(new DamageEffect(99), 2500);
            Body.IconPicture(eMirariType.Damage, true);
            Body.Properties1 = 1;
            m_buffAttack += 1000;
            Body.Attack += (double)m_buffAttack;
            Body.CallFuction(RemoveEffect, 2500);
        }
        private void PunchBeatF()
        {
            Body.BeatDirect(m_target, "beatF", 0, 1, 1);
            Body.CallFuction(PlayerMove, 1500);
        }
        private void PlayerMove()
        {
            if (Body.X > m_target.X)
            {
                //550
                m_target.StartSpeedMult(m_target.X - 250 < 550 ? 550 : m_target.X - 250, m_target.Y, 2000);
            }
            else
            {
                //1965
                m_target.StartSpeedMult(m_target.X + 250 > 1965 ? 1965 : m_target.X + 250, m_target.Y, 2000);
            }
            m_target.StartFalling(true, 3000, 1000);
        }
        private void PunchBeatA()
        {
            Body.FallFrom(Body.X, Body.Y, "", 0, 0, 1000);
            Body.ChangeDirection(m_target, 0);
            Body.PlayMovie("beatA", 0, 0);
            if (Body.Direction == -1)
            {
                Body.RangeAttacking(Body.X - 180, Body.X, "cry", 500, false);
            }
            else
            {
                Body.RangeAttacking(Body.X, Body.X + 180, "cry", 500, false);
            }
            Body.CallFuction(PunchBeatB, 1000);
        }
        private void PunchBeatB()
        {
            m_target = FindUntargetPlayer();
            if (m_target == null)
                return;
            if (Body.X > m_target.X)
            {
                Body.BoltMove(m_target.X + 150, Body.Y, 0);
            }
            else
            {
                Body.BoltMove(m_target.X - 150, Body.Y, 0);
            }
            Body.FallFrom(Body.X, Body.Y, "", 0, 0, 1000);
            Body.ChangeDirection(m_target, 0);
            Body.PlayMovie("beatB", 0, 0);
            if (Body.Direction == -1)
            {
                Body.RangeAttacking(Body.X - 180, Body.X, "cry", 500, false);
            }
            else
            {
                Body.RangeAttacking(Body.X, Body.X + 180, "cry", 500, false);
            }
            Body.CallFuction(PunchBeatC, 1000);
        }
        private void PunchBeatC()
        {
            m_target = FindUntargetPlayer();
            if (m_target == null)
                return;
            if (Body.X > m_target.X)
            {
                Body.BoltMove(m_target.X + 150, Body.Y, 0);
            }
            else
            {
                Body.BoltMove(m_target.X - 150, Body.Y, 0);
            }
            Body.FallFrom(Body.X, Body.Y, "", 0, 0, 1000);
            Body.ChangeDirection(m_target, 0);
            Body.PlayMovie("beatC", 0, 0);
            if (Body.Direction == -1)
            {
                Body.RangeAttacking(Body.X - 180, Body.X, "cry", 500, false);
            }
            else
            {
                Body.RangeAttacking(Body.X, Body.X + 180, "cry", 500, false);
            }
            Body.CallFuction(PunchBeatD, 1000);
        }
        private void PunchBeatD()
        {
            m_target = FindUntargetPlayer();
            if (m_target == null)
                return;
            if (Body.X > m_target.X)
            {
                Body.BoltMove(m_target.X + 150, Body.Y, 0);
            }
            else
            {
                Body.BoltMove(m_target.X - 150, Body.Y, 0);
            }
            Body.FallFrom(Body.X, Body.Y, "", 0, 0, 1000);
            Body.ChangeDirection(m_target, 0);
            Body.PlayMovie("beatD", 0, 0);
            if (Body.Direction == -1)
            {
                Body.RangeAttacking(Body.X - 180, Body.X, "cry", 500, false);
            }
            else
            {
                Body.RangeAttacking(Body.X, Body.X + 180, "cry", 500, false);
            }
        }
        private void RemoveEffect()
        {
            if (m_popcan != null)
            {
                Game.RemovePhysicalObj(m_popcan, true);
            }
            if ((bool)Body.Properties2)
            {
                Body.AddBlood(Body.MaxBlood / 100 * 35);
                SetState();
            }
        }
        private void MoveToTarget(LivingCallBack callback, int delay, int delaycallback)//dam thang
        {
            Body.PlayMovie("walk", delay, 0);
            m_target = FindUntargetPlayer();
            if (m_target == null)
                return;
            m_target.Properties1++;
            if (Body.X > m_target.X)
            {
                Body.BoltMove(m_target.X + 150, Body.Y, delay + 1000);
            }
            else
            {
                Body.BoltMove(m_target.X - 150, Body.Y, delay + 1000);
            }
            Body.ChangeDirection(m_target, delay + 500);
            Body.CallFuction(callback, delaycallback);
        }

        private void PunchBeatE()
        {
            Body.BeatDirect(m_target, "beatE", 0, 1, 1);
            m_target.AddEffect(new ReduceStrengthEffect(2, 100), 1000);
            m_target.AddEffect(new SealEffect(2), 1000);
            m_target.AddEffect(new LockDirectionEffect(2), 1000);
        }
        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
        public override void OnAfterTakedBomb()
        {
            base.OnAfterTakedBomb();
            SetState();
        }
        private void SetState()
        {
            if (Body.Blood <= Body.MaxBlood / 100 * 35)
            {
                ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cryB");
                ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standC");
                Body.Properties2 = true;
            }
            else
            {
                ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cry");
                ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "stand");
                Body.Properties2 = false;
            }
        }
    }
}
