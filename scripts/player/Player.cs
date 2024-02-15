using Godot;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;

    // Declare os nós das animações de movimento
    private AnimatedSprite2D animatedSprite;

    private Vector2 lastDirection = Vector2.Zero;

    public override void _Ready()
    {
        // Obtenha a referência ao AnimatedSprite no nó do jogador
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Obtenha a direção do movimento
        Vector2 direction = Vector2.Zero;

        if (Input.IsActionPressed("MOVE_LEFT"))
        {
            direction.X -= 1;
        }
        if (Input.IsActionPressed("MOVE_RIGHT"))
        {
            direction.X += 1;
        }
        if (Input.IsActionPressed("MOVE_UP"))
        {
            direction.Y -= 1;
        }
        if (Input.IsActionPressed("MOVE_DOWN"))
        {
            direction.Y += 1;
        }

        // Normalize o vetor de direção para manter a velocidade constante em movimentos diagonais
        direction = direction.Normalized();

        // Atribua a velocidade baseada na direção e na velocidade definida
        velocity = direction * Speed;

        // Atribua a velocidade ao jogador
        this.Velocity = velocity;

        // Verificar se o jogador está se movendo
        if (direction.Length() > 0)
        {
            // Definir animação com base na direção do movimento
            if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
            {
                if (direction.X > 0)
                {
                    // Movendo-se para a direita
                    animatedSprite.Play("Walk_Right");
                }
                else
                {
                    // Movendo-se para a esquerda
                    animatedSprite.Play("Walk_Left");
                }
            }
            else
            {
                if (direction.Y > 0)
                {
                    // Movendo-se para cima
                    animatedSprite.Play("Walk_Down");
                }
                else
                {
                    // Movendo-se para baixo
                    animatedSprite.Play("Walk_Up");
                }
            }

            // Atualizar a última direção apenas se o jogador estiver se movendo
            lastDirection = direction;
        }
        else
        {
            // Sem movimento, reproduzir a animação idle correspondente à última direção
            if (lastDirection.X > 0)
            {
                animatedSprite.Play("Idle_Right");
            }
            else if (lastDirection.X < 0)
            {
                animatedSprite.Play("Idle_Left");
            }
            else if (lastDirection.Y > 0)
            {
                animatedSprite.Play("Idle_Down");
            }
            else if (lastDirection.Y < 0)
            {
                animatedSprite.Play("Idle_Up");
            }
        }

        MoveAndSlide();
    }
}