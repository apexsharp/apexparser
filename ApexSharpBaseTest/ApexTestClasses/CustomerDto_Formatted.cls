public class CustomerDto
{
     public String make { get;set; }
     public String year { get;set; }
     public CustomerDto.User user { get;set; }

     public class User
     {
          public string userName { get;set; }
     }
}
