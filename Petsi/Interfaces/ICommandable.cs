namespace Petsi.Interfaces
{
    public interface ICommandable
    {
        /// <summary>
        /// ICommandable method
        /// </summary>
        /// <returns></returns>
        public string GetComponentName();

        /// <summary>
        /// ICommandable method
        /// </summary>
        public Task Actions(Stack<ICommandable> contextChain, string actionIdentifier);

        public void CommandFrameView();
    }
}
