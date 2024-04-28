using Godot;
using System;

public partial class PanelBG : Panel
{
    public override void _Input(InputEvent @event)
    {
        // Verifica se o evento de entrada é do tipo MouseEvent
        if (@event is InputEventMouseButton mouseEvent)
        {
            // Se o botão do mouse foi pressionado dentro do Panel, consuma o evento
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left &&
                GetRect().HasPoint(mouseEvent.GlobalPosition))
            {
                // Consuma o evento
                @event.Dispose();
            }
        }
    }
}
