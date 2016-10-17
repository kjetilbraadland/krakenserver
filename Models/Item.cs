namespace aspnetcoreapp.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool IsComplete { get; set; }
        public string ReservedBy { get; set; }
    }
}