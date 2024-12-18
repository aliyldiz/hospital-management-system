namespace Hospital.Models;

public class TestPrice
{
    public int Id { get; set; }
    public string TestCode { get; set; }
    public decimal Price { get; set; }
    public int LabId { get; set; }
    public int BillId { get; set; }
    public Lab Lab { get; set; }
    public Bill Bill { get; set; }
}