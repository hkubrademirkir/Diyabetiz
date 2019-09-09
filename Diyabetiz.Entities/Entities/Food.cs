namespace Diyabetiz.Entities.Entities
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public int CarbValue { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

    }
}
