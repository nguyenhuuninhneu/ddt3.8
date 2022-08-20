using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class FourHardShortNpc : ABrain
    {
        private const int BossId = 4101;

        private SimpleBoss m_barrelBoss;
        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (!m_body.IsSay) return;
            m_body.Say(NormalSay[Game.Random.Next(0, NormalSay.Length)], 0, Game.Random.Next(0, 5000));
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;
            if (m_barrelBoss == null)
                SetBarrelBoss();
            else
                if (!m_barrelBoss.IsLiving)
                SetBarrelBoss();
            if (m_barrelBoss != null)
                Body.MoveTo(m_barrelBoss.X + Game.Random.Next(50, 150), m_barrelBoss.Y, "walk", 1500, ((SimpleNpc)Body).NpcInfo.speed);
        }

        private void SetBarrelBoss()
        {
            m_barrelBoss = ((PVEGame)Game).FindBossWithID(BossId);
        }

        private static readonly string[] NormalSay =
        {
              "Thằng khốn nạn nào quăng mình ra đây thế này?",
                "Đây là đâu? Sao nóng thế?",
                "Tại sao mình lại ở đây nhỉ?",
                "Chuyện gì đang xảy ra vậy?",
                "Thật không thể hiểu nổi!"
        };
    }
}