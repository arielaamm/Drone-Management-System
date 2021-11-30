using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
namespace DAL
{

        public class DalObject
        {
            static Random random = new Random();
            public static void AddStation(string name, int num)
            {
                Station s = new Station();
                s.StationName = (string)name;
                s.ChargeSlots = num;
                s.ID = 11111111 + DataSource.staticId;
                s.Longitude = random.NextDouble() * (33.289273 - 29.494665) + 29.494665;
                s.Lattitude = random.NextDouble() * (35.569495 - 34.904675) + 34.904675;
                DataSource.staticId++;
                DataSource.stations.Add(s);
            }
        }
    
}