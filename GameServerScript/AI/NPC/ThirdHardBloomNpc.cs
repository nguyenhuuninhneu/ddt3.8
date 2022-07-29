using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class ThirdHardBloomNpc : ABrain
    {
        private bool m_canWin = false;
        public override void OnHeal(int blood)
        {
            base.OnHeal(blood);
            if (Body.Blood >= Body.MaxBlood && !m_canWin)
            {
                m_canWin = true;
                ((PVEGame)Game).SendFreeFocus(Body.X, 400, 1, 0, 0);
                Body.Say("Năng lượng đã đầy, nhớ bám chắc vào tôi!", 0, 6000, 2000);
                Body.PlayMovie("die", 8000, 0);
                Body.PlayMovie("grow", 10800, 0);
                ((PVEGame)Game).SendFreeFocus(Body.X, 400, 2, 10500, 0);
                ((PVEGame)Game).TotalKillCount++;
            }
        }

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            base.Body.CurrentDamagePlus = 1f;
            base.Body.CurrentShootMinus = 1f;
        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
    }
}
