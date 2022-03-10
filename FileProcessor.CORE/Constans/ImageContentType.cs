using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileProcessor.CORE.Constans
{
    public class ImageContentType
    {
        public static readonly IList<string> ContentTypes = new ReadOnlyCollection<string>
            (new List<string> 
            {
                "image/jpeg",
                "image/png",
                "image/bmp"
            });
    }    
}
