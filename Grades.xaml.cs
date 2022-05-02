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
        DataTable table = new DataTable();

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
            string newGroup = ((TextBlock)ClassList.SelectedItem).Text;
            Table.Columns.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Name");
            Table.Columns.Add(new DataGridTextColumn() { Binding = new Binding("Name"), Header = "Name" });

            int columnsCount = 1;
            foreach (var date in RatingLogic.GetInstance().GetAllDates(newGroup))
            {
                table.Columns.Add(date.ToString(), typeof(int));
                Table.Columns.Add(new DataGridTextColumn() { Binding = new Binding(String.Format("[{0}]", date.ToString())), Header = date.ToString() });
                Trace.WriteLine("Format: " + String.Format("[{0}]", date.ToString()));
                columnsCount++;
            }

            

            var names = RatingLogic.GetInstance().GetNames(newGroup);
            for (int i = 0; i < names.Length; i++)
            {
                object[] values = new object[columnsCount];
                values[0] = names[i];

                var grades = RatingLogic.GetInstance().GetGrades(names[i]);
                for (int j = 1; j < columnsCount; j++)
                {
                    if (j - 1 >= grades.Length)
                        values[j] = 0;
                    else
                        values[j] = grades[j - 1];
                    //Trace.WriteLine("Grade: " + grades[j - 1]);
                }

                table.Rows.Add(values);
            }

            Table.ItemsSource = table.DefaultView;

        }
    }
}
