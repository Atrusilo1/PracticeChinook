using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespace
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.DAL
{
    [DataObject]
    public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Artist> Artist_listAll()
        {
            using (var context = new ChinookContext())
            {
                return context.Artists.ToList();
            }
        }
        //Report a datasset containing data from
        //multiple entites
        //this will use Linq to entity access 
        //POCO class will be used to define the data
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ArtistAlbums> ArtistAlbums_Get()
        {
            //setup transaction area
            using (var context = new ChinookContext())
            {
                //when you bring your query from LinqPad
                //to your program you must change the 
                //reference(s) to the data source

                //You may also need to change your 
                //navigation referncing use un LinqPad
                //to the navigation properties you stated 
                //in the entity class definitions
                var result = from x in context.Albums //Needed to change this from Alums to context.Albums
                             where x.ReleaseYear == 2008
                             orderby x.Artists.Name, x.Title //needed to change this from Artist to Artists
                             select new ArtistAlbums //The POCO class
                             {
                                 //Name and Title are POCO
                                 //class property names
                                 Name = x.Artists.Name,    //needed to change this from Artist to Artists
                                 Title = x.Title
                             };
                //the following requires the query data in memory
                //.ToList()
                //At this point the query will actually execute
                return result.ToList();
            }
        }

    }
}
