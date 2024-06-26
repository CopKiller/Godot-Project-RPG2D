using DragonRunes.Logger;
using DragonRunes.Models.CustomData;
using DragonRunes.Network.CustomDataSerializable.Extension;
using Godot;
using System.ComponentModel;

namespace DragonRunes.Client.Scripts.ControlsBase
{
    public partial class PlayerBase : CharacterBody2D
    {
        private string playerName;
        protected string PlayerName
        {
            get { return playerName; }
            set
            {
                playerName = value;
                GetNode<RichTextLabel>("PlayerName").Text = value;
            }
        }

        protected bool isRunning = false;
        protected bool isMoving { get; private set; } = false;

        private Position position;
        protected new Position Position
        {
            get
            {
                if (position == null)
                {
                    position = new Position
                    {
                        X = base.Position.X,
                        Y = base.Position.Y
                    };
                    position.PropertyChanged += Position_PropertyChanged;
                } else
                {
                    position.X = base.Position.X;
                    position.Y = base.Position.Y;
                }
                return position;
            }
            set
            {
                if (position != null)
                {
                    position.PropertyChanged -= Position_PropertyChanged;
                }
                position = value;
                if (position != null)
                {
                    base.Position = new Godot.Vector2(position.X, position.Y);
                    position.PropertyChanged += Position_PropertyChanged;
                }
            }
        }

        protected private Direction Direction;

        protected Direction LastDirection;

        protected float Speed;

        private AnimatedSprite2D AnimatedSprite;

        public virtual void InitializePlayer()
        {
            AnimatedSprite = GetChild<AnimatedSprite2D>(0);
            //Direction = new Direction();
            LastDirection = new Direction();
            Speed = 100;
        }
        public override void _PhysicsProcess(double delta)
        {
            ProcessDirection(delta);

            ProcessPlayerMovement(delta);

            ProcessDirectionAnimation(delta);
        }
        private void ProcessPlayerMovement(double delta)
        {
            var speed = isRunning ? Speed * 2 : Speed;

            var velocity = Direction * speed;

            Velocity = new Godot.Vector2(velocity.X, velocity.Y);

            isMoving = Velocity.LengthSquared() > 0;

            MoveAndCollide(Velocity * (float)delta);
        }
        private void ProcessDirection(double delta)
        {
            if (Direction.Length() > 0)
            {
                LastDirection.ReplicateData(Direction);
            }
        }
        private void ProcessDirectionAnimation(double delta)
        {
            AnimatedSprite.Play(GetAnimationNameFromDirection());
        }
        private string GetAnimationNameFromDirection()
        {
            if (isRunning && isMoving)
            {
                if (Mathf.Abs(Direction.X) > Mathf.Abs(Direction.Y))
                {
                    return Direction.X > 0 ? "Walk_Right" : "Walk_Left";
                }
                else
                {
                    return Direction.Y > 0 ? "Walk_Down" : "Walk_Up";
                }
            }
            else if (isMoving)
            {
                if (Mathf.Abs(Direction.X) > Mathf.Abs(Direction.Y))
                {
                    return Direction.X > 0 ? "Walk_Right" : "Walk_Left";
                }
                else
                {
                    return Direction.Y > 0 ? "Walk_Down" : "Walk_Up";
                }
            }
            else //Stop
            {
                if (LastDirection.X > 0)
                    return "Idle_Right";
                else if (LastDirection.X < 0)
                    return "Idle_Left";
                else if (LastDirection.Y > 0)
                    return "Idle_Down";
                else
                    return "Idle_Up";
            }
        }

        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.Position = new Godot.Vector2(position.X, position.Y);
        }

    }
}
