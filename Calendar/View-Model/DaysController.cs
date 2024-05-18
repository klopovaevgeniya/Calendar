using Calendar.Model;
using System;
using System.Collections.Generic;

public class DaysController
{
    private static DaysController? instance;
    public static DaysController Instance => instance ??= new DaysController();

    private Dictionary<DateTime, DayInfo> dayData = new();

    public List<bool> GetChecksFromDate(DateTime date)
    {
        if (dayData.TryGetValue(date, out var dayInfo))
        {
            return dayInfo.Checks;
        }
        return new List<bool> { false, false, false, false, false, false, false };
    }

    public void Save(DateTime date, List<bool> checks)
    {
        if (dayData.ContainsKey(date))
        {
            dayData[date].Checks = checks;
        }
        else
        {
            dayData[date] = new DayInfo { Checks = checks };
        }
    }

    public void Serialize(string filePath)
    {
        DayInfo.Serialize(dayData, filePath);
    }

    public void Deserialize(string filePath)
    {
        dayData = DayInfo.Deserialize(filePath);
    }
}
