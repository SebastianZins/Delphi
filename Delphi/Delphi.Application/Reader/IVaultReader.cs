using Delphi.Application.Models;
using Delphi.Application.Reader;

public interface IVaultReader
{
    Task<IReadOnlyList<VaultEntry>> ListDirectoryAsync(
        string relativePath,
        CancellationToken cancellationToken = default);

    Task<MarkdownDocument> ReadMarkdownAsync(
        string relativePath,
        CancellationToken cancellationToken = default);
}