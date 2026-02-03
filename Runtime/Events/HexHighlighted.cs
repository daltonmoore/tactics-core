using TacticsCore.EventBus;
using TacticsCore.HexGrid;

namespace TacticsCore.Events
{
    public struct HexHighlighted : IEvent
    {
        public PathNodeHex PathNodeHex;

        public HexHighlighted(PathNodeHex pathNodeHex)
        {
            PathNodeHex = pathNodeHex;
        }
    }
}