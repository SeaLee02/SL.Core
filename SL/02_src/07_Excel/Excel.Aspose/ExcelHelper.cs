using Aspose.Cells;
using SL.Excel.Aspose.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SL.Excel.Aspose
{
    public static class ExcelHelper
    {

        /// <summary>
        /// 文件流转DataTable
        /// </summary>
        /// <param name="stram"></param>
        /// <returns></returns>
        public static DataTable Excel2Table(Stream stram, int index = 0)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Workbook book = new Workbook(stram);
            Worksheet worksheet = book.Worksheets[index];
            Cells cells = worksheet.Cells;
            DataTable dataTable = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
            return dataTable;
        }


        public static byte[] GetExcelForList<T>(List<T> entitys) where T : class
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0];

            Cells cells = sheet.Cells;//单元格  
            Type tAtt = typeof(T);

            //标题
            var headTitle = (ImporterHeaderAttribute)Attribute.GetCustomAttribute(tAtt, typeof(ImporterHeaderAttribute));
            if (headTitle != null)
            {
                sheet.Name = headTitle.Name;
            }

            //头部信息
            Style styleHead = workbook.CreateStyle();//新增样式 
            styleHead.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleHead.VerticalAlignment = TextAlignmentType.Center;//文字居中 
            styleHead.Font.Name = "微软雅黑";//文字字体 
            styleHead.Font.Size = 12;//文字大小 
            styleHead.Font.Color = Color.White;
            styleHead.ForegroundColor = Color.CadetBlue;
            styleHead.Pattern = BackgroundType.Solid;
            PropertyInfo[] pro = tAtt.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性pp.GetProperties();
            int z = 0;
            foreach (PropertyInfo p in pro)
            {
                var head = (ImporterHeaderAttribute)p.GetCustomAttribute(typeof(ImporterHeaderAttribute));
                if (head != null)
                {
                    cells[0, z].PutValue(head.Name);
                    cells[0, z].SetStyle(styleHead);
                    cells.SetColumnWidth(z, head.Name.Length * 4);//设置列宽
                    cells.SetRowHeight(z, 24);//设置列宽
                }
                z++;
            }

            Style styleBody = workbook.CreateStyle();//新增样式 
            styleBody.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleBody.VerticalAlignment = TextAlignmentType.Center;//文字居中 
            styleBody.Font.Name = "微软雅黑";//文字字体 
            styleBody.Font.Size = 12;//文字大小 
            int y = 1;
            foreach (T t in entitys)
            {
                for (int j = 0; j < pro.Length; j++)
                {
                    var head = (ImporterHeaderAttribute)pro[j].GetCustomAttribute(typeof(ImporterHeaderAttribute));
                    if (head != null)
                    {
                        object obj = pro[j].GetValue(t);
                        if (obj is byte)
                        {
                            if ((byte)obj == 0)
                            {
                                obj = "否";
                            }
                            else
                            {
                                obj = "是";
                            }
                        }
                        if (obj is decimal)
                        {
                            obj = ((decimal)obj).ToString("F");
                        }
                        if (obj is Enum && obj.ToString() == "None")
                        {
                            obj = string.Empty;
                        }
                        if (obj is DateTime)
                        {
                            obj = ((DateTime)obj).ToString("yyyy-MM-dd");
                        }
                        cells[y, j].PutValue(obj);
                        cells[y, j].SetStyle(styleBody);
                        cells.SetRowHeight(y, 24);//设置列宽
                    }
                }
                y++;
            }
            WorkbookDesigner designer = new WorkbookDesigner(workbook);
            var xlsStream = new MemoryStream();
            SaveFormat format = new SaveFormat();
            format = SaveFormat.Xlsx;
            designer.Workbook.Save(xlsStream, format);
            byte[] buff = xlsStream.ToArray();
            return buff;
        }


        public static (bool isOk, string errorMsg) ValidationNotNull<T>(this List<T> entitys) where T : class
        {
            bool isOk = true;
            string errorMsg = string.Empty;
            Type tAtt = typeof(T);
            PropertyInfo[] pro = tAtt.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性pp.GetProperties();
            int i = 1;
            foreach (var t in entitys)
            {
                for (int j = 0; j < pro.Length; j++)
                {
                    var head = (ImportNotNullAttribute)pro[j].GetCustomAttribute(typeof(ImportNotNullAttribute));
                    if (head != null)
                    {
                        object obj = pro[j].GetValue(t);
                        if (obj == null)
                        {
                            errorMsg += $"第 {i} 行,{head.Name} 必填。";
                        }
                    }
                }
                i++;
            }
            if (errorMsg != string.Empty)
            {
                isOk = false;
            }
            return (isOk, errorMsg);

        }

    }
}
