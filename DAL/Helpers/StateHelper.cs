using Domain;

namespace DAL.Helpers
{
    public static class StateHelper
    {
        public static State ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return State.Added;
                case State.Deleted:
                    return State.Deleted;
                case State.Modified:
                    return State.Modified;
                default:
                    return State.UnChanged;
            }
        }
    }
}
