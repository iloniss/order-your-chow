using System.Collections;
using System.Collections.Generic;

namespace FileProcessor.Tests.Data
{
    public class FileProcessorData
    {
        private static readonly string path = Directory.GetDirectory() + @"\TestImages\";
        private static readonly string productsPath = @"wwwroot\images\products";
        private static readonly string recipesPath = @"wwwroot\images\recipes";
        private static readonly string fullPathProducts = System.IO.Directory.GetCurrentDirectory() + @"\" + productsPath + @"\";
        private static readonly string fullPathReciepes = System.IO.Directory.GetCurrentDirectory() + @"\" + recipesPath + @"\";
        public class FileProcessorSuccesData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] 
                { path + "69c42771-522b-4c33-b550-99685ec8b898.jpg",
                    productsPath,
                    "ImageProduct"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.jpeg",
                   recipesPath,
                    "ImageRecipe"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.png",
                    productsPath,
                    "ImageProduct"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                   recipesPath,
                    "ImageRecipe"
                };
                yield return new object[]
                {
                    path + "69c42771-522b-4c33-b550-99685ec8b898.pdf",
                    productsPath,
                    "ImageProduct"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FileProcessorSuccesExtendedData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.jpg",
                    productsPath,
                    "ImageProduct",
                    fullPathProducts
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.jpeg",
                   recipesPath,
                    "ImageRecipe",
                    fullPathReciepes
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.png",
                    productsPath,
                    "ImageProduct",
                    fullPathProducts
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                   recipesPath,
                    "ImageRecipe",
                    fullPathReciepes
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FileProcessorFailureData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {    null,
                   recipesPath,
                    "ImageRecipe"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    null,
                    "ImageRecipe"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    "wwwroot\\images\\productsasda",
                    "ImageRecipe"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    "wwwroot\\images\\productsasda",
                    null
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FileProcessorFailureExtendedData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {    null,
                   recipesPath,
                    "ImageRecipe",
                    fullPathReciepes
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    null,
                    "ImageRecipe",
                    fullPathReciepes
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    "wwwroot\\images\\productsasda",
                    "ImageRecipe",
                    fullPathReciepes
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    "wwwroot\\images\\productsasda",
                    null,
                    fullPathProducts
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FileProcessorDeleteSuccesData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {   "69c42771-522b-4c33-b550-99685ec8b898.jpg",
                    productsPath,
                    "ImageProduct",
                    fullPathProducts + "69c42771-522b-4c33-b550-99685ec8b898.jpg"
                };
                yield return new object[]
                {   "d8155945-45c8-49da-90e6-e5604bf85e3f.jpg",
                    recipesPath,
                    "ImageRecipe",
                    fullPathReciepes + "d8155945-45c8-49da-90e6-e5604bf85e3f.jpg"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FileProcessorDeleteFailureNotExistsData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {   "99922fsa935-e74a-4efe-863c-0733c44239da.jpg",
                    productsPath,
                    "ImageProduct",
                    fullPathReciepes + "99922fsa935-e74a-4efe-863c-0733c44239da.jpg"
                };
                yield return new object[]
                {   null,
                   recipesPath,
                    "ImageRecipe",
                    fullPathReciepes
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

