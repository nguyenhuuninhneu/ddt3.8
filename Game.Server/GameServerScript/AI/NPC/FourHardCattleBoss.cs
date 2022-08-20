//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using Bussiness;
//using Game.Logic;
//using Game.Logic.AI;
//using Game.Logic.Effects;
//using Game.Logic.Phy.Object;

//namespace GameServerScript.AI.NPC
//{
//    public class FourHardCattleBoss : ABrain
//    {
//        private object Locker = new object();
//        private int _mAttackTurn;

//        private PhysicalObj _mEffectAttack;

//        private PhysicalObj _mPowerUpEffect;

//        private readonly int m_totalNpc = 4;

//        private readonly int npcId = 4207;

//        private readonly int npcId2 = 4210;

//        private readonly float m_perPowerUp = 1f;

//        private readonly int m_reduceStreng = 60;

//        private bool _isFear;

//        private Player _target;

//        private float _lastpowDamage;

//        private float _mCurrentPowDamage;

//        private bool _isSay;

//        private bool _canSacriflce;

//        private int _sacriflceFlame;

//        private readonly List<Point> _flamePoints = new List<Point>()
//        {
//            new Point(-100, -100), new Point(100, -100), new Point(0, -200), new Point(0, 0)
//        };

//        private readonly List<SimpleNpc> _flames = new List<SimpleNpc>();

//        private readonly string[] _callNpcSay =
//        {
//            "Lửa địa ngục, hãy bùng cháy!!!!",
//            "Ma trơi nè."
//        };

//        private readonly string[] _killAttackSay =
//        {
//            "Để ta tiễn ngươi!",
//            "Chết chắc rồi cưng à!",
//            "Ngươi không chết mới là lạ.",
//            "Ta sẽ ban tặng ngươi cái chết êm ái"
//        };

//        private readonly string[] _personAttack =
//        {
//            "Tên ranh kia hãy đỡ này!!!"
//        };

//        private readonly string[] _allAttackPlayerSay =
//        {
//            "Các ngươi đỡ được không?",
//            "Chết chắc rồi bọn ngu si!",
//            "Sao mà đỡ lại được đây?",
//            "Sao? Sao? Chết đi!!!!!"
//        };

//        private readonly string[] _tiredSay =
//        {
//            "Hơ hơ... Ta bị ốm à??"
//        };

//        private readonly string[] _fearSay =
//        {
//            "Mệt rồi!",
//            "Ta cảm thấy mệt quá!",
//            "Mệt quá! Nghỉ tí đã."
//        };

//        private readonly string[] _onShootedChat =
//        {
//           ""
//        };

//        private readonly string[] _specialAttackSay =
//        {
//            "Xem ngươi chạy đâu cho thoát",
//            "Dám đánh nén ta à?",
//            "Để xem các ngươi chạy đâu",
//            "Đánh nén ta là điều không thể tha thứ"
//        };

//        private readonly string[] _diedChat =
//        {
//            "Hãy đợi đấy....."
//        };


//        public override void OnBeginNewTurn()
//        {
//            base.OnBeginNewTurn();

//            ClearEffect();
//            Body.CurrentShootMinus = 1;
//            _isSay = false;
//            if (_canSacriflce)
//                _canSacriflce = false;
//            if (_sacriflceFlame > 0)
//                _sacriflceFlame = 0;

//            Body.Config.HaveShield = _isFear == false;
//        }

//        public override void OnCreated()
//        {
//            base.OnCreated();
//            _isFear = false;
//            Body.CurrentDamagePlus = 1;
//            _mCurrentPowDamage = 1;
//        }

//        public override void OnStartAttacking()
//        {
//            // check near player
//            if (!_isFear)
//            {
//                var result = false;

//                foreach (var player in Game.GetAllFightPlayers())
//                    if (player.IsLiving && player.X > Body.X - 200 && player.X < Body.X + 200)
//                        result = true;

//                if (result)
//                {
//                    KillAttack(Body.X - 200, Body.X + 200);
//                    return;
//                }
//            }

//            int index;
//            switch (_mAttackTurn)
//            {
//                case 0:
//                    {
//                        if (!_isFear)
//                        {
//                            // up power and call npc
//                            Body.CallFuction(PowerUpEffect, 2000);
//                            if (((SimpleBoss)Body).CurrentLivingNpcNum <= 0)
//                            {
//                                index = Game.Random.Next(0, _callNpcSay.Length);
//                                Body.Say(_callNpcSay[index], 1, 5000);
//                                Body.CallFuction(CallNpc, 7000);
//                            }
//                            else
//                            {
//                                Body.CallFuction(AttackAllPlayer, 4000);
//                            }
//                        }

