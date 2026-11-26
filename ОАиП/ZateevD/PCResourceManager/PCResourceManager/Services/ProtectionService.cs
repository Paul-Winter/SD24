using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using PCResourceManager.Models;
namespace PCResourceManager.Services
{
    public class ProtectionService
    {
        private readonly SettingsRepository _repo;
        private List<ProtectedItem> _protected;
        private static readonly string[] SystemRoots =
        {
            @"C:\Windows", @"C:\Program Files", @"C:\Program Files (x86)",
            @"C:\ProgramData", Environment.GetFolderPath(Environment.SpecialFolder.Windows)
        };
        public ProtectionService(SettingsRepository repo)
        {
            _repo = repo;
            _protected = _repo.LoadProtected();
        }
        public bool IsProtected(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;
            var full = Path.GetFullPath(path);
            return _protected.Any(p =>
                p.IsFolder
                    ? full.StartsWith(p.Path, StringComparison.OrdinalIgnoreCase)
                    : string.Equals(full, p.Path, StringComparison.OrdinalIgnoreCase));
        }
        public bool IsSystemPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;
            var full = Path.GetFullPath(path);
            return SystemRoots.Any(r => !string.IsNullOrEmpty(r) &&
                full.StartsWith(r, StringComparison.OrdinalIgnoreCase));
        }
        public void Protect(string path, bool isFolder)
        {
            if (IsProtected(path)) return;
            _protected.Add(new ProtectedItem { Path = Path.GetFullPath(path), IsFolder = isFolder });
            _repo.SaveProtected(_protected);
        }
        public void Unprotect(string path)
        {
            _protected.RemoveAll(p => string.Equals(p.Path, path, StringComparison.OrdinalIgnoreCase));
            _repo.SaveProtected(_protected);
        }
        public IReadOnlyList<ProtectedItem> All => _protected;
        public bool CanDelete(FileInfoModel f)
        {
            if (f.IsProtected) return false;
            if (IsProtected(f.FullPath)) return false;
            if (IsSystemPath(f.FullPath)) return false;
            return true;
        }
    }
}
