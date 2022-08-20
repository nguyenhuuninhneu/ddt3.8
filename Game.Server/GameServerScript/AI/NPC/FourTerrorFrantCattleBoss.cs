using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Drawing;
using Bussiness;

namespace GameServerScript.AI.NPC
{
    public class FourTerrorFrantCattleBoss : ABrain
    {
        private int _mAttackTurn;

        private Point _mPointMakeDamage;

        private bool _mIsBorn;

        protected Player MTarger;

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;
        }

        public override void OnCreated()
        {
            base.OnCreated();
            _mIsBorn = false;
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();

            switch (_mAttackTurn)
            {
                case 0:
                    Body.CallFuction(JumpAttack, 4000);
                    break;
                case 1:
                    Body.CallFuction(RunAttack, 1000);
                    break;
                case 2:
                    Body.CallFuction(RunAttack, 1000);
                    _mAttackTurn = 0;
                    return;
            }
            _mAttackTurn++;
        }

        private void JumpAttack()
        {
            MTarger = Game.FindRandomPlayer();
            if (MTarger != null)
            {
                Body.PlayMovie("jump", 1000, 0);
                Body.BoltMove(MTarger.X, MTarger.Y, 3000);
                ((PVEGame)Game).SendObjectFocus(MTarger, 1, 3000, 0);
                if (Game.Random.Next(100) < 50)
                {
                    Body.PlayMovie("fallB", 4000, 0);
                }
                else
                {
                    Body.CurrentDamagePlus = 2f;
                    Body.PlayMovie("fall", 4000, 0);
                }
                Body.RangeAttacking(MTarger.X - 150, MTarger.X + 150, "cry", 5000, true);
            }
        }

        private void RunAttack()
        {
            MTarger = Game.FindFarPlayer(Body.X, Body.Y);
            if (MTarger != null)
            {
                Body.ChangeDirection(MTarger, 100);
                _mPointMakeDamage = new Point(Body.X, Body.Y);
                // check it's current
                if (Body.Distance(MTarger.X, MTarger.Y) <= 100)
                {
                    Body.CallFuction(NearAttack, 1000);
                }
                else
                {
                    if (Body.Direction == -1)
                        Body.MoveTo(MTarger.X - 100, MTarger.Y, "walk", 1000, "", 25, AttackRangeRun);
                    else
                        Body.MoveTo(MTarger.X + 100, MTarger.Y, "walk", 1000, "", 25, AttackRangeRun);
                }
            }
        }

        private void NearAttack()
        {
            if (Body.Direction == -1)
            {
                Body.MoveTo(MTarger.X - 200, MTarger.Y, "walk", 500, "", 25, AttackRangeRun);
            }
            else
            {
                Body.MoveTo(MTarger.X + 200, MTarger.Y, "walk", 500, "", 25, AttackRangeRun);
            }
        }

        private void AttackRangeRun()
        {
            Body.ChangeDirection(MTarger, 100);
            if (Body.X < _mPointMakeDamage.X)
            {
                Body.RangeAttacking(Body.X, _mPointMakeDamage.X, "cry", 0, true);
            }
            else
            {
                Body.RangeAttacking(_mPointMakeDamage.X, Body.X, "cry", 0, true);
            }
        }


        public override void OnDie()
        {
            base.OnDie();
            Body.Say(LanguageMgr.GetTranslation("GameServerScript.AI.NPC.FourHardFrantCattleBoss.msg3"), 1, 1200);
        }
    }
}