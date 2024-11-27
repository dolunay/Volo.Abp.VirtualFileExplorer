using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.VirtualFileExplorer.Blazor.Models;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Components;

public partial class FilePathComponent : ComponentBase
{
    [Parameter]
    public object Data { get; set; }

    protected FileContentModal FileContentModal;

    private FileInfoViewModel FileInfo => Data.As<FileInfoViewModel>();

    protected virtual async Task FolderClickedAsync()
    {
        await FileInfo.PathChanged(FileInfo.FilePath);
    }

    protected virtual async Task FileClickedAsync()
    {
        await FileContentModal.OpenAsync(FileInfo.FilePath);
    }
}
