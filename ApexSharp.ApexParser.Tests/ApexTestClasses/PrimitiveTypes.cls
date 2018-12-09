public class PrimitiveTypes
{
    public Blob MyBlob = Blob.ValueOf('Jay');
    public Boolean IsWinner = true;
    public Date MyDate = Date.Today();
    public DateTime MyDateTime = DateTime.Now();
    public Decimal MyDecimal = 12.4567;
    public Double d=3.133433;
    public Id MyId = '006E0000004TquXIAS';
    public Integer MyInteger = 1;
    public Long MyLong = 23432424242L;
    public Time MyTime = Time.NewInstance(1, 2, 3, 4);
    public String MyString = 'Jay';

    public void DemoMethod()
    {
        object obj = 10;
        Integer i = (Integer)obj;
    }
}