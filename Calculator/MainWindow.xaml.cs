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
using System.Threading;
using System.Globalization;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string operation = "";
        private double inp1 = 0;
        private double inp2 = 0;
        bool flag = false;
        double memoryNum = 0;
        int counter = 0;
        List<Button> btns;
        public MainWindow()
        {
            //CultureInfo.CurrentCulture = new CultureInfo("");
            InitializeComponent();
            outputBig.Content = "0";
            outputSmall.Content = "";
            btns = new List<Button> { btnNum0, btnNum1, btnNum2, btnNum3, btnNum4, btnNum5, btnNum6, btnNum7, btnNum8, btnNum9, btnClear };
        }

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            btnsEnable();
            if (!flag)
            {
                outputBig.Content = "";
                outputBig.Content += (sender as Button).Content.ToString();
                flag = true;
            }
            else if (outputBig.Content.ToString()[0] == '0' && outputBig.Content.ToString().Length == 1)
            {
                outputBig.Content = (sender as Button).Content.ToString();
            }
            else outputBig.Content += (sender as Button).Content.ToString();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            operation = "+";
            btnEq(operation);
            //outputSmall.Content = outputBig.Content + operation;
            flag = false;
            memoryNum = autoEq(inp1, operation);
            counter= 0;
        }
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            operation = "-";
            btnEq(operation);
            flag = false;
            memoryNum = autoEq(inp1, operation);
        }
        private void btnMult_Click(object sender, RoutedEventArgs e)
        {
            operation = "*";
            btnEq(operation);
            flag = false;
            memoryNum = autoEq(inp1, operation);
        }
        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            operation = "/";
            btnEq(operation);
            flag = false;
            memoryNum = autoEq(inp1, operation);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (outputBig.Content.ToString() != "0" && outputBig.Content.ToString().Length != 1)
            {
                StringBuilder sb = new StringBuilder(outputBig.Content.ToString());
                sb.Remove(sb.Length-1, 1);
                outputBig.Content = sb.ToString();
            }
            else if (outputBig.Content.ToString() != "0") outputBig.Content = "0";
        }
        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            
            inp2 = double.Parse(outputBig.Content.ToString());
            double result = 0;
            switch (operation)
            {
                case "+": result = Operations.Plus(inp1, inp2); break;
                case "-": result = Operations.Subtract(inp1, inp2); break;
                case "*": result = Operations.Multiply(inp1, inp2); break;
                case "/": result = Operations.Divide(inp1, inp2); break;
            }
            
            outputBig.Content = result.ToString();

            outputSmall.Content = "";
        }

        private void btnClearE_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            outputBig.Content = "0";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            btnsEnable();
            flag = false;
            outputBig.Content = "0";
            outputSmall.Content = "";
            inp1 = 0;
            inp2 = 0;
        }
        private double autoEq(double inp1, string op)
        {
            inp2 = double.Parse(outputBig.Content.ToString());
            double result = 0;
            switch (op)
            {
                case "+": result = Operations.Plus(inp1, inp2); break;
                case "-": result = Operations.Subtract(inp1, inp2); break;
                case "*": result = Operations.Multiply(inp1, inp2); break;
                case "/": result = Operations.Divide(inp1, inp2); break;
            }
            return result;
        }
        private void btnEq(string op)
        {
            
            
            if (outputSmall.Content.ToString() != "")
            {   
                StringBuilder sb = new StringBuilder(outputSmall.Content.ToString());
                string oldOp = sb[sb.Length-1].ToString();
                
                inp1 = autoEq(double.Parse(sb.Remove(sb.Length - 2, 2).ToString()), oldOp);
                outputSmall.Content = inp1;
                
                outputSmall.Content += " " + operation;
                
            }
            else
            {
                outputSmall.Content = outputBig.Content.ToString() + " " + operation;
                inp1 = Convert.ToDouble(outputBig.Content.ToString());
            }

        }

        private void btnNegate_Click(object sender, RoutedEventArgs e)
        {
            if (outputBig.Content.ToString() != "0")
            {
                outputBig.Content = double.Parse(outputBig.Content.ToString()) * -1;
            }
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            if (inp1 != 0)
            {
                outputBig.Content = (inp1 / 100 * double.Parse(outputBig.Content.ToString())).ToString().Replace('.',',');
            }
            
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            if (outputBig.Content.ToString() != "0," && !outputBig.Content.ToString().Contains(','))
            {
                outputBig.Content += ",";
            }
        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            if (outputBig.Content.ToString() != "0")
            {
                outputBig.Content = (1 / double.Parse(outputBig.Content.ToString())).ToString().Replace('.', ',');
            }
        }

        private void btnDegree_Click(object sender, RoutedEventArgs e)
        {
            outputBig.Content = Math.Pow(double.Parse(outputBig.Content.ToString()), 2);
        }

        private void btnRoot_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(outputBig.Content.ToString()) >= 0)
                outputBig.Content = Math.Pow(double.Parse(outputBig.Content.ToString()), 0.5);
            else
            {
                outputBig.Content = "Impossible";
                flag = false;
                btnsDisable(btns);
            } 
        }
        private void btnsEnable()
        {
            foreach (var b in Grid.Children)
            {
                if (b is Button)
                {
                    
                    (b as Button).IsEnabled = true;
                }
                
            }
        }
        private void btnsDisable(List<Button> btns) 
        {
            foreach (var b in Grid.Children)
            {
                if (b is Button)
                {

                    (b as Button).IsEnabled = false;
                }
            }
            foreach (Button b in btns)
            {
                b.IsEnabled=true;
            }
        }
    }
}
