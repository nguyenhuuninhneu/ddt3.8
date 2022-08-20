using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.Messions
{
    public class DTGAH1167 : AMissionControl
    {
        private SimpleBoss m_oaitu;

        private SimpleBoss m_trongtai;

        private SimpleNpc m_fan;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_front;

        private PhysicalObj m_decuatrongtai;

        private int IsSay = 0;

        private int m_oaituID = 6331;//oai tu

        private int m_trongtaiID = 6332;//trong tai coi den

        private int m_fanID = 6334;//Fan

        private int m_decuatrongtaiID = 6335;//De cua trong tai

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 900)
            {
                return 3;
            }
            else if (score > 825)
            {
                return 2;
            }
            else if (score > 725)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            Game.AddLoadingFile(1, "bombs/61.swf", "tank.resource.bombs.Bomb61");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.boguquanwangAsset");
            Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "asset.game.six.popcan");
            Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "popcan_fla.qusan_8");
            int[] resources = { m_decuatrongtaiID, m_fanID, m_oaituID, m_trongtaiID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.SetMap(1167);
        }

        public override void OnPrepareStartGame()
        {
            base.OnPrepareStartGame();
            //fan
            LivingConfig fan = Game.BaseLivingConfig();
            fan.IsTurn = false;
            fan.isShowBlood = false;
            fan.IsFly = true;
            fan.isShowSmallMapPoint = false;
            fan.CanTakeDamage = false;
            m_fan = Game.CreateNpc(m_fanID, Game.Map.Info.DeadWidth / 2 - 3, Game.Map.Info.DeadHeight / 2 - 80, 0, -1, fan);
            m_fan.PlayMovie("stand", 0, 0);
            m_fan.OnSmallMap(false);
            Game.SendHideBlood(m_fan, 1);
            //de cua trong tai
            m_decuatrongtai = Game.CreatePhysicalObj(Game.Map.Info.DeadWidth / 2, Game.Map.Info.DeadHeight / 2 + 100, "m_decuatrongtai", "game.living.Living181", "stand", 1, 0);
            m_decuatrongtai.SetRect(-126, -120, 260, 90);
            //trong tai coi den
            LivingConfig trongtai = Game.BaseLivingConfig();
            trongtai.IsFly = true;
            trongtai.IsTurn = false;
            m_trongtai = Game.CreateBoss(m_trongtaiID, m_decuatrongtai.X + 20, m_decuatrongtai.Y - 100, 1, 1, "stand", trongtai);
            m_trongtai.PlayMovie("stand", 0, 0);
            m_trongtai.SetRect(m_trongtai.NpcInfo.X, m_trongtai.NpcInfo.Y, m_trongtai.NpcInfo.Width, m_trongtai.NpcInfo.Height);
            m_trongtai.SetRelateDemagemRect(m_trongtai.NpcInfo.X, m_trongtai.NpcInfo.Y, m_trongtai.NpcInfo.Width, m_trongtai.NpcInfo.Height);
            m_trongtai.Properties1 = 1;
            m_trongtai.Properties2 = 0;
        }
        public override void OnStartGame()
        {
            base.OnStartGame();
            m_trongtai.Say("Trong kỳ vận hội nây chúng ta sẽ được chiêm ngưỡng sức mạnh của Quyền Vương và 4 đấu sĩ vô danh!", 1500, 0);

            //oai tu
            LivingConfig config = Game.BaseLivingConfig();
            config.isShowBlood = false;
            config.KeepLife = true;
            m_oaitu = Game.CreateBoss(m_oaituID, Game.Map.Info.DeadWidth / 2 + 150, 975, -1, 6, "", config);
            m_oaitu.SetRelateDemagemRect(m_oaitu.NpcInfo.X, m_oaitu.NpcInfo.Y, m_oaitu.NpcInfo.Width, m_oaitu.NpcInfo.Height);
            m_oaitu.Properties2 = false;
            Game.SendFreeFocus(m_trongtai.X, m_trongtai.Y - 200, 0, 0, 2000);
            Game.SendFreeFocus(m_oaitu.X, m_oaitu.Y, 0, 6000, 0);
            m_trongtai.CallFuction(Layer, 6500);
        }
        private void Layer()
        {
            m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_front = Game.Createlayer(m_oaitu.X - 50, m_oaitu.Y - 150, "font", "game.asset.living.boguquanwangAsset", "out", 1, 1);
            m_kingMoive.PlayMovie("in", 1000, 0);
            m_front.PlayMovie("in", 2000, 2000);

            foreach (Player p in Game.GetAllFightPlayers())
            {
                p.Properties1 = 0;
            }
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (m_kingMoive != null)
            {
                Game.RemovePhysicalObj(m_kingMoive, true);
                m_kingMoive = null;
            }
            if (m_front != null)
            {
                Game.RemovePhysicalObj(m_front, true);
                m_front = null;
            }
            IsSay = 0;
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();

            if (Game.TurnIndex > Game.MissionInfo.TotalTurn - 1)
            {
                return true;
            }

            if (m_oaitu.IsLiving == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int UpdateUIData()
        {

            if (m_oaitu == null)
                return 0;

            if (m_oaitu.IsLiving == false)
            {
                return 1;
            }
            return base.UpdateUIData();
        }

        public override void OnGameOver()
        {
            base.OnGameOver();

            if (m_oaitu.IsLiving == false)
            {
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }
        }
    }
}
