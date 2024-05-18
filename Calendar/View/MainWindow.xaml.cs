using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Media;

namespace Calendar.View
{
    public class MainButtonArgs : EventArgs
    {
        public readonly Page? ActivePage;
        public MainButtonArgs(Page? page) => ActivePage = page; 
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, string> months = new Dictionary<int, string>()
        {
            {1, "Январь"},
            {2, "Февраль"},
            {3, "Март"},
            {4, "Апрель"},
            {5, "Май"},
            {6, "Июнь"},
            {7, "Июль"},
            {8, "Август"},
            {9, "Сентябрь"},
            {10, "Октябрь"},
            {11, "Ноябрь"},
            {12, "Декабрь"},
        };

        private List<Page> pages = new List<Page>()
        {
            new DayPage(),
            new MountPage(DateTime.Now)
        };

        private DateTime currentDate;

        public static EventHandler<MainButtonArgs>? OnLeftButtonPressed;
        public static EventHandler<MainButtonArgs>? OnRightButtonPressed;
        public static EventHandler? OnDayPageOpened;

        public MainWindow()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            dateLabel.Content = GetMonthYearFormat(currentDate);
            rightButton.Content = "-->";
            leftButton.Content = "<--";
            frame.Content = pages[1];

            MountPage.OnButtonOfDayPressed += MainWindow_OnButtonOfDayPressed;
            MountPage.OnNextMonthChanged += MainWindow_OnNextMonthChanged;
            MountPage.OnPrevMonthChanged += MainWindow_OnPrevMonthChanged;

            DayPage.OnConcreteDaySelected += MainWindow_OnConcreteDaySelected;
            DayPage.OnDayPageClosed += MainWindow_OnDayPageClosed;
        }
        
        private void MainWindow_OnConcreteDaySelected(object? sender, DateArgs e)
        {
            dateLabel.Content = e.Date.Day + " " + months[e.Date.Month] + " " + e.Date.Year;
        }

        private void MainWindow_OnPrevMonthChanged(object? sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            dateLabel.Content = GetMonthYearFormat(currentDate);
        }

        private void MainWindow_OnNextMonthChanged(object? sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            dateLabel.Content = GetMonthYearFormat(currentDate);
        }

        private string GetMonthYearFormat(DateTime dateTime) => months[dateTime.Month] + " " + dateTime.Year;

        private void MainWindow_OnButtonOfDayPressed(object? sender, EventArgs e)
        {
            rightButton.Content = "save";
            frame.Content = pages[0];
            OnDayPageOpened?.Invoke(this, EventArgs.Empty);
        }

        private void MainWindow_OnDayPageClosed(object? sender, EventArgs e)
        {
            rightButton.Content = "-->";
            dateLabel.Content = GetMonthYearFormat(currentDate);
            frame.Content = pages[1];
        }
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            if (frame.Content is Page)
            {
                OnLeftButtonPressed?.Invoke(this, new MainButtonArgs(frame.Content as Page));
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (frame.Content is Page)
            {
                OnRightButtonPressed?.Invoke(this, new MainButtonArgs(frame.Content as Page));
            }
        }
    }
}
