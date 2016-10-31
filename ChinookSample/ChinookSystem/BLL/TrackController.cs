using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject] 
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> ListTracks()
        {
            using (var context = new ChinookContext())
            {
                //return all records all attributes
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track GetTracks(int trackId)
        {
            using (var context = new ChinookContext())
            {
                //return a record all attributes
                return context.Tracks.Find(trackId);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                //any business rules 

                //any data refinements
                //review of using iif
                //composer can be a null string 
                //we do not wish to store a empty string 
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ?
                                        null : trackinfo.Composer;

                //add the instance of trackinfo to the database
                context.Tracks.Add(trackinfo);

                //commit of the transaction
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                //any business rules 

                //any data refinements
                //review of using iif
                //composer can be a null string 
                //we do not wish to store a empty string 
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ?
                                        null : trackinfo.Composer;

                //Update the existing instance of trackinfo on the database
                context.Entry(trackinfo).State = System.Data.Entity.EntityState.Modified;

                //commit of the transaction
                context.SaveChanges();
            }
        }
        //the delete is an overloaded method technique
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTrack(Track trackinfo)
        {
            DeleteTrack(trackinfo.TrackId);
        }

        public void DeleteTrack(int trackId)        //In this method we do the actul delete we only realy need the primary key (overloaded method)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //no refine method (due to it being removed)
                //do the delete
                //find the existing record on the database
                var existing = context.Tracks.Find(trackId);
                //delete the record from the database
                context.Tracks.Remove(existing);
                //commit the transaction
                context.SaveChanges();
            }
        }
    }
}
