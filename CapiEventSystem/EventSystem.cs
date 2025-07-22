using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CapiEventSystem;

public class EventSystem
{
    private readonly Subject<CapiEvent> _eventStream = new();
    public IObservable<T> Events<T>() where T : CapiEvent 
        => _eventStream.OfType<T>().AsObservable();

    public void Publish(CapiEvent evt) => _eventStream.OnNext(evt);
}