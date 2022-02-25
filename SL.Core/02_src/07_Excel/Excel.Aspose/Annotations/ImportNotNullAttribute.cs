using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Excel.Aspose.Annotations
{
    /// <summary>
    /// 导入不能为null版本
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ImportNotNullAttribute : Attribute
    {
        public string Name { get; set; }

        public ImportNotNullAttribute(string name)
        {
            this.Name = name;
        }
    }
}
