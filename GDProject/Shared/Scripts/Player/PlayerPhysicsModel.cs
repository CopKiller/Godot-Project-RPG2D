using Shared.Scripts.Player;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GdProject.Shared.Scripts.Class;
using GdProject.Shared.Scripts.NodeManager;

namespace Shared.Scripts.Player
{
    public partial class PlayerPhysicsModel : CharacterBody2D
    {
        public PlayerDataModel PlayerData;

        Vector2 LastDirection = new Vector2();
        public float Speed = 100;

        public override void _Ready()
        {
            PlayerData = new PlayerDataModel();
            //playerData.PlayerName = "Player";
            PlayerData.Position = Position;

            PlayerData.Class = new ClassModel();
            PlayerData.Class.Class = ClassType.Mage;
            PlayerData.Class.AnimatedSprite = GetNode<AnimatedSprite2D>("Sprite");
        }

        public override void _Process(double delta)
        {

        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 direction = GetInputDirection();
            MovePlayer(direction, delta);
            ProcessDirection(direction);
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
            var running = GetInputRunning();
            var speed = running ? Speed * 2 : Speed;
            Velocity = direction * speed;

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
            if (direction.LengthSquared() > 0)
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
            PlayerData.Class.AnimatedSprite.Play(animationName);
        }
        // Obtém o nome da animação com base na direção do movimento
        private string GetAnimationNameFromDirection(Vector2 direction)
        {
            var inputRunning = GetInputRunning();

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
            PlayerData.Class.AnimatedSprite.Play(animationName);
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
