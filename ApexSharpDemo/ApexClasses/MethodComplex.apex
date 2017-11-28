public with sharing class MethodComplex {
    public static void MethodOne() {

        for (Account a : [SELECT Id FROM Account]) {
            System.Debug(a.Id);
        }

        for (Integer i=0;i<10;i++) {

        }
    }

    public Database.QueryLocator QueryLocator(Database.BatchableContext bc)
    {
         return Database.getQueryLocator([SELECT Id FROM Contact]);
    }
}