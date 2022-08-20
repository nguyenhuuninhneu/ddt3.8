using System.Collections.Generic;
using Game.Logic.AI;
using Game.Logic;
using Game.Logic.Phy.Object;
using System.Drawing;

namespace GameServerScript.AI.NPC
{
    public class FourNormalBlowArmsNpc : ABrain
    {
        private int m_attackTurn = 0;

        private PhysicalObj m_Moive;

        private SimpleNpc m_npcDamage;

        private const int MTotalNpc = 1;

        private readonly List<SimpleNpc> _npcs = new List<SimpleNpc>();

        private const int NpcId = 4103;

        private SimpleBoss m_dragonBoss;

        private const int BossId = 4104;

        private bool m_showWarning;

        private bool ShowWarning
        {
            get => m_showWarning;
            set
            {
                m_showWarning = value;
                SetState();
            }
        }

        public override void OnBeginSelfTurn()
        {
            //base.OnBeginSelfTurn();
            //// check frost
            //IceFronzeEffect effect = Body.EffectList.GetOfType(eEffectType.IceFronzeEffect) as IceFronzeEffect;

            //if (effect != null)
            //    effect.Stop();

        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;
            if (m_dragonBoss == null) SetDragonBoss();

            if (m_dragonBoss.Properties1 == 1 && !ShowWarning) ShowWarning = true;
            else if (m_dragonBoss.Properties1 == 0 && ShowWarning) ShowWarning = false;
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            Point nextPoint = new Point();
            ShowWarning = false;
            switch (m_attackTurn)
            {
                case 0:
                    (Game as PVEGame)?.SendObjectFocus(Body, 1, 0, 0);
                    nextPoint = new Point(672, 746);
                    break;
                case 1:
                    nextPoint = new Point(1059, 749);
                    break;
                case 2:
                    nextPoint = new Point(1412, 751);
                    break;
                default:
                    nextPoint = new Point(1412, 751);
                    break;
            }

            int mindis = int.MaxValue;

            foreach (SimpleNpc npc in Game.FindAllNpcLiving())
            {
                if (!npc.IsLiving || npc.X < Body.X || npc.X > Body.X + nextPoint.X) continue;
                int dis = (int)Body.Distance(npc.X, npc.Y);
                if (dis >= mindis) continue;
                m_npcDamage = npc;
                mindis = dis;
            }

            if (m_npcDamage != null && m_npcDamage.Y > Body.X)//check quay về
            {
                MoveToPlace(m_npcDamage.X - 20, m_npcDamage.Y, BeatNpc);
            }
            else
            {
                if (m_attackTurn < 2)
                {
                    MoveToPlace(nextPoint.X, nextPoint.Y, CreateNpc); //CreateNpc
                }
                else
                {
                    MoveToPlace(nextPoint.X, nextPoint.Y, StartBomber);
                    m_attackTurn = 0;
                }
            }
            m_attackTurn++;
        }

        private void StandB()
        {
            Body.PlayMovie("standB", 0, 0);
            //Body.CallFuction(null, 2000); //CreateNpc
        }

        private void SetState()
        {
            ((PVEGame)Game).SendLivingActionMapping(Body, "stand", m_showWarning ? "standB" : "stand");
        }

        private void MoveToPlace(int x, int y, LivingCallBack callBack)
        {
            Body.MoveTo(x, y, "walk", 1000, "", 5, callBack);
        }

        public void BeatNpc()
        {
            Body.Beat(m_npcDamage, "die", 5000, 5000, 800);
            Body.Die(3000);
        }

        private void StartBomber()
        {
            Body.EffectList.StopAllEffect();
            (Game as PVEGame)?.SendHideBlood(Body, 0);
            Body.PlayMovie("beatA", 2000, 6000);
            Body.CallFuction(CreateCrashGate, 4500);
        }

        private void CreateCrashGate()
        {
            switch (((PVEGame)Game).TotalKillCount)
            {
                case 0:
                    if (m_Moive == null)
                        m_Moive = ((PVEGame)Game).Createlayer(1590, 750, "", "game.asset.Gate", "cryA", 1, 0);
                    else
                        m_Moive.PlayMovie("cryA", 0, 0);
                    break;

                case 1:
                    if (m_Moive == null)
                        m_Moive = ((PVEGame)Game).Createlayer(1590, 750, "", "game.asset.Gate", "cryB", 1, 0);
                    else
                        m_Moive.PlayMovie("cryB", 0, 0);
                    break;

                case 2:
                    if (m_Moive == null)
                        m_Moive = ((PVEGame)Game).Createlayer(1590, 750, "", "game.asset.Gate", "cryC", 1, 0);
                    else
                        m_Moive.PlayMovie("cryC", 0, 0);
                    break;
            }

            ((PVEGame)Game).TotalKillCount++;

            Body.Die();

        }

        private void SetDragonBoss()
        {
            m_dragonBoss = ((PVEGame)Game).FindBossWithID(BossId);
        }

        private void CreateNpc()
        {
            //foreach (SimpleNpc npc in Game.getn)
            //{
            //    if (npc != null)
            //    {
            //        if (!npc.IsLiving)
            //            _npcs.Remove(npc);
            //    }
            //}
            if (Game.GetLivedNpcs(NpcId).Count > 0)
                return;
            LivingConfig config = ((PVEGame)Game).BaseLivingConfig();
            config.CanFrost = false;
            config.CanCountKill = false;
            for (int i = 0; i < MTotalNpc; i++)
                ((PVEGame)Game).CreateNpc(NpcId, Body.X + Game.Random.Next(-50, 150), Body.Y, 0, -1, config);
        }
    }
}
