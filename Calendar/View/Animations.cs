using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Calendar.View
{
    public class Animations : Singleton
    {
        public static Animations Instance = Instance<Animations>();
        private Animations() { }

        public async Task DayButtonAnimation(DayButton dayButton)
        {
            //анимация цвета кнопки дня 
            ColorAnimation colorAnimation = new ColorAnimation();
            var dayButtonColor = ((SolidColorBrush)dayButton.Background).Color;
            dayButton.Background = ((SolidColorBrush)dayButton.Background).Clone();
            colorAnimation.To = Colors.Yellow;
            colorAnimation.Duration = TimeSpan.FromSeconds(0.2);
            dayButton.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            await Task.Run(() => Thread.Sleep(200));
            dayButton.Background = new SolidColorBrush(dayButtonColor);
        }
    }
}
