using System;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileProviders;
using Volo.Abp.VirtualFileExplorer.Blazor.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Components;

public partial class FileContentModal
{
    public string Content { get; set; }

    protected Modal _modal;

    [Inject]
    protected IVirtualFileProvider VirtualFileProvider { get; set; }

    public FileContentModal()
    {
        LocalizationResource = typeof(VirtualFileExplorerResource);
    }

    public virtual async Task OpenAsync(string filePath)
    {
        try
        {
            var fileInfo = VirtualFileProvider.GetFileInfo(filePath);
            if (fileInfo == null || fileInfo.IsDirectory)
            {
                Content = "ERROR";
            }
            else
            {
                Content = await fileInfo.ReadAsStringAsync();
            }
            await InvokeAsync(_modal.Show);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected Task CloseModal()
    {
        return InvokeAsync(_modal.Hide);
    }

    protected virtual Task ClosingModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }
}
