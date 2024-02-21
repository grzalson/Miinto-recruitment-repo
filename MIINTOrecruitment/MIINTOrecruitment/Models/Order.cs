namespace MIINTOrecruitment.Models
{

    public class Order
    {
        public int order_id { get; set; }
        public DateOnly order_date { get; set; }
        public string order_status { get; set; }

    }
}
