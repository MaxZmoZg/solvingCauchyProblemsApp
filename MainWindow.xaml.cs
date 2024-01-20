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
            if (string.IsNullOrWhiteSpace(x0Text.Text) || string.IsNullOrWhiteSpace(yText.Text) 
                || string.IsNullOrWhiteSpace(xnText.Text) || string.IsNullOrWhiteSpace(n1Text.Text) || string.IsNullOrWhiteSpace(Ntext.Text))
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
                int N = int.Parse(Ntext.Text);
                shagH1Read.Text = shag1.ToString();
                shagH2Read.Text = shag2.ToString();
                n2Read.Text = n2.ToString();
                Eilera(x0, shag1, n1,y,N);
                Runge(x0,shag2, n2,y, N);
                Uluchshennii_metod_Eilera(x0, shag1, n1,y, N);
                Runge_Kutte4(x0, shag1, n1,y, N);

                Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
                resultRungeText.Text = ((2* (Runge(x0, shag2, n2, y, N))) - (Eilera(x0, shag1, n1, y, N))).ToString();
                resultRungeText.Background = brush;
            }
            

        } 


        //Явный метод Эйлера»
        private double Eilera(double x0, double h1, int n1, double y,int N)
        {
            int i;
            double y1;
            double s, x;
            double yg = y;
            x = x0; 
           
            List<dynamic> result = new List<dynamic>();
            resultEilera.ItemsSource = result;
            for (i = 0; i < n1; i++)
            {
               
                y1 = yg + h1 * (x / N + 2 * yg * x * Math.Log(yg));
                //Заполнение таблицы resultEilera данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N1"),
                    yg = yg.ToString("N4"),
                    y1 = y1.ToString("N5"),
                    
                });
                yg = y1;
                x += h1;
            }
            s = yg;
            Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
            resultElleraText.Text = s.ToString();
            resultElleraText.Background = brush;
            return s;
        }

        //Уточненное решение с помощью формулы Рунге
        private double Runge(double x0, double h2, int n2,double y, int N)
        {
            int i;
            double y1;
            double s, x;
            double yg = y;
            List<dynamic> result = new List<dynamic>();
            resultRunge.ItemsSource = result;
            x = x0;
            

            for (i = 0; i < n2; i++)
            {
                y1 = yg + h2 * (x / N + 2 * yg * x * Math.Log(yg));
                
                //Заполнение таблицы resultRunge данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N2"),
                    yg = yg.ToString("N4"),
                    y1 = y1.ToString("N5"),

                });
                yg = y1;
                x += h2;
            }

             s = yg;
             return s;
        }
        //Улучшенный метод Эйлера
        private double Uluchshennii_metod_Eilera(double x0, double h1, int n1,double y,int N)
        {
            int i;
            double y1;
            double s, x, y2, x2;
            double yg = y;
            List<dynamic> result = new List<dynamic>();
            resultUlEilera.ItemsSource = result;
            x = x0;
            x2 = x + h1 / 2;
            y2 = yg + (h1/2) * (x / N + 2 * yg * x * Math.Log(yg));


            for (i = 0; i < 2 * n1; i++)
            {

                y1 = yg + h1 * (x2 / N + 2 * y2 * x2 * Math.Log(y2));
              

                //Заполнение таблицы resultUlEilera данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N1"),
                    yg = yg.ToString("N4"),
                    y1 = y1.ToString("N5"),

                });
                yg = y1;
                x += h1;
            }
                       
            s = yg;
            Brush brush = new SolidColorBrush(Color.FromRgb(187, 243, 154));
            resultUlEileraText.Text = s.ToString();
            resultUlEileraText.Background = brush;
            return s;
        }
        //метод Рунге-Кутта 4 порядка
        private double Runge_Kutte4(double x0,  double h1, int n1, double y, int N)
        {
            double k1, k2, k3, k4;
            int i;
            double y1;
            double s, x;
            double yg = y;
            List<dynamic> result = new List<dynamic>();
            resultRungeKutt.ItemsSource = result;
            x = x0;
            

            for (i = 0; i < n1; i++)
            {
                k1 =  h1 * (x / N + 2 * yg * x * Math.Log(yg));
                k2 = h1 * ((x + (h1 / 2))/ N + 2 * (yg + (k1 / 2)) * (x + (h1 / 2)) * Math.Log((yg + (k1 / 2))));
                k3 = h1 * ((x + (h1 / 2)) / N + 2 * (yg + (k2 / 2)) * (x + (h1 / 2)) * Math.Log((yg + (k2 / 2)))); ;
                k4 = h1 * ((x + h1) / N + 2 * (yg + k3) * (x + h1) * Math.Log((yg + k3))); ;
                y1 = yg + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                
                //Заполнение таблицы resultRungeKutt данными
                result.Add(new
                {
                    Итерация = i.ToString("N0"),
                    x = x.ToString("N1"),
                    yg = yg.ToString("N4"),
                    y1 = y1.ToString("N5"),

                });
                yg = y1;
                x += h1;
            }

            s = yg;
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

        private void Ntext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Ntext.Text))
            {
                Ntext.Background = Brushes.Red;

            }
            else
            {
                Ntext.Background = Brushes.White;
            }
        }
    }



}
