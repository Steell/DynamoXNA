using Dynamo;
using Dynamo.Connectors;
using Dynamo.Nodes;
using Microsoft.FSharp.Collections;
using Microsoft.Xna.Framework;

using Value = Dynamo.FScheme.Value;

namespace DynamoXNA.Nodes
{
    [NodeName("GameTime.Milliseconds")]
    [NodeDescription("Gets the amount of milliseconds since the last update from a GameTime object.")]
    [NodeCategory("XNA.GameTime")]
    public class GameTimeElapsedMilliseconds : dynNodeWithOneOutput
    {
        public GameTimeElapsedMilliseconds()
        {
            InPortData.Add(new PortData("gametime", "", null));
            OutPortData.Add(new PortData("ms", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            var gameTime = (GameTime)((Value.Container)args[0]).Item;

            return Value.NewNumber(gameTime.ElapsedGameTime.TotalMilliseconds);
        }
    }
}
