namespace DataMseAPI.Model
{
    public class MseData
    {
        public string code {  get; set; }
        public DateTime date { get; set; }
        public float minimum { get; set; }
        public float maximum { get; set; }
        public float last_transaction { get; set; }
        public float average_price {  get; set; }
        public float gain {  get; set; }
        public int volume { get; set; }
        public float gain_best { get; set; }
        public float total_gain { get; set; }
    }
    public class DateClosePrice
    {
        public DateTime date { get; set; }
        public float last_transaction { get; set; }
    }
}
