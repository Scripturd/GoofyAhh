namespace GoofyAhh.BuckshotRoulette;

public class Context
{
    private readonly Dictionary<Type, IState> _states = [];
    private IState _state;

    public void AddState(IState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void TransitionTo<TState>()
    where TState : IState
    {
        if (!_states.TryGetValue(typeof(TState), out IState? state))
            throw new InvalidOperationException(
                $"State of type {typeof(TState).Name} has not been added.");

        if (_state == state)
            return;

        _state = state;
        _state.Start();
    }
}