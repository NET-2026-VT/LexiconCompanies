namespace Domain.Contracts;

public interface IPagedList<T>
{
    IReadOnlyList<T> Items { get; }
    int TotalCount { get; }
}