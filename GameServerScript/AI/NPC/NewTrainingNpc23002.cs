using Game.Logic.AI;

namespace GameServerScript.AI.NPC
{
    public class NewTrainingNpc23002 : ABrain
    {
        private int dis = 0;

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			int[] array = new int[2]
			{
				1,
				-1
			};
			dis = base.Game.Random.Next(30, 90);
			base.Body.MoveTo(base.Body.X + dis * array[base.Game.Random.Next(0, 1)], base.Body.Y, "walk", 3000, "", 3);
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
        }
    }
}
