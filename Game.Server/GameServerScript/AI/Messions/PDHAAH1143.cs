using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;


namespace GameServerScript.AI.Messions
{
    public class PDHAAH1143 : AMissionControl
    {
        private SimpleBoss _mHawkBoss;

        private SimpleBoss _mBoss;

        private readonly int _bossId = 4305;

        private readonly int _bossId2 = 4306;

        private readonly int _npcId = 4302;

        private bool m_canWin = false;

        private PhysicalObj _mMoive;

        private PhysicalObj _mFront;

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
            int[] resources = { _bossId, _bossId2, _npcId };
            int[] gameOverResource = { _bossId, _bossId2, _npcId };
            Game.AddLoadingFile(2, "image/game/effect/4/feather.swf", "asset.game.4.feather");
            Game.AddLoadingFile(2, "image/game/thing/bossbornbgasset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/bossbornbgasset.swf", "game.asset.living.tingyuanlieshouAsset");
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.SetMap(1143);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();


            _mMoive = Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 0);
            _mFront = Game.Createlayer(658, 607, "front", "game.asset.living.tingyuanlieshouAsset", "out", 1, 0);

            LivingConfig config = Game.BaseLivingConfig();
            config.IsFly = true;
            config.IsShowBloodBar = true;
            config.FriendlyBoss = new LivingConfig.FriendlyLiving(_bossId2, true);

            _mHawkBoss = Game.CreateBoss(_bossId, 354, 344, -1, 1, "", config);
            _mHawkBoss.SetRelateDemagemRect(_mHawkBoss.NpcInfo.X, _mHawkBoss.NpcInfo.Y, _mHawkBoss.NpcInfo.Width, _mHawkBoss.NpcInfo.Height);
            _mHawkBoss.DoAction = 2;

            Game.SendObjectFocus(_mHawkBoss, 1, 100, 0);

            Game.SendFreeFocus(1460, 962, 1, 3000, 0);

            _mHawkBoss.CallFuction(CreateBoss, 4000);
        }

        private void CreateBoss()
        {
            LivingConfig config = Game.BaseLivingConfig();
            config.IsShowBloodBar = true;
            config.FriendlyBoss = new LivingConfig.FriendlyLiving(_bossId, true);

            _mBoss = Game.CreateBoss(_bossId2, 1460, 962, -1, 1, "", config);
            _mBoss.SetRelateDemagemRect(_mBoss.NpcInfo.X, _mBoss.NpcInfo.Y, _mBoss.NpcInfo.Width, _mBoss.NpcInfo.Height);
            _mBoss.DoAction = 2;
            _mBoss.Delay++;

            Game.SendFreeFocus(740, 680, 1, 2500, 0);

            _mBoss.Config.FriendlyBoss.FriendBoss = _mHawkBoss;
            _mBoss.Config.FriendlyBoss.ActionStr = "shield";

            _mHawkBoss.Config.FriendlyBoss.FriendBoss = _mBoss;
            _mHawkBoss.Config.FriendlyBoss.ActionStr = "shield";

            _mMoive.PlayMovie("in", 3000, 0);
            _mFront.PlayMovie("in", 3200, 0);
            _mMoive.PlayMovie("out", 6000, 0);
            _mFront.PlayMovie("out", 6000, 0);
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            if (Game.TurnIndex <= 1) return;
            if (_mMoive != null)
            {
                Game.RemovePhysicalObj(_mMoive, true);
                _mMoive = null;
            }
            if (_mFront != null)
            {
                Game.RemovePhysicalObj(_mFront, true);
                _mFront = null;
            }
        }

        public override bool CanGameOver()
        {
            if (_mBoss != null && !_mBoss.IsLiving && !m_canWin)
            {
                Game.GameStateModify = eGameState.Waiting;
                Game.SendFreeFocus(_mHawkBoss.X, _mHawkBoss.Y - 100, 0, 0, 0);

                _mHawkBoss.PlayMovie("die", 2500, 0);
                _mHawkBoss.Die(4000);

                m_canWin = true;
            }
            if (_mHawkBoss != null && !_mHawkBoss.IsLiving && !m_canWin)
            {
                Game.GameStateModify = eGameState.Waiting;
                Game.SendFreeFocus(_mBoss.X, _mBoss.Y - 100, 0, 0, 0);

                _mBoss.PlayMovie("die", 2500, 0);
                _mBoss.Die(4000);

                m_canWin = true;
            }
            return Game.TotalKillCount >= Game.MissionInfo.TotalCount || Game.TotalTurn > Game.MissionInfo.TotalTurn;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return Game.TotalKillCount;
        }
        public override void OnWaitingGameState()
        {
            base.OnWaitingGameState();
            Game.GameStateModify = eGameState.Playing;
        }
        public override void OnGameOver()
        {
            base.OnGameOver();
            Game.IsWin = Game.TotalKillCount >= Game.MissionInfo.TotalCount;
        }
    }
}
