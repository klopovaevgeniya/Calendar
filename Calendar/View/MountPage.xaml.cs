using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Calendar.View
{
    public class DateArgs : EventArgs
    {
        public readonly DateTime Date;
        public DateArgs(DateTime date) => Date = date;
    }

    /// <summary>
    /// Логика взаимодействия для MountPage.xaml
    /// </summary>
    public partial class MountPage : Page
    {
        public static EventHandler<DateArgs>? OnButtonOfDayPressed;
        public static EventHandler? OnNextMonthChanged;
        public static EventHandler? OnPrevMonthChanged;
        
        private DateTime currentDate;
        public MountPage(DateTime dateTime)
        {
            InitializeComponent();
            currentDate = dateTime;
            InitButtonsOfDate(GetDaysInCurrentMonth(currentDate));
            
            MainWindow.OnLeftButtonPressed += MountPage_OnLeftButtonPressed;
            MainWindow.OnRightButtonPressed += MountPage_OnRightButtonPressed;
        }

        private void MountPage_OnRightButtonPressed(object? sender, MainButtonArgs e)
        {
            if (e.ActivePage is MountPage)
            {
                currentDate = currentDate.AddMonths(1);
                InitButtonsOfDate(GetDaysInCurrentMonth(currentDate));
                OnNextMonthChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void MountPage_OnLeftButtonPressed(object? sender, MainButtonArgs e)
        {
            if (e.ActivePage is MountPage)
            {
                currentDate = currentDate.AddMonths(-1);
                InitButtonsOfDate(GetDaysInCurrentMonth(currentDate));
                OnPrevMonthChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int GetDaysInCurrentMonth(DateTime currentDate)
        {
            DateTime firstDayOfNextMonth = currentDate.AddMonths(1).AddDays(-currentDate.Day + 1);
            DateTime lastDayOfCurrentMonth = firstDayOfNextMonth.AddDays(-1);
            return lastDayOfCurrentMonth.Day;
        }

        private void InitButtonsOfDate(int daysInCurrentMonth)
        {
            grid.Children.Clear();
            for (int i = 0; i < daysInCurrentMonth; i++)
            {
                DayButton dayButton = new DayButton();
                dayButton.Name = "_" + (i + 1).ToString();
                
                dayButton.dateLable.Content = (i + 1).ToString();
                dayButton.MouseDown += ButtonOfDate_Click;
                dayButton.Margin = new Thickness(5);
                //dayButton.image.Source = 
                //    DaysController.Instance.GetImageFromDate(new DateTime(currentDate.Year, currentDate.Month, i + 1));

                Grid.SetRow(dayButton, i / 6);
                Grid.SetColumn(dayButton, i % 6);
                grid.Children.Add(dayButton);
            }
        }

        private async void ButtonOfDate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is DayButton dayButton)
            {
                await Animations.Instance.DayButtonAnimation(dayButton);
                var day = Convert.ToInt32((dayButton).Name.Substring(1));
                OnButtonOfDayPressed?.Invoke(
                    this,
                    new DateArgs(new DateTime(currentDate.Year, currentDate.Month, day)));
            }
        }

        internal DateTime GetPageDate()
        {
            throw new NotImplementedException();
        }
    }
}
