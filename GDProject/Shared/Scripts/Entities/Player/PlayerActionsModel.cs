namespace GdProject.Shared.Scripts.Entities.Player
{
    public class PlayerActionsModel
    {
        public bool IsStanding { get; set; }
        public bool IsWalking { get; set; }
        public bool IsRunning { get; set; }

        public void SetAction(string action, bool value)
        {
            switch (action)
            {
                case "IsStanding":
                    IsStanding = value;
                    break;
                case "IsWalking":
                    IsWalking = value;
                    break;
                case "IsRunning":
                    IsRunning = value;
                    break;
            }
        }
    }
}
