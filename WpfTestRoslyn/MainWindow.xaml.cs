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
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Reflection;
using System.Diagnostics;

namespace WpfTestRoslyn
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var script = CSharpScript.Create<IHello>(codeTextBox.Text).WithOptions(ScriptOptions.Default.WithReferences(Assembly.GetEntryAssembly()));
            try
            {
                var result = script.RunAsync().Result;
                var value = result.ReturnValue;
                resultTextBox.Text = value.Hello();
            }
            catch (CompilationErrorException exception)
            {
                resultTextBox.Text = exception.ToString();
            }
            finally
            {
                sw.Stop();
                timeSpanTextBlock.Text = sw.Elapsed.ToString();
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = "";
            timeSpanTextBlock.Text = "";
        }
    }
    public interface IHello
    {
        string Hello();
    }
}
