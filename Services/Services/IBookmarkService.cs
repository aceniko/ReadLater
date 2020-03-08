using System.Collections.Generic;
using ReadLater.Entities;

namespace ReadLater.Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark);
        Bookmark UpdateBookmark(Bookmark bookmark);
        bool DeleteBookmark(int bookmark);
        List<Bookmark> GetBookmarks(string category);
    }
}