using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hausarbeit_Worldbuilding.Windows
{
    /// <summary>
    /// Interaktionslogik für WorldWindow.xaml
    /// </summary>
    public partial class WorldWindow : Window
    {
        MainWindow parent;
        WorldbuildingDBEntities Context;

        public WorldWindow(MainWindow parent, WorldbuildingDBEntities Context)
        {
            this.parent = parent;
            this.Context = Context;

            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = NameTextBox.Text;

            if (Name == "")
                return;

            var temp = new World();
            temp.Name = Name;

            temp = Context.World.Add(temp);

            Context.SaveChanges();
            parent.UpdateComboBox(temp.WorldID);
            this.Close();
        }
    }
}
