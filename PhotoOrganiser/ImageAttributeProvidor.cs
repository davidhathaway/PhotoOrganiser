using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganiser
{
    public class ImageAttributeProvidor : IAttributeProvidor
    {
        public Dictionary<string, string> GetAttributes(string filename)
        {
            var fileInfo = new FileInfo(filename);

            var attributes = fileInfo.Attributes;

            // Create an Image object. 
            Image theImage = new Bitmap(filename);

            // Get the PropertyItems property from image.
            PropertyItem[] propItems = theImage.PropertyItems;

            return null;
        }

    }
}
