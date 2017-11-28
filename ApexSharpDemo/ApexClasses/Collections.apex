public with sharing class Collections {

    public List<String> StringList = new List<String> { 'one', 'two' };
    public Integer[] IntegerArray = new Integer[]{1, 2, 3};

    public void ArrayDemo() {
        List<String> stringListLocal = new List<String> { 'one', 'two' };
        Integer[] integerArrayLocal = new Integer[]{1, 2, 3};
    }


    public void ListExample()
    {
        List<Integer> myList = new List<Integer>();
        myList.Add(47);

        Integer i = myList.get(0);
        myList.Set(0, 1);
        myList.Clear();

        List<SelectOption> options = new List<SelectOption>();
        options.Add(new SelectOption('A','United States'));
        options.Add(new SelectOption('C','Canada'));
        options.Add(new SelectOption('A','Mexico'));

        System.Debug('Before sorting: ' + options);
        options.Sort();
        System.Debug('After sorting: ' + options);
    }

    public void SetExample()
    {
        Set<Integer> s = new Set<Integer>();
        s.Add(1);
        s.Remove(1);
    }

    public void MapExample()
    {
        Map<Integer, String> m = new Map<Integer, String>();
        m.Put(1, 'First entry');
        m.Out(2, 'Second entry');
        string value = m.Get(2);
    }

    public void MapSoqlExample() {

        // Map<Id, Contact> m = new Map<Id, Contact>(Soql.Query<Contact>("SELECT Id FROM Jay__c"));
        Map<Id, Contact> m = new Map<Id, Contact>([SELECT Id, Name FROM Contact LIMIT 10]);

        for (ID idKey : m.KeySet())
        {
            Contact contact = m.Get(idKey);
        }
    }
}