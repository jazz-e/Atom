using System;
using Atom.Messaging;
using Microsoft.Xna.Framework;

namespace Atom.Rendering.Static
{
    public class PositionMessage : IMessage
    {
        public string[] Data { get; set; }

        public PositionMessage()
        {
            Data = new string[2];
        }

        public PositionMessage SetPosition(Point position)
        {
            Data[0] = position.X + "," + position.Y;

            return this;
        }

        public PositionMessage SetEntityId(int entityId)
        {
            Data[1] = entityId.ToString();

            return this;
        }

        public Point GetPostion()
        {
            Point point = Point.Zero;

            if (Data[0] != null)
            {
                string[] coords = Data[0].Split(',');

                if (coords.GetLength(0) > 0)
                {
                    point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                }
            }

            return point;
        }

        public int GetEntityId()
        {
            int id = 0;

            if (Data[1] != null)
            {
                id = Convert.ToInt32(Data[1]);
            }

            return id;
        }

    }
}