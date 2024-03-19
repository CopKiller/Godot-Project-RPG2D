using System;
using System.Collections.Generic;
using System.Linq;
using GodotProject.Window.CustomControl;

namespace GodotProject.Window.Controller
{
    public class ActiveWindows
    {
        // Lista para armazenar as janelas ativas
        private List<WindowTextureRect> activeWindows = new List<WindowTextureRect>();

        public List<WindowTextureRect> ActiveWindowsList
        {
            get
            {
                return activeWindows;
            }
        }

        public List<WindowTextureRect> ActiveWindowsListReverse
        {
            get
            {
                return activeWindows.Reverse<WindowTextureRect>().ToList();
            }
        }

        // Método para adicionar uma janela à lista de janelas ativas
        public void AddActiveWindow(WindowTextureRect window)
        {
            if (IsActiveWindow(window))
            {
                BringToFront(window);
                return;
            }
            else
            {
                activeWindows.Add(window);
                window.ShowWindow();
                RearrangeWindows();
            }
        }

        // Método para remover uma janela da lista de janelas ativas
        private void RemoveActiveWindow(WindowTextureRect window)
        {
            if (IsActiveWindow(window))
            {
                activeWindows.Remove(window);
                window.HideWindow();
            } else
            {
                throw new Exception("Window not found");
            }
        }

        // Método para ordenar as janelas restantes e trazê-las para frente
        private void RearrangeWindows()
        {
            int zIndex = 0;
            foreach (WindowTextureRect window in activeWindows)
            {
                window.ZIndex = zIndex++;
                window.MoveToFront();
            }
        }

        // Método para fechar uma janela
        public void CloseWindow(WindowTextureRect window)
        {
            RemoveActiveWindow(window);
            window.QueueFree(); // Remover a janela da cena
            RearrangeWindows(); // Rearranjar as janelas restantes
        }

        // Método para trazer uma janela para frente
        public void BringToFront(WindowTextureRect window)
        {
            activeWindows.Remove(window);
            activeWindows.Add(window);
            RearrangeWindows();
        }

        // Método para verificar se a janela está na frente
        public bool IsFront(WindowTextureRect window)
        {
            return activeWindows.Last() == window;
        }

        // Método para verificar se a janela está na parte de trás
        public bool IsBack(WindowTextureRect window)
        {
                return !IsFront(window);
        }

        // Método para fechar todas as janelas
        public void CloseAllWindows()
        {
            foreach (WindowTextureRect window in activeWindows)
            {
                window.QueueFree();
            }
            activeWindows.Clear();
        }

        // Método para verificar se uma janela está ativa
        public bool IsActiveWindow(WindowTextureRect window)
        {
            return activeWindows.Contains(window);
        }

        // Método para obter o zIndex máximo
        public int GetMaxZIndex()
        {
            return activeWindows.Count;
        }
    }
}
