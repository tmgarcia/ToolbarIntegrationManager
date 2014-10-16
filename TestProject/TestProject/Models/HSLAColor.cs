using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestProject.Models
{
    public class HSLAColor
    {
        //H = 0-360
        //S = 0-1
        //L = 0-1
        //A = 0-1
        public float H;
        public float S;
        public float L;
        public float A;

        public HSLAColor()
        {
            H = 0;
            S = 1;
            L = 1;
            A = 1;
        }
        public HSLAColor(float h, float s, float l, float a)
        {
            this.H = h;
            this.S = s;
            this.L = l;
            this.A = a;
        }

        public HSLAColor Copy()
        {
            return new HSLAColor(H, S, L, A);
        }

        public Color ToColor()
        {
            return this.ToRGBA().ToColor();
        }

        public RGBAColor ToRGBA()
        {
            float Chroma = (1 - Math.Abs((2 * L) - 1)) * S;
            float H1 = H / 60.0f;
            float X = Chroma * (1 - Math.Abs((H1 % 2) - 1));

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
            float m = L - (0.5f * Chroma);

            float R = R1 + m;
            float G = G1 + m;
            float B = B1 + m;

            RGBAColor r = new RGBAColor(R, G, B, this.A);
            return r;
        }
    }
}
