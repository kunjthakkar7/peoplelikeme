using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using panorama.Resources;
using System.Windows.Media;
using SQLite;
using Windows.Storage;
using System.IO;

namespace panorama
{
    public sealed class EventType_db
    {
        /// <summary>
        /// You can create an integer primary key and let the SQLite control it.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
    public sealed class Event_db
    {
        /// <summary>
        /// You can create an integer primary key and let the SQLite control it.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int host_id { get; set; }

        public string Type { get; set; }

        public string date { get; set; }
        
        public int cost { get; set; }

        public string description { get; set; }

        public string location { get; set; }

   //     public string pic { get; set; }
    }

    public sealed class member_db
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string name { get; set; }

        public long number { get; set; }

        public string email { get; set; }
    }

    public sealed class my_event_db
    {
        public int event_id { get; set; }

        public int user_id { get; set; }
    }
}
