using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace LBSToDo.Model
{
    public class ToDoDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public ToDoDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a table for the to-do items.
        public Table<LocationItem> LocationItems;

        // Specify a table for the categories.
     //   public Table<ToDoCategory> Categories;
    }

    [Table]
    public class LocationItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

        //
        // TODO: Add columns and associations, as applicable, here.
        //

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion


        // Define ID: private field, public property, and database column.
        private int _toDoItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get { return _toDoItemId; }
            set
            {
                if (_toDoItemId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    _toDoItemId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _locationName;

        [Column]
        public string LocationName
        {
            get { return _locationName; }
            set
            {
                if (_locationName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _locationName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        private string _locationAddress;

        [Column]
        public string LocationAddress
        {
            get { return _locationAddress; }
            set
            {
                if (_locationAddress != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _locationAddress = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private int _locationRange;
        [Column]
        public int LocationRange
        {
            get { return _locationRange; }
            set
            {
                if (_locationRange != value)
                {
                    NotifyPropertyChanging("LocationRange");
                    _locationRange = value;
                    NotifyPropertyChanged("LocationRange");
                }
            }
        }

        private double _locationLatitude;
        [Column]
        public double LocationLatitude
        {
            get { return _locationLatitude; }
            set
            {
                if (_locationLatitude != value)
                {
                    NotifyPropertyChanging("LocationRange");
                    _locationLatitude = value;
                    NotifyPropertyChanged("LocationRange");
                }
            }
        }

        private double _locationLongitude;
        [Column]
        public double LocationLongitude
        {
            get { return _locationLongitude; }
            set
            {
                if (_locationLongitude != value)
                {
                    NotifyPropertyChanging("LocationRange");
                    _locationLongitude = value;
                    NotifyPropertyChanged("LocationRange");
                }
            }
        }
    }
}
