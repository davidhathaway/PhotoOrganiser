using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganiser
{
    public interface IAttributeOrganiser
    {
        string GetDirectory(Dictionary<string, string> attributes);
        string GetFileName(Dictionary<string, string> attributes, string currentFilePath);
    }

    public class DateTakenDirectoryOrganiser : IAttributeOrganiser
    {
        public DateTakenDirectoryOrganiser()
        {

        }
        public string GetDirectory(Dictionary<string, string> attributes)
        {
            var key = "DateTaken";
            if (attributes.ContainsKey(key))
            {
                var value = attributes[key];
                var parsed = DateTime.MinValue;

                if(DateTime.TryParse(value, out parsed))
                {
                    //todo
                }
                

            }
        }

        public string GetFileName(Dictionary<string, string> attributes, string currentFilePath)
        {
            var fileName = Path.GetFileName(currentFilePath);

            return fileName;
        }
    }
}
