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

            switch (command.TargetType)
            {
                case EntityType.Region:
                {
                    var region = _serviceProvider.GlobalState.World.Regions
                        .Find(region => region.Id == (EntityId<Region>)command.TargetId);
                    region.Modifiers.Add(modifier);
                    // TODO: check if it's production modifier
                    region.ComputeProduction = true;
                    break;
                }
                case EntityType.Empire:
                {
                    var empire = _serviceProvider.GlobalState.World.Empires
                        .Find(empire => empire.Id == (EntityId<Empire>)command.TargetId);
                    empire.Modifiers.Add(modifier);
                    // TODO: check if it's production modifier
                    empire.ComputeProduction = true;
                    break;
                }
            }
        }
    }
}