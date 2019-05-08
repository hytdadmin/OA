<%@ WebHandler Language="C#" Class="Code" %>

using System;
using System.Web;
using System.Drawing;
using System.Web.SessionState;

public class Code : IHttpHandler,IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "image/jpeg";
        string code = CreateCode();
        context.Session["code"] = code;
        using (Image img = CreateImg(code))
        {
            img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
       
    }

    Image CreateImg(string code)
    {
        Bitmap bitmap = new Bitmap(75, 29);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.FillRectangle(Brushes.Gray, 0, 0, bitmap.Width, bitmap.Height);
            g.DrawString(code, new Font("楷体", 20), Brushes.Red, 5, 5);
        }
        return bitmap;
    }

    string CreateCode()
    {
        string s = "1234567890";

        string code = "";
        Random ran = new Random();
        for (int i = 0; i < 4; i++)
        {
            code += s[ran.Next(0, s.Length)];
        }
        return code;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}