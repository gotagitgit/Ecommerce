namespace Infrastructure.Common;

public interface IDatabaseContext<T>
{
    public T DbContext { get; }
}