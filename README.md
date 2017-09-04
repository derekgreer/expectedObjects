# ExpectedObjects 
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)


ExpectedObjects is a testing library implementing the [Expected Object pattern](http://xunitpatterns.com/State%20Verification.html#Expected%20Object).  Use of the Expected Object pattern eliminates the need to encumber system objects with test-specific equality behavior, helps to reduce test code duplication and can aid in expressing the logical intent of automated tests.

## Quickstart
### Installation
```
$> nuget install ExpectedObjects
```

### Example Usage

```C#
using Xunit;

namespace MyProject.Specs
{
  public class CustomerSpecs
  {        
    [Fact]
    public void WhenRetrievingACustomer_ShouldReturnTheExpectedCustomer()
    {
      // Arrange
      var expectedCustomer = new Customer
      {
        FirstName = "Silence",
        LastName = "Dogood",
        Address = new Address
        {
          AddressLineOne = "The New-England Courant",
          AddressLineTwo = "3 Queen Street",
          City = "Boston",
          State = "MA",
          PostalCode = "02114"
        }                                            
      }.ToExpectedObject();

      // Act
      var actualCustomer = new CustomerService().GetCustomerByName("Silence", "Dogood");

      // Assert
      expectedCustomer.ShouldEqual(actualCustomer);
    }
  }
}
```


For more examples, see the [documentation](https://github.com/derekgreer/expectedObjects/wiki) or [browse the specifications](https://github.com/derekgreer/expectedObjects/tree/master/src/ExpectedObjects.Specs).
