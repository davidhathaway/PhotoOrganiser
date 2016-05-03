using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganiser
{
    public interface IAttributeProvidor
    {
        Dictionary<string, string> GetAttributes(string filename);
    }

    
}
