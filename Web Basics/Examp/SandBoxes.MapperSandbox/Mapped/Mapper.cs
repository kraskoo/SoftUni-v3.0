namespace SandBoxes.MapperSandbox.Mapped
{
    using System;
    using System.Linq.Expressions;

    public static class Mapper
    {
        public static TOut GetMapped<TIn, TOut>(this TIn @in, Expression<Func<TIn, TOut>> expr)
            where TIn : class, new()
            where TOut : class, new()
        {
            return expr.Compile().Invoke(@in);
        }
    }
}