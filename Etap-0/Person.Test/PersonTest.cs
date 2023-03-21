namespace Person.Test;
using PersonNS;

[TestClass]
public class PersonTest
{
    private readonly Person _testPerson;

    public PersonTest() 
    {
        _testPerson = new Person("John", "Doe", DateTime.Now.AddYears(-18));
    }

    [TestMethod]
    public void NameTest()
    {
        bool hasReachedMaturity = _testPerson.ReachedMaturity();

        // Assert
        Assert.IsTrue(hasReachedMaturity);
    }
}