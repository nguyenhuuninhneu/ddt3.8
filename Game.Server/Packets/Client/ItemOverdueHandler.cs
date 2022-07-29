using Bussiness;
using Game.Base.Packets;
using Game.Server.GameUtils;
using SqlDataProvider.Data;
using System;

namespace Game.Server.Packets.Client
{
    [PacketHandler(77, "物品过期")]
	public class ItemOverdueHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
			if (client.Player.CurrentRoom == null || !client.Player.CurrentRoom.IsPlaying)
			{
				int bagType = packet.ReadByte();
				int place = packet.ReadInt();
				PlayerInventory inventory = client.Player.GetInventory((eBageType)bagType);
				ItemInfo item = inventory.GetItemAt(place);
				if (item != null && !item.IsValidItem())
				{
					if (bagType == 0 && place < 30)
					{
						int slot = inventory.FindFirstEmptySlot();
						//Console.WriteLine(num3);
						if (slot == -1 || !inventory.MoveItem(item.Place, slot, item.Count))
						{
							inventory.RemoveItem(item);
                            client.Player.SendItemToMail(item, LanguageMgr.GetTranslation("ItemOverdueHandler.Content"), LanguageMgr.GetTranslation("ItemOverdueHandler.Title"), eMailType.ItemOverdue);
                            client.Player.Out.SendMailResponse(client.Player.PlayerCharacter.ID, eMailRespose.Receiver);
						}
					}
					else
					{
						inventory.UpdateItem(item);
					}
				}
			}
			return 0;
        }
    }
}
