using Godot;

public partial class Monster : CharacterBody2D
{
    [Export]
    string MonsterName = "";



    public override void _Ready()
    {
        base._Ready();
    }

    //public const float Speed = 40.0f; // Defina a velocidade desejada aqui

    //// Declare os nós das animações de movimento
    //private AnimatedSprite2D animatedSprite;

    //private Vector2 lastDirection = Vector2.Zero;
    //private Vector2 initialPosition = Vector2.Zero;
    //private Vector2 targetPosition = Vector2.Zero;

    //public override void _Ready()
    //{
    //    // Obtenha a referência ao AnimatedSprite no nó do monstro
    //    animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
    //    initialPosition = Position; // Armazena a posição inicial do monstro
    //    targetPosition = initialPosition; // Inicializa a posição alvo como a posição inicial
    //}

    //public override void _PhysicsProcess(double delta)
    //{
    //    // Se o monstro alcançou a posição alvo, escolha uma nova posição alvo
    //    if (Position == targetPosition)
    //    {
    //        ChooseNewTargetPosition();
    //    }

    //    // Mova o monstro em direção à posição alvo
    //    MoveTowardTargetPosition();
    //}

    //private void ChooseNewTargetPosition()
    //{
    //    // Escolhe uma nova posição alvo aleatória dentro de um raio de 32 pixels
    //    RandomNumberGenerator rng = new RandomNumberGenerator();
    //    rng.Randomize();
    //    targetPosition = initialPosition + new Vector2(rng.RandfRange(-64, 64), rng.RandfRange(-64, 64));
    //}

    //private void MoveTowardTargetPosition()
    //{
    //    // Calcula a direção para a posição alvo
    //    Vector2 direction = (targetPosition - Position).Normalized();

    //    // Calcula a distância entre a posição atual e a posição alvo
    //    float distanceToTarget = Position.DistanceTo(targetPosition);

    //    // Se a distância for maior que 64 pixels, mova o monstro na direção da posição alvo
    //    if (distanceToTarget > 64)
    //    {
    //        // Atribua a velocidade baseada na direção e na velocidade definida
    //        this.Velocity = direction * Speed;

    //        // Definir animação com base na direção do movimento
    //        if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
    //        {
    //            if (direction.X > 0)
    //            {
    //                // Movendo-se para a direita
    //                animatedSprite.Play("Walk_Right");
    //            }
    //            else
    //            {
    //                // Movendo-se para a esquerda
    //                animatedSprite.Play("Walk_Left");
    //            }
    //        }
    //        else
    //        {
    //            if (direction.Y > 0)
    //            {
    //                // Movendo-se para baixo
    //                animatedSprite.Play("Walk_Down");
    //            }
    //            else
    //            {
    //                // Movendo-se para cima
    //                animatedSprite.Play("Walk_Up");
    //            }
    //        }

    //        // Atualizar a última direção apenas se o monstro estiver se movendo
    //        lastDirection = direction;
    //    }
    //    else
    //    {
    //        // Se a distância for menor ou igual a 32 pixels, pare o monstro
    //        this.Velocity = Vector2.Zero;

    //        // Reproduz a animação idle correspondente à última direção
    //        if (lastDirection.X > 0)
    //        {
    //            animatedSprite.Play("Idle_Right");
    //        }
    //        else if (lastDirection.X < 0)
    //        {
    //            animatedSprite.Play("Idle_Left");
    //        }
    //        else if (lastDirection.Y > 0)
    //        {
    //            animatedSprite.Play("Idle_Down");
    //        }
    //        else if (lastDirection.Y < 0)
    //        {
    //            animatedSprite.Play("Idle_Up");
    //        }

    //        // Define a posição do monstro como a posição alvo para evitar pequenos desvios
    //        Position = targetPosition;
    //    }

    //    MoveAndSlide();
    //}
}
