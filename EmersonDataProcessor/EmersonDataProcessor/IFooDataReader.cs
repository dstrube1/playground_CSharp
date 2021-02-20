using System.Collections.Generic;
using EmersonDataProcessor.model;

namespace EmersonDataProcessor
{
    public interface IFooDataReader
    {
        public List<MergedListItem> Read(IFoo foo);
    }
}
