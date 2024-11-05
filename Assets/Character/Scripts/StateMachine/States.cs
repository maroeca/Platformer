public class IdleState : PlayerState
{
    public IdleState(CharacterBehaviour characterBehaviour) : base(characterBehaviour) { }

    public override void Enter()
    {
        // Inicia a anima��o de idle
        characterBehaviour.PlayAnimation("Character_Idle");
    }

    public override void Update()
    {
        // Transi��o para MovingState se o personagem come�ar a se mover
        if (characterBehaviour.IsMoving())
        {
            characterBehaviour.ChangeState(new MovingState(characterBehaviour));
        }

        // Transi��o para JumpingState se o personagem n�o estiver mais no ch�o
        if (!characterBehaviour.IsGrounded())
        {
            characterBehaviour.ChangeState(new JumpingState(characterBehaviour));
        }
    }
}


public class MovingState : PlayerState
{
    public MovingState(CharacterBehaviour characterBehaviour) : base(characterBehaviour) { }

    public override void Enter()
    {
        characterBehaviour.PlayAnimation("Character_Walk");
    }

    public override void Update()
    {
        float direction = characterBehaviour.GetMovementDirection();

        // Aplica o Flip com base na dire��o de movimento
        if (direction != 0)
        {
            characterBehaviour.Flip(direction);
        }

        if (!characterBehaviour.IsMoving())
        {
            characterBehaviour.ChangeState(new IdleState(characterBehaviour));
        }

        if (!characterBehaviour.IsGrounded())
        {
            characterBehaviour.ChangeState(new JumpingState(characterBehaviour));
        }

        if (characterBehaviour.IsRunning())
        {
            characterBehaviour.ChangeState(new RunningState(characterBehaviour));
        }
    }
}

public class RunningState : PlayerState
{
    public RunningState(CharacterBehaviour characterBehaviour) : base(characterBehaviour) { }

    public override void Enter()
    {
        characterBehaviour.PlayAnimation("Character_Run");
    }

    public override void Update()
    {
        float direction = characterBehaviour.GetMovementDirection();

        // Aplica o Flip com base na dire��o de movimento
        if (direction != 0)
        {
            characterBehaviour.Flip(direction);
        }

        if (!characterBehaviour.IsMoving())
        {
            characterBehaviour.ChangeState(new IdleState(characterBehaviour));
        }

        if (!characterBehaviour.IsGrounded())
        {
            if (characterBehaviour.GetVerticalVelocity() > 0 )
            {
                characterBehaviour.ChangeState(new JumpingState(characterBehaviour));
            }
            else
            {
                characterBehaviour.ChangeState(new FallingState(characterBehaviour));
            }            
        }

        if (!characterBehaviour.IsRunning() && characterBehaviour.IsMoving())
        {
            characterBehaviour.ChangeState(new MovingState(characterBehaviour));
        }
    }
}

public class JumpingState : PlayerState
{
    public JumpingState(CharacterBehaviour characterBehaviour) : base(characterBehaviour) { }

    public override void Enter()
    {
        characterBehaviour.PlayAnimation("Character_Jump");
        characterBehaviour.ApplyJump();
    }

    public override void Update()
    {
        float direction = characterBehaviour.GetMovementDirection();

        // Aplica o Flip com base na dire��o de movimento
        if (direction != 0)
        {
            characterBehaviour.Flip(direction);
        }
        if (characterBehaviour.GetVerticalVelocity() < 0)
        {
            characterBehaviour.ChangeState(new FallingState(characterBehaviour));
        }
        if (characterBehaviour.IsGrounded() && characterBehaviour.GetVerticalVelocity() <= 0)
        {
            characterBehaviour.ChangeState(new IdleState(characterBehaviour));
        }
    }
}

public class FallingState : PlayerState
{
    public FallingState(CharacterBehaviour characterBehaviour) : base(characterBehaviour) { }

    public override void Enter()
    {
        characterBehaviour.PlayAnimation("Character_Fall");
    }

    public override void Update()
    {
        float direction = characterBehaviour.GetMovementDirection();

        // Aplica o Flip com base na dire��o de movimento
        if (direction != 0)
        {
            characterBehaviour.Flip(direction);
        }
        // Transi��o para Idle ou Running se o personagem estiver no ch�o
        if (characterBehaviour.IsGrounded())
        {
            characterBehaviour.ChangeState(new IdleState(characterBehaviour));
        }

        if (!characterBehaviour.IsGrounded() && characterBehaviour.GetVerticalVelocity() > 0)
        {
            characterBehaviour.ChangeState(new JumpingState(characterBehaviour)); //altera para pulo caso utilize o coyote time
        }
    }
}


