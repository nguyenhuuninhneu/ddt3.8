using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class FourNormalGunNpc : ABrain
    {
        private bool m_isOpenEye;

        private SimpleBoss m_barrel;
        
        private const int BarrelId = 4101;

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;
        }

        public override void OnCreated()
        {
            base.OnCreated();
            Body.Properties1 = 0;
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            bool result = false;

            foreach (Player player in Game.GetAllFightPlayers())
            {
                if (!player.IsLiving || player.X <= Body.X - 300 || player.X >= Body.X + 300) continue;
                //var distance = (int)Body.Distance(player.X, player.Y);
                result = true;
            }

            if (result)
            {
                KillAttack(Body.X - 300, Body.X + 300);
                return;
            }

            if (!m_isOpenEye)
            {
                Body.Properties1 = 1;
                m_isOpenEye = true;
                SetStand();
                ((PVEGame)Game).SendFreeFocus(Body.X, Body.Y, 1, 100, 0);
                Body.PlayMovie("beatA", 1000, 0);
                //Body.CallFuction(SetStand, 2000);
            }
            else
            {
                Body.Properties1 = 0;
                m_isOpenEye = false;
                SetStand();
                ((PVEGame)Game).SendFreeFocus(Body.X, Body.Y, 1, 100, 0);
                Body.PlayMovie("beatB", 1000, 0);
                ((PVEGame)Game).SendFreeFocus(1305, 655, 2, 1000, 500);
                Body.RangeAttacking(475, 1900, "cry", 4000, false);
                ((PVEGame)Game).SendFreeFocus(900, 700, 1, 2000, 500);
                //Body.CallFuction(SetStand, 4000);
                Body.CallFuction(BarrelDamage, 4000);
            }
        }

        private void BarrelDamage()
        {
            if (m_barrel == null)
                SetBarrelBoss();
            else
                if (!m_barrel.IsLiving)
                SetBarrelBoss();
            if (m_barrel.X < 475) return;
            if (m_barrel.IsFrost)
                m_barrel.IsFrost = false;
            else
            {
                m_barrel.EffectList.StopAllEffect();
                ((PVEGame)Game).SendHideBlood(m_barrel, 0);
                m_barrel.PlayMovie("die", 0, 0);
                m_barrel.Die(2000);
            }
        }

        private void SetBarrelBoss()
        {
            m_barrel = ((PVEGame)Game).FindBossWithID(BarrelId);
        }

        private void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 1000f;
            Body.PlayMovie("beatC", 1000, 0);
            Body.RangeAttacking(fx, tx, "cry", 2000, null);
            //Body.CallFuction(SetStand, 2500);
        }

        private void SetStand()
        {
            ((PVEGame)Game).SendLivingActionMapping(Body, "stand", m_isOpenEye ? "standB" : "stand");
        }
    }
}