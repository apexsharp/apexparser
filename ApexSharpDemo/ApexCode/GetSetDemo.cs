using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSharpDemo.ApexCode
{
    public class GetSetDemo
    {
        public double MyReadWritePropPublic { get; set; }
        private int propPrvt;
        public int prop
        {
            get
            {
                return propPrvt;
            }
            set
            {
                propPrvt = value;
            }
        }
        public int MyReadOnlyProp
        {
            get;
        }

        private static int myStaticPropPrvt;
        public static int MyStaticProp
        {
            get
            {
                return myStaticPropPrvt;
            }
        }

        int MyReadOnlyPropPrvt { get; }
        double MyReadWritePropPrvt
        {
            get; set;
        }
        public String MyWriteOnlyProp { get; }
        protected String MyWriteOnlyPropPrvt
        {
            set; get;
        }

        private string namePrvt;

        public string getName
        {
            get
            {
                return namePrvt;
            }
            private set
            {
                namePrvt = value;
            }
        }
        public string Stubbing
        {
            private get
            {
                return namePrvt;
            }
            set
            {
                namePrvt = value;
            }
        }
        public string DoThrowWhenException
        {
            get
            {
                return namePrvt;
            }
            set
            {
                namePrvt = value;
            }
        }
    }
}
