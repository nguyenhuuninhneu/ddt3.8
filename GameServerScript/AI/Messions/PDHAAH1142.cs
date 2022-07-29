using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Effects;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class PDHAAH1142 : AMissionControl
    {
        private SimpleBoss _dragonBoss;

        private SimpleBoss _barrelHelperBoss;

        private const int BarrelNpcId = 4301;

        private const int NpcId = 4303;

        private const int BossId = 4304;

        private readonly int _mBloodReduce = 2100;

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 1750)
            {
                return 3;
            }
            else if (score > 1675)
            {
                return 2;
            }
            else if (score > 1600)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            int[] resources = { BossId, NpcId, BarrelNpcId };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.AddLoadingFile(2, "image/game/effect/4/gate.swf", "game.asset.Gate");
            Game.SetMap(1142);
        }
        public override void OnPrepareStartGame()
        {
            base.OnPrepareStartGame();
            LivingConfig config = Game.BaseLivingConfig();
            config.CanTakeDamage = false;

            _dragonBoss = Game.CreateBoss(BossId, 1520, 350, -1, 0, "", config);
            Game.SendHideBlood(_dragonBoss, 0);
            _dragonBoss.OnSmallMap(false);

        }
        public override void OnStartGame()
        {
            base.OnStartGame();
            Game.SendFreeFocus(_dragonBoss.X, _dragonBoss.Y, 1, 1000, 1500);
            Game.SendFreeFocus(335, 630, 1, 3000, 3500);
            _dragonBoss.CallFuction(CreateHelper, 4500);
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            if (_barrelHelperBoss == null || _barrelHelperBoss.IsLiving) return;
            Game.RemoveLiving(_barrelHelperBoss, true);
            CreateHelper();

        }

        private void CreateHelper()
        {
            LivingConfig config = Game.BaseLivingConfig();
            config.IsHelper = true;
            config.CanFrost = true;
            _barrelHelperBoss = Game.CreateBoss(BarrelNpcId, 321, 746, 1, 0, "", config);
            _barrelHelperBoss.AddEffect(new ContinueReduceBloodEffect(3, _mBloodReduce, _barrelHelperBoss), 3000);
            _barrelHelperBoss.Delay++;
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();
            return Game.TotalKillCount >= Game.MissionInfo.TotalCount || Game.TurnIndex >= Game.MissionInfo.TotalTurn;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            Game.IsWin = Game.TotalKillCount >= Game.MissionInfo.TotalCount;
        }
    }
}