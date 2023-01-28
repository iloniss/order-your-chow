using System;

namespace FileProcessor.Tests.Data
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
                System.IO.File.Copy(_projectDirectory + @"\TestImages\69c42771-522b-4c33-b550-99685ec8b898.jpg", _projectDirectory + @"\wwwroot\images\products\69c42771-522b-4c33-b550-99685ec8b898.jpg");
                System.IO.File.Copy(_projectDirectory + @"\TestImages\d8155945-45c8-49da-90e6-e5604bf85e3f.jpg", _projectDirectory + @"\wwwroot\images\recipes\d8155945-45c8-49da-90e6-e5604bf85e3f.jpg");
            }
        }
    }
}