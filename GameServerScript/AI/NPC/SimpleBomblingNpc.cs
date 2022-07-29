using Bussiness;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class SimpleBomblingNpc : ABrain
    {
        private Player _mTarget;

        private int _mTargetDis;

        private static readonly string[] BombNpcChat = new string[4]
		{
			LanguageMgr.GetTranslation("Game.Server.GameServerScript.AI.NPC.SimpleBomblingNpc.msg1"),
			LanguageMgr.GetTranslation("Game.Server.GameServerScript.AI.NPC.SimpleBomblingNpc.msg2"),
			LanguageMgr.GetTranslation("Game.Server.GameServerScript.AI.NPC.SimpleBomblingNpc.msg3"),
			LanguageMgr.GetTranslation("Game.Server.GameServerScript.AI.NPC.SimpleBomblingNpc.msg4")
		};

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			base.Body.CurrentDamagePlus = 1f;
			base.Body.CurrentShootMinus = 1f;
			if (base.Body.IsSay)
			{
				string oneChat = GetOneChat();
				int delay = base.Game.Random.Next(0, 5000);
				base.Body.Say(oneChat, 0, delay);
			}
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			_mTarget = base.Game.FindNearestPlayer(base.Body.X, base.Body.Y);
			_mTargetDis = (int)_mTarget.Distance(base.Body.X, base.Body.Y);
			if (_mTargetDis <= 50)
			{
				base.Body.PlayMovie("beatA", 100, 0);
				base.Body.RangeAttacking(base.Body.X - 50, base.Body.X + 50, "cry", 1500, null);
				base.Body.Die(1000);
			}
			else
			{
				MoveToPlayer(_mTarget);
			}
        }

        public void MoveToPlayer(Player player)
        {
			int num = base.Game.Random.Next(((SimpleNpc)base.Body).NpcInfo.MoveMin, ((SimpleNpc)base.Body).NpcInfo.MoveMax);
			if (!((player.X <= base.Body.X) ? base.Body.MoveTo((base.Body.X - num > player.X) ? (base.Body.X - num) : (player.X + 50), base.Body.Y, "walk", 2000, "", 4, Beat) : base.Body.MoveTo((base.Body.X + num < player.X) ? (base.Body.X + num) : (player.X - 50), base.Body.Y, "walk", 2000, "", 4, Beat)))
			{
				base.Body.CallFuction(Beat, 1000);
			}
        }

        public void Beat()
        {
			_mTargetDis = (int)_mTarget.Distance(base.Body.X, base.Body.Y);
			if (_mTargetDis <= 50)
			{
				base.Body.PlayMovie("beatA", 100, 0);
				base.Body.RangeAttacking(base.Body.X - 100, base.Body.X + 100, "cry", 1500, null);
				base.Body.Die(1000);
			}
        }

        private string GetOneChat()
        {
			int num = base.Game.Random.Next(0, BombNpcChat.Length);
			return BombNpcChat[num];
        }
    }
}
