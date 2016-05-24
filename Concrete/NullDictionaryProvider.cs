using System.Collections.Generic;

namespace Barbar.Ball.Concrete
{
    internal class NullDictionaryProvider : IDictionaryProvider
    {
        public IList<string> Load()
        {
            return new List<string>();
        }

        public void Save(IList<string> keywords)
        {
        }
    }
}
