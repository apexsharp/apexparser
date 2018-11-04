namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.System;
    using SObjects;

    public class PrimitiveTypes
    {
        public Blob MyBlob = Blob.ValueOf("Jay");

        public bool IsWinner = true;

        public Date MyDate = Date.today();

        public Datetime MyDateTime = Datetime.now();

        private Date AnotherDate = Date.today().date();

        public decimal MyDecimal = 12.4567m;

        //public Double d=3.133433;
        public ID MyId = "006E0000004TquXIAS";

        public int MyInteger = 1;

        public long MyLong = 23432424242L;

        public Time MyTime = Time.NewInstance(1, 2, 3, 4);

        public string MyString = "Jay";

        public string abc, def, jkl;

        public void DemoMethod()
        {
            object obj = 10;
            int i = (int)obj;
            string abc1, def1, jkl1;
        }
    }
}
