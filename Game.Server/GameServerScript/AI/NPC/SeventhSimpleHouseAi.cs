using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
	public class SeventhSimpleHouseAi : ABrain
	{
		private int m_gatrongID = 7022;

		private int m_gamaiID = 7021;

		private int m_gamaiMaxCount = 3;

		private int m_gatrongMaxCount = 8;

		private int m_gamaiCount = 1;

		private int m_gatrongCount = 3;

		private bool m_halfBlood = false;
		public override void OnBeginSelfTurn()
		{
			base.OnBeginSelfTurn();
		}

		public override void OnBeginNewTurn()
		{
			base.OnBeginNewTurn();
			base.Body.CurrentDamagePlus = 1f;
			base.Body.CurrentShootMinus = 1f;
		}

		public override void OnCreated()
		{
			base.OnCreated();
		}

		public override void OnStartAttacking()
		{
			bool flag = false;
			foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
			{
				if (allFightPlayer.IsLiving && allFightPlayer.X > base.Body.X - 400 && allFightPlayer.X < base.Body.X + 400)
				{
					flag = true;
				}
			}
			if (flag)
			{
				KillAttack(base.Body.X - 400, base.Body.X + 400);
				return;
			}
			CreateNpc1();
			CreateNpc2();
		}


		public override void OnAfterTakedBomb()
		{
			base.OnAfterTakedBomb();
			if (Body.Blood <= Body.MaxBlood / 2 && !m_halfBlood)
			{
				((PVEGame)Game).SendLivingActionMapping(Body, "cry", "cryA");
				((PVEGame)Game).SendLivingActionMapping(Body, "stand", "standA");
				((PVEGame)Game).SendLivingActionMapping(Body, "to", "toA");
				m_halfBlood = true;
			}
			else if (Body.Blood >= Body.MaxBlood / 2)
			{
				((PVEGame)Game).SendLivingActionMapping(Body, "toA", "to");
			}
		}

		public override void OnStopAttacking()
		{
			base.OnStopAttacking();
		}


		public void KillAttack(int fx, int tx)
		{
			base.Body.RangeAttacking(fx, tx, "cry", 1000, null);
		}

		public void CreateNpc1()
		{
			if (m_gamaiCount > m_gamaiMaxCount)
			{
				m_gamaiCount = m_gamaiMaxCount;
			}
			int Count = m_gamaiMaxCount - base.Game.GetLivedNpcs(m_gamaiID).Count;
			if (m_gamaiCount > Count)
			{
				m_gamaiCount = Count;
			}
			if (m_gamaiCount > 0)
			{
				for (int i = 0; i < m_gamaiCount; i++)
				{
					((SimpleBoss)base.Body).CreateChild(m_gamaiID, 596 + i * 75, 955, 0, 1, true, ((PVEGame)base.Game).BaseLivingConfig());
				}
			}
		}

		public void CreateNpc2()
		{
			if (m_gatrongCount > m_gatrongMaxCount)
			{
				m_gatrongCount = m_gatrongMaxCount;
			}
			int Count = m_gatrongMaxCount - base.Game.GetLivedNpcs(m_gatrongID).Count;
			if (m_gatrongCount > Count)
			{
				m_gatrongCount = Count;
			}
			if (m_gatrongCount > 0)
			{
				for (int i = 0; i < m_gatrongCount; i++)
				{
					((SimpleBoss)base.Body).CreateChild(m_gatrongID, 792 + i * 80, 950, 0, 1, showBlood: true, ((PVEGame)base.Game).BaseLivingConfig());
				}
			}
		}

	}
}
