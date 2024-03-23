

using Godot;

namespace GdProject.Client.Scripts.Window.Interface
{
    public interface IControlWindow
    {
        string WindowName { get; set; }
        int ZIndex { get; set; }
        void _Ready();
        void _Input(InputEvent @event);
        void ShowWindow();
        void HideWindow();
        void QueueFree();
        void MoveToFront();
        Rect2 GetGlobalRect();
    }
}
