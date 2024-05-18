using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Calendar.View
{
    /// <summary>
    /// Логика взаимодействия для DayPage.xaml
    /// </summary>
    public partial class DayPage : Page
    {
        public static EventHandler<DateArgs>? OnConcreteDaySelected;
        public static EventHandler? OnDayPageClosed;

        private DateTime currentDate;
        private const string DataFilePath = "dayData.json";

        public DayPage()
        {
            InitializeComponent();
            InitInDayCheckBoxes();

            MountPage.OnButtonOfDayPressed += DayPage_OnButtonOfDayPressed;

            MainWindow.OnLeftButtonPressed += DayPage_OnLeftButtonPressed;
            MainWindow.OnRightButtonPressed += DayPage_OnRightButtonPressed;
            MainWindow.OnDayPageOpened += DayPage_OnDayPageOpened;

            LoadData();
        }

        private void DayPage_OnDayPageOpened(object? sender, EventArgs e)
        {
            List<bool> checks = DaysController.Instance.GetChecksFromDate(currentDate);

            for (int i = 0; i < 7; i++)
            {
                var element = FindName($"_{i}InDayCheckBox");

                if (element != null && element is InDayCheckBox inDayCheckBox)
                {
                    inDayCheckBox.checkBox.IsChecked = checks[i];
                }
                else
                {
                    throw new Exception("Элемент не найден или имеет неправильный тип");
                }
            }
        }

        private void InitInDayCheckBoxes()
        {
            _0InDayCheckBox.label.Content = "Бег 100 метров";
            _0InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/бег 100 м.png", UriKind.Relative));

            _1InDayCheckBox.label.Content = "Бег 1 километр";
            _1InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/бег 1км.png", UriKind.Relative));

            _2InDayCheckBox.label.Content = "Отжимания";
            _2InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/отжимания.png", UriKind.Relative));

            _3InDayCheckBox.label.Content = "Подтягивания";
            _3InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/подтягивания.png", UriKind.Relative));

            _4InDayCheckBox.label.Content = "Приседания";
            _4InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/приседания.png", UriKind.Relative));

            _5InDayCheckBox.label.Content = "Растяжка";
            _5InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/растяжка.png", UriKind.Relative));

            _6InDayCheckBox.label.Content = "Пресс";
            _6InDayCheckBox.image.Source = new BitmapImage(new Uri("../../../../Images/пресс.png", UriKind.Relative));
        }

        // Сохранение данных о текущей дате при переходе вправо
        private void DayPage_OnRightButtonPressed(object? sender, MainButtonArgs e)
        {
            if (e.ActivePage is DayPage)
            {
                List<bool> checks = new();
                for (int i = 0; i < 7; i++)
                {
                    var element = FindName($"_{i}InDayCheckBox");
                    if (element != null && element is InDayCheckBox inDayCheckBox)
                    {
                        checks.Add(inDayCheckBox.checkBox.IsChecked ?? false);
                    }
                }
                DaysController.Instance.Save(currentDate, checks);
                SaveData();
            }
        }

        // Возврат на страницу с месяцами и датами
        private void DayPage_OnLeftButtonPressed(object? sender, MainButtonArgs e)
        {
            if (e.ActivePage is DayPage)
            {
                OnDayPageClosed?.Invoke(this, EventArgs.Empty);
            }
        }

        private void DayPage_OnButtonOfDayPressed(object? sender, DateArgs e)
        {
            currentDate = e.Date;
            OnConcreteDaySelected?.Invoke(this, new DateArgs(currentDate));
        }

        private void SaveData()
        {
            DaysController.Instance.Serialize(DataFilePath);
        }

        private void LoadData()
        {
            DaysController.Instance.Deserialize(DataFilePath);
        }
    }
}
