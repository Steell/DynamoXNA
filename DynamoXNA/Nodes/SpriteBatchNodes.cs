using Dynamo.Connectors;
using Dynamo.Nodes;
using Microsoft.FSharp.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Value = Dynamo.FScheme.Value;

namespace DynamoXNA.Nodes
{
    [NodeName("Draw Sprite")]
    [NodeDescription("Draws a sprite on a sprite batch.")]
    [NodeCategory("XNA.SpriteBatch")]
    public class DrawSprite : dynNodeWithOneOutput
    {
        public DrawSprite()
        {
            InPortData.Add(new PortData("batch", "Sprite Batch", null));
            InPortData.Add(new PortData("sprite", "Sprite texture to draw", null));
            InPortData.Add(new PortData("position", "Vector2 representing position", null));

            OutPortData.Add(new PortData("", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            var batch  = (SpriteBatch)((Value.Container)args[0]).Item;
            var sprite = (Texture2D)  ((Value.Container)args[1]).Item;
            var pos    = (Vector2)    ((Value.Container)args[2]).Item;

            batch.Draw(sprite, pos, Color.White);

            return Value.NewDummy("draw");
        }
    }

    [NodeName("Test Sprite")]
    [NodeDescription("30x30 black square")]
    [NodeCategory("XNA.SpriteBatch")]
    public class TestSprite : dynNodeWithOneOutput
    {
        public TestSprite()
        {
            OutPortData.Add(new PortData("s", "", null));
            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            return Value.NewContainer(DynamoGame.Square);
        }
    }
}
