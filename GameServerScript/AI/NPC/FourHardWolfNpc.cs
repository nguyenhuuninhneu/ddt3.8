using System;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.NPC
{
    public class FourHardWolfNpc : ABrain
    {
        private Player _mTargetPlayer;

        private const int FriendlyNpcId = 4205;

        private SimpleBoss _friendBoss;

        private bool _openEye;

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;

            Body.Config.HaveShield = _openEye;
            SetState();
        }

        public override void OnCreated()
        {
            base.OnCreated();
            if (_friendBoss == null)
            {
                // create friend bosss
                foreach (SimpleBoss boss in Game.FindLivingTurnBossWithID(FriendlyNpcId))
                {
                    _friendBoss = boss;
                    break;
                }
            }
            SetState();
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            Game.ShowBloodItem(Body.Id);
            switch (_friendBoss.Properties1)
            {
                case 0 when _openEye:
                    // change state B -> A
                    Body.PlayMovie("BtoA", 1000, 4000);
                    Body.CallFuction(RemoveTargetPlayer, 5000);
                    _openEye = false;
                    SetState();
                    break;
                case 1 when !_openEye:
                    Body.PlayMovie("AtoB", 2000, 4000);
                    Body.CallFuction(FindRandomPlayer, 4000);
                    _openEye = true;
                    SetState();
                    break;
                default:
                    {
                        if (_openEye && _mTargetPlayer != null)
                        {
                            int rand = Game.Random.Next(100);
                            if (rand < 50 && Body.Distance(_mTargetPlayer.X, _mTargetPlayer.Y) > 600)
                            {
                                // jump to target
                                JumpToTargetPlayer();
                            }
                            else
                            {
                                RunToTargetPlayer();
                            }
                        }
                        else if (!_openEye)
                        {
                            RandomMove();
                        }
                        else
                        {
                            Console.WriteLine(@"eye: " + _openEye + @" - friendBoss.Properties1: " + _friendBoss.Properties1);
                        }

                        break;
                    }
            }
        }

        private void RandomMove()
        {
            Body.ChangeDirection(-1, 1200);
            Body.ChangeDirection(1, 1800);
            Body.ChangeDirection(-1, 2300);
            int dismove = Game.Random.Next(-300, 300);
            int delay = Game.GetDelayDistance(Body.X, Body.X + dismove, 3) + 800;
            Body.MoveTo(Body.X + dismove, Body.Y, "walkA", ((SimpleBoss)Body).NpcInfo.speed, "", 3500, NothingAction, delay);
        }

        private void NothingAction()
        {
            Body.ChangeDirection(1, 500);
            Body.ChangeDirection(-1, 1000);
            Body.PlayMovie("beatA", 1000, 2000);
        }

        private void FindRandomPlayer()
        {
            _mTargetPlayer = Game.FindRandomPlayer();

            if (_mTargetPlayer != null)
            {
                Body.ChangeDirection(_mTargetPlayer, 500);
                // notice player
                ((PVEGame)Game).SendPlayersPicture(_mTargetPlayer, (int)eLivingPictureType.Targeting, true);
                Body.Say("Lo chạy đi <p class=\"red\">" + _mTargetPlayer.PlayerDetail.PlayerCharacter.NickName + "</p>Ta đến đây...", 0, 2000, 0);
            }
        }

        private void RemoveTargetPlayer()
        {
            if (_mTargetPlayer != null)
            {
                ((PVEGame)Game).SendPlayersPicture(_mTargetPlayer, (int)eLivingPictureType.Targeting, false);
                _mTargetPlayer = null;
            }
        }
        private void RunToTargetPlayer()
        {
            Body.ChangeDirection(_mTargetPlayer, 200);

            double dis = Body.Distance(_mTargetPlayer.X, _mTargetPlayer.Y);
            bool check = Body.FindDirection(_mTargetPlayer) == -1;
            int disX = _mTargetPlayer.X + (check ? 100 : -100);
            int delay = 0;
            if (dis > 600)
            {
                delay = Game.GetDelayDistance(Body.X, disX, 10) + 800;
                Body.MoveTo(disX, _mTargetPlayer.Y, "walkB", 10, "", 2200, BeatTargetPlayer, delay);
            }
            else
            {
                delay = Game.GetDelayDistance(Body.X, disX, 5) + 800;
                Body.MoveTo(disX, _mTargetPlayer.Y, "walkC", 5, "", 2200, BeatTargetPlayer, delay);
            }
        }

        private void JumpToTargetPlayer()
        {
            Body.ChangeDirection(_mTargetPlayer, 200);

            Body.PlayMovie("jump", 2000, 0);

            ((PVEGame)Game).SendObjectFocus(_mTargetPlayer, 1, 5000, 0);

            Body.BoltMove(_mTargetPlayer.X, _mTargetPlayer.Y, 6000);

            Body.PlayMovie("fall", 7000, 0);

            Body.RangeAttacking(_mTargetPlayer.X - 100, _mTargetPlayer.X + 100, "cry", 8000, null);

            Body.CallFuction(BeatTargetPlayer, 8500);
        }

        private void BeatTargetPlayer()
        {
            Body.ChangeDirection(_mTargetPlayer, 200);
            Body.CurrentDamagePlus = 2f;
            Body.Beat(_mTargetPlayer, "beatB", 0, 0, 1000); //1000
        }


        //public override void OnAfterTakedBomb()
        //{
        //    base.OnAfterTakedBomb();
        //    var bloodReduce = Body.Blood - _friendBoss.Blood;
        //    if (_friendBoss.Blood - bloodReduce <= 0)
        //    {
        //        if (_friendBoss.Properties1 == 2)
        //            return;
        //        _friendBoss.Properties1 = 2;
        //        _friendBoss.AddBlood(_friendBoss.Blood - 1, 1);
        //        return;
        //    }
        //    _friendBoss.AddBlood(bloodReduce, 1);
        //    //if (Body.Blood <= 0 || _friendBoss.Blood <= 0) _friendBoss.Die(2000);
        //}

        public override void OnDie()
        {
            base.OnDie();
            //if (_openEye)
            //    return;
            //((PVEGame)Game).SendGameFocus(_friendBoss, 4000, 0);
            //_friendBoss.Die(5500);
        }

        //public override void OnAfterTakedPetDamage()
        //{
        //    base.OnAfterTakedPetDamage();
        //    var bloodReduce = Body.Blood - _friendBoss.Blood;
        //    if (_friendBoss.Blood - bloodReduce <= 0)
        //    {
        //        if (_friendBoss.Properties1 == 2)
        //            return;
        //        _friendBoss.Properties1 = 2;
        //        _friendBoss.AddBlood(_friendBoss.Blood - 1, 1);
        //        return;
        //    }
        //    _friendBoss.AddBlood(bloodReduce, 1);
        //    //if (Body.Blood <= 0 || _friendBoss.Blood <= 0) _friendBoss.Die(2000);
        //}


        private void SetState()
        {
            if (_openEye)
            {
                //Body.PlayMovie("standB", 0, 0);
                ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standB");
                ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "shield");
            }
            else
            {
                //Body.PlayMovie("standA", 0, 0);
                ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standA");
                ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cry");
            }
        }
    }
}
