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

namespace BlockyExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowCodeButton.IsEnabled = false;
            RunCodeButton.IsEnabled = false;
            Browser.NavigateToString(System.IO.File.ReadAllText(GetFilePath("blockyHTML.html")));
        }

        private async void Browser_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
        {
            ShowCodeButton.IsEnabled = true;
            RunCodeButton.IsEnabled = true;
            string toolboxXML = System.IO.File.ReadAllText(GetFilePath("blockyToolbox.xml"));
            string workspaceXML = System.IO.File.ReadAllText(GetFilePath("blockyWorkspace.xml"));
            //Initialize blocky using toolbox and workspace
            await Browser.InvokeScriptAsync("init", new string[] { toolboxXML, workspaceXML }).ConfigureAwait(true);

        }

        private async void RunCodeButton_Click(object sender, RoutedEventArgs e)
        {
            await Browser.InvokeScriptAsync("runCode", Array.Empty<string>()).ConfigureAwait(false);
        }

        private async void ShowCodeButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await Browser.InvokeScriptAsync("showCode", Array.Empty<string>()).ConfigureAwait(true);
            MessageBox.Show(result);
        }


        private string GetFilePath(string file)
        {
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;
            return System.IO.Path.Combine(directory, file);
        }

    }
}
