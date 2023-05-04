using System;
using System.ComponentModel;

public class Date : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int year
    {
        get
        {
            return _year;
        }
        private set
        {
            _year = value;
        }
    }

    public int month
    {
        get
        {
            return _month;
        }
        private set
        {
            _month = value;
            if (_month > 4)
            {
                year += 1;
                _month = 1;
            }
        }
    }

    public int day
    {
        get
        {
            return _day;
        }
        private set
        {
            if (_day == value)
            {
                return;
            }

            _day = value;
            if (_day > 30)
            {
                month += 1;
                _day = 1;

                //SendMessage(new MESSAGE_MONTH_INC(year, month));
            }
        }
    }

    public int hour
    {
        get
        {
            return _hour;
        }
        internal set
        {
            if (_hour == value)
            {
                return;
            }

            _hour = value;
            if (_hour > 24)
            {
                day += 1;
                _hour = 0;
            }
        }
    }

    //public Action<MESSAGE> SendMessage { get; set; }

    private int _year;
    private int _month;
    private int _day;
    private int _hour;

    public Date()
    {
        year = 1;
        month = 1;
        day = 1;
        hour = 7;
    }

    public void DaysInc()
    {
        day++;
    }
}
