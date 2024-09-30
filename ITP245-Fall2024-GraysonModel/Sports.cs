using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITP245_Fall2024_GraysonModel
{
    public interface ISports
    {
        string Name { get; }
    }
    public partial class Player : ISports
    {
        public string Name => $"{LastName}, {FirstName}";
    }
    public partial class Team : ISports
    {
       
    }

}
