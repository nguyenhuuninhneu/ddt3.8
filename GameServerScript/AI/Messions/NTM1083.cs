using System.Collections.Generic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class NTM1083 : AMissionControl
    {
        private int mapId = 2013;
        private int CaptainNpcID = 23003;//NewTrainingBoss23003
        private int blueNpcID = 21001;//NewTrainingNpc21001
        private int totalNpcCount = 3;
        private List<SimpleNpc> simpleNpcList = new List<SimpleNpc>();

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 900)
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

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] npcIdList = { CaptainNpcID, blueNpcID };
            Game.LoadResources(npcIdList);
            Game.LoadNpcGameOverResources(npcIdList);
            Game.SetMap(mapId);
        }

        public override void OnStartGame()
        {
            CreateNpc();
        }

        public override void OnNewTurnStarted()
        {

        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
        }

        public override bool CanGameOver()
        {
            foreach (SimpleNpc npc in simpleNpcList)
            {
                if (npc.IsLiving)
                { return false; }
            }
            if (simpleNpcList.Count == totalNpcCount)
            { return true; }
            else
            { return false; }
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            foreach (Player p in Game.GetAllFightPlayers())
            {
                p.CanGetProp = true;

            }
            if (Game.GetLivedLivings().Count == 0)
            {
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }
        }
        private void CreateNpc()
        {
            simpleNpcList.Add(Game.CreateNpc(blueNpcID, 775, 553, 1, 1));
        }
    }
}
