using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoOrganiser
{
    public class DotNet3ImageAttributeProvidor : IAttributeProvidor
    {
        public Dictionary<string, string> GetAttributes(string filename)
        {
            var result = new Dictionary<string, string>();

            using (var fs = new FileStream(
                                       filename,
                                       FileMode.Open,
                                       FileAccess.Read,
                                       FileShare.Read))
            {
                BitmapSource bitmapSource = BitmapFrame.Create(fs);
                BitmapMetadata data =(BitmapMetadata)bitmapSource.Metadata;
                
                result.Add("DateTaken", data.DateTaken);

                result.Add("ApplicationName", data.ApplicationName);

                return result;
            }

            

            
        }

    }
}
