using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
    public class DTGT1168 : AMissionControl
    {
        private SimpleBoss m_trongtai;

        private SimpleNpc m_fan;

        private PhysicalObj m_kingMoive;

        private PhysicalObj m_front;

        private int IsSay = 0;

        private int m_trongtaiID = 6141;//trong tai coi den

        private int m_khienID = 6144;//khien cuatrong tai coi den

        private int m_fanID = 6134;//Fan

        private static string[] m_shootedChat = new string[]
        {
            "Ây da!",
            "Ai cho phép mang vũ khí vào đâyy!"
        };

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
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.bogucaipanAsset");
            Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "asset.game.six.popcan");
            Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "asset.game.six.aup");
            Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "asset.game.six.pup");
            Game.AddLoadingFile(2, "image/game/effect/6/redcircle.swf", "asset.game.six.redcircle");
            Game.AddLoadingFile(2, "image/game/effect/6/bluecircle.swf", "asset.game.six.bluecircle");
            Game.AddLoadingFile(2, "image/game/effect/6/greencircle.swf", "asset.game.six.greencircle");
            Game.AddLoadingFile(2, "image/game/effect/6/chang.swf", "asset.game.six.chang");
            Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "popcan_fla.qusan_8");
            int[] resources = { m_fanID, m_trongtaiID, m_khienID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.SetMap(1168);
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
            m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "", 1, 1);
            m_front = Game.Createlayer(1190, 770, "font", "game.asset.living.bogucaipanAsset", "", 1, 1);
            //trong tai coi den
            LivingConfig trongtai = Game.BaseLivingConfig();
            trongtai.isShowBlood = false;
            m_trongtai = Game.CreateBoss(m_trongtaiID, 1270, 975, 1, 1, "", trongtai);
            m_trongtai.PlayMovie("shengqi", 0, 0);
            m_trongtai.DoAction = 2;
            Game.SendHideBlood(m_trongtai, 0);
        }
        public override void OnStartGame()
        {
            base.OnStartGame();
            m_trongtai.PlayMovie("xialai", 1500, 0);
            m_trongtai.Properties1 = 1;
            m_trongtai.CallFuction(Layer, 6000);
        }
        private void Layer()
        {
            m_kingMoive.PlayMovie("out", 0, 0);
            m_front.PlayMovie("out", 0, 0);
            m_kingMoive.PlayMovie("in", 1000, 0);
            m_front.PlayMovie("in", 2000, 2000);

        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Game.SendHideBlood(m_fan, 0);

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

            if (m_trongtai.IsLiving == false)
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

            if (m_trongtai == null)
                return 0;

            if (m_trongtai.IsLiving == false)
            {
                return 1;
            }
            return base.UpdateUIData();
        }

        public override void OnGameOver()
        {
            base.OnGameOver();

            if (m_trongtai.IsLiving == false)
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
