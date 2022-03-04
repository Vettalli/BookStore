using System.Collections.Generic;

namespace BookStore.Contractors
{
    public class SelectionField : Field
    {
        public IReadOnlyDictionary<string, string> Items { get; }

        public SelectionField(string label,string title, string value, IReadOnlyDictionary<string, string> items) : base(label, title, value)
        {
            Items = items;
        }
    }
}
