using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System;

namespace GameServerScript.AI.NPC
{
    public class FourNormalFireNpc : ABrain
    {

        private readonly int shieldId = 4110;

        private SimpleNpc shield;

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;
        }

        public override void OnCreated()
        {
            base.OnCreated();
            Body.Properties1 = 0;
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();

            int randX = Game.Random.Next(50, Game.Map.Info.DeadWidth - 50);
            int randY = Game.Random.Next(50, 500);

            int delay = Game.GetDelayDistance(Body.X, randX, 10) + 800;
            Body.MoveTo(randX, randY, "fly", 1000, 10);


            //if (shield == null)
            //    Body.MoveTo(randX, randY, "fly", 10, "", 1000, CreateShield, delay);
            //else
            //{
            //    Body.MoveTo(randX, randY, "fly", 1000, 10);
            //    shield?.MoveTo(randX, randY, "fly", 1000, 10);
            //}
        }

        private void CreateShield()
        {
            LivingConfig config = ((PVEGame)Game).BaseLivingConfig();
            config.IsFly = true;
            config.CanCountKill = false;
            config.IsTurn = false;
            shield = ((PVEGame)Game).CreateNpc(shieldId, Body.X, Body.Y, 1, -1, config);
            shield.OnSmallMap(false);
            ((PVEGame)Game).SendHideBlood(shield, 0);
            shield.OnSmallMap(false);
        }

        public override void OnAfterTakedBomb()
        {
            base.OnAfterTakedBomb();
           
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
        public override void OnBeforeTakedBomb()
        {
            base.OnBeforeTakedBomb();
            //if (shield != null && !shield.IsLiving && Body.Config.HaveShield)
            //{
            //    Body.Config.HaveShield = false;
            //}
        }
    }
}