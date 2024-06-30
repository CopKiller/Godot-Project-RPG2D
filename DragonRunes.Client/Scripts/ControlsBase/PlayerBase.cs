using DragonRunes.Logger;
using DragonRunes.Models.CustomData;
using DragonRunes.Models.Enum;
using DragonRunes.Network.CustomDataSerializable.Extension;
using Godot;
using System;
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
        protected bool isMoving { get; set; } = false;

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
                else
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


        protected Direction Direction { get; set; } = Direction.Down;
        protected float offSetX = 0f;
        protected float offSetY = 0f;
        public const float GridSize = 32.0f;

        private Position positionGrid;
        protected Position PositionGrid
        {
            get
            {
                if (positionGrid == null)
                {
                    positionGrid = new Position
                    {
                        X = 0,
                        Y = 0
                    };
                }
                return positionGrid;
            }
            set
            {
                positionGrid = value;
            }
        }


        protected float Speed;

        private AnimatedSprite2D AnimatedSprite;

        private long WalkTimer;


        public virtual void InitializePlayer()
        {
            AnimatedSprite = GetChild<AnimatedSprite2D>(0);
            Speed = 2.0f;
        }
        public override void _PhysicsProcess(double delta)
        {
            ProcessDirectionAnimation(delta);
            ProcessPlayerMovement(delta);
        }
        private void ProcessPlayerMovement(double delta)
        {
            if (!isMoving) return;

            var tickCount = System.Environment.TickCount64;

            if (WalkTimer < tickCount)
            {
                var speed = isRunning ? Speed * 2.0f : Speed;
                var velocity = new Godot.Vector2();

                switch (Direction)
                {
                    case Direction.Up:
                        velocity.Y = -speed;
                        offSetY -= speed;
                        if (offSetY < 0)
                            offSetY = 0;
                        break;
                    case Direction.Down:
                        velocity.Y = speed;
                        offSetY += speed;
                        if (offSetY > 0)
                            offSetY = 0;
                        break;
                    case Direction.Left:
                        velocity.X = -speed;
                        offSetX -= speed;
                        if (offSetX < 0)
                            offSetX = 0;
                        break;
                    case Direction.Right:
                        velocity.X = speed;
                        offSetX += speed;
                        if (offSetX > 0)
                            offSetX = 0;
                        break;
                    case Direction.UpLeft:
                        velocity.X = -speed;
                        velocity.Y = -speed;
                        offSetX -= speed;
                        offSetY -= speed;
                        if (offSetY < 0f)
                            offSetY = 0f;
                        if (offSetX < 0f)
                            offSetX = 0f;
                        break;
                    case Direction.UpRight:
                        velocity.X = speed;
                        velocity.Y = -speed;
                        offSetX += speed;
                        offSetY -= speed;
                        if (offSetY < 0f)
                            offSetY = 0f;
                        if (offSetX > 0f)
                            offSetX = 0f;
                        break;
                    case Direction.DownLeft:
                        velocity.X = -speed;
                        velocity.Y = speed;
                        offSetX -= speed;
                        offSetY += speed;
                        if (offSetY > 0f)
                            offSetY = 0f;
                        if (offSetX < 0f)
                            offSetX = 0f;
                        break;
                    case Direction.DownRight:
                        velocity.X = speed;
                        velocity.Y = speed;
                        offSetX += speed;
                        offSetY += speed;
                        if (offSetY > 0f)
                            offSetY = 0f;
                        if (offSetX > 0f)
                            offSetX = 0f;
                        break;
                }

                if (Direction == Direction.Right || Direction == Direction.Down || Direction == Direction.DownRight)
                {
                    if (offSetX >= 0f && offSetY >= 0f)
                    {
                        isMoving = false;

                        return;
                    }
                }
                else
                {
                    if (offSetX <= 0f && offSetY <= 0f)
                    {
                        isMoving = false;

                        return;
                    }
                }

                velocity.Normalized();

                // Use MoveAndCollide para mover o jogador
                var collision = MoveAndCollide(velocity);

                // Se houver colisão, parar o movimento
                if (collision != null)
                {
                    isMoving = false;
                    GD.Print($"colisao aqui");
                }

                WalkTimer = System.Environment.TickCount64 + 30;
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
                switch (Direction)
                {
                    case Direction.Up:
                    case Direction.UpLeft:
                    case Direction.UpRight:
                        return "Walk_Up";
                    case Direction.Down:
                    case Direction.DownLeft:
                    case Direction.DownRight:
                        return "Walk_Down";
                    case Direction.Left:
                        return "Walk_Left";
                    case Direction.Right:
                        return "Walk_Right";
                    default:
                        return "Idle_Down";
                }
            }
            else if (isMoving)
            {
                switch (Direction)
                {
                    case Direction.Up:
                    case Direction.UpLeft:
                    case Direction.UpRight:
                        return "Walk_Up";
                    case Direction.Down:
                    case Direction.DownLeft:
                    case Direction.DownRight:
                        return "Walk_Down";
                    case Direction.Left:
                        return "Walk_Left";
                    case Direction.Right:
                        return "Walk_Right";
                    default:
                        return "Idle_Down";
                }
            }
            else //Stop
            {
                switch (Direction)
                {
                    case Direction.Up:
                    case Direction.UpLeft:
                    case Direction.UpRight:
                        return "Idle_Up";
                    case Direction.Down:
                    case Direction.DownLeft:
                    case Direction.DownRight:
                        return "Idle_Down";
                    case Direction.Left:
                        return "Idle_Left";
                    case Direction.Right:
                        return "Idle_Right";
                    default:
                        return "Idle_Down";
                }
            }
        }

        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.Position = new Godot.Vector2(position.X, position.Y);
        }

    }
}
