﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
                red   = Convert.ToInt32(htmlColor[1] + htmlColor[2]);
                green = Convert.ToInt32(htmlColor[3] + htmlColor[4]);
                blue  = Convert.ToInt32(htmlColor[5] + htmlColor[6]);
            }
            return (int)red | (int)green << 8 | (int)blue << 16;
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