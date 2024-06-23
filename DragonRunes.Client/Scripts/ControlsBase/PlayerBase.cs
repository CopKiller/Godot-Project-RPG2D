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
                GetChild<RichTextLabel>(0).Text = value;
            }
        }

        protected bool isRunning = false;

        private bool isMoving = false;

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

        public virtual void InitializePlayerBase()
        {
            AnimatedSprite = GetChild<AnimatedSprite2D>(0);
            Direction = new Direction();
            LastDirection = new Direction();
            Speed = 100;
        }
        public override void _PhysicsProcess(double delta)
        {
            ProcessDirectionAnimation(delta);

            SetInputDirection(); // --> Isso aqui pode ser processado em outra camada, que o resto vai funcionar.

            ProcessDirection(delta);

            ProcessPlayerMovement(delta);

        }
        private void ProcessPlayerMovement(double delta)
        {
            var speed = isRunning ? Speed * 2 : Speed;

            var velocity = Direction * speed;

            isMoving = velocity.Length() > 0;

            Velocity = new Godot.Vector2(velocity.X, velocity.Y);

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
            if (isRunning)
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

        protected void SetInputDirection()
        {
            Direction.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            Direction.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            Direction.Normalized();
        }

        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.Position = new Godot.Vector2(position.X, position.Y);
        }

    }
}
