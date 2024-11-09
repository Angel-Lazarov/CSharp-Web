namespace DeskMarket
{
	public static class Constraints
	{
		public const int ProductNameMinLength = 2;
		public const int ProductNameMaxLength = 60;
		public const int ProductDescriptionMinLength = 10;
		public const int ProductDescriptionMaxLength = 250;

		public const string ProductPriceMinValue = "1.00";
		public const string ProductPriceMaxValue = "3000.00";

		public const string AddedOnDateFormat = " dd-MM-yyyy";

		public const int CategoryNameMinLength = 3;
		public const int CategoryNameMaxLength = 20;
	}
}
