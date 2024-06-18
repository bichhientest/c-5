namespace NET105_ASM.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<ComboFoodItem>? ComboFoodItems { get; set; } = new List<ComboFoodItem>(); // Nullable collection
    }
}
