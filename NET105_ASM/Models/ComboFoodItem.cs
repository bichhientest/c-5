namespace NET105_ASM.Models
{
    public class ComboFoodItem
    {
        public int ComboId { get; set; }
        public Combo? Combo { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem? FoodItem { get; set; }
    }
}
