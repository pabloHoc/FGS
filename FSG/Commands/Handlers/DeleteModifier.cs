using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class DeleteModifier : CommandHandler<Commands.DeleteModifier>
    {
        public DeleteModifier(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.DeleteModifier command)
        {
            bool CommandPayloadModifier(Modifier modifier) => 
                modifier.Name == command.ModifierName && 
                modifier.ModifierType == command.ModifierType && 
                modifier.SourceId.Equals(command.SourceId) &&  // Equals is necessary due to boxing
                modifier.Value == command.Value;

            switch (command.TargetType)
            {
                case EntityType.Region:
                {
                    var region = _serviceProvider.GlobalState.World.Regions
                        .Find(region => region.Id == (EntityId<Region>)command.TargetId);
                    var modifier = region.Modifiers.Find(CommandPayloadModifier);
                    region.Modifiers.Remove(modifier);
                    // TODO: check if it's production modifier
                    region.ComputeProduction = true;
                    break;
                }
                case EntityType.Empire:
                {
                    var empire = _serviceProvider.GlobalState.World.Empires
                        .Find(empire => empire.Id == (EntityId<Empire>)command.TargetId);
                    var modifier = empire.Modifiers.Find(CommandPayloadModifier);
                    empire.Modifiers.Remove(modifier);
                    // TODO: check if it's production modifier
                    empire.ComputeProduction = true;
                    break;
                }
            }
        }
    }
}