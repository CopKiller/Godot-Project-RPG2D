using DragonRunes.Network.CustomData;
using Godot;
using System;

namespace GdProject.Model
{
    public partial class PlayerPhysic : CharacterBody2D
    {
        public PlayerDataModel playerDataModel;


        public bool IsLocalPlayer = false;

        public bool InGame = false;

        protected Godot.Vector2 Direction = Godot.Vector2.Zero;

        protected Godot.Vector2 LastDirection = Godot.Vector2.Zero;

        private Godot.Vector2 JoystickDirection = Godot.Vector2.Zero;

        protected float Speed;

        protected bool isRunning = false;

        private bool isMoving = false;

        private AnimatedSprite2D AnimatedSprite;

        protected event Action OnPlayerMove;

        protected float xMoving;
        protected float yMoving;

        public void InitializePlayerPhysics()
        {
            AnimatedSprite = GetChild<AnimatedSprite2D>(0);

            Position = new Godot.Vector2(playerDataModel.Position.X, playerDataModel.Position.Y);

            Direction = new Godot.Vector2(playerDataModel.Direction.X, playerDataModel.Direction.Y);

            LastDirection = Direction;

            Speed = 100; //playerDataModel.Speed;
        }


        public override void _PhysicsProcess(double delta)
        {
            if (InGame)
            {
                ProcessLocalPlayer(delta);

                ProcessDirection(delta);
            }
        }


        #region LocalPlayer
        private void ProcessLocalPlayer(double delta)
        {
            if (IsLocalPlayer)
            {
                ProcessLocalPlayerDirection(delta);

                ProcessLocalPlayerMovement(delta);

                ProcessNetworkSincronization();
            }
        }
        private void ProcessLocalPlayerDirection(double delta)
        {
            var vector2 = GetInputDirection();

            Direction = vector2;
        }
        private void ProcessLocalPlayerMovement(double delta)
        {
            isRunning = GetInputRunning();
            var speed = isRunning ? Speed * 2 : Speed;
            Velocity = Direction * speed;

            MoveAndCollide(Velocity * (float)delta);
        }
        private void ProcessNetworkSincronization()
        {
            var needToSync = Velocity != Godot.Vector2.Zero;

            if (needToSync)
            {
                OnPlayerMove?.Invoke();
                isMoving = true;
            }
            else
            {
                if (isMoving)
                {
                    OnPlayerMove?.Invoke();
                    isMoving = false;
                }
            }
        }

        private Godot.Vector2 GetInputDirection()
        {
            return new Godot.Vector2(xMoving, yMoving).Normalized();
        }

        protected void SetInputDirection()
        {
            xMoving = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            yMoving = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        }

        public void OnVirtualJoystickAnalogicChange(Godot.Vector2 direction)
        {
            xMoving = direction.X;
            yMoving = direction.Y;
        }

        private bool GetInputRunning()
        {
            return isRunning;
        }

        protected void SetInputRunning()
        {
            isRunning = Input.IsActionPressed("ui_running");
        }
        #endregion



        #region AllPlayers
        private void ProcessDirection(double delta)
        {
            if (Direction.Length() > 0)
            {
                PlayMovementAnimation(delta);
                LastDirection = Direction;
            }
            else
            {
                PlayIdleAnimation(delta);
            }
        }
        #endregion

        // Reproduz a animação de movimento adequada com base na direção
        private void PlayMovementAnimation(double delta)
        {
            string animationName = GetAnimationNameFromDirection(Direction);
            AnimatedSprite.Play(animationName);
        }
        // Obtém o nome da animação com base na direção do movimento
        private string GetAnimationNameFromDirection(Godot.Vector2 direction)
        {
            bool inputRunning;

            inputRunning = GetInputRunning();

            if (inputRunning)
            {
                if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
                {
                    return direction.X > 0 ? "Run_Right" : "Run_Left";
                }
                else
                {
                    return direction.Y > 0 ? "Run_Down" : "Run_Up";
                }
            }
            else
            {
                if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
                {
                    return direction.X > 0 ? "Walk_Right" : "Walk_Left";
                }
                else
                {
                    return direction.Y > 0 ? "Walk_Down" : "Walk_Up";
                }
            }
        }
        // Reproduz a animação idle correspondente à última direção
        private void PlayIdleAnimation(double delta)
        {
            string animationName = GetIdleAnimationName();
            AnimatedSprite.Play(animationName);
        }
        // Obtém o nome da animação idle com base na última direção
        private string GetIdleAnimationName()
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
}
