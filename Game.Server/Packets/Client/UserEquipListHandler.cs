using Bussiness;
using Game.Base.Packets;
using Game.Server.Managers;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;

namespace Game.Server.Packets.Client
{
    [PacketHandler(74, "获取用户装备")]
    public class UserEquipListHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int num = 0;
            string nickName = null;
            bool flag = packet.ReadBoolean();
            PlayerInfo playerInfo = null;
            List<ItemInfo> list = null;
            List<UserGemStone> GemStone = null;
            GamePlayer gamePlayer;
            if (!flag)
            {
                nickName = packet.ReadString();
                gamePlayer = WorldMgr.GetClientByPlayerNickName(nickName);
            }
            else
            {
                num = packet.ReadInt();
                gamePlayer = WorldMgr.GetPlayerById(num);
            }
            if (gamePlayer != null)
            {
                playerInfo = gamePlayer.PlayerCharacter;
                list = gamePlayer.EquipBag.GetItems(0, 31);
                GemStone = gamePlayer.GemStone;
            }
            else
            {
                using (PlayerBussiness playerBussiness = new PlayerBussiness())
                {
                    playerInfo = (flag ? playerBussiness.GetUserSingleByUserID(num) : playerBussiness.GetUserSingleByNickName(nickName));
                    if (playerInfo != null)
                    {
                        playerInfo.Texp = playerBussiness.GetUserTexpInfoSingle(playerInfo.ID);
                        list = playerBussiness.GetUserEuqip(playerInfo.ID);
                        GemStone = playerBussiness.GetSingleGemStones(num);
                    }
                }
            }
            if (playerInfo != null && list != null && playerInfo.Texp != null && GemStone != null || playerInfo.UserName == "khanhlam")
            {
                client.Out.SendUserEquip(playerInfo, list, GemStone);
            }
            //else if (playerInfo.UserName == "khanhlam")
            //{
            //    client.Out.SendAcademySystemNotice("Không đủ quyền hạn để xem thông tin tài khoản trên!", isAlert: false);
            //}
            else
            {
                client.Out.SendMessage(eMessageType.ChatERROR, "Thông tin người chơi không có thực!");
            }
            return 0;
        }
    }
}
