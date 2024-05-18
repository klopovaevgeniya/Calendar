namespace Calendar.Model
{
    public class Days : Singleton
    {
        public Days Instance => Instance<Days>();
        public Dictionary<DateTime, DayInfo> DaysDictionary { get; private set; }

        private Days() 
        {
            DaysDictionary = new Dictionary<DateTime, DayInfo>();
        }
    }
}
