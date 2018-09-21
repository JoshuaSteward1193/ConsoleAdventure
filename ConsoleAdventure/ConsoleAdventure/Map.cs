﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Map
    {
        public string Name { get; set; }
        public Tile[,] TerrainData { get; set; }
        public List<Coordinate> SpawnPoints = new List<Coordinate>();

        public Map(string name, Tile[,] terrain, Coordinate spawn)
        {
            Name = name;
            TerrainData = terrain;
            SpawnPoints.Add(spawn);
        }
    }
}