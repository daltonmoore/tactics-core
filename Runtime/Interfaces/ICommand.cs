using TacticsCore.Commands;

namespace TacticsCore.Interfaces
{
    public interface ICommand
    {
        bool CanHandle(CommandContext context);
        void Handle(CommandContext context);
    }
}