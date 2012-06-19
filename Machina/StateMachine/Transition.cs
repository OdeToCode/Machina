namespace Machina.StateMachine
{
    internal class Transition<TEnum>
    {
        public Transition(TEnum when, string then, TEnum goesTo)
        {
            When = when;
            Then = then;
            GoesTo = goesTo;
        }

        public TEnum When { get; protected set; }
        public string Then { get; protected set; }
        public TEnum GoesTo { get; protected set; }

        public override string ToString()
        {
            return string.Format("When {0} Then {1} Goes To {2}", When, Then, GoesTo);
        }
    }
}