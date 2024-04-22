using GdProject.Client.Scripts.Window.Controller;
using GdProject.Client.Scripts.Window.Interface;
using Godot;

namespace Shared.Window.CustomControl
{
    public partial class WindowTextureRect : TextureRect, ITextureRectWindow
    {
        private bool dragging = false;

        private Vector2 offset = new Vector2();

        private ActiveWindows activeWindows => GetParent<Windows>().activeWindows;

        public string WindowName
        {
            get { return Name; }
            set { Name = value; }
        }

        public override void _Ready()
        {
            SetProcessInput(true);
        }

        public void Initialize()
        {
            activeWindows.AddActiveWindow(this);
        }

        public void ShowWindow()
        {
            Visible = true;
            dragging = false;
        }
        public void HideWindow()
        {
            Visible = false;
            dragging = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (activeWindows.IsActiveWindow(this))
            {
                if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left)
                {
                    if (mouseButton.Pressed)
                    {
                        if (activeWindows.IsFront(this))
                        {

                            if (GetGlobalRect().HasPoint(GetGlobalMousePosition()))
                            {
                                dragging = true;
                                offset = GetGlobalMousePosition() - GetRect().Position;
                            }
                            else
                            {
                                var listReverse = activeWindows.ActiveWindowsListReverse;

                                foreach (var window in listReverse)
                                {
                                    if (window.GetGlobalRect().HasPoint(GetGlobalMousePosition()))
                                    {
                                        dragging = false;
                                        activeWindows.BringToFront(window);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        dragging = false;
                    }
                }

                if (@event is InputEventMouse && dragging)
                {
                    // Calcula a posição desejada da janela após o movimento
                    Vector2 newPosition = GetGlobalMousePosition() - offset;

                    // Limita a posição da janela aos limites da janela principal
                    newPosition.X = Mathf.Clamp(newPosition.X, 0, GetParentAreaSize().X - GetRect().Size.X);
                    newPosition.Y = Mathf.Clamp(newPosition.Y, 0, GetParentAreaSize().Y - GetRect().Size.Y);

                    GlobalPosition = newPosition;
                }
            }
        }
    }
}
