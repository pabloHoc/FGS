namespace FSG.Data
{
    public struct Modifier
    {
        public int Add { get; }

        public int Mult { get; }

        public int Reduction { get; }
    }

    public struct Attribute
    {
        public int Value { get; }

        public Modifier Modifiers { get; }
    }
}