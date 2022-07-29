using Bussiness;
using Bussiness.Managers;
using Game.Base.Packets;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;

namespace Game.Server.Packets.Client
{
    [PacketHandler(256, "LEAGUE_GETAWARD")]
    public class DailyLeagueGetRewardHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int num = packet.ReadInt();
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            if (DateTime.Compare(client.Player.LastOpenCard.AddSeconds(0.5), DateTime.Now) > 0)
            {
                return 0;
            }
            string translateId = "DailyLeagueGetReward.Successfull";
            ProduceBussiness produceBussiness = new ProduceBussiness();
            DailyLeagueAwardList[] allDailyLeagueAwardList = produceBussiness.GetAllDailyLeagueAwardList();
            DailyLeagueAwardItems[] allDailyLeagueAwardItems = produceBussiness.GetAllDailyLeagueAwardItems();
            List<ItemInfo> list = new List<ItemInfo>();
            DailyLeagueAwardList[] array = allDailyLeagueAwardList;
            DailyLeagueAwardList[] array2 = array;
            foreach (DailyLeagueAwardList dailyLeagueAwardList in array2)
            {
                if (dailyLeagueAwardList.Class == num)
                {
                    num3 = dailyLeagueAwardList.Score;
                    num4 = dailyLeagueAwardList.Grade;
                    switch (num3)
                    {
                        case 150:
                            num2 = 1;
                            break;
                        case 200:
                            num2 = 3;
                            break;
                        case 250:
                            num2 = 7;
                            break;
                        case 300:
                            num2 = 15;
                            break;
                        case 400:
                            num2 = 31;
                            break;
                        case 550:
                            num2 = 63;
                            break;
                        case 700:
                            num2 = 127;
                            break;
                        case 850:
                            num2 = 255;
                            break;
                        case 900:
                            num2 = 511;
                            break;
                        default:
                            num2 = 1023;
                            translateId = "DailyLeagueGetReward.Error";
                            break;
                    }
                }
            }
            num2 = (1 << num) - 1;
            DailyLeagueAwardItems[] array3 = allDailyLeagueAwardItems;
            DailyLeagueAwardItems[] array4 = array3;
            foreach (DailyLeagueAwardItems dailyLeagueAwardItems in array4)
            {
                if (dailyLeagueAwardItems.Class == num)
                {
                    ItemInfo itemInfo = ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(dailyLeagueAwardItems.TemplateID), 1, 104);
                    itemInfo.StrengthenLevel = dailyLeagueAwardItems.StrengthLevel;
                    itemInfo.AttackCompose = dailyLeagueAwardItems.AttackCompose;
                    itemInfo.DefendCompose = dailyLeagueAwardItems.DefendCompose;
                    itemInfo.AgilityCompose = dailyLeagueAwardItems.AgilityCompose;
                    itemInfo.LuckCompose = dailyLeagueAwardItems.LuckCompose;
                    itemInfo.IsBinds = dailyLeagueAwardItems.IsBind;
                    itemInfo.ValidDate = dailyLeagueAwardItems.ValidDate;
                    itemInfo.Count = dailyLeagueAwardItems.Count;
                    list.Add(itemInfo);
                }
            }
            if (list != null)
            {
                if (client.Player.PlayerCharacter.Grade >= num4)
                {
                    if (client.Player.MatchInfo.weeklyScore >= num3)
                    {
                        client.Player.RefreshLeagueGetReward(num2, num3);
                        client.Player.SendItemsToMail(list, LanguageMgr.GetTranslation("DailyLeagueGetReward.Content"), LanguageMgr.GetTranslation("Game.Server.LeagueReward.Title"), eMailType.Manage);
                        return 1;
                    }
                    translateId = "Bạn không có đủ điểm hàng tuần để mua.";
                    return 0;
                }
                translateId = "Cấp độ không đủ để nhận";
                return 0;
            }
            client.Player.Out.SendMessage(eMessageType.GM_NOTICE, LanguageMgr.GetTranslation(translateId));
            client.Player.LastOpenCard = DateTime.Now;
            return 1;
        }
    }
}
