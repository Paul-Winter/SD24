using System;
using System.Collections.Generic;
using System.Text;
using PCResourceManager.Models;
using PCResourceManager.Services;
namespace PCResourceManager.Services
{
    public interface IDiskScanner
    {
        Task<ScanResult> ScanAsync(string root, IProgress<int>? progress, CancellationToken ct);
        List<FileInfoModel> Search(ScanResult result, string query);
    }
    public class DiskScanner(ProtectionService protection) : IDiskScanner
    {
        private static readonly EnumerationOptions SafeOptions = new()
        {
            RecurseSubdirectories = false,
            IgnoreInaccessible = true,
            AttributesToSkip = FileAttributes.System | FileAttributes.ReparsePoint,
            MatchType = MatchType.Simple
        };
        public List<(string Path, long Size)> GetHeaviestFolders(ScanResult result, int topCount = 20)
        {
            return result.Items
                .GroupBy(f => Path.GetDirectoryName(f.FullPath))
                .Where(g => g.Key != null)
                .Select(g => (Path: g.Key!, Size: g.Sum(f => f.SizeBytes)))
                .OrderByDescending(x => x.Size)
                .Take(topCount)
                .ToList();
        }
        public Task<ScanResult> ScanAsync(string root, IProgress<int>? progress, CancellationToken ct)
            => Task.Run(() => Scan(root, progress, ct), ct);
        private ScanResult Scan(string root, IProgress<int>? progress, CancellationToken ct)
        {
            var result = new ScanResult { RootPath = root, StartedAt = DateTime.UtcNow };
            int count = 0;
            var stack = new Stack<string>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                ct.ThrowIfCancellationRequested();
                var dir = stack.Pop();
                try
                {
                    foreach (var file in Directory.EnumerateFiles(dir, "*", SafeOptions))
                    {
                        try
                        {
                            var fi = new FileInfo(file);
                            bool isSystem = protection.IsSystemPath(fi.FullName);
                            var model = new FileInfoModel
                            {
                                FullPath = fi.FullName,
                                Name = fi.Name,
                                SizeBytes = fi.Length,
                                LastModified = fi.LastWriteTime,
                                FileType = fi.Extension.TrimStart('.').ToLowerInvariant(),
                                IsProtected = protection.IsProtected(fi.FullName),
                                IsSystem = isSystem,
                                Category = isSystem ? FileCategory.System : FileCategory.NeedsReview
                            };
                            result.Items.Add(model);
                            result.TotalSizeBytes += fi.Length;
                            if (++count % 200 == 0) progress?.Report(count);
                        }
                        catch (UnauthorizedAccessException) { }
                        catch (IOException) { }
                    }
                    foreach (var sub in Directory.EnumerateDirectories(dir, "*", SafeOptions))
                    {
                        if (!protection.IsSystemPath(sub)) stack.Push(sub);
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (IOException) { }
            }
            result.FinishedAt = DateTime.UtcNow;
            progress?.Report(count);
            return result;
        }
        public List<FileInfoModel> Search(ScanResult result, string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return result.Items;
            return result.Items
                .Where(i => i.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                         || i.FullPath.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
