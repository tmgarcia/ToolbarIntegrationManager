using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public interface IDeactivatableTool
    {
        public void Activate();
        public void Deactivate();
    }
}
