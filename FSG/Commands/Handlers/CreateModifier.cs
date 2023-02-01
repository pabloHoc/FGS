using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateModifier : CommandHandler<Commands.CreateModifier>
    {
        public CreateModifier(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateModifier command)
        {
            var modifier = new Modifier
            {
                Id = new EntityId<Modifier>(),
                Name = command.ModifierName,
                ModifierType = command.ModifierType,
                Value = command.Value,
                TargetId = command.TargetId,
                SourceId = command.SourceId,
                RemainingTurns = command.Duration
            };

            if (command.TargetType == EntityType.Region)
            {
                var region = _serviceProvider.GlobalState.World.Regions
                    .Find(region => region.Id == (EntityId<Region>)command.TargetId);
                region.Modifiers.Add(modifier);
            }

            if (command.TargetType == EntityType.Empire)
            {
                var region = _serviceProvider.GlobalState.World.Empires
                    .Find(empire => empire.Id == (EntityId<Empire>)command.TargetId);
                region.Modifiers.Add(modifier);
            }
        }
    }
}