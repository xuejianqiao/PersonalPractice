using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Unlity
{
    public class Export
    {

        public static void ExportHTMLToExcel(Dictionary<string, string> dicSheet, string fileTitle)
        {

            StringBuilder sbBody = new StringBuilder();
            StringBuilder sbSheet = new StringBuilder();

            //定义Excel头部
            sbBody.AppendFormat(
                "MIME-Version: 1.0\r\n" +
                "X-Document-Type: Workbook\r\n" +
                "Content-Type: multipart/related; boundary=\"-=BOUNDARY_EXCEL\"\r\n\r\n" +
                "---=BOUNDARY_EXCEL\r\n" +
                "Content-Type: text/html; charset=\"gb2312\"\r\n\r\n" +
                "<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n" +
                "xmlns:x=\"urn:schemas-microsoft-com:office:excel\">\r\n\r\n" +
                "<head>\r\n" +
                "<xml>\r\n" +
                "<x:ExcelWorkbook>\r\n" +
                "<x:ExcelWorksheets>\r\n");

            //定义Sheet
            foreach (KeyValuePair<string, string> kv in dicSheet)
            {
                string gid = Guid.NewGuid().ToString();
                sbBody.AppendFormat("<x:ExcelWorksheet>\r\n" +
                    "<x:Name>{0}</x:Name>\r\n" +
                    "<x:WorksheetSource HRef=\"cid:{1}\"/>\r\n" +
                    "</x:ExcelWorksheet>\r\n"
                    , kv.Key
                    , gid);

                sbSheet.AppendFormat(
                     "---=BOUNDARY_EXCEL\r\n" +
                     "Content-ID: {0}\r\n" +
                     "Content-Type: text/html; charset=\"gb2312\"\r\n\r\n" +
                     "<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n" +
                     "xmlns:x=\"urn:schemas-microsoft-com:office:excel\">\r\n\r\n" +
                     "<head>\r\n" +
                     "<xml>\r\n" +
                     "<x:WorksheetOptions>\r\n" +
                     "<x:ProtectContents>False</x:ProtectContents>\r\n" +
                     "<x:ProtectObjects>False</x:ProtectObjects>\r\n" +
                     "<x:ProtectScenarios>False</x:ProtectScenarios>\r\n" +
                     "</x:WorksheetOptions>\r\n" +
                     "</xml>\r\n" +
                     "</head>\r\n" +
                     "<body>\r\n"
                     , gid);

                sbSheet.Append("<table border='1'>");
                sbSheet.Append(kv.Value);
                sbSheet.Append("</table>");
                sbSheet.Append("</body>\r\n" +
                    "</html>\r\n\r\n");
            }

            //定义Excel尾部
            StringBuilder sb = new StringBuilder(sbBody.ToString());
            sb.Append("</x:ExcelWorksheets>\r\n" +
                "</x:ExcelWorkbook>\r\n" +
               "</xml>\r\n" +
                "</head>\r\n" +
                "</html>\r\n\r\n");
            sb.Append(sbSheet.ToString());
            sb.Append("---=BOUNDARY_EXCEL--");

            //导出文件
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            bool isFireFox = false;
            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
            {
                isFireFox = true;
            }
            if (isFireFox)
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileTitle + ".xls");
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(fileTitle)) + ".xls");
            }
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }
    }
}
