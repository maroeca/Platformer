public abstract class PlayerState
{
    protected CharacterBehaviour characterBehaviour;

    public PlayerState(CharacterBehaviour characterBehaviour)
    {
        this.characterBehaviour = characterBehaviour;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
