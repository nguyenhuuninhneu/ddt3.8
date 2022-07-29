using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class ThirteenNormalNpc : ABrain
    {
        private int int_0;

        private Player player_0;

        private void method_0()
        {
			base.Body.PlayMovie("die", 1000, 0);
			if (player_0 != null)
			{
				base.Body.CallFuction(method_1, 2500);
				player_0.Die(3000);
			}
			base.Body.Die(4000);
        }

        private void method_1()
        {
			player_0.SetVisible(state: true);
			player_0.BlockTurn = false;
        }

        private void nSusVjDjvAj()
        {
			_ = base.Body.Properties1;
			if (true)
			{
				player_0 = base.Game.FindPlayerWithId(base.Body.Properties1);
				if (player_0 != null && player_0.IsLiving)
				{
					base.Body.PlayMovie("beatA", 500, 0);
					base.Body.BeatDirect(player_0, "", 2000, 1, 1);
				}
			}
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			m_body.CurrentDamagePlus = 1f;
			m_body.CurrentShootMinus = 1f;
        }

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnDie()
        {
			base.OnDie();
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			switch (int_0)
			{
			case 0:
				nSusVjDjvAj();
				break;
			case 1:
				method_0();
				break;
			}
			int_0++;
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }
    }
}
