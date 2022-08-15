using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Bussiness;
using Bussiness.Managers;
using log4net;
using SqlDataProvider.Data;

public class SpiritMgr
{
    private static readonly ILog ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    private static List<SpiritInfo> _infos;

    private static ReaderWriterLock m_lock;

    private static Random rand;

    public static bool ReLoad()
    {
        try
        {
            List<SpiritInfo> dict = new List<SpiritInfo>();
            if (Load())
            {
                m_lock.AcquireWriterLock(-1);
                try
                {
                    _infos = dict;
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
            if (ILog.IsErrorEnabled)
            {
                ILog.Error("SpiritMgr", exception);
            }
        }
        return false;
    }

    public static bool Init()
    {
        try
        {
            m_lock = new ReaderWriterLock();
            _infos = new List<SpiritInfo>();
            rand = new Random();
            return Load();
        }
        catch (Exception exception)
        {
            if (ILog.IsErrorEnabled)
            {
                ILog.Error("SpiritMgr", exception);
            }
            return false;
        }
    }

    private static bool Load()
    {
        using (ProduceBussiness produceBussiness = new ProduceBussiness())
        {
            SpiritInfo[] all = produceBussiness.GetAllSpiritInfo();
            SpiritInfo[] array = all;
            foreach (SpiritInfo info in array)
            {
                _infos.Add(info);
            }
        }
        return true;
    }

    public static SpiritInfo GetSpiritInfo(UserEquipGhostInfo info)
    {
        if(info == null)
        {
            return null;
        }

        foreach(var spirit in _infos)
        {
            if(spirit.Level == info.Level && spirit.BagType == info.BagType && spirit.BagPlace == info.Place)
            {
                return spirit;
            }
        }

        return null;
    }
}
