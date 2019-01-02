namespace Hurricane_Evacuation_Planner.Environment
{
    public class Simulator
    {
        public int Deadline { get; }
        public IState State { get; }
        public StatesGenerator Clazz { get; }

        public Simulator(IState initialState)
        {
            Deadline = initialState.Deadline;
            State = initialState;
            Clazz = new StatesGenerator(State);
        }

    }
}
