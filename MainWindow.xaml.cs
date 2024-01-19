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

namespace solvingCauchyProblemsApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

       
      
        private void Raschet_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(x0Text.Text) || string.IsNullOrWhiteSpace(yText.Text) || string.IsNullOrWhiteSpace(xnText.Text) || string.IsNullOrWhiteSpace(n1Text.Text))
            {
                MessageBox.Show("Введите все значения для расчетов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
               

                int x0 = int.Parse(x0Text.Text);
                int y = int.Parse(yText.Text);
                double xn = double.Parse(xnText.Text);
                int n1 = int.Parse(n1Text.Text);
                int n2 = 2 * n1;
                double shag1 = (xn - x0) / n1;
                double shag2 = (xn - x0) / n2;
                shagH1Read.Text = shag1.ToString();
                shagH2Read.Text = shag2.ToString();
                n2Read.Text = n2.ToString();
                Eilera(x0, shag1, n1);
                Runge(x0,shag2, n2);
                Uluchshennii_metod_Eilera(x0, shag1, n1);
                Runge_Kutte4(x0, shag1, n1);
            }
            

        } 


        //Явный метод Эйлера»
        private double Eilera(double x0, double h1, int n1)
        {
            int i;
            double[] y1 = new double[100];
            double s, x;

            x = x0;
            y1[0] = 1;
            List<dynamic> result = new List<dynamic>();
            resultEilera.ItemsSource = result;
            for (i = 0; i < n1; i++)
            {
                y1[i + 1] = y1[i] + h1 * (Math.Pow(2.71, x) * Math.Sin(y1[i]) + n1 * x);
                x += h1;
                //Заполнение таблицы resultEilera данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N1"),
                    y1 = y1[i + 1].ToString("N5"),
                    
                });

            }
            s = y1[i];
            Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
            resultElleraText.Text = s.ToString();
            resultElleraText.Background = brush;
            return s;
        }

        //Уточненное решение с помощью формулы Рунге
        private double Runge(double x0, double h2, int n2)
        {
            int i;
            double[] y1 = new double[100];
            double s, x;
            List<dynamic> result = new List<dynamic>();
            resultRunge.ItemsSource = result;
            x = x0;
            y1[0] = 1;

            for (i = 0; i < n2; i++)
            {
                y1[i + 1] = y1[i] + h2 * (Math.Pow(2.71, x) * Math.Sin(y1[i]) + n2 * x);
                x += h2;
                //Заполнение таблицы resultRunge данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N2"),
                    y1 = y1[i + 1].ToString("N5"),

                });

            }

            s = y1[i];
            Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
            resultRungeText.Text = s.ToString();
            resultRungeText.Background = brush;
            return s;
        }
        //Улучшенный метод Эйлера
        private double Uluchshennii_metod_Eilera(double x0, double h1, int n1)
        {
            int i;
            double[] y1 = new double[100];
            double s, x, y2, x2;
            List<dynamic> result = new List<dynamic>();
            resultUlEilera.ItemsSource = result;
            x = x0;
            y1[0] = 1;
            y2 = y1[0] + (h1 / 2) * (Math.Pow(2.71, x0) * Math.Sin(y1[0]) + n1 * x0);
            x2 = x0 + h1 / 2;

            for (i = 0; i < 2 * n1; i++)
            {
                y1[i + 1] = y1[0] + h1 * ((Math.Pow(2.71, x) + x2) * (Math.Sin(y1[i]) + y2) + n1 * x);
                x += h1;
                //Заполнение таблицы resultUlEilera данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N1"),
                    y1 = y1[i + 1].ToString("N5"),

                });
            }
                       
            s = y1[i];
            Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
            resultUlEileraText.Text = s.ToString();
            resultUlEileraText.Background = brush;
            return s;
        }

        private double Runge_Kutte4(double x0,  double h1, int n1)
        {
            double k1, k2, k3, k4;
            int i;
            double[] y1 = new double[100];
            double s, x;
            List<dynamic> result = new List<dynamic>();
            resultRungeKutt.ItemsSource = result;
            x = x0;
            y1[0] = 1;

            for (i = 0; i < n1; i++)
            {
                k1 = h1 * (Math.Pow(2.71, x) * Math.Sin(y1[i]) + n1 * x);
                k2 = h1 * ((Math.Pow(2.71, x) + h1 / 2) * (Math.Sin(y1[i]) + k1 / 2) + n1 * x);
                k3 = h1 * ((Math.Pow(2.71, x) + h1) * (Math.Sin(y1[i]) + 2 * k2 - k1) + n1 * x);
                k4 = h1 * (Math.Pow(2.71, x + h1) * Math.Sin(y1[i] + k3) + n1 * (x + h1));
                y1[i + 1] = y1[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                x += h1;
                //Заполнение таблицы resultRungeKutt данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N1"),
                    y1 = y1[i + 1].ToString("N5"),

                });
            }

            s = y1[i];
            Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
            resultRungeKuttText.Text = s.ToString();
            resultRungeKuttText.Background = brush;
            return s;
        }

        private void x0Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(x0Text.Text))
            {
                x0Text.Background = Brushes.Red;
               
            }
            else
            {
                x0Text.Background = Brushes.White;
            }
        }

        private void yText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(yText.Text))
            {
                yText.Background = Brushes.Red;

            }
            else
            {
                yText.Background = Brushes.White;
            }
        }

        private void xnText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(xnText.Text))
            {
                xnText.Background = Brushes.Red;

            }
            else
            {
                xnText.Background = Brushes.White;
            }
        }

        private void n1Text_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(n1Text.Text))
            {
                n1Text.Background = Brushes.Red;

            }
            else
            {
                n1Text.Background = Brushes.White;
            }
        }
    }



}
