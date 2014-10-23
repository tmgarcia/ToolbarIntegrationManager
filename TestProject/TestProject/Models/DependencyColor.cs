using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TestProject.Models
{
    class DependencyColor : DependencyObject
    {
        public DependencyColor()
        {
            
        }
        public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(DependencyColor),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
                )
                );

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        

        public static DependencyProperty RProperty = DependencyProperty.Register("R", typeof(byte), typeof(DependencyColor));
        public byte R
        {
            get { return (byte)GetValue(RProperty); }
            set { SetValue(RProperty, value); }
        }

        public static DependencyProperty GProperty = DependencyProperty.Register("G", typeof(byte), typeof(DependencyColor));
        public byte G
        {
            get { return (byte)GetValue(GProperty); }
            set { SetValue(GProperty, value); }
        }
        public static DependencyProperty BProperty = DependencyProperty.Register("B", typeof(byte), typeof(DependencyColor));
        public byte B
        {
            get { return (byte)GetValue(BProperty); }
            set { SetValue(BProperty, value); }
        }
        public static DependencyProperty AProperty = DependencyProperty.Register("A", typeof(byte), typeof(DependencyColor));
        public byte A
        {
            get { return (byte)GetValue(AProperty); }
            set { SetValue(AProperty, value); }
        }
    }
}
