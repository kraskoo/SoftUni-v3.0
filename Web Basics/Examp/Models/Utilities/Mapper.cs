namespace Models.Utilities
{
    using System;
    using System.Linq.Expressions;

    internal class Mapper
    {
        public static TOut GetMapped<TIn, TOut>(TIn @in, Expression<Func<TIn, TOut>> expr)
            where TIn : class, new()
            where TOut : class, new()
        {
            return expr.Compile().Invoke(@in);
        }
    }
}