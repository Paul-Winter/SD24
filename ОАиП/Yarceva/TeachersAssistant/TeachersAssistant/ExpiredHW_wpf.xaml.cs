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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeachersAssistant
{
    public partial class ExpiredHW_wpf : Window
    {
        public ExpiredHW_wpf()
        {
            InitializeComponent();
        }
        
        private void Na_ButtonClick(object sender, RoutedEventArgs e)
        {
            Na_wpf na_wpf = new Na_wpf();
            na_wpf.Show();
            this.Close();
        }
        private void StatisticsOnLateness_ButtonClick(object sender, RoutedEventArgs e)
        {
            StatisticsOnLateness_wpf statisticsOnLateness_wpf = new StatisticsOnLateness_wpf();
            statisticsOnLateness_wpf.Show();
            this.Close();
        }
        private void AverageGrades_ButtonClick(object sender, RoutedEventArgs e)
        {
            AverageGrades_wpf averageGrades_wpf = new AverageGrades_wpf();
            averageGrades_wpf.Show();
            this.Close();
        }

        //Галочки по группам
        private void CbAllGroups_Checked(object sender, RoutedEventArgs e)
        {

            if (CbGroup1 != null) CbGroup1.IsChecked = false;
            if (CbGroup2 != null) CbGroup2.IsChecked = false;
            if (CbGroup3 != null) CbGroup3.IsChecked = false;
            if (CbGroup4 != null) CbGroup4.IsChecked = false;
        }
        private void CbSpecificGroup_Checked(object sender, RoutedEventArgs e)
        {
            if (CbAllGroups != null) CbAllGroups.IsChecked = false;
        }
        //
    }
}
