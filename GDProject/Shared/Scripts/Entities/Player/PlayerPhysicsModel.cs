﻿using GdProject.Model;
using GdProject.Network;
using GdProject.Network.Packet.Client;
using Godot;

namespace Shared.Scripts.Player
{
    public partial class PlayerPhysicsModel : CharacterBody2D
    {
        public bool IsLocalPlayer = false;

        public PlayerDataModel PlayerData;

        public CPlayerAction PlayerAction;

        public Vector2 LastDirection;

        public AnimatedSprite2D AnimatedSprite;

        public float Speed = 100;

        // Posição do jogador -> Conversão de um DataType.Vector2 para um Godot.Vector2
        //public new SharedLibrary.DataType.Vector2 Position
        //{
        //    get => new SharedLibrary.DataType.Vector2(base.Position.X, base.Position.Y);
        //    set => base.Position = new Vector2(value.X, value.Y);
        //}

        //public new SharedLibrary.DataType.Vector2 Direction
        //{
        //    get => new SharedLibrary.DataType.Vector2(PlayerAction.Direction.X, PlayerAction.Direction.Y);
        //    set => PlayerAction.Direction = value;
        //}

        public ClientNetworkService GameClient { get; set; }


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
            var xMoving = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            var yMoving = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            return new Vector2(xMoving, yMoving).Normalized();
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
                PlayerAction = new CPlayerAction
                {
                    ActionType = PlayerActionType.Move,
                    Position = new SharedLibrary.DataType.Vector2(Position.X, Position.Y),
                    Direction = new SharedLibrary.DataType.Vector2(direction.X, direction.Y),
                    Speed = speed,
                    Running = PlayerAction.Running
                };

                GameClient.SendPlayerPosition(PlayerAction);
            }
            else
            {
                if (PlayerAction.ActionType == PlayerActionType.Move)
                {
                    PlayerAction = new CPlayerAction
                    {
                        ActionType = PlayerActionType.Stop,
                        Position = new SharedLibrary.DataType.Vector2(Position.X, Position.Y),
                        Direction = SharedLibrary.DataType.Vector2.Zero,
                        Speed = speed,
                        Running = PlayerAction.Running
                    };

                    GameClient.SendPlayerPosition(PlayerAction);

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