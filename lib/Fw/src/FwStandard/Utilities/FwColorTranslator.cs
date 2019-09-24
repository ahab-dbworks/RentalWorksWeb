using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace FwStandard.Utilities
{
    public class FwColorTranslator
    {
        public static int HtmlColorToOleColor(string htmlColor)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            if (htmlColor.StartsWith("#"))
            {
                red = Convert.ToInt32(htmlColor.Substring(1, 2), 16);
                green = Convert.ToInt32(htmlColor.Substring(3, 2), 16);
                blue = Convert.ToInt32(htmlColor.Substring(5, 2), 16);
            }
            else if (htmlColor.StartsWith("rgb("))
            {
                htmlColor = htmlColor.Substring(4);
                htmlColor = htmlColor.Substring(0, htmlColor.Length-1);
                string[] colors = htmlColor.Split(',');
                red = Convert.ToInt32(colors[0]);
                green = Convert.ToInt32(colors[1]);
                blue = Convert.ToInt32(colors[2]);
            }
            Color c = Color.FromArgb(red, green, blue);
            return BitConverter.ToInt32(new byte[] { c.R, c.G, c.B, 0x00 }, 0);
        }

        public static string OleColorToHtmlColor(int oleColor)
        {
            int red = (int)((byte)(oleColor & 255));
            int green = (int)((byte)(oleColor >> 8 & 255));
            int blue = (int)((byte)(oleColor >> 16 & 255));
            string htmlColor = String.Format("rgb({0},{1},{2})", red, green, blue);
            return htmlColor;
        }

        public static string OleColorToHtmlColor(int oleColor, double opacity)
        {
            int red = (int)((byte)(oleColor & 255));
            int green = (int)((byte)(oleColor >> 8 & 255));
            int blue = (int)((byte)(oleColor >> 16 & 255));
            string htmlColor = String.Format("rgba({0},{1},{2},{3})", red, green, blue, opacity);
            return htmlColor;
        }
    }
}