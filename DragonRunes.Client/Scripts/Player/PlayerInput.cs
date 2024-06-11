


using Godot;

namespace GdProject.Client.Scripts.Entities.Player
{
    public partial class PlayerInput : PlayerNetwork
    {

        public override void _Input(InputEvent @event)
        {

            if (@event is InputEventKey)
                ProcessPlayerMovementInput(@event);

            base._Input(@event);
        }

        private void ProcessPlayerMovementInput(InputEvent @event)
        {
            if (IsLocalPlayer)
            {
                SetInputRunning();
                SetInputDirection();

                //ExternalLogger.Print($"Direction: {Direction}");
            }
        }

    }
}
