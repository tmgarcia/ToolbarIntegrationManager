using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestProject.Models
{
    public class HSBAColor
    {
        //H = 0-360
        //S = 0-1
        //B = 0-1 = V
        //A = 0-1
        public float H { get; set; }
        public float S { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        public HSBAColor()
        {
            H = 0;
            S = 1;
            B = 1;
            A = 1;
        }
        public HSBAColor(float h, float s, float b, float a)
        {
            this.H = h;
            this.S = s;
            this.B = b;
            this.A = a;
        }
        public HSBAColor Copy()
        {
            return new HSBAColor(H, S, B, A);
        }
        public Color ToColor()
        {
            return this.ToRGBA().ToColor();
        }
        public RGBAColor ToRGBA()
        {
            float Chroma = S * this.B;
            float H1 = H / 60.0f;
            float X = Chroma * (1 - Math.Abs((H1 % 2.0f) - 1));

            float R1 = 0;
            float G1 = 0;
            float B1 = 0;

            if (H1 >= 0 && H1 < 1)
            {
                R1 = Chroma;
                G1 = X;
            }
            else if (H1 >= 1 && H1 < 2)
            {
                R1 = X;
                G1 = Chroma;
            }
            else if (H1 >= 2 && H1 < 3)
            {
                G1 = Chroma;
                B1 = X;
            }
            else if (H1 >= 3 && H1 < 4)
            {
                G1 = X;
                B1 = Chroma;
            }
            else if (H1 >= 4 && H1 < 5)
            {
                R1 = X;
                B1 = Chroma;
            }
            else if (H1 >= 5 && H1 < 6)
            {
                R1 = Chroma;
                B1 = X;
            }
            float m = this.B - Chroma;

            float R = R1 + m;
            float G = G1 + m;
            float B = B1 + m;

            RGBAColor r = new RGBAColor(R, G, B, this.A);
            return r;
        }
    }
}
