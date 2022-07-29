using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using System.Drawing;
using Game.Logic.Actions;
using Bussiness;
using Game.Logic.Effects;

namespace GameServerScript.AI.NPC
{
    public class SixNormalSecondBoss : ABrain
    {
        private int m_redNpc = 6121;

        private int m_blueNpc = 6122;

        private SimpleNpc[] m_blue;

        private SimpleNpc[] m_red;

        private int m_bloodBuff = 1200;//đây nè a

        private bool m_affectAll = false;

        private List<PhysicalObj> m_phy = new List<PhysicalObj>();

        private SimpleNpc m_randomNpc;

        private SimpleNpc FindRandom(List<SimpleNpc> npcs)
        {
            SimpleNpc result = null;

            int ranđ = Game.Random.Next(0, npcs.Count);
            if (npcs.Count > 0)
            {
                result = npcs[ranđ];
            }
            return result;
        }
        private List<SimpleNpc> GetNpcList(SimpleNpc[] npcs)
        {
            List<SimpleNpc> list = new List<SimpleNpc>();
            foreach (SimpleNpc npc in npcs)
            {
                if (!npc.Config.IsComplete)
                    list.Add(npc);
            }
            return list;
        }
        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
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

            if (m_blue == null || m_red == null)
            {
                m_blue = Game.GetNPCLivingWithID(m_blueNpc);
                m_red = Game.GetNPCLivingWithID(m_redNpc);
            }
            m_affectAll = false;
            int redCount = 0;
            int blueCount = 0;
            int redBlood = 0;
            int redDied = 0;
            int blueBlood = 0;
            int blueDied = 0;
            List<SimpleNpc> bList = GetNpcList(m_blue);
            List<SimpleNpc> rList = GetNpcList(m_red);
            List<SimpleNpc> blueList = bList.OrderBy(p => p.Blood).ToList();
            List<SimpleNpc> redList = rList.OrderByDescending(p => p.Blood).ToList();

            // check have deadbody
            if (GetNpcList(m_blue) == null)
            {
                m_affectAll = true;
                Body.PlayMovie("beatB", 1000, 0);
                (Game as PVEGame).SendFreeFocus(1100, 650, 1, 2500, 3000);
                Body.CallFuction(DamageNpcEffect, 3500);
                return;
            }    
            foreach (SimpleNpc npc in blueList)
            {
                if (npc.Blood <= npc.MaxBlood / 100 * 60)
                    blueCount++;
                blueBlood += npc.Blood;
                if (npc.Blood <= 1)
                    blueDied++;
            }
            if (blueDied >= 1)
            { 
                Body.PlayMovie("beat", 1000, 0);
                (Game as PVEGame).SendFreeFocus(1100, 650, 1, 2500, 3000);
                for (int i = 0; i < blueDied; i++)
                {
                    blueList[i].AddBlood(m_bloodBuff, 0, 4000);
                    Body.CallFuction(RestoreSingleNpcEffect, blueList[i], 3500);
                }
                return;
            }
            foreach (SimpleNpc npc in redList)
            {
                if (npc.Blood >= npc.MaxBlood / 100 * 60)
                    redCount++;
                redBlood += npc.Blood;
                if (npc.Blood <= 1)
                    redDied++;
            }

            if (redBlood / 100 * 80 > blueBlood)
            {
                m_randomNpc = blueList[0];
                m_affectAll = redCount >= blueCount ? true : false;
                Body.PlayMovie("beat", 1000, 0);
                (Game as PVEGame).SendFreeFocus(1100, 650, 1, 2500, 3000);
                Body.CallFuction(RestoreBloodNpcEffect, 3500);
            }
            else
            {
                // damage red team
                m_randomNpc = redList[0];
                m_affectAll = blueCount <= redCount ? true : false;
                Body.PlayMovie("beatB", 1000, 0);
                (Game as PVEGame).SendFreeFocus(1100, 650, 1, 2500, 3000);
                Body.CallFuction(DamageNpcEffect, 3500);
            }
        }

        private void RestoreSingleNpcEffect(Living living)
        {
            m_phy.Add((Game as PVEGame).Createlayer(living.X, living.Y, "front", "asset.game.six.qunjia", "", 1, 0));
        }

        private void DamageNpcEffect()
        {
            List<SimpleNpc> reds = GetNpcList(m_red);
            if (m_affectAll)
            {
                foreach (SimpleNpc npc in reds)
                {
                    m_phy.Add((Game as PVEGame).Createlayer(npc.X, npc.Y, "front", "asset.game.six.qunti", "", 1, 0));
                }
            }
            else
            {
                m_phy.Add((Game as PVEGame).Createlayer(m_randomNpc.X, m_randomNpc.Y, "front", "asset.game.six.qunti", "", 1, 0));
            }
            Body.CallFuction(DamageNpcBuff, 500);
        }

        private void DamageNpcBuff()
        {
            List<SimpleNpc> reds = GetNpcList(m_red);
            if (m_affectAll)
            {
                foreach (SimpleNpc npc in reds)
                {
                    Body.BeatDirect(npc, "", 100, 1, 1);
                }
            }
            else
            {
                Body.BeatDirect(m_randomNpc, "", 100, 1, 1);
            }
        }

        private void RestoreBloodNpcEffect()
        {
            List<SimpleNpc> blues = GetNpcList(m_blue);
            if (m_affectAll)
            {
                foreach (SimpleNpc npc in blues)
                {
                    m_phy.Add((Game as PVEGame).Createlayer(npc.X, npc.Y, "front", "asset.game.six.qunjia", "", 1, 0));
                }
            }
            else
            {
                if (m_randomNpc == null)
                {
                    DamageNpcEffect();
                }    
                m_phy.Add((Game as PVEGame).Createlayer(m_randomNpc.X, m_randomNpc.Y, "front", "asset.game.six.qunjia", "", 1, 0));
            }
            Body.CallFuction(RestoreBloodNpcBuff, 500);
        }

        private void RestoreBloodNpcBuff()
        {
            List<SimpleNpc> blues = GetNpcList(m_blue);
            if (m_affectAll)
            {
                foreach (SimpleNpc npc in blues)
                {
                    npc.AddBlood(m_bloodBuff);
                }
            }
            else
            {
                m_randomNpc.AddBlood(m_bloodBuff);
            }

        }

        public override void OnDie()
        {
            base.OnDie();
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
    }
}