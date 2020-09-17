using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SampleWPFApp
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int canvasHeight = 700;
            int canvasWidth = 500;
            System.Drawing.Bitmap bitmapSource = new Bitmap(canvasWidth, canvasHeight);

            Graphics gr = Graphics.FromImage(bitmapSource);

            //create and fill canvas
            Color backGroundColor = Color.FromArgb(245, 245, 245);
            gr.FillRectangle(new SolidBrush(backGroundColor), new Rectangle(0, 0, canvasWidth, canvasHeight));

            // нарисовать диаграмму Гантта
            {
                // draw axises
                int leftBorderOfAxis = 20;
                const int bottomBorderOfAxis = 470;
                const int rightBorderOfAxis = 480;
                const int topBorderOfAxis = 60;
                gr.DrawLine(new Pen(Color.Black, 2), leftBorderOfAxis, bottomBorderOfAxis, rightBorderOfAxis, bottomBorderOfAxis);
                gr.DrawLine(new Pen(Color.Black, 2), leftBorderOfAxis, topBorderOfAxis, leftBorderOfAxis, bottomBorderOfAxis);

                // нарисовать заголовок
                gr.DrawString("Диаграмма Гантта", new Font(System.Drawing.FontFamily.GenericSansSerif, 32), new SolidBrush(Color.Black), 35, 5);

                // нарисовать вспомогательные линии
                var pen = new Pen(Color.Black, 1) { DashPattern = new float[] { 5, 10 } };
                gr.DrawLine(pen, 70, bottomBorderOfAxis, 70, topBorderOfAxis);
                gr.DrawLine(pen, 120, bottomBorderOfAxis, 120, topBorderOfAxis);

                // нарисовать отметки на оси Х
                pen = new Pen(Color.Black, 2);
                const int bottomOfAxisXMark = bottomBorderOfAxis + 5;
                gr.DrawLine(pen, leftBorderOfAxis + 25, bottomOfAxisXMark, leftBorderOfAxis + 25, bottomBorderOfAxis);
                gr.DrawLine(pen, leftBorderOfAxis + 50, bottomOfAxisXMark, leftBorderOfAxis + 50, bottomBorderOfAxis);
                gr.DrawLine(pen, leftBorderOfAxis + 75, bottomOfAxisXMark, leftBorderOfAxis + 75, bottomBorderOfAxis);
                gr.DrawLine(pen, leftBorderOfAxis + 100, bottomOfAxisXMark, leftBorderOfAxis + 100, bottomBorderOfAxis);

                // нарисовать подписи отметок на оси Х
                gr.DrawString("10", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 35, 475);
                gr.DrawString("20", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 60, 475);
                gr.DrawString("30", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 85, 475);
                gr.DrawString("40", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 110, 475);

                // нарисовать интервалы
                {
                    // нарисовать интервалы одного графика
                    {
                        gr.FillRectangle(new SolidBrush(Color.Blue), 30, 400, 30, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 30, 400, 30, 10);
                    }

                    // нарисовать интервалы второго графика
                    {
                        // нарисовать обычный интервал
                        gr.FillRectangle(new SolidBrush(Color.Green), 30, 440, 30, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 30, 440, 30, 10);

                        // нарисовать второй обычный интервал
                        gr.FillRectangle(new SolidBrush(Color.Green), 70, 440, 33, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 70, 440, 33, 10);

                        // нарисовать третий интервал отличный от обычного 
                        gr.FillRectangle(new HatchBrush(HatchStyle.ForwardDiagonal, Color.Black, Color.Green), 109, 440, 33, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 109, 440, 33, 10);
                    }
                }

                //нарисовать легенду
                {
                    //нарисовать легенду первого графика
                    {
                        //нарисовать пример обозначения из первого графика 
                        gr.FillRectangle(new SolidBrush(Color.Blue), 30, 500, 30, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 30, 500, 30, 10);

                        //нарисовать комментарий 
                        gr.DrawString("интервал 1-го графика", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 70, 497);
                    }

                    //нарисовать легенду второго графика
                    {
                        //нарисовать пример обозначения из второго графика 
                        gr.FillRectangle(new SolidBrush(Color.Green), 30, 515, 30, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 30, 515, 30, 10);

                        //нарисовать комментарий 
                        gr.DrawString("интервал 2-го графика", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 70, 511);

                        //нарисовать пример обозначения из второго графика 
                        gr.FillRectangle(new HatchBrush(HatchStyle.ForwardDiagonal, Color.Black, Color.Green), 230, 515, 30, 10);
                        gr.DrawRectangle(new Pen(Color.Black), 230, 515, 30, 10);

                        //нарисовать комментарий 
                        gr.DrawString("интервал* 2-го графика", new Font(System.Drawing.FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Black), 270, 511);
                    }
                }
            }
            var bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmapSource.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
            }
            image1.Source = bitmapImage;
        }
    }
}
