using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class WAS13001 : AMissionControl
    {
        private int bossAntID = 13001;

        private int bossChickenID = 13002;

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private SimpleBoss bossAnt;

        private SimpleBoss bossChicken;

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 600)
            {
                return 3;
            }
            if (score > 520)
            {
                return 2;
            }
            if (score > 450)
            {
                return 1;
            }
            return 0;
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();
            if ((bossAnt != null && !bossAnt.IsLiving && bossChicken != null && !bossChicken.IsLiving) || base.Game.TotalTurn > 200)
            {
                return true;
            }
            return false;
        }

        private void CreateBossAnt()
        {
            bossAnt = base.Game.CreateBoss(bossAntID, 1295, 445, -1, 1, "");
            bossAnt.SetRelateDemagemRect(bossAnt.NpcInfo.X, bossAnt.NpcInfo.Y, bossAnt.NpcInfo.Width, bossAnt.NpcInfo.Height);
            bossAnt.Delay = 1;
            base.Game.SendObjectFocus(bossAnt, 0, 0, 0);
            m_moive.PlayMovie("in", 2000, 0);
            m_front.PlayMovie("in", 2200, 0);
            m_moive.PlayMovie("out", 6000, 0);
            m_front.PlayMovie("out", 6200, 0);
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (bossAnt != null && !bossAnt.IsLiving && bossChicken != null && !bossChicken.IsLiving)
            {
                base.Game.IsWin = true;
            }
            else
            {
                base.Game.IsWin = false;
            }
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] resources = { bossAntID, bossChickenID };
            base.Game.AddLoadingFile(1, "bombs/51.swf", "tank.resource.bombs.Bomb51");
            base.Game.AddLoadingFile(1, "bombs/61.swf", "tank.resource.bombs.Bomb61");
            base.Game.AddLoadingFile(1, "bombs/99.swf", "tank.resource.bombs.Bomb99");
            base.Game.AddLoadingFile(2, "image/game/effect/10/jianyu.swf", "asset.game.ten.jianyu");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.canbaoAsset");
            base.Game.LoadResources(resources);
            base.Game.LoadNpcGameOverResources(resources);
            base.Game.SetMap(1214);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_moive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_front = base.Game.Createlayer(820, 400, "front", "game.asset.living.canbaoAsset", "out", 1, 0);
            bossChicken = base.Game.CreateBoss(bossChickenID, 1290, 1013, -1, 1, "");
            bossChicken.SetRelateDemagemRect(bossChicken.NpcInfo.X, bossChicken.NpcInfo.Y, bossChicken.NpcInfo.Width, bossChicken.NpcInfo.Height);
            base.Game.SendObjectFocus(bossChicken, 0, 0, 0);
            bossChicken.CallFuction(CreateBossAnt, 1500);
        }

        public override int UpdateUIData()
        {
            return base.Game.TotalKillCount;
        }
    }
}
