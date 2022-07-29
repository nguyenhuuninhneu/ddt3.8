using Bussiness;
using log4net;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Game.Server.Managers
{
    public class FairBattleRewardMgr
    {
        private static Dictionary<int, FairBattleRewardInfo> _fairBattleRewards;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static ReaderWriterLock m_lock;

        private static ThreadSafeRandom rand;

        public static FairBattleRewardInfo FindLevel(int Level)
        {
			m_lock.AcquireReaderLock(10000);
			try
			{
				if (_fairBattleRewards.ContainsKey(Level))
				{
					return _fairBattleRewards[Level];
				}
			}
			catch
			{
			}
			finally
			{
				m_lock.ReleaseReaderLock();
			}
			return null;
        }

        public static FairBattleRewardInfo GetBattleDataByPrestige(int Prestige)
        {
			for (int num = _fairBattleRewards.Values.Count - 1; num >= 0; num--)
			{
				if (Prestige >= _fairBattleRewards[num].Prestige)
				{
					return _fairBattleRewards[num];
				}
			}
			return null;
        }

        public static int GetGP(int level)
        {
			if (MaxLevel() > level && level > 0)
			{
				return FindLevel(level - 1).Prestige;
			}
			return 0;
        }

        public static int GetLevel(int GP)
        {
			if (GP >= FindLevel(MaxLevel()).Prestige)
			{
				return MaxLevel();
			}
			for (int i = 1; i <= MaxLevel(); i++)
			{
				if (GP < FindLevel(i).Prestige)
				{
					if (i - 1 != 0)
					{
						return i - 1;
					}
					return 1;
				}
			}
			return 1;
        }

        public static bool Init()
        {
			try
			{
				m_lock = new ReaderWriterLock();
				_fairBattleRewards = new Dictionary<int, FairBattleRewardInfo>();
				rand = new ThreadSafeRandom();
				return LoadFairBattleReward(_fairBattleRewards);
			}
			catch (Exception exception)
			{
				if (log.IsErrorEnabled)
				{
					log.Error("FairBattleRewardMgr", exception);
				}
				return false;
			}
        }

        private static bool LoadFairBattleReward(Dictionary<int, FairBattleRewardInfo> Level)
        {
			using (ProduceBussiness produceBussiness = new ProduceBussiness())
			{
				FairBattleRewardInfo[] allFairBattleReward = produceBussiness.GetAllFairBattleReward();
				FairBattleRewardInfo[] array = allFairBattleReward;
				foreach (FairBattleRewardInfo fairBattleRewardInfo in array)
				{
					if (!Level.ContainsKey(fairBattleRewardInfo.Level))
					{
						Level.Add(fairBattleRewardInfo.Level, fairBattleRewardInfo);
					}
				}
			}
			return true;
        }

        public static int MaxLevel()
        {
			if (_fairBattleRewards == null)
			{
				Init();
			}
			return _fairBattleRewards.Values.Count;
        }

        public static bool ReLoad()
        {
			try
			{
				Dictionary<int, FairBattleRewardInfo> dictionary = new Dictionary<int, FairBattleRewardInfo>();
				if (LoadFairBattleReward(dictionary))
				{
					m_lock.AcquireWriterLock(-1);
					try
					{
						_fairBattleRewards = dictionary;
						return true;
					}
					catch
					{
					}
					finally
					{
						m_lock.ReleaseWriterLock();
					}
				}
			}
			catch (Exception exception)
			{
				if (log.IsErrorEnabled)
				{
					log.Error("FairBattleMgr", exception);
				}
			}
			return false;
        }
    }
}
