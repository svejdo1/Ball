using System.Collections.Generic;

namespace Barbar.Ball
{
    public interface IDictionaryProvider
    {
        IList<string> Load();
        void Save(IList<string> keywords);
    }
}
