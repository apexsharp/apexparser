namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    using SalesForceAPI.Apex;

    class PrimitiveTypes
    {
        public Blob MyBlob = Blob.ValueOf("Jay");
        public bool IsWinner = true;
        public Date MyDate = Date.Today();
        public DateTime MyDateTime = DateTime.Now();
        public decimal MyDecimal = 12.45m;
        public double d = 3.133433;
        public Id MyId = "006E0000004TquXIAS";
        public int MyInteger = 1;
        public long MyLong = 23432424242L;
        public Time MyTime = Time.NewInstance(1, 2, 3, 4);
        public string MyString = "Jay";

        public void DemoMethod()
        {
            object obj = 10;
            int i = (int)obj;
        }
    }
}
