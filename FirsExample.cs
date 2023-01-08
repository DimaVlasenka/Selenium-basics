namespace SeleniumLearning
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setup method execution");
        }

        [Test]
        public void Test1()
        {
            // Assert.Pass();
            TestContext.Progress.WriteLine("This is the Test1");
        }

        [Test]
        public void Test2()
        {
            // Assert.Pass();
            TestContext.Progress.WriteLine("This is the Test2");
        }

        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("TearDown method execution");        
                
        }

    }
}