using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
    public class NewTrainingBoss25002 : ABrain
    {
        protected Player m_targer;

        private static Random random = new Random();

        private static string[] listChat = new string[3]
		{
			"Ý! Bị đánh đến vầy sao！",
			"Chỉ là một đứa nhóc mà mạnh vậy sao?!",
			"Ta đến báo thù đây, cẩn thận！"
		};

        public override void OnBeginSelfTurn()
        {
			base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
			base.OnBeginNewTurn();
			m_body.CurrentDamagePlus = 1f;
			m_body.CurrentShootMinus = 1f;
			if (m_body.IsSay)
			{
				string oneChat = GetOneChat();
				int delay = base.Game.Random.Next(0, 5000);
				m_body.Say(oneChat, 0, delay);
			}
        }

        public override void OnCreated()
        {
			base.OnCreated();
        }

        public override void OnStartAttacking()
        {
			base.OnStartAttacking();
			m_targer = base.Game.FindNearestPlayer(base.Body.X, base.Body.Y);
			Beating();
        }

        public override void OnStopAttacking()
        {
			base.OnStopAttacking();
        }

        public void MoveToPlayer(Player player)
        {
			int num = (int)player.Distance(base.Body.X, base.Body.Y);
			int num2 = base.Game.Random.Next(((SimpleBoss)base.Body).NpcInfo.MoveMin, ((SimpleBoss)base.Body).NpcInfo.MoveMax);
			if (num <= 97)
			{
				return;
			}
			num = ((num <= ((SimpleBoss)base.Body).NpcInfo.MoveMax) ? (num - 90) : num2);
			if (player.Y < 420 && player.X < 210)
			{
				if (base.Body.Y > 420)
				{
					base.Body.MoveTo(base.Body.X - num, base.Body.Y, "walk", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1200, MoveBeat);
				}
				else if (player.X > base.Body.X)
				{
					base.Body.MoveTo(base.Body.X + num, base.Body.Y, "walk", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1200, MoveBeat);
				}
				else
				{
					base.Body.MoveTo(base.Body.X - num, base.Body.Y, "walk", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1200, MoveBeat);
				}
			}
			else if (base.Body.Y < 420)
			{
				if (base.Body.X + num > 200)
				{
					base.Body.MoveTo(200, base.Body.Y, "walk", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1200, Fall);
				}
			}
			else if (player.X > base.Body.X)
			{
				base.Body.MoveTo(base.Body.X + num, base.Body.Y, "walk", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1200, MoveBeat);
			}
			else
			{
				base.Body.MoveTo(base.Body.X - num, base.Body.Y, "walk", "", ((SimpleBoss)base.Body).NpcInfo.speed, 1200, MoveBeat);
			}
        }

        public void MoveBeat()
        {
			base.Body.Beat(m_targer, "beatA", 100, 0, 200, 1, 1);
        }

        public void Beating()
        {
			if (m_targer != null && !base.Body.Beat(m_targer, "beatA", 100, 0, 200, 1, 1))
			{
				MoveToPlayer(m_targer);
			}
        }

        public void Fall()
        {
			base.Body.FallFrom(base.Body.X, base.Body.Y + 240, null, 200, 0, 12, Beating);
        }

        public static string GetOneChat()
        {
			int num = random.Next(0, listChat.Length);
			return listChat[num];
        }

        public static void LivingSay(List<Living> livings)
        {
			if (livings == null || livings.Count == 0)
			{
				return;
			}
			int num = 0;
			int count = livings.Count;
			foreach (Living living in livings)
			{
				living.IsSay = false;
			}
			num = ((count <= 5) ? random.Next(0, 2) : ((count <= 5 || count > 10) ? random.Next(1, 4) : random.Next(1, 3)));
			if (num <= 0)
			{
				return;
			}
			int[] array = new int[num];
			int num2 = 0;
			while (num2 < num)
			{
				int index = random.Next(0, count);
				if (!livings[index].IsSay)
				{
					livings[index].IsSay = true;
					int delay = random.Next(0, 5000);
					livings[index].Say(SimpleNpcAi.GetOneChat(), 0, delay);
					num2++;
				}
			}
        }
    }
}
