using Dynamo;
using Dynamo.Connectors;
using Dynamo.Nodes;
using Microsoft.FSharp.Collections;

using Value = Dynamo.FScheme.Value;

namespace DynamoXNA.Nodes
{
    [NodeName("New Game")]
    [NodeCategory("XNA.Game")]
    [NodeDescription("Starts a new XNA game with the given initial world, update, and drawing functions.")]
    public class NewGame : dynNodeWithOneOutput
    {
        public NewGame()
        {
            InPortData.Add(new PortData("update", "Update loop: f(world, tick)", null));
            InPortData.Add(new PortData("draw", "Draw loop: f(world, spriteBatch)", null));
            InPortData.Add(new PortData("world0", "Initial World", null));
            InPortData.Add(new PortData("width", "Width of the game world (pixels)", null));
            InPortData.Add(new PortData("height", "Height of the game world (pixels)", null));

            OutPortData.Add(new PortData("", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            var update = ((Value.Function)args[0]).Item;
            var draw = ((Value.Function)args[1]).Item;
            var world0 = args[2];
            var w = (int)((Value.Number)args[3]).Item;
            var h = (int)((Value.Number)args[4]).Item;

            using (var game = new DynamoGame(update, draw, world0, w, h))
            {
                game.Run();
            }

            return Value.NewDummy("Game Over");
        }
    }
}
