using Godot;
using System;

public partial class WindowBase : Window
{

    private Timer timerClose;

    private Vector2I originalSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InitializeTimer();

        AssignSignals();
    }

    public override void _EnterTree()
    {
        originalSize = this.Size;    
    }
    private void InitializeTimer()
    {
        // Add Timer
        timerClose = new Timer();
        timerClose.WaitTime = 0.01;
        timerClose.OneShot = false;
        this.AddChild(timerClose);
    }
    private void AssignSignals()
    {
        // Assign Signals
        this.Connect("close_requested", new Callable(this, nameof(OnCloseRequest)));

        timerClose.Connect("timeout", new Callable(this, nameof(CloseWindow)));
    }

    public virtual void OnCloseRequest()
    {
        // Start Timer
        timerClose.Start();
    }
    public void CloseWindow()
    {
        //this.Title = "";

        var size = this.Size;

        this.Size = this.Size - (originalSize / 8);

        if (this.Size.X <= 0 || this.Size.Y <= 0 || size == this.Size)
        {
            this.Hide();
            timerClose.Stop();

            this.Size = originalSize;
        }
    }
}
