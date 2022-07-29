using System;
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class PDHAK1144 : AMissionControl
    {
        private SimpleBoss _mKing;
        private SimpleBoss _mKing2;
        private int _mStep = 1;
        private int bossID = 4208;
        private int bossID2 = 4209;
        private int npcId = 4207;
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
            int[] resources = { bossID, bossID2, npcId };
            int[] gameOverResource = { bossID, bossID2 };
            base.Game.AddLoadingFile(2, "image/game/effect/4/power.swf", "game.crazytank.assetmap.Buff_powup");
            base.Game.AddLoadingFile(2, "image/game/effect/4/blade.swf", "asset.game.4.blade");
            base.Game.AddLoadingFile(2, "image/game/thing/bossbornbgasset.swf", "game.asset.living.BossBgAsset");
            base.Game.AddLoadingFile(2, "image/game/thing/bossbornbgasset.swf", "game.asset.living.tingyuanlieshouAsset");
            base.Game.LoadResources(resources);
            base.Game.LoadNpcGameOverResources(gameOverResource);
            base.Game.SetMap(1144);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            LivingConfig config = base.Game.BaseLivingConfig();
            config.HaveShield = true;
            _mMoive = base.Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 0);
            _mFront = base.Game.Createlayer(1019, 620, "front", "game.asset.living.emozhanshiAsset", "out", 1, 0);
            _mKing = base.Game.CreateBoss(bossID, 1255, 958, -1, 3, "born", config);
            _mKing.SetRelateDemagemRect(_mKing.NpcInfo.X, _mKing.NpcInfo.Y, _mKing.NpcInfo.Width, _mKing.NpcInfo.Height);
            _mKing.CallFuction(new LivingCallBack(MovieCreateBoss), 1000);
        }

        private void MovieCreateBoss()
        {
            base.Game.SendObjectFocus(_mKing, 1, 500, 0);
            _mKing.PlayMovie("in", 2000, 0);
            base.Game.SendObjectFocus(_mKing, 2, 2000, 3000);
            _mKing.PlayMovie("standA", 9000, 0);
            _mKing.Say("Ngọn lửa sôi sục đang cháy trong ta!", 0, 9200);
            _mMoive.PlayMovie("in", 13000, 0);
            _mFront.PlayMovie("in", 13200, 0);
            _mMoive.PlayMovie("out", 16200, 0);
            _mFront.PlayMovie("out", 16000, 0);
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (!(Game.TurnIndex <= 1))
            {
                if (_mMoive != null)
                {
                    base.Game.RemovePhysicalObj(_mMoive, true);
                    _mMoive = null;
                }
                if (_mFront != null)
                {
                    base.Game.RemovePhysicalObj(_mFront, true);
                    _mFront = null;
                }
            }
        }

        public override bool CanGameOver()
        {
            bool result;
            if (_mKing2 != null && !_mKing2.IsLiving && _mStep >= 2)
            {
                result = true;
            }
            else
            {
                if (base.Game.TotalTurn > base.Game.MissionInfo.TotalTurn)
                {
                    result = true;
                }
                else
                {
                    if (_mStep <= 1 && _mKing != null && !_mKing.IsLiving)
                    {
                        _mStep++;
                        _mKing2 = base.Game.CreateBoss(bossID2, _mKing.X, _mKing.Y, _mKing.Direction, 1, "standB");
                        _mKing2.CallFuction(new LivingCallBack(CreateBoss), 1000);
                    }
                    result = false;
                }
            }
            return result;
        }

        private void CreateBoss()
        {
            base.Game.RemoveLiving(_mKing.Id);
            _mKing2.PlayMovie("born", 0, 0);
            _mKing2.Say("<span class=\"red\">Thực sự là giận lắm rồi. Ta sẽ nghiền nát tất cả!</span>", 0, 200);
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return base.Game.TotalKillCount;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (_mStep >= 2 && _mKing2 != null && !_mKing2.IsLiving && _mKing != null && !_mKing.IsLiving)
            {
                base.Game.IsWin = true;
            }
            else
            {
                base.Game.IsWin = false;
            }
        }

        public override void OnShooted()
        {
            base.OnShooted();
        }
    }
}
