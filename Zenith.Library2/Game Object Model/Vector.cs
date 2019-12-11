//-----------------------------------------------------------
//File:   Vector.cs
//Desc:   Holds the class responsible for most of the physics
//        in Zenith
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This class holds 4 values, x, y, angle
    // and magnitude which greatly simplify
    // velocity and collision. The main reason
    // they exist is for positioning.
    public class Vector
    {
        // The x and y coordinates
        double x, y;

        // The angle (in radians) and magnitude of the Vector
        double angle, magnitude;

        public double X
        {
            get { return x; }
            set { x = value; RecalculateAngleAndMagnitude(); }
        }
        public double Y
        {
            get { return y; }
            set { y = value; RecalculateAngleAndMagnitude(); }
        }

        public double Angle
        {
            get { return angle; }
            set { angle = value; RecalculateXAndY(); }
        }
        public double Magnitude
        {
            get { return magnitude; }
            set { magnitude = value; RecalculateXAndY(); }
        }

        // Methods

        // Recalculates angle and magnitude based on the current x and y values.
        private void RecalculateAngleAndMagnitude() {
            angle = Math.Atan2(y, x);
            magnitude = Math.Sqrt(x * x + y * y);
        }

        // Recalculates x and y based on the current angle and magnitude values.
        private void RecalculateXAndY()
        {
            x = Math.Cos(angle) * magnitude;
            y = Math.Sin(angle) * magnitude;
        }

        // If the magnitude of the Vector is greater than the value given,
        // then resize the vector to fit within the new magnitude.
        public void Cap(double newMagnitude)
        {
            if (magnitude > newMagnitude)
            {
                x *= Math.Cos(angle) * newMagnitude;
                y *= Math.Sin(angle) * newMagnitude;
                magnitude = newMagnitude;
            }
        }

        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading
        
        // Overloaded adding operation
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y);
        }

        // Overloaded subtracting operation
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y);
        }

        // Overloaded multiplication operation
        public static Vector operator *(Vector v1, double x)
        {
            return new Vector(v1.x * x, v1.y * x);
        }

        // Overloaded division operation
        public static Vector operator /(Vector v1, double x)
        {
            return new Vector(v1.x / x, v1.y / x);
        }

        // Constructor
        // isPolar is an optional parameter with its default set to false.
        // If true, the x and y parameters will act like their corresponding Vector variables
        // If false, they will act like the angle and magnitude, respectively.

        public Vector(double x, double y, bool isPolar = false)
        {
            if (isPolar)
            {
                this.x = Math.Cos(x) * y;
                this.y = Math.Sin(x) * y;
                this.magnitude = y;
                this.angle = x;
            }
            else
            {
                this.x = x;
                this.y = y;
                this.angle = Math.Atan2(y, x);
                this.magnitude = Math.Sqrt(x * x + y * y);
            }
        }

        // ??
        public override string ToString()
        {
            return x.ToString() + ":" + y.ToString();
        }

    }
}
