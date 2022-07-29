using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class SixHardThirdNpc : ABrain
    {
        private int m_attackTurn = 0;

        private int m_x = 0;

        private int m_y = 0;

        private PhysicalObj m_popcan;

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;

        }
        public override void OnCreated()
        {
            base.OnCreated();
        }
        private void SetState()
        {
            ((PVEGame)Game).SendLivingActionMapping(Body, "standA", "stand");
            ((PVEGame)Game).SendLivingActionMapping(Body, "standB", "stand");
            ((PVEGame)Game).SendLivingActionMapping(Body, "cryA", "cry");
            ((PVEGame)Game).SendLivingActionMapping(Body, "shield", "cry");
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            StopDirtyPlay();
        }
        private void CreateEffectQusan()
        {
            m_popcan = ((PVEGame)Game).Createlayer(m_x - 150, m_y + 120, "blue", "popcan_fla.qusan_8", "", 1, 0);
            Body.CallFuction(RemoveEffect, 1200);
        }
        private void RemoveEffect()
        {
            if (m_popcan != null)
            {
                Game.RemovePhysicalObj(m_popcan, true);
            }
        }
        private void StopDirtyPlay()
        {
            foreach (SimpleBoss oaitu in Game.FindLivingTurnBossWithID(6131))
            {
                if (oaitu.Properties1 == 1)
                {
                    Body.Say("Sao mặt đỏ thế? Dùng chất kích thích à!", 0, 0);
                    Body.PlayMovie("beat", 1000, 0);
                    ((PVEGame)Game).SendGameFocus(oaitu, 3500, 0);
                    m_x = oaitu.X;
                    m_y = oaitu.Y;
                    oaitu.Properties1 = 0;
                    oaitu.IconPicture(eMirariType.Guard, false);
                    oaitu.IconPicture(eMirariType.Damage, false);
                    oaitu.Attack = oaitu.NpcInfo.Attack;
                    oaitu.Defence = oaitu.NpcInfo.Defence;
                    Body.CallFuction(CreateEffectQusan, 4000);
                }
            }
        }
        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
        public override void OnAfterTakeDamage(Living living)
        {
            if (living is Player)
            {
                Player player = (Player)living;
                if (player.ShootCount <= 0)
                {
                    Body.Properties2 = 1;
                    Body.Properties1 = 1;
                    Body.BlockTurn = false;
                    Body.SyncAtTime = true;
                    ((PVEGame)Game).SendFreeFocus(Body.X, Body.Y - 100, 0, 3000, 0);
                    switch ((int)player.Properties1)
                    {
                        case 0:
                            player.Properties1 = 1;
                            Body.Say("<p class=\"red\">" + player.PlayerDetail.PlayerCharacter.NickName + "</p> đánh trọng tài là không được nha...", 0, 3500, 0);
                            break;
                        case 1:
                            player.Properties1 = 2;
                            Body.Say("<p class=\"red\">" + player.PlayerDetail.PlayerCharacter.NickName + "</p> đánh nữa ta đuổi cổ ra đó!", 0, 3500, 0);
                            break;
                        case 2:
                            player.Properties1 = 3;
                            Body.Say("<p class=\"red\">" + player.PlayerDetail.PlayerCharacter.NickName + "</p> chết này!", 0, 3500, 0);
                            ((PVEGame)Game).SendGameFocus(player, 4500, 0);
                            player.Die(5000);
                            break;
                    }
                }
            }
        }
    }
}
