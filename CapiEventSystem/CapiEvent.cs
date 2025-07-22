namespace CapiEventSystem;

public abstract record CapiEvent(DateTime OccuredAt)
{
    protected CapiEvent() : this(DateTime.Now) { }
}

public record NotificationEvent(string Message) : CapiEvent;