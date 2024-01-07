using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAutomation.Models
{
    public enum Mode
    {
        Manual,
        Remote,
        Both
    }
    public abstract class BaseModel
    {
        public string Name { get; set; }
    }
}
