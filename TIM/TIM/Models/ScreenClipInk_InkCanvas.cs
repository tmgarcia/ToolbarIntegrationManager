using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TIM.Models
{
    class ScreenClipInk_InkCanvas : InkCanvas
    {
        public static readonly RoutedEvent FinishedClipEvent = EventManager.RegisterRoutedEvent("FinishedClip", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ScreenClipInk_InkCanvas));
        public event RoutedEventHandler FinishedClip
        {
            add { AddHandler(FinishedClipEvent, value); }
            remove { RemoveHandler(FinishedClipEvent, value); }
        }
        ScreenClipInk_DynamicRenderer renderer;
        public BitmapSource fullScreenCap;
        public BitmapSource clippedImage;
        public ScreenClipInk_InkCanvas(BitmapSource fullScreenCap)
            : base()
        {
            this.fullScreenCap = fullScreenCap;
            this.Background = new SolidColorBrush(Color.FromArgb(10,0,0,0));
            renderer = new ScreenClipInk_DynamicRenderer(fullScreenCap);
            renderer.fullScreenCap = fullScreenCap;
            this.DynamicRenderer = renderer;
            clippedImage = null;
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            this.Strokes.Remove(e.Stroke);
            e.Handled = true;
            this.RaiseEvent(new RoutedEventArgs(FinishedClipEvent, this));
        }
    }
}
