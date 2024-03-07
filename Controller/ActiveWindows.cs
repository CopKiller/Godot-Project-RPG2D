using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodotProject.CustomControl;

namespace GodotProject.Controller
{
    public class ActiveWindows
    {
        // Lista para armazenar as janelas ativas
        private List<WindowTextureRect> activeWindows = new List<WindowTextureRect>();

        // Método para adicionar uma janela à lista de janelas ativas
        public void AddActiveWindow(WindowTextureRect window)
        {
            activeWindows.Add(window);
            window.Visible = true;
            window.BringToFront();
        }

        // Método para remover uma janela da lista de janelas ativas
        private void RemoveActiveWindow(WindowTextureRect window)
        {
            activeWindows.Remove(window);
            window.Visible = false;
        }

        // Método para ordenar as janelas restantes e trazê-las para frente
        private void RearrangeWindows()
        {
            int zIndex = 0;
            foreach (WindowTextureRect window in activeWindows)
            {
                window.ZIndex = zIndex++;
                window.BringToFront();
            }
        }

        // Método para fechar uma janela
        public void CloseWindow(WindowTextureRect window)
        {
            RemoveActiveWindow(window);
            window.QueueFree(); // Remover a janela da cena
            RearrangeWindows(); // Rearranjar as janelas restantes
        }
    }
}
