using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Services.Entity;
using Services.Model;
using Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ChapterService : IChapterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChapterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Chapter> CreateChapter(ChapterModel req)
        {
            Chapter chapter = new Chapter()
            {
                Id = (req.Id.IsNullOrEmpty()) ? Guid.NewGuid().ToString() : req.Id,
                Title = req.Title,
                CreationDate = DateTime.Now,
                IsDeleted = false,
                CourseId = req.CourseId,
                Description = req.Description,
                videoUrl = req.videoUrl,
                isPulished = req.isPulished
            };
            await _unitOfWork.chapterRepo.CreateAsync(chapter);
            await _unitOfWork.SaveChangeAsync();
            return chapter;
        }
        public async Task<bool> UpdateChapter(ChapterModel req, string id)
        {
            Chapter chapter = await _unitOfWork.chapterRepo.GetEntityByIdAsync(id);
            if (chapter == null)
            {
                return false;
            }
            chapter.Title = req.Title;
            chapter.ModificationDate = DateTime.Now;
            chapter.Description = req.Description;
            chapter.videoUrl = req.videoUrl;
            chapter.isPulished = req.isPulished;
            _unitOfWork.chapterRepo.UpdateAsync(chapter);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<bool> UpdateChapterStatus(string id)
        {
            Chapter chapter = await _unitOfWork.chapterRepo.GetEntityByIdAsync(id);
            if (chapter == null)
            {
                return false;
            }
            chapter.isPulished = true;
            _unitOfWork.chapterRepo.UpdateAsync(chapter);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<bool> UpdateUnPublishChapter(string id)
        {
            Chapter chapter = await _unitOfWork.chapterRepo.GetEntityByIdAsync(id);
            if (chapter == null)
            {
                return false;
            }
            chapter.isPulished = false;
            _unitOfWork.chapterRepo.UpdateAsync(chapter);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }

        public async Task<bool> DeleteChapter(string id)
        {
            Chapter chapter = await _unitOfWork.chapterRepo.GetEntityByIdAsync(id);
            if (chapter == null)
            {
                return false;
            }
            chapter.DeletionDate = DateTime.Now;
            _unitOfWork.chapterRepo.UpdateAsync(chapter);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<Chapter> GetChapterById(string id)
        {
            Chapter chapter = await _unitOfWork.chapterRepo.GetEntityByIdAsync(id);
            return chapter;
        }
        public async Task<List<Chapter>> GetChaptersAsync()
        {
            List<Chapter> chapters = (await _unitOfWork.chapterRepo.GetAllAsync()).ToList();
            List<Chapter> result = new List<Chapter>();
            foreach (var item in chapters)
            {
                if (item.IsDeleted == false)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
