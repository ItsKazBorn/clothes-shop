
public interface IInteractible
{
    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor);
}
