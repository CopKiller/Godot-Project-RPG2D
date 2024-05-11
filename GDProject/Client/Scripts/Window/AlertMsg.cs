using Godot;

public partial class AlertMsg : BaseWindow
{
    [Export]
    public bool OffuscateBG { get; set; } = false;

    private Timer focusTimer;

    public override void _Ready()
    {
        base._Ready();

        AlwaysOnTop = true;
    }

    private void InitAlertMsg()
    {
        ShowOffuscateBG();
        CreateFocusTimer();
        ConnectButtonsSignals();
    }

    private void DestroyAlertMsg()
    {
        HideOffuscateBG();
        DisconnectButtonsSignals();
        DestroyFocusTimer();
    }

    public void ShowAlert(string message)
    {
        DestroyAlertMsg();

        InitAlertMsg();

        SetAlertMsg(message);

        SetFocus();
    }

    private void SetFocus()
    {
        this.Show();
        this.PopupCentered();
        this.GrabFocus();
    }

    private void SetAlertMsg(string message)
    {
        var alertMsgLabel = GetNode<Label>("VerticalBox/AlertMsgLabel");

        alertMsgLabel.Text = message;
    }

    private void ShowOffuscateBG()
    {
        if (OffuscateBG)
            GetParent().GetNode<Panel>("PanelBG").Show();
    }
    private void HideOffuscateBG()
    {
        if (OffuscateBG)
            GetParent().GetNode<Panel>("PanelBG").Hide();
    }

    public void OnCloseButtonPressed()
    {
        HideOffuscateBG();

        DestroyAlertMsg();

        this.Close();
    }

    public void OnOkButtonPressed()
    {
        HideOffuscateBG();

        DestroyAlertMsg();

        this.Close();
    }

    private void ConnectButtonsSignals()
    {

        this.Connect("close_requested", new Callable(this, nameof(OnCloseButtonPressed)));

        GetNode<Button>("VerticalBox/ButtonAccept").Connect("pressed", new Callable(this, nameof(OnOkButtonPressed)));

        this.Connect("focus_exited", new Callable(this, nameof(StartFocusTimer)));

        focusTimer.Connect("timeout", new Callable(this, nameof(SetFocus)));
    }

    private void DisconnectButtonsSignals()
    {

        if (this.IsConnected("close_requested", new Callable(this, nameof(OnCloseButtonPressed))))
            this.Disconnect("close_requested", new Callable(this, nameof(OnCloseButtonPressed)));

        if (GetNode<Button>("VerticalBox/ButtonAccept").IsConnected("pressed", new Callable(this, nameof(OnOkButtonPressed))))
            GetNode<Button>("VerticalBox/ButtonAccept").Disconnect("pressed", new Callable(this, nameof(OnOkButtonPressed)));

        if (this.IsConnected("focus_exited", new Callable(this, nameof(StartFocusTimer))))
            this.Disconnect("focus_exited", new Callable(this, nameof(StartFocusTimer)));

        if (focusTimer != null && focusTimer.IsConnected("timeout", new Callable(this, nameof(SetFocus))))
            focusTimer.Disconnect("timeout", new Callable(this, nameof(SetFocus)));
    }

    private void CreateFocusTimer()
    {
        focusTimer = new Timer();
        AddChild(focusTimer);
        focusTimer.WaitTime = 0.1f; // Ajuste o tempo conforme necessário
        focusTimer.OneShot = true;
    }
    private void StartFocusTimer()
    {
        if (focusTimer != null)
        {
            focusTimer.Start();
        }
    }
    private void StopFocusTimer()
    {
        if (focusTimer != null)
        {
            focusTimer.Stop();
        }
    }

    private void DestroyFocusTimer()
    {
        if (focusTimer != null)
        {
            this.RemoveChild(focusTimer);
            focusTimer.Stop();
            focusTimer.QueueFree();
            focusTimer = null;
        }
    }
}
