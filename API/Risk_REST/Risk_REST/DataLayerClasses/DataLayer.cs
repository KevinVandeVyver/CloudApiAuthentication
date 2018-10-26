﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Risk_REST.Models;

namespace Risk_REST.DataLayerClasses
{
    public class DataLayer
    {
        SqlConnection connection;

        public DataLayer(IConfiguration configuration)
        {
            String connectionString = configuration.GetConnectionString("localDB");
            connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Player> getPlayer(long ID)
        {
            List<Player> playerList = new List<Player>();

            SqlCommand command;

            if (ID == 0)
            {
                command = new SqlCommand("SELECT * FROM dbo.Players", connection);
            }

            else
            {
                command = new SqlCommand("SELECT * FROM dbo.Players WHERE PlayerId = " + ID, connection);
            }

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Player player = new Player(Convert.ToInt64(reader["PlayerId"]), reader["PlayerUsername"].ToString(), reader["PlayerEmail"].ToString(), reader["PlayerPassword"].ToString(), reader["PlayerTitle"].ToString(), Convert.ToInt32(reader["PlayerTroops"]), Convert.ToInt32(reader["PlayerReserveTroops"]), Convert.ToInt32(reader["PlayerLevel"]), Convert.ToInt32(reader["PlayerExp"]), Convert.ToInt32(reader["PlayerSilverCoins"]));
                    playerList.Add(player);
                }
            }
            connection.Close();
            return playerList;
        }

    }
}