//                        _mAttackTurn++;
//                        break;
//                    }
//                case 1:
//                    {
//                        if (!_isFear)
//                        {
//                            // up power and attack near player
//                            Body.CallFuction(PowerUpEffect, 2000);
//                            Body.CallFuction(AttackPerson, 4000);
//                        }

//                        _mAttackTurn++;
//                        break;
//                    }
//                case 2:
//                    {
//                        if (!_isFear)
//                        {
//                            // up power and attack all player
//                            Body.CallFuction(PowerUpEffect, 2000);
//                            Body.CallFuction(AttackAllPlayer, 4000);
//                        }
//                        else
//                        {
//                            goto case 3;
//                        }

//                        _mAttackTurn++;
//                        break;
//                    }
//                case 3 when !_isFear:
//                    {
//                        Body.CallFuction(PowerUpEffect, 2000);
//                        index = Game.Random.Next(0, _tiredSay.Length);
//                        Body.Say(_tiredSay[index], 1, 4000);
//                        if (((SimpleBoss)Body).CurrentLivingNpcNum <= 0)
//                        {
//                            _isFear = true;
//                            Body.CallFuction(ChangeAtoB, 4000);
//                            _mAttackTurn = 0;
//                        }
//                        else
//                        {
//                            lock (Locker)
//                            {
//                                _flames.AddRange((Body as SimpleBoss).FindChildLivings());
//                                Body.CallFuction(GetReadySacriflce, 6000);
//                                _mAttackTurn++;
//                            }

//                        }

//                        break;
//                    }
//                case 3:
//                    // wake up
//                    _isFear = false;
//                    _mCurrentPowDamage = 1;
//                    Body.CallFuction(ChangeBtoA, 2000);
//                    _mAttackTurn = 0;
//                    break;
//                case 4:
//                    _mCurrentPowDamage = 1;
//                    index = Game.Random.Next(0, _specialAttackSay.Length);
//                    Body.Say(_specialAttackSay[index], 1, 2000);
//                    Body.CallFuction(JumpAndAttack, 2000);
//                    _mAttackTurn = 0;
//                    break;
//            }
//        }

//        private void GetReadySacriflce()
//        {
//            var flamePoint = 0;
//            foreach (SimpleNpc item in _flames)
//            {
//                (Game as PVEGame)?.SendHideBlood(item, 1);
//            }
//            foreach (SimpleNpc item in Game.GetLivedNpcs(npcId2))
//            {
//                item.PlayMovie("die", 0, 0);
//                item.Die(1000);
//            }
//            foreach (var flame in _flames)
//            {
//                flame.MoveTo(Body.X, Body.Y - 100, "fly", 1500, "", 15, FlameSacriflce);
//            }
//        }

//        private void FlameSacriflce()
//        {
//            _sacriflceFlame++;
//            if (_sacriflceFlame == _flames.Count)
//                _canSacriflce = true;
//            if (_canSacriflce)
//            {
//                foreach (var flame in _flames)
//                {
//                    flame.PlayMovie("die", 2000, 1000);
//                    flame.Die(3000);
//                }

//                Body.PlayMovie("beatC", 1000, 0);
//                Body.CallFuction(() => AddBlood(10000), 4500);
//                _flames.Clear();
//                Body.Say("Aaaaaaaa........ Đã quá!!!", 1, 5000);
//            }
//        }

//        private void AddBlood(int blood)
//        {
//            Body.AddBlood(blood);
//        }

//        private void KillAttack(int fx, int tx)
//        {
//            _lastpowDamage = Body.CurrentDamagePlus;

//            Body.CurrentDamagePlus = 1000f;

//            Body.ChangeDirection(Game.FindlivingbyDir(Body), 100);

//            var index = Game.Random.Next(0, _killAttackSay.Length);
//            Body.Say(_killAttackSay[index], 1, 1000);
//            Body.PlayMovie("beatC", 2000, 0); //3s
//            Body.PlayMovie("beatE", 5000, 0);
//            Body.RangeAttacking(fx, tx, "cry", 7000, null);
//            Body.CallFuction(SetState, 8000);
//        }

