namespace Models{

    public class SaleDTO
    {
        public int ClientID {get;set;}
        public int ProductID {get; set;}
        public int Quantity {get; set;}
    }

    public class SaleResultDTO {
        public int SaleID {get; set;}
        public int ProductID {get; set;}
        public string ProductName {get; set;}
        public int Quantity {get; set;}
        public decimal Price {get; set;}
        public decimal TotalAmount {get; set;}
        public decimal ClientTotalAmount {get; set;}
        public decimal ProductTotalAmount {get; set;}
        public decimal ProductByClientTotalAmount {get; set;}
        public decimal LastMinuteTotalAmount {get; set;}
    }
}