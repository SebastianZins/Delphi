using Delphi.Application.Models;
using Delphi.Application.Reader;

public interface IVaultRepository
{
    Task<IReadOnlyList<VaultEntry>> ListDirectoryAsync(
        string relativePath,
        CancellationToken cancellationToken = default);

    Task<MarkdownDocument> ReadMarkdownAsync(
        string relativePath,
        CancellationToken cancellationToken = default);

    Task CreateMarkdownAsync(
        string relativePath,
        string content,
        CancellationToken cancellationToken = default);

    Task AppendMarkdownAsync(
        string relativePath,
        string content,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<string> EnumerateMarkdownFilesAsync(
        string relativeDirectory,
        CancellationToken cancellationToken = default);
}