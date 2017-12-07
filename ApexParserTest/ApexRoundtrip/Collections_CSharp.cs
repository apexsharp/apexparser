namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;

    [WithSharing]
    public class Collections
    {
        public List<string> StringList = new List<string>{"one", "two"};

        public int[] IntegerArray = new int[]{1, 2, 3};

        public void ArrayDemo()
        {
            List<string> stringListLocal = new List<string>{"one", "two"};
            int[] integerArrayLocal = new int[]{1, 2, 3};
        }

        public void ListExample()
        {
            List<int> myList = new List<int>();
            myList.Add(47);
            int i = myList.Get(0);
            myList.Set(0, 1);
            myList.Clear();
            List<SelectOption> options = new List<SelectOption>();
            options.Add(new SelectOption("A","United States"));
            options.Add(new SelectOption("C","Canada"));
            options.Add(new SelectOption("A","Mexico"));
            System.Debug("Before sorting: "+ options);
            options.Sort();
            System.Debug("After sorting: "+ options);
        }

        public void SetExample()
        {
            Set<int> s = new Set<int>();
            s.Add(1);
            s.Remove(1);
        }

        public void MapExample()
        {
            Map<int, string> m = new Map<int, string>();
            m.Put(1, "First entry");
            m.Put(2, "Second entry");
            string value = m.Get(2);
        }

        public void MapSoqlExample()
        {
            // Map<Id, Contact> m = new Map<Id, Contact>(Soql.Query<Contact>("SELECT Id FROM Jay__c"));
            Map<ID, Contact> m = new Map<ID, Contact>(Soql.Query<Contact>("SELECT Id, Name FROM Contact LIMIT 10"));
            foreach (ID idKey in m.KeySet())
            {
                Contact contact = m.Get(idKey);
            }
        }
    }
}
