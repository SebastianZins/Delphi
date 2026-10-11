public interface IVaultWriter
{
    Task CreateMarkdownAsync(
        string relativePath,
        string content,
        CancellationToken cancellationToken = default);

    Task AppendMarkdownAsync(
        string relativePath,
        string content,
        CancellationToken cancellationToken = default);
}