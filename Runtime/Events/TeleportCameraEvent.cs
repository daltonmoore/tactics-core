using TacticsCore.Commands;
using TacticsCore.EventBus;
using UnityEngine;

namespace TacticsCore.Events
{
    public struct TeleportCameraEvent : IEvent
    {
        public Vector3 Position { get; }
        
        public TeleportCameraEvent(Vector3 position)
        {
            Position = position;
        }
    }
}