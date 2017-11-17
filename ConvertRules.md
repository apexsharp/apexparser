Apex

```csharp
public with sharing abstract class SoqlDemo
```
C#

```csharp
[ApexWithSharing]
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



		           