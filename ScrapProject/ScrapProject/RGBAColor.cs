using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ScrapProject
{
    class RGBAColor
    {
        //R = 0-1
        //G = 0-1
        //B = 0-1
        //A = 0-1
        public float R;
        public float G;
        public float B;
        public float A;

        public int RDec
        {
            get { return (int)(R * 255); }
        }
        public int GDec
        {
            get { return (int)(G * 255); }
        }
        public int BDec
        {
            get { return (int)(B * 255); }
        }
        public int ADec
        {
            get { return (int)(A * 255); }
        }

        public string RHex
        {
            get { return RDec.ToString("X"); }
        }
        public string GHex
        {
            get { return GDec.ToString("X"); }
        }
        public string BHex
        {
            get { return BDec.ToString("X"); }
        }
        public string AHex
        {
            get { return ADec.ToString("X"); }
        }

        public RGBAColor()
        {
            R = 0;
            G = 0;
            B = 0;
            A = 1;
        }
        public RGBAColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public RGBAColor(int r, int g, int b, int a)
        {
            R = r / 255;
            G = g / 255;
            B = b / 255;
            A = a / 255;
        }
        public RGBAColor(Color color)
        {
            R = color.R/255.0f;
            G = color.G/255.0f;
            B = color.B/255.0f;
            A = color.A/255.0f;
        }

        public HSBAColor ToHSBA()
        {
            float Max = Math.Max(Math.Max(R, G), B);
            float min = Math.Min(Math.Min(R, G), B);
            float Chroma = Max - min;

            float H1 = 0;
            if (Chroma != 0)
            {
                if (Max == R)
                {
                    H1 = ((G - B) / Chroma);
                    H1 = (H1 < 0) ? H1 + 6 : H1;
                }
                else if (Max == G)
                {
                    H1 = ((B - R) / Chroma) + 2;
                }
                else//Max == B
                {
                    H1 = ((R - G) / Chroma) + 4;
                }
            }
            float H = 60.0f * H1;

            float V = Max;

            float S = (V == 0) ? 0 : (Chroma / V);

            HSBAColor hsba = new HSBAColor(H, S, V, this.A);
            return hsba;
        }
        public HSLAColor ToHSLA()
        {
            float Max = Math.Max(Math.Max(R, G), B);
            float min = Math.Min(Math.Min(R, G), B);
            float Chroma = Max - min;

            float H1 = 0;
            if (Chroma != 0)
            {
                if (Max == R)
                {
                    H1 = ((G - B) / Chroma);
                    H1 = (H1 < 0) ? H1 + 6 : H1%6;
                }
                else if (Max == G)
                {
                    H1 = ((B - R) / Chroma) + 2;
                }
                else//Max == B
                {
                    H1 = ((R - G) / Chroma) + 4;
                }
            }
            float H = 60 * H1;

            float L = 0.5f * (Max + min);

            float S = (L == 0 || L == 1) ? 0 : Chroma / (1 - Math.Abs((2 * L) - 1));
            return new HSLAColor(H, S, L, this.A);
        }
        public Color ToColor()
        {
            Color c = new Color();
            c.R = (byte)RDec;
            c.G = (byte)GDec;
            c.B = (byte)BDec;
            c.A = (byte)ADec;
            return c;
        }
    }
}
