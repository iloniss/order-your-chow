namespace OrderYourChow.Integration.Tests.Data
{
    public static class Directory
    {
        private static readonly string _projectDirectory = System.IO.Directory.GetCurrentDirectory();

        public static string GetDirectory() => _projectDirectory;

        public static void CreateTestDirectory()
        {
            if (!System.IO.Directory.Exists(_projectDirectory + "/wwwroot"))
            {
                System.IO.Directory.CreateDirectory(_projectDirectory + "/wwwroot");
                System.IO.Directory.CreateDirectory(_projectDirectory + "/wwwroot/images");
                System.IO.Directory.CreateDirectory(_projectDirectory + "/wwwroot/images/products");
                System.IO.Directory.CreateDirectory(_projectDirectory + "/wwwroot/images/recipes");               
            }
        }
    }
}
