namespace AdventOfCode2023
{
    internal interface IDay<out T>
    {
        public IDay<T> Run();
        public T Result { get; }
    }
}
