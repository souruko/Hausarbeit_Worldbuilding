using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hausarbeit_Worldbuilding.Pages
{
    public interface IPage
    {

        void SelectedWorldChanged(int? SelectedWorld);

        void UpdatePage();
    }
}
