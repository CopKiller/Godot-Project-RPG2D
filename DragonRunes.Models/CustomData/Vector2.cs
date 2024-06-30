using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Models.CustomData
{
    public class Vector2 : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private float x { get; set;}
        private float y { get; set; }

        public float X
        {
            get => x;
            set
            {
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Y
        {
            get => y;
            set
            {
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public Vector2 Normalized()
        //{
        //    Vector2 result = this;
        //    result.Normalize();
        //    return result;
        //}

        //private void Normalize()
        //{
        //    float num = LengthSquared();
        //    if (num == 0f)
        //    {
        //        X = (Y = 0f);
        //        return;
        //    }

        //    float num2 = MathF.Sqrt(num);
        //    X /= num2;
        //    Y /= num2;
        //}

        private float LengthSquared()
        {
            return X * X + Y * Y;
        }
        public float Length()
        {
            return MathF.Sqrt(LengthSquared());
        }
        public float DistanceTo(Vector2 to)
        {
            return MathF.Sqrt((X - to.X) * (X - to.X) + (Y - to.Y) * (Y - to.Y));
        }

        public static Vector2 operator *(Vector2 vector, int scalar)
        {
            return new Vector2 { X = vector.X * scalar, Y = vector.Y * scalar };
        }

        public static Vector2 operator *(int scalar, Vector2 vector)
        {
            return vector * scalar; // Reutiliza a implementação anterior
        }

        // Sobrecarga do operador /
        public static Vector2 operator /(Vector2 vector, int scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return new Vector2 { X = vector.X / scalar, Y = vector.Y / scalar };
        }

        // Sobrecarga do operador +
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2 { X = left.X + right.X, Y = left.Y + right.Y };
        }

        // Sobrecarga do operador -
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2 { X = left.X - right.X, Y = left.Y - right.Y };
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        //// Conversão implícita de Godot.Vector2 para Vector2
        //public static implicit operator Vector2(Godot.Vector2 godotVector)
        //{
        //    return new Vector2 { X = godotVector.x, Y = godotVector.y };
        //}
    }
}
