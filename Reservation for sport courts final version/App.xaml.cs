using MySql.Data.MySqlClient;
using Reservation_for_sport_courts_final_version.service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reservation_for_sport_courts_final_version
{
    public partial class App : Application
    {
        public static MySqlConnector sql{ get; private set; }
        
        App()
        {
            sql = new MySqlConnector();
            sql.openConnection();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            sql.closeConnection();
            base.OnExit(e);
        }
    }
}
