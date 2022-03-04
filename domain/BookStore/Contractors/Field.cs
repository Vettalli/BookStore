namespace BookStore.Contractors
{
    public abstract class Field
    {
        public string Label { get; } //select smth(city example)

        public string Title { get; } //title of the field, texhnical stuff (city etc)

        public string Value { get; } 

        protected Field(string label, string title, string value)
        {
            Label = label;
            Title = title;
            Value = value;
        }
    }
}