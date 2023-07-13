namespace TreeView.Tree
{
    public static class Extensions
    {
        public static void DoEither(this bool condition, Action action1, Action action2)
        {
            if (condition)
            {
                action1();
            }
            else
            {
                action2();
            }
        }
    }
}
