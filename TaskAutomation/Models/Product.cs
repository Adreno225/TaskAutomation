namespace TaskAutomation.Models
{
    public class Product:BaseModel
    {
        public string Parameters { get; set; }
        public Product()
        {
            Name = "";
            Parameters = "";
        }
    }
}
