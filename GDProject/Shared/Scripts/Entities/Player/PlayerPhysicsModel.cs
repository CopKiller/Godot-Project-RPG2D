using GdProject.Infrastructure;
using GdProject.Logger;
using GdProject.Model;
using Godot;
using Network.Packet;

namespace Shared.Scripts.Player
{
    public partial class PlayerPhysicsModel : CharacterBody2D
    {
        public bool IsLocalPlayer = false;

        public PlayerDataModel PlayerData;

        public CPlayerAction PlayerAction;

        public Vector2 LastDirection;

        public AnimatedSprite2D AnimatedSprite;

        Vector2 JoystickDirection = Vector2.Zero;

        public float Speed = 100;



        public override void _Ready()
        {
            AnimatedSprite = GetChild<AnimatedSprite2D>(0);
            PlayerAction = new CPlayerAction();
        }

        public override void _PhysicsProcess(double delta)
        {
            if (IsLocalPlayer)
            {
                var vector2 = GetInputDirection();
                PlayerAction.Direction = new SharedLibrary.DataType.Vector2(vector2.X, vector2.Y);
            }

            var _vector2 = new Vector2(PlayerAction.Direction.X, PlayerAction.Direction.Y);
            MovePlayer(_vector2, delta);

            ProcessDirection(_vector2);
        }

        // Obtém a direção de entrada do jogador

        private Vector2 GetInputDirection()
        {
            if (InitClient.PlatformName == "Windows")
            {
                var xMoving = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
                var yMoving = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
                return new Vector2(xMoving, yMoving).Normalized();
            }
            else if (InitClient.PlatformName == "Android")
            {
                return JoystickDirection.Normalized();
            }

            return Vector2.Zero;
        }

        public void OnVirtualJoystickAnalogicChange(Vector2 direction)
        {
            JoystickDirection = direction;
        }

        //Move o jogador com base na direção
        private void MovePlayer(Vector2 direction, double delta)
        {
            if (IsLocalPlayer)
            {
                MoveLocalPlayer(direction, delta);
            }
        }
        // Move o jogador local
        private void MoveLocalPlayer(Vector2 direction, double delta)
        {
            var running = GetInputRunning();
            this.PlayerAction.Running = running;
            var speed = running ? Speed * 2 : Speed;
            Velocity = direction * speed;

            var needToSync = Velocity != Vector2.Zero;

            if (needToSync)
            {
                PlayerAction.ActionType = PlayerActionType.Move;
                PlayerAction.Position = new SharedLibrary.DataType.Vector2(Position.X, Position.Y);
                PlayerAction.Direction = new SharedLibrary.DataType.Vector2(direction.X, direction.Y);
                PlayerAction.Speed = speed;
                PlayerAction.Running = PlayerAction.Running;

                PlayerAction.WritePacket(InitClient.LocalPlayer.PacketProcessor);
            }
            else
            {
                if (PlayerAction.ActionType == PlayerActionType.Move)
                {
                    PlayerAction.ActionType = PlayerActionType.Move;
                    PlayerAction.Position = new SharedLibrary.DataType.Vector2(Position.X, Position.Y);
                    PlayerAction.Direction = SharedLibrary.DataType.Vector2.Zero;
                    PlayerAction.Speed = speed;
                    PlayerAction.Running = PlayerAction.Running;

                    PlayerAction.WritePacket(InitClient.LocalPlayer.PacketProcessor);

                    PlayerAction.ActionType = PlayerActionType.Stop;
                }
            }
            MoveAndCollide(Velocity * (float)delta);
        }

        // Verifica se o botão de corrida está pressionado
        private bool GetInputRunning()
        {
            return Input.IsActionPressed("ui_running");
        }
        // Processa a direção para reproduzir as animações corretas
        private void ProcessDirection(Vector2 direction)
        {

            if (direction.Length() > 0)
            {
                PlayMovementAnimation(direction);
                LastDirection = direction;
            }
            else
            {
                PlayIdleAnimation();
            }
        }
        // Reproduz a animação de movimento adequada com base na direção
        private void PlayMovementAnimation(Vector2 direction)
        {
            string animationName = GetAnimationNameFromDirection(direction);
            AnimatedSprite.Play(animationName);
        }
        // Obtém o nome da animação com base na direção do movimento
        private string GetAnimationNameFromDirection(Vector2 direction)
        {
            bool inputRunning;

            if (IsLocalPlayer)
                inputRunning = GetInputRunning();
            else
                inputRunning = PlayerAction.Running;

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
        private void PlayIdleAnimation()
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
