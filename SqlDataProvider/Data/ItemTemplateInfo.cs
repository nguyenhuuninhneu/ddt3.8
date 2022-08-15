using System;

namespace SqlDataProvider.Data
{
	public enum eCategoryType
    {
		HAT = 1, //	Nón
		GLASSES = 2, //	Mắt kính
		HAIR = 3, //	Tóc
		DECORATIONS = 4, //	Đồ trang trí
		CLOTHE5 = 5, //	Quần áo
		EYES = 6, //	Mắt
		MAIN_WEAPON = 7, //	Vũ khí
		BRACELET = 8, //	Vòng tay
		RING = 9, //	Nhẫn
		PROPS1 = 10, //	Đạo cụ
		PROPS2 = 11, //	Đạo cụ
		QUEST_PROPS = 12, //	Đạo cụ nhiệm vụ
		SET = 13, //	Set
		NECKLACE = 14, //	Dây chuyền
		WING = 15, //	Cánh
		BALLOON = 16, //	Bong bóng
		SECONDARY_WEAPON = 17, //	Vũ khí phụ
		CARD_BOX = 18, //	Hộp thẻ
		SUPPORT = 19, //	Hỗ trợ
		XIULIAN = 20, //	Nước tu luyện
		QUEST_BOOK = 23, //	Sách nhiệm vụ
		GIFT = 25, //	Quà tặng
		SPECIAL_WEAPON = 27, //	Vũ khí đặc biệt
		// 30	Propriedade especial
		// 31	Mão Secundária Especial
		// 32	Sementes
		// 33	Fertilizante
		// 34	alimento
		// 35	ovo de bicho de estimação
		// 36	Colheita
		BADGE = 40, //	Huy hiệu
		// 50	Arma Mascote
		// 51	Chapéu Mascote
		// 52	Arma Mascote
	}

    public class ItemTemplateInfo : DataObject
    {
        public string AddTime { get; set; }

        public int Agility { get; set; }

        public int Attack { get; set; }

        public eBageType BagType
        {
			get
			{
				switch (CategoryID)
				{
				case 10:
				case 11:
				case 12:
				case 20:
				case 26:
				case 34:
				case 35:
				case 53:
					return eBageType.PropBag;
				case 32:
					return eBageType.FarmBag;
				default:
					return eBageType.EquipBag;
				}
			}
        }

        public bool IsOnly=> MaxCount > 1;

        public int BindType { get; set; }

        public bool CanCompose { get; set; }

        public bool CanDelete { get; set; }

        public bool CanDrop { get; set; }

        public bool CanEquip { get; set; }

        public int CanRecycle { get; set; }

        public bool CanStrengthen { get; set; }

        public bool CanUse { get; set; }

        public int CategoryID { get; set; }

        public string Colors { get; set; }

        public string Data { get; set; }

        public int Defence { get; set; }

        public string Description { get; set; }

        public int FloorPrice { get; set; }

        public int FusionNeedRate { get; set; }

        public int FusionRate { get; set; }

        public int FusionType { get; set; }

        public string Hole { get; set; }

        public int Level { get; set; }

        public int Luck { get; set; }

        public int MaxCount { get; set; }

        public string Name { get; set; }

        public int NeedLevel { get; set; }

        public int NeedSex { get; set; }

        public string Pic { get; set; }

        public int Property1 { get; set; }

        public int Property2 { get; set; }

        public int Property3 { get; set; }

        public int Property4 { get; set; }

        public int Property5 { get; set; }

        public int Property6 { get; set; }

        public int Property7 { get; set; }

        public int Property8 { get; set; }

        public int Quality { get; set; }

        public int ReclaimType { get; set; }

        public int ReclaimValue { get; set; }

        public int RefineryLevel { get; set; }

        public int RefineryType { get; set; }

        public string Script { get; set; }

        public int SuitId { get; set; }

        public int TemplateID { get; set; }

        public bool CanAdvanced()
        {
			switch (CategoryID)
			{
			case 1:
			case 5:
			case 7:
			case 17:
				return true;
			default:
				return false;
			}
        }
		public bool IsMainWeapon()
		{
			return CategoryID == (int)eCategoryType.MAIN_WEAPON;
		}

		public bool IsArm()
        {
			return CategoryID == (int)eCategoryType.MAIN_WEAPON || CategoryID == (int)eCategoryType.SECONDARY_WEAPON;
		}

		public bool IsRing()
        {
			switch (TemplateID)
			{
			case 9022:
			case 9122:
			case 9222:
			case 9322:
			case 9422:
			case 9522:
				return true;
			default:
				return false;
			}
        }

        public bool IsSpecial()
        {
			switch (TemplateID)
			{
			case -2300:
			case -2200:
			case -2100:
			case -2000:
			case -1900:
			case -1800:
			case -1700:
			case -1600:
			case -1500:
			case -1400:
			case -1300:
			case -1200:
			case -1100:
			case -1000:
			case -900:
			case -800:
			case -400:
			case -300:
			case -200:
			case -100:
			case 11107:
				return true;
			default:
				return false;
			}
        }

		public static double getHertAddition(double baseValue, double coefficient)
		{
			return Math.Round(baseValue * (Math.Pow(1.1, coefficient) - 1));
		}
	}
}
