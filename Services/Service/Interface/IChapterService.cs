using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface IChapterService
    {
        Task<Chapter> CreateChapter(ChapterModel req);
        Task<bool> UpdateChapter(ChapterModel req, string id);
        Task<bool> DeleteChapter(string id);
        Task<Chapter> GetChapterById(string id);
        Task<List<Chapter>> GetChaptersAsync();
    }
}
