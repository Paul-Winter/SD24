using System;
using System.Collections.Generic;
using System.Text;
namespace PCResourceManager.Models;
public enum FileCategory
{
    CanDelete,
    DoNotDelete,
    NeedsReview,
    System
}