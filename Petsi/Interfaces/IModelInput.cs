using Petsi.Units;

namespace Petsi.Interfaces
{
    public interface IModelInput
    {
        /// <summary>
        /// From IModelInput interface.
        /// </summary>
        /// <param name="unit"></param>
        public void AddData(ModelUnitBase unit);

        public void Complete();
    }
}
