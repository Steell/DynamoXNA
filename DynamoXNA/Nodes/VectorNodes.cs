using System.Collections.Generic;
using Dynamo;
using Dynamo.Connectors;
using Dynamo.FSchemeInterop.Node;
using Dynamo.Nodes;
using Microsoft.FSharp.Collections;
using Microsoft.Xna.Framework;

using Value = Dynamo.FScheme.Value;

namespace DynamoXNA.Nodes
{
    [NodeName("Vector2.Zero")]
    [NodeDescription("Zero vector.")]
    [NodeCategory("XNA.Vector2")]
    public class ZeroVector : dynNodeWithOneOutput
    {
        public ZeroVector()
        {
            OutPortData.Add(new PortData("(0,0)", "origin", null));
            RegisterAllPorts();
        }

        protected override INode Build(
            Dictionary<dynNodeModel, Dictionary<int, INode>> preBuilt, int outPort)
        {
            return new ObjectNode(Vector2.Zero);
        }
    }

    [NodeName("Vector2")]
    [NodeDescription("Creates a new Vector2.")]
    [NodeCategory("XNA.Vector2")]
    public class NewVector2 : dynNodeWithOneOutput
    {
        public NewVector2()
        {
            InPortData.Add(new PortData("x", "", null));
            InPortData.Add(new PortData("y", "", null));

            OutPortData.Add(new PortData("(x,y)", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            return Value.NewContainer(
                new Vector2
                {
                    X = (float)((Value.Number)args[0]).Item,
                    Y = (float)((Value.Number)args[1]).Item,
                });
        }
    }

    [NodeName("Add Vector2")]
    [NodeDescription("Adds two Vector2s.")]
    [NodeCategory("XNA.Vector2")]
    public class AddVector2 : dynNodeWithOneOutput
    {
        public AddVector2()
        {
            InPortData.Add(new PortData("(x1,y1)", "", null));
            InPortData.Add(new PortData("(x2,y2)", "", null));

            OutPortData.Add(new PortData("(x1+x2,y1+y2)", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            return
                Value.NewContainer(
                    (Vector2)((Value.Container)args[0]).Item
                    + (Vector2)((Value.Container)args[1]).Item);
        }
    }

    [NodeName("Vector2.X")]
    [NodeDescription("Gets the X component of a Vector2")]
    [NodeCategory("XNA.Vector2")]
    public class Vector2X : dynNodeWithOneOutput
    {
        public Vector2X()
        {
            InPortData.Add(new PortData("(x,y)", "", null));
            OutPortData.Add(new PortData("x", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            return Value.NewNumber(((Vector2)((Value.Container)args[0]).Item).X);
        }
    }

    [NodeName("Vector2.Y")]
    [NodeDescription("Gets the Y component of a Vector2")]
    [NodeCategory("XNA.Vector2")]
    public class Vector2Y : dynNodeWithOneOutput
    {
        public Vector2Y()
        {
            InPortData.Add(new PortData("(x,y)", "", null));
            OutPortData.Add(new PortData("y", "", null));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            return Value.NewNumber(((Vector2)((Value.Container)args[0]).Item).Y);
        }
    }
}
