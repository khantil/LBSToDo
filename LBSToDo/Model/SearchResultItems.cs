using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBSToDo.Model
{
    class SearchResultItems : System.Collections.IEnumerable 
    {
        public System.Collections.Generic.List<SearchResultItem> Items { get; private set; }

               public void AddSearchResultItem(SearchResultItem foodItem)
                {
	                Items.Add(foodItem);
                }

               public System.Collections.IEnumerator GetEnumerator()
               {
                   return this.Items.GetEnumerator();
               }
    }

    class SearchResultItem
    {
     

                public string Result { get; private set; }
                public double Latitude { get; private set; }
                public double Longitude { get; private set; }

                public SearchResultItem(string result, double latitude, double longitude)
                {
                    Result = result;
                    Latitude = latitude;
                    Longitude = longitude;

                }

              
    }
}
