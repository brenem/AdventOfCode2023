namespace AdventOfCode2023
{
    public interface IDay<in TIn, out TOut>
    {
        public IDay<TIn, TOut> Run(TIn inputData);
        public TOut Result { get; }
    }
}
