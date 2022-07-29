using Bussiness;
using Game.Base.Packets;
using Game.Logic;
using Game.Server.Buffer;
using Game.Server.Managers;
using SqlDataProvider.Data;
using System;

namespace Game.Server.Packets.Client
{
    [PacketHandler(99, "场景用户离开")]
    public class TexpHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int num = packet.ReadInt();
            int num2 = packet.ReadInt();
            int slot = packet.ReadInt();
            string text = "HP";
            ItemInfo itemAt = client.Player.StoreBag.GetItemAt(slot);
            TexpInfo texp = client.Player.PlayerCharacter.Texp;
            int oldExp = 0;
            if (itemAt == null || texp == null || itemAt.TemplateID != num2 || client.Player.isPlayerWarrior())
            {
                return 0;
            }
            if (!itemAt.isTexp())
            {
                return 0;
            }
            int num3 = client.Player.PlayerCharacter.Grade;
            if (client.Player.UsePayBuff(BuffType.Train_Good))
            {
                AbstractBuffer ofType = client.Player.BufferList.GetOfType(BuffType.Train_Good);
                num3 += ofType.Info.Value;
            }
            if (texp.texpTaskDate.Date.AddDays(1.0).Date <= DateTime.Now.Date && texp.texpCount >= num3)
            {
                texp.texpCount = 0;
                texp.texpTaskDate = DateTime.Now;
            }
            if (texp.texpCount >= num3)
            {
                client.Out.SendMessage(eMessageType.GM_NOTICE, LanguageMgr.GetTranslation("texpSystem.texpCountToplimit"));
            }
            else
            {
                client.Player.OnUsingItem(num2, 1);
                client.Player.StoreBag.RemoveTemplate(num2, 1);
                switch (num)
                {
                    case 0:
                        oldExp = texp.hpTexpExp;
                        texp.hpTexpExp += itemAt.Template.Property2;
                        client.Player.OnUsingItem(45005, 1);
                        break;
                    case 1:
                        oldExp = texp.attTexpExp;
                        texp.attTexpExp += itemAt.Template.Property2;
                        client.Player.OnUsingItem(45001, 1);
                        text = "Tấn Công";
                        break;
                    case 2:
                        oldExp = texp.defTexpExp;
                        texp.defTexpExp += itemAt.Template.Property2;
                        client.Player.OnUsingItem(45002, 1);
                        text = "Phòng Thủ";
                        break;
                    case 3:
                        oldExp = texp.spdTexpExp;
                        texp.spdTexpExp += itemAt.Template.Property2;
                        client.Player.OnUsingItem(45003, 1);
                        text = "Nhanh Nhẹn";
                        break;
                    case 4:
                        oldExp = texp.lukTexpExp;
                        texp.lukTexpExp += itemAt.Template.Property2;
                        client.Player.OnUsingItem(45004, 1);
                        text = "May Mắn";
                        break;
                }
                texp.texpCount++;
                texp.texpTaskCount++;
                client.Player.PlayerCharacter.Texp = texp;
                using (PlayerBussiness playerBussiness = new PlayerBussiness())
                {
                    playerBussiness.UpdateUserTexpInfo(texp);
                }
                client.Player.EquipBag.UpdatePlayerProperties();

            }
            return 0;
        }
    }
}
