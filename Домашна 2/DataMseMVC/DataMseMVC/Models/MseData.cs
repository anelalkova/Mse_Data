namespace DataMseMVC.Model
{
    public class MseData
    {
        required public string code { get; set; }
        required public DateTime date { get; set; }
        required public float minimum { get; set; }
        required public float maximum { get; set; }
        required public float last_transaction { get; set; }
        required public float average_price { get; set; }
        required public float gain { get; set; }
        required public int volume { get; set; }
        required public float gain_best { get; set; }
        required public float total_gain { get; set; }
    }
}
