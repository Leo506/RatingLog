using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RatingLog
{
    /// <summary>
    /// Логика взаимодействия для Grades.xaml
    /// </summary>
    public partial class Grades : Page
    {
        public Grades()
        {
            InitializeComponent();
            foreach (var group in RatingLogic.GetInstance().GetGroups())
            {
                ClassList.Items.Add(new TextBlock() { Text = group });
            }
        }

        private void OnGroupChange(object sender, SelectionChangedEventArgs e)
        {
            Table.Columns.Clear();
            Table.Columns.Add(new DataGridTextColumn() { Header = "Name" } );
            foreach (var date in RatingLogic.GetInstance().GetAllDates(((TextBlock)ClassList.SelectedItem).Text))
            {
                Trace.WriteLine(date.ToString());
                Table.Columns.Add(new DataGridTextColumn() { Header = date.ToString() } );
            }
        }
    }
}
