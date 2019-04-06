using LogixHealth.Eligibility.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.BusinessFunctions.Interfaces
{
    public interface IFileViewerService
    {
        List<FileViewerViewModel> GetAll();

        List<FileViewerViewModel> GetAll(string keyword);
        FileViewerViewModel GetById(int id);
        FileViewerViewModel Add(FileViewerViewModel fileViewer);

        void Update(FileViewerViewModel fileViewer);
        void Delete(int id);
        void Save();

    }
}
