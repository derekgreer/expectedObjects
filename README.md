# ExpectedObjects

ExpectedObjects is a testing library implementing the [Expected Object pattern](http://xunitpatterns.com/State%20Verification.html#Expected%20Object).  Use of the Expected Object pattern eliminates the need to encumber system objects with test-specific equality behavior, helps to reduce test code duplication and can aid in expressing the logical intent of automated tests.

# Quickstart
## Installation
```
$> nuget install ExpectedObjects
```

## Example Usage

### Strongly-Typed Comparison
```C#
class Customer
{
  public string Name { get; set; }
  public string PhoneNumber { get; set; }
  public Address Address { get; set; }
}

class Address
{
  public string AddressLineOne { get; set; }
  public string AddressLineTwo { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Zipcode { get; set; }
}
```

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

### Partial Comparision

```C#
public class when_retrieving_a_customer
{
  static Customer _actual;
  static ExpectedObject _expected;
  static ICustomerService _customerService;

  Establish context = () =>
    {
      _expected = new
                    {
                      Name = "Jane Doe",
                      Address = new
                                  {
                                    City = "Austin"
                                  }
                    }.ToExpectedObject();


    };

  Because of = () => _actual = _customerService.GetCustomerByName("Jane Doe");

  It should_have_the_correct_name_and_city = () => _expected.ShouldMatch(_actual);
}
```

For more examples, take a look at the code's specifications or the article [Introducing the Expected Objects Library](http://lostechies.com/derekgreer/2011/06/28/introducing-the-expected-objects-library/).
