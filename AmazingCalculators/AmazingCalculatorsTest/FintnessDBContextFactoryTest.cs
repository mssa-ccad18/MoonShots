using AmazingCalculatorLibrary.Models;

namespace AmazingCalculatorsTest;

[TestClass]
public class FintnessDBContextFactoryTest
{
    [TestMethod]
    public void TestMethod1()
    {
        //Arrange
        string[] args = { "Server=tcp:ccad18.database.windows.net,1433;Initial Catalog=fitnessdb;Persist Security Info=False;User ID=c3repo;Password=Pa$$w0rd!!!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" }; // Replace with your actual connection string
        // Act
        var factory = new FitnessDbContextFactory();
        var context = factory.CreateDbContext(args);
        // Assert
        Assert.IsNotNull(context);
        Assert.IsInstanceOfType(context, typeof(FitnessDbContext));
    }
}
