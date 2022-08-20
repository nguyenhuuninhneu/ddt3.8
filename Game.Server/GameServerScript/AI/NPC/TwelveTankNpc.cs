using Game.Logic;
using Game.Logic.AI;

namespace GameServerScript.AI.NPC
{
    public class TwelveTankNpc : ABrain
    {
		private string OldActionMovie = "cry";
		
		public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
        }

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnStartAttacking()
        {
        }

        public override void OnDie()
        {
			base.OnDie();
		}

        public override void OnBeforeTakedDamage(Living source, ref int damage, ref int crit)
        {
			if ((double)base.Body.Blood >= (double)base.Body.MaxBlood * 0.7)
			{
				OldActionMovie = "cry";
				if (base.Body.ActionMovie != "cryA")
				{
					base.Body.ActionMovie = "cryA";
				}
			}
			else if ((double)base.Body.Blood < (double)base.Body.MaxBlood * 0.7 && (double)base.Body.Blood > (double)base.Body.MaxBlood * 0.3)
			{
				if (base.Body.ActionMovie == "cryA")
				{
					base.Body.PlayMovie("cryAtoB", 0, 1000);
					if (base.Body.ActionMovie != "cryB")
					{
						base.Body.ActionMovie = "cryB";
					}
					OldActionMovie = "cryA";
				}
			}
			else if ((double)base.Body.Blood < (double)base.Body.MaxBlood * 0.3 && base.Body.Blood > 0)
			{
				if (base.Body.ActionMovie == "cryB")
				{
					base.Body.PlayMovie("cryBtoC", 0, 1000);
					if (base.Body.ActionMovie != "cryC")
					{
						base.Body.ActionMovie = "cryC";
					}
					OldActionMovie = "cryB";
				}
			}
			(Game as PVEGame).SendLivingActionMapping(Body, OldActionMovie, Body.ActionMovie);
			base.OnBeforeTakedDamage(source, ref damage, ref crit);
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }
    }
}
