namespace BAPMap.Model
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public City(int ID)
        {
            this.ID = ID;
            Name = string.Empty;
            Latitude = 0;
            Longitude = 0;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}