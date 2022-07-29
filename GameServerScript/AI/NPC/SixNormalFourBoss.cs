using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Game.Logic.Effects;
using System.Drawing;

namespace GameServerScript.AI.NPC
{
    public class SixNormalFourBoss : ABrain
    {
        private int m_attackTurn = 0;

        private SimpleNpc m_khien = null;

        private int m_khienID = 6144;

        private PhysicalObj m_redCircle;

        private PhysicalObj m_greenCircle;

        private PhysicalObj m_blueCircle;

        private Player m_target = null;

        private int m_targetDis = 0;

        private Point[] m_birth = { new Point(1280, 990), new Point(1835, 990), new Point(760, 990) };

        private List<Point> m_newBirth = new List<Point>();

        private List<Player> m_greenPlayers = new List<Player>();

        private List<Player> m_redPlayers = new List<Player>();

        private List<Player> m_bluePlayers = new List<Player>();

        private PhysicalObj m_effectTop = null;

        private static string[] m_turnChats = new string[]
        {
            "Ngay cả Quốc Vương cũng không dám chọc giận Grace!",
            "Chết này!"
        };
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
            m_target = Game.FindRandomPlayer();
            //Body.Direction = Game.FindlivingbyDir(Body);
            if (Body.X > m_target.X)
            {
                m_targetDis = Body.X - m_target.X;
            }
            else
            {
                m_targetDis = m_target.X - Body.X;
            }
            if (m_attackTurn == 0)
            {
                ResetPlayersList();
                CreateCicrle();
                if (m_targetDis <= 450)
                {
                    if (Body.X > m_target.X)
                    {
                        Body.MoveTo(m_target.X + 150, Body.X, "walk", 5, "", 1000, AttackFarPlayer, 3000);
                    }
                    else
                    {
                        Body.MoveTo(m_target.X - 150, Body.X, "walk", 5, "", 1000, AttackFarPlayer, 3000);
                    }
                }
                else
                {
                    ShootAttack();
                }
                SetState();
                m_attackTurn++;
            }
            else if (m_attackTurn == 1)
            {
                CreateEffectCircle();
                if (Body.X > m_target.X)
                {
                    Body.MoveTo(Body.X - 50, Body.X, "walk", 5, "", 4000, DrinkPowerJuice, 2000);
                }
                else
                {
                    Body.MoveTo(Body.X + 50, Body.X, "walk", 5, "", 4000, DrinkPowerJuice, 2000);
                }
                SetState();
                m_attackTurn++;
            }
            else if (m_attackTurn == 2)
            {
                ResetPlayersList();
                CreateCicrle();
                Body.CallFuction(BodyAddBlood, 1500);
                m_attackTurn++;
            }
            else if (m_attackTurn == 3)
            {
                CreateEffectCircle();
                Body.CallFuction(JumpAndAttack, 4000);
                m_attackTurn++;
            }
            else if (m_attackTurn == 4)
            {
                ResetPlayersList();
                if (Body.X > m_target.X)
                {
                    Body.MoveTo(Body.X - 50, Body.X, "walk", 5, "", 0, CreateCicrle, 1000);
                }
                else
                {
                    Body.MoveTo(Body.X + 50, Body.X, "walk", 5, "", 0, CreateCicrle, 1000);
                }
                ShootAttack();
                m_attackTurn++;
            }
            else if (m_attackTurn == 5)
            {
                CreateEffectCircle();
                Body.CallFuction(AllAttack, 4000);
                m_attackTurn++;
            }
            else if (m_attackTurn == 6)
            {
                ResetPlayersList();
                if (Body.X > m_target.X)
                {
                    Body.MoveTo(Body.X - 50, Body.X, "walk", 5, "", 0, CreateCicrle, 1000);
                }
                else
                {
                    Body.MoveTo(Body.X + 50, Body.X, "walk", 5, "", 0, CreateCicrle, 1000);
                }
                ShootAttack();
                m_attackTurn++;
            }
            else if (m_attackTurn == 7)
            {
                CreateEffectCircle();
                Body.CallFuction(ReadNewsPapperAttack, 4000);
                m_attackTurn = 0;
            }
            Body.CallFuction(SetState, 9000);
            Body.CallFuction(SetRect, 9000);

        }
        private void SetRect()
        {
            Body.SetRect(((SimpleBoss)Body).NpcInfo.X, ((SimpleBoss)Body).NpcInfo.Y, ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Height);
            Body.FireY = -120;
            if (Body.Direction == -1)
            {
                Body.SetRelateDemagemRect(35, -60, 25, 80);
                Body.SetRect(((SimpleBoss)Body).NpcInfo.X, ((SimpleBoss)Body).NpcInfo.Y, ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Height);
            }
            else
            {
                Body.SetRelateDemagemRect(-35, -60, 25, 80);
                Body.SetRect(-((SimpleBoss)Body).NpcInfo.X - ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Y, ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Height);
            }

        }
        private void ResetPlayersList()
        {
            m_bluePlayers = new List<Player>();
            m_redPlayers = new List<Player>();
            m_greenPlayers = new List<Player>();
        }
        private void ReadNewsPapperAttack()
        {
            Body.Say("Dạo này tin tức hot nhỉ.", 0, 0);
            Body.PlayMovie("beatC", 500, 0);
            if (Body.Direction == -1)
            {
                Body.RangeAttacking(Body.X - 10000, Body.X, "cry", 2500, false);
            }
            else
            {
                Body.RangeAttacking(Body.X, Body.X + 10000, "cry", 2500, false);
            }
        }
        private void AllAttack()
        {
            Body.Say("Đưa tôi 1 cái mic.", 0, 0);
            //((PVEGame)Game).SendGameFocus(Body, 0, 0);
            Body.CallFuction(new LivingCallBack(CreateGlobalAttackEffect), 2500);
            Body.RangeAttacking(Body.X - 10000, Body.X + 10000, "cry", 9500, false);
            Body.Say("Ôi! Sống rồi, lương tháng này có rồi.", 0, 9000);
            Body.PlayMovie("beatE", 9000, 800);
            Body.PlayMovie("standA", 10000, 0);
        }
        private void CreateGlobalAttackEffect()
        {
            m_effectTop = ((PVEGame)Game).CreateLayerTop(0, 0, "", "asset.game.six.chang", "", 1, 0);
            Body.CallFuction(new LivingCallBack(RemoveGlobalAttackEffect), 9800);
        }

