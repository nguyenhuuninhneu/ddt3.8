using Bussiness;
using Bussiness.Managers;
using Game.Base.Packets;
using Game.Server.GameUtils;
using Game.Server.Managers;
using SqlDataProvider.Data;
using System;

namespace Game.Server.Packets.Client
{
    [PacketHandler((short)ePackageType.EQUIP_GHOST, "Equip Ghost Item")]
    public class EquipGhostHandler : IPacketHandler
    {
        private static RandomSafe random = new RandomSafe();

        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            GSPacketIn pkg = new GSPacketIn((short)ePackageType.EQUIP_GHOST, client.Player.PlayerCharacter.ID);

            // 0 - luck , 1 - equip, 2 - stone
            ItemInfo stone = client.Player.StoreBag.GetItemAt(2);
            ItemInfo item = client.Player.StoreBag.GetItemAt(1);
            ItemInfo luck = client.Player.StoreBag.GetItemAt(0);

            if (item == null || item.Count != 1 || (item.Template.CategoryID != 1 && item.Template.CategoryID != 5 && item.Template.CategoryID != 7))
            {
                // Chưa chọn vũ khí hay trang bị
                client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("EquipGhostHandler.Material1")); ;
                return 0;
            }
            if (stone == null || stone.Count <= 0)
            {
                // Chưa đặt Đá Tăng Sao!
                client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("EquipGhostHandler.Material2"));
                return 0;
            }

            UserEquipGhostInfo equipGhost = client.Player.GetEquipGhostByCategory(item.Template.CategoryID);
            SpiritInfo spiritInfo = SpiritMgr.GetSpiritInfo(equipGhost);

            int oldLv = equipGhost.Level;
            if (oldLv < 0)
            {
                oldLv = 0;
            } else if(oldLv > 10)
            {
                // Trang bị đã Tăng Sao đến cấp cao nhất!
                client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("EquipGhostHandler.MaxLevel")); ;
                return 0;
            }

            double probability = 0.0;
            double rate = 100.0;

            // calculate equip ghost probability
            if(luck != null)
            {
                rate += (double)luck.Template.Property2;
            }
            // TODO: update this formula using GhostTime and some information in SpiritInfo
            probability = Math.Floor(5.0 * rate * Math.Pow(2, Math.Pow(2, stone.Template.Level - 1) + 2 - oldLv));

            int removeCount = 1;
            bool isSuccess = false;
            int rand = random.Next(10000);

            if (probability > (double)rand)
            {
                isSuccess = true;
                equipGhost.TotalGhost = 0;
                equipGhost.Level += 1;
            } 
            else
            {
                equipGhost.TotalGhost += 1;
            }
            client.Player.UpdateEquipGhost(equipGhost);
            pkg.WriteBoolean(isSuccess);
            client.Player.StoreBag.RemoveCountFromStack(stone, removeCount);
            if(luck != null)
            {
                client.Player.StoreBag.RemoveCountFromStack(luck, removeCount);
            }
            //client.Player.StoreBag.UpdateItem(item);
            //LogMgr.LogItemAdd(client.Player.PlayerCharacter.ID, LogItemType.Strengthen, BeginProperty, item, AddItem, 1);//强化日志                
            client.Out.SendTCP(pkg);
            //if (item.ItemID == 0)
            //    client.Player.StoreBag.SaveToDatabase();
            if (isSuccess && item.ItemID > 0 && equipGhost.Level > 8)
            {
                string msg = LanguageMgr.GetTranslation("EquipGhostHandler.congratulation", client.Player.ZoneName, client.Player.PlayerCharacter.NickName, item.TemplateID, equipGhost.Level);
                GSPacketIn sys_notice = WorldMgr.SendSysNotice(eMessageType.SYS_TIP_NOTICE, msg, item.ItemID, item.TemplateID/*, client.Player.ZoneId*/, null);
                GameServer.Instance.LoginServer.SendPacket(sys_notice);
            }
            
            if (item.Place < 31)
            {
                client.Player.EquipBag.UpdatePlayerProperties();
            }

            return 0;
        }
    }
}
