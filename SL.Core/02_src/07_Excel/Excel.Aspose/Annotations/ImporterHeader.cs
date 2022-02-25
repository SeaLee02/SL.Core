using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Excel.Aspose.Annotations
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ImporterHeaderAttribute : Attribute
    {
        public string Name { get; set; }

        public ImporterHeaderAttribute(string name)
        {
            this.Name = name;
        }
    }
}
