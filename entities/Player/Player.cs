using Godot;
using GodotProject.Model.Player;

public partial class Player : PlayerModel
{
    public override void _Ready()
    {
        LoadPlayer(1, "Rodorfo", ClassType.Mage);
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = GetInputDirection();
        MovePlayer(direction, delta);
        ProcessDirection(direction);
    }
    // Obtém a direção de entrada do jogador
    private Vector2 GetInputDirection()
    {
        var xMoving = Input.GetActionStrength("Move_Right") - Input.GetActionStrength("Move_Left");
        var yMoving = Input.GetActionStrength("Move_Down") - Input.GetActionStrength("Move_Up");
        return new Vector2(xMoving, yMoving).Normalized();
    }
    // Move o jogador com base na direção
    private void MovePlayer(Vector2 direction, double delta)
    {
        var running = GetInputRunning();
        var speed = running ? Speed * 2 : Speed;
        Velocity = direction * speed;

        MoveAndCollide(Velocity * (float)delta);
    }
    // Verifica se o botão de corrida está pressionado
    private bool GetInputRunning()
    {
        return Input.IsActionPressed("Shift_Down");
    }
    // Processa a direção para reproduzir as animações corretas
    private void ProcessDirection(Vector2 direction)
    {
        if (direction.LengthSquared() > 0)
        {
            PlayMovementAnimation(direction);
            LastDirection = direction;
        }
        else
        {
            PlayIdleAnimation();
        }
    }
    // Reproduz a animação de movimento adequada com base na direção
    private void PlayMovementAnimation(Vector2 direction)
    {
        string animationName = GetAnimationNameFromDirection(direction);
        Class.AnimatedSprite.Play(animationName);
    }
    // Obtém o nome da animação com base na direção do movimento
    private string GetAnimationNameFromDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
        {
            return direction.X > 0 ? "Walk_Right" : "Walk_Left";
        }
        else
        {
            return direction.Y > 0 ? "Walk_Down" : "Walk_Up";
        }
    }
    // Reproduz a animação idle correspondente à última direção
    private void PlayIdleAnimation()
    {
        string animationName = GetIdleAnimationName();
        Class.AnimatedSprite.Play(animationName);
    }
    // Obtém o nome da animação idle com base na última direção
    private string GetIdleAnimationName()
    {
        if (LastDirection.X > 0)
            return "Idle_Right";
        else if (LastDirection.X < 0)
            return "Idle_Left";
        else if (LastDirection.Y > 0)
            return "Idle_Down";
        else
            return "Idle_Up";
    }
}
