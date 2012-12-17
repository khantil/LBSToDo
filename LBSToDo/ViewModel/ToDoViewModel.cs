using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBSToDo.Model;


namespace LBSToDo.ViewModel
{
    public class ToDoViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private ToDoDataContext toDoDB;

        // Class constructor, create the data context object.
        public ToDoViewModel(string toDoDBConnectionString)
        {
            toDoDB = new ToDoDataContext(toDoDBConnectionString);
        }

        //
        // TODO: Add collections, list, and methods here.
        //

        // All to-do items.
        private ObservableCollection<LocationItem> _allLocationItems;
        public ObservableCollection<LocationItem> AllLocationItems
        {
            get { return _allLocationItems; }
            set
            {
                _allLocationItems = value;
                NotifyPropertyChanged("AllLocationItems");
            }
        }


        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            toDoDB.SubmitChanges();
        }


        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {
            // Specify the query for all to-do items in the database.
            var toDoItemsInDB = from LocationItem location in toDoDB.LocationItems
                                select location;

            // Query the database and load all to-do items.
            AllLocationItems = new ObservableCollection<LocationItem>(toDoItemsInDB);

        }

        public void AddToDoItem(LocationItem newLocationItem)
        {
            // Add a to-do item to the data context.
            toDoDB.LocationItems.InsertOnSubmit(newLocationItem);

            // Save changes to the database.
            toDoDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            AllLocationItems.Add(newLocationItem);
        
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }
}
