using Core.Enumerators;
using RAN.Mobile.Enumerators;

namespace RAN.Mobile.Helper
{
	public class GameHelper
	{
		public static RAN.Mobile.Enumerators.SchoolEnum GetSchoolEnumValueByIndex(int index)
		{
			return RAN.Mobile.Enumerators.SchoolEnum.SacredGate;
		}

		public static string GetSchooNameValueByIndex(int index)
		{
			return null;
		}

		public static int GetSchoolIndexByEnumValue(RAN.Mobile.Enumerators.SchoolEnum schoolEnum)
		{
			return 0;
		}

		public static string GetSchoolNameBySchoolEnum(Core.Enumerators.SchoolEnum schoolEnum)
		{
			return null;
		}

		public static RAN.Mobile.Enumerators.GenderEnum GetGenderEnumValue(RAN.Mobile.Enumerators.CharacterClassEnum characterClassEnum)
		{
			return RAN.Mobile.Enumerators.GenderEnum.Female;
		}

		public static RAN.Mobile.Enumerators.GenderEnum GetGenderEnumValue(RAN.Mobile.Enumerators.CharacterIndexEnum characterIndexEnum)
		{
			return RAN.Mobile.Enumerators.GenderEnum.Female;
		}

		public static RAN.Mobile.Enumerators.CharacterClassEnum GetCharacterClassEnumValue(RAN.Mobile.Enumerators.CharacterIndexEnum characterIndexEnum)
		{
			return RAN.Mobile.Enumerators.CharacterClassEnum.None;
		}

		public static RAN.Mobile.Enumerators.CharacterIndex2Enum GetCharacterIndex2EnumValue(RAN.Mobile.Enumerators.CharacterClassEnum characterClassEnum)
		{
			return RAN.Mobile.Enumerators.CharacterIndex2Enum.Brawler;
		}

		public static RAN.Mobile.Enumerators.CharacterIndexEnum GetCharacterIndexEnumValue(RAN.Mobile.Enumerators.CharacterClassEnum characterClassEnum)
		{
			return RAN.Mobile.Enumerators.CharacterIndexEnum.BrawlerMale;
		}

		public static string GetCharacterClassNameWithGender(RAN.Mobile.Enumerators.CharacterClassEnum characterClassEnum)
		{
			return null;
		}

		public static string GetCharacterClassName(Core.Enumerators.CharacterClassEnum characterClassEnum)
		{
			return null;
		}
	}
}
