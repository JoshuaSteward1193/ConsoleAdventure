using System.Collections.Generic;


namespace ConsoleAdventure
{
    class Map
    {
        public string Name { get; set; }
        public Tile[,] TerrainData { get; set; }
        public List<Coordinate> SpawnPoints = new List<Coordinate>();
        public List<Character> AICharacters = new List<Character>();

        public Map(string name, Tile[,] terrain, Coordinate spawn)
        {
            Name = name;
            TerrainData = terrain;
            SpawnPoints.Add(spawn);
        }
    }
}
