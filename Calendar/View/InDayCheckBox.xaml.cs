using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Calendar.View
{
    public partial class InDayCheckBox : UserControl
    {
        private double originalFontSize;

        public InDayCheckBox()
        {
            InitializeComponent();
            originalFontSize = label.FontSize; // Сохраняем исходный размер шрифта
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            // Анимация для картинки
            if (checkBox.IsChecked == true)
            {
                // Увеличение картинки
                ScaleTransform scaleTransform = new ScaleTransform(1.4, 1.4); // Увеличиваем
                image.RenderTransform = scaleTransform;
            }
            else
            {
                // Возвращение к исходному размеру
                ScaleTransform scaleTransform = new ScaleTransform(1, 1);
                image.RenderTransform = scaleTransform;
            }

            // Анимация для текста
            DoubleAnimation fontSizeAnimation = new DoubleAnimation();
            fontSizeAnimation.Duration = TimeSpan.FromSeconds(0.2); // Длительность анимации
            fontSizeAnimation.From = label.FontSize; // Начальный размер шрифта

            if (checkBox.IsChecked == true)
            {
                fontSizeAnimation.To = originalFontSize * 1.4; // Увеличиваем размер шрифта при выборе флажка
            }
            else
            {
                fontSizeAnimation.To = originalFontSize; // Возвращаем к исходному размеру при снятии флажка
            }

            label.BeginAnimation(Label.FontSizeProperty, fontSizeAnimation); // Применяем анимацию к свойству FontSize
        }
    }
}