//        private void AttackPerson()
//        {
//            _target = Game.FindNearestPlayer(Body.X, Body.Y);
//            if (_target != null)
//            {
//                Body.ChangeDirection(_target, 100);
//                var index = Game.Random.Next(0, _personAttack.Length);
//                Body.Say(_personAttack[index], 1, 1000);
//                Body.PlayMovie("beatA", 1200, 0);

//                ((PVEGame)Game).SendObjectFocus(_target, 1, 3200, 0);
//                Body.CallFuction(CreateAttackEffect, 4000);
//                if (Body.FindDirection(_target) == -1)
//                    Body.RangeAttacking(_target.X - 50, Body.X, "cry", 4800, null);
//                else
//                    Body.RangeAttacking(Body.X, _target.X + 50, "cry", 4800, null);
//                Body.CallFuction(SetState, 6000);
//            }
//        }

//        private void AttackAllPlayer()
//        {
//            var index = Game.Random.Next(0, _allAttackPlayerSay.Length);
//            Body.Say(_allAttackPlayerSay[index], 1, 1000);
//            Body.PlayMovie("beatB", 1000, 0);
//            Body.RangeAttacking(Body.X - 10000, Body.X + 10000, "cry", 4100, null);
//            foreach (var p in Game.GetAllLivingPlayers())
//                p.AddEffect(new ReduceStrengthEffect(1, m_reduceStreng), 4200);
//            Body.CallFuction(SetState, 5000);
//        }

//        private void ChangeAtoB()
//        {
//            Body.PlayMovie("beatD", 1000, 0);
//            var index = Game.Random.Next(0, _fearSay.Length);
//            Body.Say(_fearSay[index], 1, 2000);
//            Body.PlayMovie("AtoB", 4000, 0);
//            Body.CallFuction(SetState, 7000);
//        }

//        private void ChangeBtoA()
//        {
//            Body.PlayMovie("BtoA", 1000, 0);
//            var index = Game.Random.Next(0, _specialAttackSay.Length);
//            Body.Say(_specialAttackSay[index], 1, 2000);
//            Body.CallFuction(JumpAndAttack, 3000);
//        }

//        private void JumpAndAttack()
//        {
//            _target = Game.FindRandomPlayer();

//            if (_target == null) return;
//            Body.PlayMovie("jump", 500, 0);

//            ((PVEGame)Game).SendObjectFocus(_target, 1, 2000, 0);

//            Body.BoltMove(_target.X, _target.Y, 2500);

//            Body.PlayMovie("fall", 2600, 0);

//            Body.RangeAttacking(_target.X - 100, _target.X + 100, "cry", 3000, null);

//            Body.CallFuction(SetState, 4000);
//        }

//        private void PowerUpEffect()
//        {
//            _mCurrentPowDamage += m_perPowerUp;
//            Body.CurrentDamagePlus = _mCurrentPowDamage;
//            // power up
//            _mPowerUpEffect =
//                ((PVEGame)Game).Createlayer(Body.X, Body.Y - 60, "", "game.crazytank.assetmap.Buff_powup", "", 1, 0);
//        }

//        private void CreateAttackEffect()
//        {
//            if (_target != null)
//                _mEffectAttack = ((PVEGame)Game).Createlayer(_target.X, _target.Y, "", "asset.game.4.blade", "", 1, 0);
//        }

//        private void ClearEffect()
//        {
//            if (_mPowerUpEffect != null)
//                Game.RemovePhysicalObj(_mPowerUpEffect, true);

//            if (_mEffectAttack != null)
//                Game.RemovePhysicalObj(_mEffectAttack, true);
//        }

//        private void CallNpc()
//        {
//            ((SimpleBoss)Body).ClearDiedLiving();
//            var config = ((PVEGame)Game).BaseLivingConfig();
//            config.IsFly = true;
//            config.HaveShield = true;
//            config.CanCountKill = false;

//            for (var i = 0; i < m_totalNpc; i++)
//            {
//                lock (Locker)
//                {
//                    var randX = Game.Random.Next(350, 1300);
//                    var randY = Game.Random.Next(100, 700);
//                    var npc = ((SimpleBoss)Body).CreateChild(npcId, randX, randY, 0, -1, true, config);
//                    Console.WriteLine($"NpcID: {npc.Id}");
//                }
//            }
//        }

//        private void SetState()
//        {
//            ((PVEGame)Game).SendLivingActionMapping(Body, "stand", _isFear ? "standB" : "standA");
//        }

