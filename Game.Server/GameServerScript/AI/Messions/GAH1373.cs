using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.Messions
{
    public class GAH1373 : AMissionControl
    {
        private List<PhysicalObj> m_bord = new List<PhysicalObj>();

        private List<PhysicalObj> m_key = new List<PhysicalObj>();

        private PhysicalObj m_door = null;

        private string KeyIndex = null;

        private int m_count = 0;

        public override int CalculateScoreGrade(int score)
        {
			base.CalculateScoreGrade(score);
			if (score > 900)
			{
				return 3;
			}
			if (score > 825)
			{
				return 2;
			}
			if (score > 725)
			{
				return 1;
			}
			return 0;
        }

        public override void OnPrepareNewSession()
        {
			base.OnPrepareNewSession();
			base.Game.AddLoadingFile(2, "image/map/1075/objects/1075Object.swf", "game.crazytank.assetmap.Board001");
			base.Game.AddLoadingFile(2, "image/map/1075/objects/1075Object.swf", "game.crazytank.assetmap.CrystalDoor001");
			base.Game.AddLoadingFile(2, "image/map/1075/objects/1075Object.swf", "game.crazytank.assetmap.Key");
			base.Game.SetMap(1075);
        }

        public override void OnStartGame()
        {
			base.OnStartGame();
			base.Game.TotalCount = base.Game.PlayerCount;
			base.Game.TotalTurn = base.Game.PlayerCount * 4;
			base.Game.SendMissionInfo();
			m_bord.Add(base.Game.CreatePhysicalObj(76, 167, "board1", "game.crazytank.assetmap.Board001", "1", 1, 336));
			m_bord.Add(base.Game.CreatePhysicalObj(402, 159, "board2", "game.crazytank.assetmap.Board001", "1", 1, 23));
			m_bord.Add(base.Game.CreatePhysicalObj(699, 156, "board3", "game.crazytank.assetmap.Board001", "1", 1, 350));
			m_bord.Add(base.Game.CreatePhysicalObj(959, 148, "board4", "game.crazytank.assetmap.Board001", "1", 1, 325));
			m_bord.Add(base.Game.CreatePhysicalObj(177, 261, "board5", "game.crazytank.assetmap.Board001", "1", 1, 22));
			m_bord.Add(base.Game.CreatePhysicalObj(514, 277, "board6", "game.crazytank.assetmap.Board001", "1", 1, 336));
			m_bord.Add(base.Game.CreatePhysicalObj(782, 285, "board7", "game.crazytank.assetmap.Board001", "1", 1, 23));
			m_bord.Add(base.Game.CreatePhysicalObj(1061, 280, "board8", "game.crazytank.assetmap.Board001", "1", 1, 22));
			m_bord.Add(base.Game.CreatePhysicalObj(273, 406, "board9", "game.crazytank.assetmap.Board001", "1", 1, 350));
			m_bord.Add(base.Game.CreatePhysicalObj(620, 408, "board10", "game.crazytank.assetmap.Board001", "1", 1, 23));
			m_bord.Add(base.Game.CreatePhysicalObj(873, 414, "board11", "game.crazytank.assetmap.Board001", "1", 1, 336));
			m_bord.Add(base.Game.CreatePhysicalObj(1155, 428, "board12", "game.crazytank.assetmap.Board001", "1", 1, 336));
			m_door = base.Game.CreatePhysicalObj(1275, 556, "door", "game.crazytank.assetmap.CrystalDoor001", "start", 1, 0);
			int[] array = new int[12]
			{
				12,
				12,
				12,
				12,
				12,
				12,
				12,
				12,
				12,
				12,
				12,
				12
			};
			for (int i = 0; i < base.Game.TotalCount; i++)
			{
				int num = base.Game.Random.Next(0, 12);
				if (array[num] == num)
				{
					i--;
					continue;
				}
				array[num] = num;
				m_bord.ToArray()[num].PlayMovie("2", 0, 0);
				KeyIndex = $"Key{num}";
				m_key.Add(base.Game.CreatePhysicalObj(m_bord.ToArray()[num].X, m_bord.ToArray()[num].Y - 8, KeyIndex, "game.crazytank.assetmap.Key", "1", 1, 0));
				base.Game.SendGameObjectFocus(1, m_bord.ToArray()[num].Name, 0, 0);
			}
			base.Game.SendGameObjectFocus(1, "door", 1000, 0);
			List<LoadingFileInfo> list = new List<LoadingFileInfo>();
			list.Add(new LoadingFileInfo(2, "sound/Sound201.swf", "Sound201"));
			list.Add(new LoadingFileInfo(2, "sound/Sound202.swf", "Sound202"));
			base.Game.SendLoadResource(list);
			base.Game.GameOverResources.Add("game.crazytank.assetmap.CrystalDoor001");
        }

        public override void OnNewTurnStarted()
        {
			base.OnNewTurnStarted();
			if (base.Game.CurrentLiving != null)
			{
				//((Player)base.Game.CurrentLiving).Seal((Player)base.Game.CurrentLiving, 0, 0);
			}
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			if (base.Game.CurrentLiving != null)
			{
				((Player)base.Game.CurrentLiving).SetBall(3);
			}
        }

        public override bool CanGameOver()
        {
			for (int i = 0; i < 12; i++)
			{
				foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
				{
					if (allFightPlayer.X > m_bord[i].X - 40 && allFightPlayer.X < m_bord[i].X + 40 && allFightPlayer.Y < m_bord[i].Y && allFightPlayer.Y > m_bord[i].Y - 40 && m_bord[i].CurrentAction == "2")
					{
						m_bord[i].PlayMovie("3", 0, 0);
						KeyIndex = $"Key{i}";
						base.Game.RemovePhysicalObj(base.Game.FindPhysicalObjByName(KeyIndex)[0], sendToClient: true);
						m_count++;
					}
				}
			}
			if (m_count == base.Game.TotalCount)
			{
				base.Game.SendGameObjectFocus(2, "door", 0, 6000);
				base.Game.SendPlaySound("201");
				m_door.PlayMovie("end", 4000, 3000);
				base.Game.SendPlaySound("202");
				base.Game.SendUpdateUiData();
				base.Game.TurnQueue.Clear();
			}
			if (base.Game.TurnIndex > base.Game.TotalTurn - 1 && m_count != base.Game.TotalCount)
			{
				return true;
			}
			if (m_door.CurrentAction == "end")
			{
				return true;
			}
			return false;
        }

        public override int UpdateUIData()
        {
			return m_count;
        }

        public override void OnGameOver()
        {
			base.OnGameOver();
			if (m_door.CurrentAction == "end")
			{
                foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
                {
                    allFightPlayer.SetSeal(state: false);
                }
                base.Game.AddAllPlayerToTurn();
				base.Game.IsWin = true;
			}
			else
			{
				base.Game.IsWin = false;
			}
        }
    }
}
