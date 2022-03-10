using System.Collections;
using System.Collections.Generic;

namespace FileProcessor.Tests.Data
{
    public class FIleProcessorValidatorData
    {
        private static readonly string path = Directory.GetDirectory() + @"\TestImages\";
        public class FileProcessorValidatorSuccesData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.jpg",
                    "image/jpeg"
                };
                yield return new object[]
                { path + "69c42771-522b-4c33-b550-99685ec8b898.jpeg",
                    "image/jpeg"
                };
                yield return new object[]
                {  path + "69c42771-522b-4c33-b550-99685ec8b898.png",
                    "image/png"
                };
                yield return new object[]
                {  path + "69c42771-522b-4c33-b550-99685ec8b898.bmp",
                    "image/bmp"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class FileProcessorValidatorFailureData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                { null,
                  null
                };
                yield return new object[]
                {  path + "69c42771-522b-4c33-b550-99685ec8b898.pdf",
                    "application/pdf"
                };
                yield return new object[]
                {  path + "69c42771-522b-4c33-b550-99685ec8b898.txt",
                    "text/plain"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
