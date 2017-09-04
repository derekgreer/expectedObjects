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
public class when_retrieving_a_customer
{
  static Customer _actual;
  static ExpectedObject _expected;
  static ICustomerService _customerService;

  Establish context = () =>
    {
      _expected = new Customer
                    {
                      Name = "Jane Doe",
                      PhoneNumber = "5128651000",
                      Address = new Address
                                  {
                                    AddressLineOne = "123 Street",
                                    AddressLineTwo = string.Empty,
                                    City = "Austin",
                                    State = "TX",
                                    Zipcode = "78717"
                                  }
                    }.ToExpectedObject();

      _customerService = new CustomerService();
    };

  Because of = () => _actual = _customerService.GetCustomerByName("Jane Doe");

  It should_return_the_expected_customer = () => _expected.ShouldEqual(_actual);
}
```


For more examples, see the [documentation](https://github.com/derekgreer/expectedObjects/wiki) or [browse the specifications](https://github.com/derekgreer/expectedObjects/tree/master/src/ExpectedObjects.Specs).
