using UnityEngine;
using UnityEngine.Tilemaps;

namespace TacticsCore.HexGrid
{
    [CreateAssetMenu(menuName = "Tiles/TacticsTile")]
    public class TacticsTile : Tile
    {
        [SerializeField] public TerrainType terrainType;
    }
}
