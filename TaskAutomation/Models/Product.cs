namespace TaskAutomation.Models
{
    public class Product:BaseModel
    {

        #region Параметры среды 
        private string _Parameters;
        public string Parameters
        {
            get => _Parameters;
            set => Set<string>(ref _Parameters, value);
        }
        #endregion

        public Product()
        {
            Name = "";
            Parameters = "";
        }
    }
}
