using Godot;

namespace GodotProject.CustomControl
{
    public partial class WindowTextureRect : TextureRect
    {
        private bool dragging = false;

        private Vector2 offset = new Vector2();

        public override void _Ready()
        {
            SetProcessInput(true);
        }

        public void ShowWindow()
        {
            Visible = true;
            BringToFront();
        }
        public void HideWindow()
        {
            Visible = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left)
            {
                if (mouseButton.Pressed)
                {
                    var mousePos = GetGlobalMousePosition();
                    if (GetGlobalRect().HasPoint(mousePos))
                    {
                        // Verifica se esta janela está na frente de todas as outras
                        if (IsFrontWindow())
                        {
                            dragging = true;
                            offset = mousePos - GetRect().Position;
                            BringToFront();
                        }
                    }
                }
                else
                {
                    dragging = false;
                }
            }

            if (@event is InputEventMouseMotion && dragging)
            {
                // Calcula a posição desejada da janela após o movimento
                Vector2 newPosition = GetGlobalMousePosition() - offset;

                // Limita a posição da janela aos limites da janela principal
                newPosition.X = Mathf.Clamp(newPosition.X, 0, GetParentAreaSize().X - GetRect().Size.X);
                newPosition.Y = Mathf.Clamp(newPosition.Y, 0, GetParentAreaSize().Y - GetRect().Size.Y);

                GlobalPosition = newPosition;
            }

            // Verifica se o botão do mouse foi liberado fora da janela atual
            if (@event is InputEventMouseButton mouseButtonUp && mouseButtonUp.ButtonIndex == MouseButton.Left && !GetGlobalRect().HasPoint(mouseButtonUp.Position))
            {
                // Itera sobre os filhos do MainMenu
                foreach (Node child in GetParent().GetChildren())
                {
                    // Verifica se o filho é uma janela
                    if (child is WindowTextureRect window && window != this && window.GetGlobalRect().HasPoint(mouseButtonUp.Position))
                    {
                        // Se encontrou outra janela fora da janela atual, chama BringToFront() para trazê-la para a frente
                        window.BringToFront();
                        break;
                    }
                }
            }
        }

        private int GetZIndex(WindowTextureRect node)
        {
            if (node.GetParent() == null)
                return 0;

            return node.ZIndex;
        }

        public void BringToFront()
        {
            int maxZIndex = -1;

            // Itera sobre os filhos do MainMenu
            foreach (Node child in GetParent().GetChildren())
            {
                // Verifica se o filho é uma janela
                if (child is WindowTextureRect window && window != this)
                {
                    // Atualiza o máximo ZIndex encontrado
                    maxZIndex = Mathf.Max(maxZIndex, window.ZIndex);
                }
            }

            // Define o ZIndex da janela atual como o próximo valor após o máximo ZIndex encontrado
            ZIndex = maxZIndex + 1;
        }

        private bool IsFrontWindow()
        {
            int myZIndex = ZIndex;

            // Itera sobre os filhos do MainMenu
            foreach (Node child in GetParent().GetChildren())
            {
                // Verifica se o filho é uma janela e tem um ZIndex maior que o atual
                if (child is WindowTextureRect window && window != this && window.ZIndex > myZIndex)
                {
                    return false; // Se encontrou uma janela na frente, retorna falso
                }
            }

            return true; // Se não encontrou nenhuma janela na frente, retorna verdadeiro
        }
    }
}
