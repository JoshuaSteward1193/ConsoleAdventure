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
        public List<Tile> GetNeighbors(int y, int x)
        {
            List<Tile> Neighbors = new List<Tile>();
            if (y > 0)
            {
                Neighbors.Add(TerrainData[y - 1, x]); //North
            }
            if (y < Program.currentMap.TerrainData.GetLength(0) - 1)
            {
                Neighbors.Add(TerrainData[y + 1, x]); //South
            }
            if (x < Program.currentMap.TerrainData.GetLength(1) - 1)
            {
                Neighbors.Add(TerrainData[y, x + 1]); //East
            }
            if (x > 0)
            {
                Neighbors.Add(TerrainData[y, x - 1]); //West
            }
            return Neighbors;
        }
    }
}
