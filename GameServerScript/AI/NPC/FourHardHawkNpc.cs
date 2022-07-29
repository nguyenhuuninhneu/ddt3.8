using System.Collections.Generic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class FourHardHawkNpc : ABrain
    {
        private int _mAttackTurn;

        private List<PhysicalObj> _mFeatherEffect = new List<PhysicalObj>();

        private readonly int _friendlyNpcId = 4206;

        private int m_totalNpc = 4;

        private int npcId = 4202;

        private SimpleBoss _friendBoss;



        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            ClearFeatherEffect();

            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;

            Body.Config.HaveShield = Body.Properties1 == 0;
            SetState();
            //Console.WriteLine("x: " + Body.Bound.X + " - y: " + Body.Bound.Y + " - w: " + Body.Bound.Width + " - h: " + Body.Bound.Height);
        }

        public override void OnCreated()
        {
            base.OnCreated();
            Body.Properties1 = 0;
            SetState();
        }

        public override void OnStartAttacking()
        {
            Game.ShowBloodItem(Body.Id);
            switch (_mAttackTurn)
            {
                case 0:
                    {
                        if (Body.Properties1 == 0)
                            FlyRandom(AllAttackFeather);
                        _mAttackTurn++;
                        break;
                    }
                case 1:
                    {
                        if (Body.Properties1 == 0)
                            FlyRandom(AllAttackFeather);
                        _mAttackTurn++;
                        break;
                    }
                case 2:
                    {
                        if (Body.Properties1 == 0)
                        {
                            Body.Properties1 = 1;
                            FlyRandom(ChangeAtoB);
                        }
                        else
                        {
                            Body.Properties1 = 0;
                            ChangeBtoA();
                        }
                        _mAttackTurn = 0;
                        break;
                    }
            }
        }
        //public override void OnAfterTakedBomb()
        //{
        //    base.OnAfterTakedBomb();
        //    if (_friendBoss == null)
        //        GetFriendBoss();
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
        //    if (Body.Blood <= 0 || _friendBoss.Blood <= 0) _friendBoss.Die(2000);
        //}

        public override void OnDie()
        {
            base.OnDie();
            //if (Body.Properties1 == 0)
            //    return;
            //(Game as PVEGame).SendGameFocus(_friendBoss, 4000, 0);
            //_friendBoss.Die(5500);
        }

        //public override void OnAfterTakedPetDamage()
        //{
        //    base.OnAfterTakedPetDamage();
        //    if (_friendBoss == null)
        //        GetFriendBoss();
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


        private void GetFriendBoss()
        {
            // create friend bosss
            foreach (SimpleBoss boss in Game.FindLivingTurnBossWithID(_friendlyNpcId))
            {
                _friendBoss = boss;
                break;
            }
        }

        private void FlyRandom(LivingCallBack callBack)
        {
            int randX = Game.Random.Next(400, 1283);
            int randY = Game.Random.Next(400, 654);

            int delayX = Game.GetDelayDistance(Body.X, randX, 7);
            Body.MoveTo(randX, randY, "fly", 7, "", 2000, callBack, delayX / 100 * 80);
        }
        private void CreateNpc()
        {
            for (int i = 0; i < m_totalNpc; i++)
            {
                int randX = Body.X + Game.Random.Next(-300, 300);
                int randY = Body.Y + Game.Random.Next(0, 300);

                randX = randX < 0 ? 0 : randX;

                randX = randX > Game.Map.Info.DeadWidth ? Game.Map.Info.DeadWidth : randX;

                randY = randY > 778 ? 778 : randY;

                LivingConfig config = ((PVEGame)Game).BaseLivingConfig();
                config.CanCountKill = false;
                config.CanTakeDamage = false;
                config.IsFly = true;

                ((SimpleBoss)Body).CreateChild(npcId, randX, randY, false, config);
            }
            Body.CallFuction(new LivingCallBack(SetState), 2000);
        }

        private void RemoveAllNpc()
        {
            ((SimpleBoss)Body).RemoveAllChild();
        }
        private void AllAttackFeather()
        {
            Player rand = Game.FindRandomPlayer();
            Body.ChangeDirection(rand, 0);
            Body.PlayMovie("beatA", 1000, 0);

            ((PVEGame)Game).SendObjectFocus(rand, 1, 2000, 0);

            Body.CallFuction(CreateFeatherEffect, 2500);
            Body.RangeAttacking(0, Game.Map.Info.DeadWidth, "cry", 2600, null);
            //m_featherEffect = ((PVEGame)Game).Createlayer(0, 0, "", "asset.game.4.feather", "", 1, 0);
        }

        private void CreateFeatherEffect()
        {
            foreach (Player p in Game.GetAllLivingPlayers())
            {
                _mFeatherEffect.Add(((PVEGame)Game).Createlayer(p.X, p.Y, "", "asset.game.4.feather", "", 1, 0));
            }
        }

        private void ClearFeatherEffect()
        {
            foreach (PhysicalObj phy in _mFeatherEffect)
            {
                Game.RemovePhysicalObj(phy, true);
            }
            _mFeatherEffect = new List<PhysicalObj>();
        }

        private void ChangeAtoB()
        {
            Body.PlayMovie("AtoB", 500, 0);
            Body.CallFuction(new LivingCallBack(CreateNpc), 3500); 
            SetState();
        }

        private void ChangeBtoA()
        {
            Body.PlayMovie("BtoA", 1000, 0);
            RemoveAllNpc();
            SetState();
        }

        private void SetState()
        {
            switch (Body.Properties1)
            {
                case 1:
                    //Body.PlayMovie("standB", 0, 0);
                    ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standB");
                    ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cry");
                    break;
                default:
                    //Body.PlayMovie("standA", 0, 0);
                    ((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standA");
                    ((PVEGame)Game).SendLivingActionMapping(Body, "cry", "shield");
                    break;
            }
        }
    }
}