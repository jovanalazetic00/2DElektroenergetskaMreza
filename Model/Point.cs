﻿using PZ1.Helpers;
using System;

namespace PZ1.Model
{
    public class Point : PowerEntity
    {
        private double x;

        private double y;

        public double X
        {
            get { return x; }
            set
            {
                if (x != value)
                {
                    x = value;
                }
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                if (y != value)
                {
                    y = value;
                }
            }
        }

        public Point() { }

        public Point(double x, double y)
        {
            double convertedX;
            double convertedY;
            GeneralHelper.ToLatLon(x, y, 34, out convertedX, out convertedY);

            X = convertedX;
            Y = convertedY;
        }

        public override bool Equals(object obj)
        {
            Point point = (Point)obj;
            return (point.X == X) && (point.Y == Y);
        }

        public bool IsTooClose(Point point, double allowed)
        {
            if ((Math.Abs(point.X - this.X) < allowed) && (Math.Abs(point.Y - this.Y) < allowed))
            {
                return true;
            }

            return false;
        }
    }
}
