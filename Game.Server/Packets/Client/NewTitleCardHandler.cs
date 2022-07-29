using Bussiness.Managers;
using Game.Base.Packets;
using Game.Server.Managers;
using SqlDataProvider.Data;

namespace Game.Server.Packets.Client
{
    [PacketHandler(265, "客户端日记")]
	public class NewTitleCardHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
			eBageType bagType = (eBageType)packet.ReadByte();
			int place = packet.ReadInt();
			ItemInfo itemAt = client.Player.GetItemAt(bagType, place);
			if (itemAt == null)
			{
				client.Player.SendMessage("Vật phẩm không tồn tại.");
			}
			else
			{
				NewTitleInfo newTitleInfo = NewTitleMgr.FindNewTitle(itemAt.Template.Property1);
				if (newTitleInfo == null)
				{
					client.Player.SendMessage("Danh hiệu chưa mở.");
				}
				else if (client.Player.RemoveCountFromStack(itemAt, 1))
				{
					client.Player.Rank.AddNewRank(newTitleInfo.ID, itemAt.Template.Property2);
					GameServer.Instance.LoginServer.SendPacket(WorldMgr.SendSysNotice($"[{client.Player.ZoneName}] Chúc mừng người chơi [{client.Player.PlayerCharacter.NickName}] Vừa nhận được danh hiệu ~{newTitleInfo.Name}~"));
				}
				else
				{
					client.Player.SendMessage("Xử lý dữ liệu thất bại. Vui lòng thử lại sau.");
				}
			}
			return 0;
        }
    }
}