        private void RemoveGlobalAttackEffect()
        {
            if (m_effectTop != null)
                Game.RemovePhysicalObj(m_effectTop, true);
        }
        private void JumpAndAttack()
        {
            Body.Say("Đường đang thi công, phải đi đường vòng.", 0, 0);
            Body.PlayMovie("beatD", 1500, 0);
            Body.BoltMove(m_target.X, Body.Y, 3200);
            Body.RangeAttacking(Body.X - 200, Body.X + 200, "cry", 4500, false);
        }
        private void AttackFarPlayer()
        {
            int rand = Game.Random.Next(0, m_turnChats.Length);
            Body.ChangeDirection(m_target, 0);
            Body.Say(m_turnChats[rand], 0, 0);
            Body.PlayMovie("beatB", 1000, 0);
            Body.RangeAttacking(m_target.X - 80, m_target.X + 80, "cry", 2500, false);
        }

        private void ShootAttack()
        {
            Body.SetRect(-75, -120, 136, 182);

            List<Player> players = Game.GetAllLivingPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] != null)
                {
                    int delay = i * 1000 + (i * 400);
                    Body.ChangeDirection(players[i], delay);
                    int mtX = Game.Random.Next(players[i].X - 50, players[i].X + 50);

                    if (Body.ShootPoint(mtX, players[i].Y, 61, 1000, 10000, 1, 1, delay + 2200))
                    {
                        Body.PlayMovie("beatA", delay + 1700, 0);
                    }
                }
            }
        }
        private void DrinkPowerJuice()
        {
            Body.ChangeDirection(m_target, 0);
            Body.Say("Chất kích thích à? Ta cũng muốn thử!", 0, 0);
            Body.PlayMovie("inX", 1000, 5300);
            Body.CallFuction(CreateChild, 5300);
        }
        private void CreateEffectCircle()
        {
            PhysicalObj green = null;
            PhysicalObj blue = null;
            PhysicalObj red = null;
            PhysicalObj armor = null;
            foreach (Player allPlayer in Game.GetAllFightPlayers())
            {
                green = ((PVEGame)Game).Createlayer(m_greenCircle.X, m_greenCircle.Y, "green", "asset.game.six.popcan", "green", 1, 0);
                blue = ((PVEGame)Game).Createlayer(m_blueCircle.X, m_blueCircle.Y, "green", "asset.game.six.popcan", "blue", 1, 0);
                red = ((PVEGame)Game).Createlayer(m_redCircle.X, m_redCircle.Y, "green", "asset.game.six.popcan", "red", 1, 0);
                if (allPlayer.X > m_greenCircle.X - 120 && allPlayer.X < m_greenCircle.X + 120)
                {
                    m_greenPlayers.Add(allPlayer);
                }
                if (allPlayer.X > m_blueCircle.X - 120 && allPlayer.X < m_blueCircle.X + 120)
                {
                    m_bluePlayers.Add(allPlayer);
                }
                if (allPlayer.X > m_redCircle.X - 120 && allPlayer.X < m_redCircle.X + 120)
                {
                    m_redPlayers.Add(allPlayer);
                }

                if (m_redPlayers.Count > 0)
                {
                    Body.CallFuction(CreateRed, 1600);
                    PlayerAddAttack(m_redPlayers);
                }
                if (m_bluePlayers.Count > 0)
                {
                    Body.CallFuction(CreateBlue, 1600);
                    PlayerAddArmor(m_bluePlayers);
                }
                if (m_greenPlayers.Count > 0)
                {
                    PlayerAddBlood(m_greenPlayers);
                }
                Body.CallFuction(RemoveAllPhysicalObj, 3500);
            }
        }
        private void RemoveAllPhysicalObj()
        {
            List<PhysicalObj> allPhys = Game.Map.GetAllPhysicalObjSafe();
            for (int i = 0; i < allPhys.Count; i++)
            {
                Game.RemovePhysicalObj(allPhys[i], true);
            }
        }
        private void PlayerAddBlood(List<Player> player)
        {
            foreach (Player p in player)
            {
                p.AddBlood(2500);
            }
            //m_greenPlayers = new List<Player>();
        }
        private void CreateRed()
        {
            foreach (Player player in m_redPlayers)
            {
                PhysicalObj attack = null;
                attack = ((PVEGame)Game).Createlayer(player.X, player.Y, "attack", "asset.game.six.pup", "", 1, 0);
            }
        }
        private void CreateBlue()
        {
            foreach (Player player in m_bluePlayers)
            {
                PhysicalObj armor = null;
                armor = ((PVEGame)Game).Createlayer(player.X, player.Y, "armor", "asset.game.six.aup", "", 1, 0);
            }
        }
        private void PlayerAddAttack(List<Player> player)
        {
            foreach (Player p in player)
            {
                p.Attack += 250;
                p.AddEffect(new DamageEffect(2), 2000);
            }
            //m_redPlayers = new List<Player>();
        }
        private void PlayerAddArmor(List<Player> player)
        {
            foreach (Player p in player)
            {
                p.AddEffect(new AddGuardEquipEffect(2, 2, true), 2000);
            }
            //m_bluePlayers = new List<Player>();
        }
        private void BodyAddBlood()
        {
            SimpleNpc[] khiens = Game.FindAllNpcWithXandY(Body.X, Body.Y);
            List<SimpleNpc> khien = new List<SimpleNpc>();
            if (khiens != null)
                khien.Add(khiens[0]);
            if (khien.Count > 0)//uong nuoc tang luc
            {
                Body.Say("Ôi! Đã quá...............", 0, 0);
                Body.PlayMovie("xixue", 0, 2000);
                khien[0].SetHidden(true);
                khien[0].BoltMove(0, 0, 0);
                khien[0].Die(0);
                Game.RemoveLiving(khien[0], true);
                Body.CallFuction(BloodBuff, 2000);
                khien.RemoveAt(0);
            }
            else
            {
                ShootAttack();
            }
            Body.CallFuction(SetState, 1000);
        }
        private void BloodBuff()
        {
            Body.AddBlood(Body.MaxBlood / 2);
        }
        private void CreateChild()
        {
            LivingConfig khien = ((PVEGame)Game).BaseLivingConfig();
            khien.isShowBlood = true;
            khien.isShowSmallMapPoint = false;
            khien.IsTurn = false;
            m_khien = ((SimpleBoss)Body).CreateChild(m_khienID, Body.X, Body.Y, 0, Body.Direction, true, khien);
            m_khien.SetRelateDemagemRect(-75, -130, 160, 130);
            m_khien.SetRect(-75, -130, 160, 130);
            m_khien.Properties1 = 1;
            //m_khien.SetOffsetX(-42);
            Body.Properties1 = 1;
            SetState();
        }
        private void SetState()
        {
            SimpleNpc[] khiens = Game.FindAllNpcWithXandY(Body.X, Body.Y);
            List<SimpleNpc> khien = new List<SimpleNpc>();
            if (khiens != null)
                khien.Add(khiens[0]);
            if (khien.Count > 0)
            {
                ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cryA");
                ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standC");
                ((PVEGame)Game).SendLivingActionMapping(Body, "shield", "shieldB");
                Body.SetRelateDemagemRect(0, 0, 0, 0);
            }
            else
            {
                ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cryA");
                ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standA");
                ((PVEGame)Game).SendLivingActionMapping(Body, "shield", "shield");
                if (Body.Direction == -1)
                    Body.SetRelateDemagemRect(35, -60, 25, 80);
                else
                    Body.SetRelateDemagemRect(-35, -60, 25, 80);
                Body.FireX = -120;
            }

        }
        private void CreateCicrle()
        {
            int[] rand = { 0, 1, 2 };
            Game.Shuffer(rand);
            m_greenCircle = ((PVEGame)Game).Createlayer(m_birth[rand[0]].X, m_birth[rand[0]].Y, "green", "asset.game.six.greencircle", "", 1, 0);
            m_redCircle = ((PVEGame)Game).Createlayer(m_birth[rand[1]].X, m_birth[rand[1]].Y, "red", "asset.game.six.redcircle", "", 1, 0);
            m_blueCircle = ((PVEGame)Game).Createlayer(m_birth[rand[2]].X, m_birth[rand[2]].Y, "blue", "asset.game.six.bluecircle", "", 1, 0);
        }
        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
        public override void OnAfterTakedBomb()
        {
            base.OnAfterTakedBomb();
            if (m_khien != null)
            {
                Body.Properties1 = 1;
            }
            else
            {
                Body.Properties1 = 0;
            }
            SetState();
        }
    }
}
