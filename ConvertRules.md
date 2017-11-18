Apex
```csharp
global class ClassGlobal
```
C#
```csharp
[Globel]
public class ClassGlobal
```
Apex
```csharp

public with sharing abstract class SoqlDemo
```
C#

```csharp
[WithSharing]
public abstract class SoqlDemo
```
Apex

```csharp
Contact contactNew = new Contact(LastName = 'Jay', Email = 'abc@abc.com');
```
C#

```csharp
Contact contactNew = new Contact() { LastName = "Jay", Email = "abc@abc.com" };
```

Apex

```csharp
List<Contact> contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
```
C#

```csharp
List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
```
Apex

```csharp
update contacts;
```
C#

```csharp
Soql.Update(contacts);
```
Apex

```csharp	          
insert contactNew;
```
C#

```csharp
Soql.Insert(contactNew);
```
Apex
```csharp
delete contacts;
```
C#
```csharp
Soql.Delete(contacts);
```
Apex
```csharp
global class ClassGlobal
```
C#
```csharp
Soql.Delete(contacts);
```
Apex
```csharp
@RestResource(urlMapping='/api/v1/RestDemo')
global class ClassRest
{
    @httpDelete
    global static void DoDelete() {
    }
    
    @httpPost
    global static void Post() {
    }

    @httpGet
    global static string Get() {
        return 'Jay';
    }

    @httpPatch
    global static void Patch() {
    }

    @httpPut
    global static void Put() {
    }
}
```
C#
```csharp
[RestResource("/api/v1/RestDemo")]
    [Globel]
    public class ClassRest
    {
        [HttpDelete]
        [Globel]
        public static void DoDelete()
        {
        }

        [HttpPost]
        [Globel]
        public static void Post()
        {
        }

        [HttpGet]
        [Globel]
        public static string Get()
        {
            return "Jay";
        }

        [HttpPatch]
        [Globel]
        public static void Patch()
        {
        }

        [HttpPut]
        [Globel]
        public static void Put()
        {
        }
    }
```

### Unit Testing Example

Apex
```csharp
@isTest
public class ClassUnitTest {
    @TestSetup
    public static void Setup() {
        System.Debug('One Time Setup Got Called');
    }

    @isTest
    public static void Assert() {
        System.Assert(true, 'Assert True');
    }
    public static testMethod void AssertTestMethod() {
        System.Assert(true, 'Assert True');
    }
}
```
C#
```csharp
    [TestFixture]
    public class ClassUnitTest
    {
        [SetUp]
        public static void Setup()
        {
            System.Debug("One Time Setup Got Called");
        }

        [Test]
        public static void Assert()
        {
            System.Assert(true, "Assert True");
        }

        [Test]
        public static void AssertTestMethod()
        {
            System.Assert(true, "Assert True");
        }
    }
```

### JSON Deserialization Example

Apex
```csharp
Contact newContact = (Contact)JSON.Deserialize(jsonString, String.class);
```
C#
```csharp
Contact newContact = JSON.Deserialize<Contact>(jsonString);
```

Apex

```csharp
@future
public static void FutureMethod()
{
}
@future(callOut=true)
public static void FutureMethodWithCallOut()
{
}
```
C#

```csharp
[Future]
public static void FutureMethod()
{
}

[Future(callOut:true)]
public static void FutureMethodWithCallOut()
{
}
```

		           