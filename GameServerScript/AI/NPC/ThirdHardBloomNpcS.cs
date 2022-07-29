using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System;

namespace GameServerScript.AI.NPC
{
    public class ThirdHardBloomNpcS : ABrain
    {
        private int addBloom = 1200;

        private static Random random = new Random();

        private static string[] AntChat = new string[4]
		{
			"Cẩn thận... chúng ta phải tiêu diệt tà thần",
			"Không được đầu hàng !",
			"Tôi sẽ tiếp cho các bạn sức mạnh！",
			"Chúng ta sẽ đẩy lùi bộ tộc tà thần！"
		};

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
			base.Body.PlayMovie("", 0, 1000);
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			if (base.Game.Random.Next(0, 5) == 0)
			{
				int num = base.Game.Random.Next(0, AntChat.Length);
				string msg = AntChat[num];
				m_body.Say(msg, 0, 1000);
			}
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			int num = 0;
			foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
			{
				if (allFightPlayer.IsLiving && allFightPlayer.X > base.Body.X - 100 && allFightPlayer.X < base.Body.X + 100)
				{
					num++;
				}
			}
			base.Body.PlayMovie("renew", 0, 0);
			foreach (Player allFightPlayer2 in base.Game.GetAllFightPlayers())
			{
				if (allFightPlayer2.IsLiving && allFightPlayer2.X > base.Body.X - 100 && allFightPlayer2.X < base.Body.X + 100)
				{
					allFightPlayer2.AddBlood(addBloom / num);
				}
			}
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }

        public override void OnKillPlayerSay()
        {
			base.OnKillPlayerSay();
        }

        public override void OnDiedSay()
        {
        }

        public override void OnShootedSay(int delay)
        {
        }
    }
}
