using Game.Logic.AI;

namespace GameServerScript.AI.Game
{
    public class EvilTribeNormalGame : APVEGameControl
    {
        public override void OnCreated()
        {
			base.OnCreated();
            base.Game.SetupMissions("3101,3102,3103,3106");
            //base.Game.SetupMissions("3103,3106");
            base.Game.TotalMissionCount = 4;
        }

        public override void OnPrepated()
        {
			base.OnPrepated();
			//base.Game.SessionId = 0;
        }

        public override int CalculateScoreGrade(int score)
        {
			base.CalculateScoreGrade(score);
			if (score > 800)
			{
				return 3;
			}
			if (score > 825)
			{
				return 2;
			}
			if (score > 725)
			{
				return 1;
			}
			return 0;
        }

        public override void OnGameOverAllSession()
        {
			base.OnGameOverAllSession();
        }
    }
}
