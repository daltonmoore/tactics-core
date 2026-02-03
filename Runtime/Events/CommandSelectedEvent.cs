using TacticsCore.Commands;
using TacticsCore.EventBus;

namespace TacticsCore.Events
{
    public struct CommandSelectedEvent : IEvent
    {
        public BaseCommand Command { get; }
        
        public CommandSelectedEvent(BaseCommand command)
        {
            Command = command;
        }
    }
}