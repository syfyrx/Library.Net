using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Library.Net.CLCommon
{
    /// <summary>
    /// 验证码产生类
    /// </summary>
    public class CValidateCode
    {
        /// <summary>
        /// 获取随机码
        /// </summary>
        /// <param name="allChar">所有可供生成的码（用“,”隔开），如果为空字符串，则使用默认码</param>
        /// <param name="codeCount">生成随机码的个数</param>
        /// <returns></returns>
        public static string GetRandomCode(string allChar, int codeCount)
        {
            try
            {
                string randomCode = "";//随机码
                if (allChar.Length == 0)
                {
                    allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                }
                string[] allCharArray = allChar.Split(',');
                Random rand = new Random();
                List<int> temp = new List<int>();
                for (int i = 0; i < codeCount; i++)
                {
                    int t = rand.Next(allCharArray.Length);
                    while (temp.Contains(t))
                    {
                        t = rand.Next(allCharArray.Length);
                    }
                    temp.Add(t);
                    randomCode += allCharArray[t];
                }
                return randomCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取验证码图片流
        /// </summary>
        ///<param name="checkCode">验证码字符串</param>
        ///<param name="hasLine">是否有水平干扰线</param>
        ///<param name="hasPixel">是否有噪音点</param>
        /// <returns></returns>
        public static MemoryStream GetCodeImage(string checkCode, bool hasLine, bool hasPixel)
        {
            
            Bitmap image = new Bitmap(Convert.ToInt32(Math.Ceiling((checkCode.Length * 15.0))), 22);
            Font codeFont = new System.Drawing.Font("Arial", 18, (FontStyle.Bold | FontStyle.Italic));
            Graphics g = Graphics.FromImage(image);
            image = new Bitmap(Convert.ToInt32(g.MeasureString(checkCode, codeFont).Width)+4, Convert.ToInt32(g.MeasureString(checkCode, codeFont).Height));
            g = Graphics.FromImage(image);

            try
            {
                g.Clear(Color.White);
                MemoryStream ms = new MemoryStream();
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, codeFont, brush,0, 0);
                List<int> temp = new List<int>();
                Random rand = new Random();
                if (hasLine)
                {
                    temp.Clear();
                    //for循环用来生成一些随机的水平线
                    Pen blackPen = new Pen(Color.Silver, 0);
                    for (int i = 0; i < 5; i++)
                    {
                        int y = rand.Next(image.Height);
                        while (temp.Contains(y) || temp.Contains(y + 1) || temp.Contains(y - 1)||temp.Contains(y + 2) || temp.Contains(y - 2))
                        {
                            y = rand.Next(image.Height);
                        }
                        temp.Add(y);
                        g.DrawLine(blackPen, 0, y, image.Width, y);
                    }
                }
                if (hasPixel)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        x = rand.Next(image.Width);
                        y = rand.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(rand.Next()));
                    }
                }
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
