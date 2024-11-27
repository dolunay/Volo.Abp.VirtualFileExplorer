using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.VirtualFileExplorer.Blazor.Models;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.VirtualFileSystem.Embedded;

namespace Volo.Abp.VirtualFileExplorer.Blazor;

public class VirtualFileExplorerAppService(IVirtualFileProvider virtualFileProvider) : IVirtualFileExplorerAppService
{
    public Task<FileInfoViewModel> GetAsync(string id)
    {
        throw new System.NotImplementedException();
    }

    public Task<PagedResultDto<FileInfoViewModel>> GetListAsync(GetVirtualFileExplorerInput input)
    {
        var query = virtualFileProvider.GetDirectoryContents(input.Path)
            .Where(d => VirtualFileExplorerConsts.AllowFileInfoTypes.Contains(d.GetType().Name))
            .OrderByDescending(f => f.IsDirectory).ToList();

        var fileInfos = query.Skip(input.SkipCount).Take(input.MaxResultCount);
        var fileInfoList = SetViewModel(fileInfos, input);
        var result = new PagedResultDto<FileInfoViewModel>
        {
            TotalCount = query.Count,
            Items = fileInfoList
        };
        return Task.FromResult(result);
    }
    private List<FileInfoViewModel> SetViewModel(IEnumerable<IFileInfo> fileInfos, GetVirtualFileExplorerInput input)
    {
        var fileInfoList = new List<FileInfoViewModel>();

        foreach (var fileInfo in fileInfos)
        {
            var fileInfoViewModel = new FileInfoViewModel()
            {
                IsDirectory = fileInfo.IsDirectory,
                Icon = "fas fa-file",
                FileType = "file",
                Length = fileInfo.Length + " bytes",
                FileName = fileInfo.Name,
                LastUpdateTime = fileInfo.LastModified.LocalDateTime
            };

            var filePath = fileInfo.PhysicalPath ?? $"{input.Path.EnsureEndsWith('/')}{fileInfo.Name}";

            if (fileInfo.IsDirectory)
            {
                fileInfoViewModel.Icon = "fas fa-folder";
                fileInfoViewModel.FileType = "folder";
                fileInfoViewModel.Length = "/";
                //fileInfoViewModel.FileName = $"<a href='{Url.Content("~/")}VirtualFileExplorer?path={filePath}'>{fileInfo.Name}</a>";
                fileInfoViewModel.FileName = fileInfo.Name;
                fileInfoViewModel.FilePath = filePath;
            }
            else
            {
                if (fileInfo is EmbeddedResourceFileInfo embeddedResourceFileInfo)
                {
                    fileInfoViewModel.FilePath = embeddedResourceFileInfo.VirtualPath;
                }
                else
                {
                    fileInfoViewModel.FilePath = filePath;
                }
            }

            fileInfoViewModel.Id = fileInfoViewModel.FilePath;
            fileInfoViewModel.PathChanged = input.PathChanged;
            fileInfoList.Add(fileInfoViewModel);
        }
        return fileInfoList;
    }
}