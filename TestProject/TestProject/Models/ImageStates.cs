using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestProject.Models
{
    public class ImageStates
    {
        private string _activePath;
        public string ActivePath 
        {
            get { return _activePath; } 
            set
            {
                _activePath = value;
                activeImage = new BitmapImage();
                activeImage.BeginInit();
                activeImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _activePath);
                activeImage.EndInit();
                
            } 
        }
        private string _activeHoverPath;
        public string ActiveHoverPath
        {
            get { return _activeHoverPath; } 
            set
            {
                _activeHoverPath = value;
                activeHoverImage = new BitmapImage();
                activeHoverImage.BeginInit();
                activeHoverImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _activeHoverPath);                
                activeHoverImage.EndInit();
            }
        }
        private string _activePressedPath;
        public string ActivePressedPath
        {
            get { return _activePressedPath; } 
            set
            {
                _activePressedPath = value;
                activePressedImage = new BitmapImage();
                activePressedImage.BeginInit();
                activePressedImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _activePressedPath);
                activePressedImage.EndInit();
            }
        }
        private string _inactivePath;
        public string InactivePath
        {
            get { return _inactivePath; } 
            set
            {
                _inactivePath = value;
                inactiveImage = new BitmapImage();
                inactiveImage.BeginInit();
                inactiveImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _inactivePath);
                inactiveImage.EndInit();
            }
        }
        private string _inactiveHoverPath;
        public string InactiveHoverPath
        {
            get { return _inactiveHoverPath; } 
            set
            {
                _inactiveHoverPath = value;
                inactiveHoverImage = new BitmapImage();
                inactiveHoverImage.BeginInit();
                inactiveHoverImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _inactiveHoverPath);
                inactiveHoverImage.EndInit();
            }
        }
        private string _inactivePressedPath;
        public string InactivePressedPath
        {
            get { return _inactivePressedPath; } 
            set
            {
                _inactivePressedPath = value;
                inactivePressedImage = new BitmapImage();
                inactivePressedImage.BeginInit();
                inactivePressedImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _inactivePressedPath);
                inactivePressedImage.EndInit();
            }
        }
        private string _disabledPath;
        public string DisabledPath
        {
            get { return _disabledPath; } 
            set
            {
                _disabledPath = value;
                disabledImage = new BitmapImage();
                disabledImage.BeginInit();
                disabledImage.UriSource = new Uri("pack://application:,,,/TestProject;component/" + _disabledPath);
                disabledImage.EndInit();
            }
        }

        public BitmapImage activeImage { get; private set; }
        public BitmapImage activeHoverImage { get; private set; }
        public BitmapImage activePressedImage { get; private set; }
        public BitmapImage inactiveImage { get; private set; }
        public BitmapImage inactiveHoverImage { get; private set; }
        public BitmapImage inactivePressedImage { get; private set; }
        public BitmapImage disabledImage { get; private set; }
    }
}
