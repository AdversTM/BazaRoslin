namespace BazaRoslin.Model {
    public interface IPlant {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int WateringFrequency { get; set; }
        public string Fertilization { get; set; }
        public string Size { get; set; }
        public string VegetationStart { get; set; }
        public string VegetationEnd { get; set; }
        public int Temperature { get; set; }
        
        public ICategory Category { get; set; }
    }
}