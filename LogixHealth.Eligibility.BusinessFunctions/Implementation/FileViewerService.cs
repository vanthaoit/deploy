using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogixHealth.Eligibility.BusinessFunctions.Interfaces;
using LogixHealth.Eligibility.DataAccess.Infrastructure.Interfaces;
using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.Eligibility.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogixHealth.Eligibility.BusinessFunctions.Implementation
{
    public class FileViewerService : IFileViewerService
    {
        private IRepository<FileViewer, int> _fileViewerRepository;
       
        public FileViewerService(IRepository<FileViewer,int> fileViewerRepository)
        {
            _fileViewerRepository = fileViewerRepository;
           
        }


        public void Delete(int id)
        {
            _fileViewerRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<FileViewerViewModel> GetAll()
        {
            var response = _fileViewerRepository
                .FindAll()
                .OrderBy(x=>x.ModifiedDate)
                .ProjectTo<FileViewerViewModel>()
                .ToList();
            return response;
        }

        public List<FileViewerViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _fileViewerRepository.FindAll(x => x.Name.Contains(keyword)
                || x.LineCount.Contains(keyword)).OrderBy(x=>x.ModifiedDate)
                    .ProjectTo<FileViewerViewModel>().ToList();
            else
                return _fileViewerRepository.FindAll()
                    .OrderBy(x => x.ModifiedDate)
                    .ProjectTo<FileViewerViewModel>()
                    .ToList();
        }

        public FileViewerViewModel GetById(int id)
        {
            return Mapper.Map<FileViewerViewModel>(_fileViewerRepository.FindById(id));
        }
        public void Update(FileViewerViewModel fileViewer)
        {
            var response = Mapper.Map<FileViewerViewModel, FileViewer>(fileViewer);
            _fileViewerRepository.Update(response);
        }

        public void Save()
        {
            
        }

        public FileViewerViewModel Add(FileViewerViewModel fileViewer)
        {
            var response = Mapper.Map<FileViewerViewModel, FileViewer>(fileViewer);
            _fileViewerRepository.Add(response);
            return fileViewer;
        }
    }
}