//        public override void OnShootedSay()
//        {
//            base.OnShootedSay();
//            //var index = Game.Random.Next(0, _onShootedChat.Length);
//            //if (!_isSay && Body.IsLiving)
//            //{
//            //    Body.Say(_onShootedChat[index], 1, 2000, 0);
//            //    _isSay = true;
//            //}
//        }

//        public override void OnKillPlayerSay()
//        {
//            base.OnKillPlayerSay();
//            ((SimpleBoss)Body).Say(LanguageMgr.GetTranslation("GameServerScript.AI.NPC.FourNormalCattleBoss.msg30"), 1,
//                1000);
//        }

//        public override void OnDiedSay()
//        {
//            base.OnDiedSay();
//            var index = Game.Random.Next(0, _diedChat.Length);
//            Body.Say(_diedChat[index], 1, 0, 1500);
//        }
//    }
//}
using System;
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Effects;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
	// Token: 0x020000F8 RID: 248
	public class FourHardCattleBoss : ABrain
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00042158 File Offset: 0x00040358
		public override void OnBeginSelfTurn()
		{
			base.OnBeginSelfTurn();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0004FC68 File Offset: 0x0004DE68
		public override void OnBeginNewTurn()
		{
			base.OnBeginNewTurn();
			this.method_8();
			base.Body.CurrentShootMinus = 1f;
			bool flag = !this.bool_0;
			if (flag)
			{
				base.Body.Config.HaveShield = true;
			}
			else
			{
				base.Body.Config.HaveShield = false;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0004FCC5 File Offset: 0x0004DEC5
		public override void OnCreated()
		{
			base.OnCreated();
			this.bool_0 = false;
			base.Body.CurrentDamagePlus = 1f;
			this.float_2 = 1f;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
		public override void OnStartAttacking()
		{
			bool flag2 = this.bool_0 && this.int_4 > 0;
			if (flag2)
			{
				this.int_4--;
				bool flag3 = this.int_4 > 0;
				if (!flag3)
				{
					this.int_0 = 3;
				}
			}
			else
			{
				bool flag4 = !this.bool_0;
				if (flag4)
				{
					bool flag = false;
					foreach (Player allFightPlayer in base.Game.GetAllFightPlayers())
					{
						bool flag5 = allFightPlayer.IsLiving && allFightPlayer.X > base.Body.X - 200 && allFightPlayer.X < base.Body.X + 200;
						if (flag5)
						{
							flag = true;
						}
					}
					bool flag6 = flag;
					if (flag6)
					{
						this.method_0(base.Body.X - 200, base.Body.X + 200);
						return;
					}
				}
				bool flag7 = this.int_0 == 0;
				if (flag7)
				{
					bool flag8 = !this.bool_0;
					if (flag8)
					{
						base.Body.CallFuction(new LivingCallBack(this.method_6), 2000);
						bool flag9 = ((SimpleBoss)base.Body).CurrentLivingNpcNum <= 0;
						if (flag9)
						{
							base.Body.Say("Lửa địa ngục đâu, ra mau.", 0, 5000);
							base.Body.CallFuction(new LivingCallBack(this.method_9), 7000);
						}
						else
						{
							base.Body.CallFuction(new LivingCallBack(this.method_2), 4000);
						}
					}
					this.int_0++;
				}
				else
				{
					bool flag10 = this.int_0 == 1;
					if (flag10)
					{
						bool flag11 = !this.bool_0;
						if (flag11)
						{
							base.Body.CallFuction(new LivingCallBack(this.method_6), 2000);
							base.Body.CallFuction(new LivingCallBack(this.method_1), 4000);
							this.int_0++;
						}
						else
						{
							this.int_0 = 3;
						}
					}
					else
					{
						bool flag12 = this.int_0 == 2;
						if (flag12)
						{
							bool flag13 = !this.bool_0;
							if (flag13)
							{
								base.Body.CallFuction(new LivingCallBack(this.method_6), 2000);
								base.Body.CallFuction(new LivingCallBack(this.method_2), 4000);
							}
							this.int_0++;
						}
						else
						{
							bool flag14 = this.int_0 == 3;
							if (flag14)
							{
								bool flag15 = !this.bool_0;
								if (flag15)
								{
									base.Body.CallFuction(new LivingCallBack(this.method_6), 2000);
									bool flag16 = ((SimpleBoss)base.Body).CurrentLivingNpcNum <= 0;
									if (flag16)
									{
										this.bool_0 = true;
										this.int_4 = 2;
										base.Body.Say("Hơ hơ... Ta bị ốm à??", 0, 2200);
										base.Body.CallFuction(new LivingCallBack(this.method_3), 4000);
									}
									else
									{
										base.Body.CallFuction(new LivingCallBack(this.method_5), 4000);
									}
								}
								else
								{
									this.bool_0 = false;
									this.float_2 = 1f;
									base.Body.CallFuction(new LivingCallBack(this.method_4), 2000);
								}
								this.int_0 = 0;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000500D4 File Offset: 0x0004E2D4
		private void method_0(int int_5, int int_6)
		{
			this.float_1 = base.Body.CurrentDamagePlus;
			base.Body.CurrentDamagePlus = 1000f;
			base.Body.ChangeDirection(base.Game.FindlivingbyDir(base.Body), 100);
			((SimpleBoss)base.Body).RandomSay(this.string_3, 0, 2000, 0);
			base.Body.PlayMovie("beatC", 2000, 0);
			base.Body.PlayMovie("beatE", 5000, 0);
			base.Body.RangeAttacking(int_5, int_6, "cry", 7000, null);
			base.Body.CallFuction(new LivingCallBack(this.method_11), 8000);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000501A4 File Offset: 0x0004E3A4
		private void method_1()
		{
			this.player_0 = base.Game.FindNearestPlayer(base.Body.X, base.Body.Y);
			bool flag = this.player_0 == null;
			if (!flag)
			{
				base.Body.ChangeDirection(this.player_0, 100);
				base.Body.Say("Tên ranh kia hãy đỡ này!!!", 0, 1000);
				base.Body.PlayMovie("beatA", 1200, 0);
				((PVEGame)base.Game).SendObjectFocus(this.player_0, 1, 3200, 0);
				base.Body.CallFuction(new LivingCallBack(this.method_7), 4000);
				bool flag2 = base.Body.FindDirection(this.player_0) == -1;
				if (flag2)
				{
					base.Body.RangeAttacking(this.player_0.X - 50, base.Body.X, "cry", 4800, null);
				}
				else
				{
					base.Body.RangeAttacking(base.Body.X, this.player_0.X + 50, "cry", 4800, null);
				}
				base.Body.CallFuction(new LivingCallBack(this.method_11), 6000);
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00050300 File Offset: 0x0004E500
		private void method_2()
		{
			((SimpleBoss)base.Body).RandomSay(this.string_0, 0, 1000, 0);
			base.Body.PlayMovie("beatB", 1000, 0);
			base.Body.RangeAttacking(base.Body.X - 10000, base.Body.X + 10000, "cry", 4100, null);
			foreach (Living allLivingPlayer in base.Game.GetAllLivingPlayers())
			{
				allLivingPlayer.AddEffect(new ReduceStrengthEffect(1, this.int_3), 4200);
			}
			base.Body.CallFuction(new LivingCallBack(this.method_11), 5000);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000503F8 File Offset: 0x0004E5F8
		private void method_3()
		{
			base.Body.PlayMovie("beatD", 1000, 0);
			((SimpleBoss)base.Body).RandomSay(this.string_2, 0, 4100, 0);
			base.Body.PlayMovie("AtoB", 4000, 0);
			base.Body.CallFuction(new LivingCallBack(this.method_11), 7000);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00050470 File Offset: 0x0004E670
		private void method_4()
		{
			base.Body.PlayMovie("AtoB", 1000, 0);
			((SimpleBoss)base.Body).RandomSay(this.string_1, 0, 2200, 0);
			base.Body.CallFuction(new LivingCallBack(this.method_5), 2000);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000504D0 File Offset: 0x0004E6D0
		private void method_5()
		{
			this.player_0 = base.Game.FindRandomPlayer();
			bool flag = this.player_0 == null;
			if (!flag)
			{
				base.Body.PlayMovie("jump", 500, 0);
				((PVEGame)base.Game).SendObjectFocus(this.player_0, 1, 2000, 0);
				base.Body.BoltMove(this.player_0.X, this.player_0.Y, 2500);
				base.Body.PlayMovie("fall", 2600, 0);
				base.Body.RangeAttacking(this.player_0.X - 100, this.player_0.X + 100, "cry", 3000, null);
				base.Body.CallFuction(new LivingCallBack(this.method_11), 4000);
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000505C4 File Offset: 0x0004E7C4
		private void method_6()
		{
			this.float_2 += this.float_0;
			base.Body.CurrentDamagePlus = this.float_2;
			this.physicalObj_1 = ((PVEGame)base.Game).Createlayer(base.Body.X, base.Body.Y - 60, "", "game.crazytank.assetmap.Buff_powup", "", 1, 0);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00050638 File Offset: 0x0004E838
		private void method_7()
		{
			bool flag = this.player_0 == null;
			if (!flag)
			{
				this.physicalObj_0 = ((PVEGame)base.Game).Createlayer(this.player_0.X, this.player_0.Y, "", "asset.game.4.blade", "", 1, 0);
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00050694 File Offset: 0x0004E894
		private void method_8()
		{
			bool flag = this.physicalObj_1 != null;
			if (flag)
			{
				base.Game.RemovePhysicalObj(this.physicalObj_1, true);
			}
			bool flag2 = this.physicalObj_0 == null;
			if (!flag2)
			{
				base.Game.RemovePhysicalObj(this.physicalObj_0, true);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000506E4 File Offset: 0x0004E8E4
		private void method_9()
		{
			LivingConfig config = ((PVEGame)base.Game).BaseLivingConfig();
			config.IsFly = true;
			for (int index = 0; index < this.int_1; index++)
			{
				((SimpleBoss)base.Body).CreateChild(this.int_2, base.Game.Random.Next(350, 1300), base.Game.Random.Next(100, 700), true, config);
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0005076A File Offset: 0x0004E96A
		private void method_10()
		{
			((SimpleBoss)base.Body).RemoveAllChild();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00050780 File Offset: 0x0004E980
		private void method_11()
		{
			bool flag = this.bool_0;
			if (flag)
			{
				((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "standB");
			}
			else
			{
				((PVEGame)base.Game).SendLivingActionMapping(base.Body, "stand", "standA");
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000422B8 File Offset: 0x000404B8
		public override void OnStopAttacking()
		{
			base.OnStopAttacking();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000507DC File Offset: 0x0004E9DC
		public FourHardCattleBoss()
		{
			this.int_1 = 4;
			this.int_2 = 4207;
			this.float_0 = 1f;
			this.int_3 = 100;
			this.string_0 = new string[]
			{
				"Các ngươi đỡ được không?",
				"Chết chắc rồi bọn ngu si!",
				"Sao mà đỡ lại được đây?",
				"Sao? Sao? Chết đi!!!!!"
			};
			this.string_1 = new string[]
			{
				"Xem ngươi chạy đâu cho thoát",
				"Dám đánh nén ta à?",
				"Để xem các ngươi chạy đâu",
				"Đánh nén ta là điều không thể tha thứ"
			};
			this.string_2 = new string[]
			{
				"Mệt rồi!",
				"Ta cảm thấy mệt quá!",
				"Mệt quá! Nghỉ tí đã."
			};
			this.string_3 = new string[]
			{
				"Để ta tiễn ngươi!",
				"Chết chắc rồi cưng à!",
				"Ngươi không chết mới là lạ.",
				"Ta sẽ ban tặng ngươi cái chết êm ái"
			};
		}

		// Token: 0x04000759 RID: 1881
		private int int_0;

		// Token: 0x0400075A RID: 1882
		private PhysicalObj physicalObj_0;

		// Token: 0x0400075B RID: 1883
		private PhysicalObj physicalObj_1;

		// Token: 0x0400075C RID: 1884
		private int int_1;

		// Token: 0x0400075D RID: 1885
		private int int_2;

		// Token: 0x0400075E RID: 1886
		private float float_0;

		// Token: 0x0400075F RID: 1887
		private int int_3;

		// Token: 0x04000760 RID: 1888
		private bool bool_0;

		// Token: 0x04000761 RID: 1889
		private Player player_0;

		// Token: 0x04000762 RID: 1890
		private float float_1;

		// Token: 0x04000763 RID: 1891
		private float float_2;

		// Token: 0x04000764 RID: 1892
		private int int_4;

		// Token: 0x04000765 RID: 1893
		private string[] string_0;

		// Token: 0x04000766 RID: 1894
		private string[] string_1;

		// Token: 0x04000767 RID: 1895
		private string[] string_2;

		// Token: 0x04000768 RID: 1896
		private string[] string_3;
	}
}
