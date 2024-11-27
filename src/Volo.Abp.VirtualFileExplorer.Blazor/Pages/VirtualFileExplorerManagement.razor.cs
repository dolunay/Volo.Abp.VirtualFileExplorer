using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.VirtualFileExplorer.Blazor.Components;
using Volo.Abp.VirtualFileExplorer.Blazor.Localization;
using Volo.Abp.VirtualFileExplorer.Blazor.Models;
using System.IO;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Pages;

public partial class VirtualFileExplorerManagement
{
    protected FileContentModal FileContentModal;

    //protected string ManageVirtualFileExplorerPolicyName;

    protected bool HasManageVirtualFileExplorerPermission { get; set; } = true;

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> VirtualFileExplorerManagementTableColumns => TableColumns.Get<VirtualFileExplorerManagement>();

    public VirtualFileExplorerManagement()
    {
        ObjectMapperContext = typeof(AbpVirtualFileExplorerBlazorModule);
        LocalizationResource = typeof(VirtualFileExplorerResource);

        //ManageVirtualFileExplorerPolicyName = VirtualFileExplorerVirtualFileExplorer.Roles.ManageVirtualFileExplorer;
    }

    public string CurrentPath { get; set; } = "/";

    protected override Task UpdateGetListInputAsync()
    {
        GetListInput.Path = CurrentPath;
        GetListInput.PathChanged = PathChanged;
        return base.UpdateGetListInputAsync();
    }

    public async Task PathChanged(string path)
    {
        CurrentPath = path;
        await GetEntitiesAsync();
        BreadcrumbItems.Clear();
        await SetBreadcrumbItemsAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new BlazoriseUI.BreadcrumbItem(L["Menu:VirtualFileExplorer"].Value));
        var paths = CurrentPath.TrimStart('/').TrimEnd('/').Split("/");
        foreach (var path in paths)
        {
            BreadcrumbItems.Add(new BlazoriseUI.BreadcrumbItem(path));
        }

        return base.SetBreadcrumbItemsAsync();
    }

    public async Task GotoParentPath()
    {
        if (CurrentPath != null)
        {
            var paths = CurrentPath.TrimStart('/').TrimEnd('/').Split("/").ToList();
            if (paths.Count > 0)
            {
                var newPath = "/";
                for (var i = 0; i < paths.Count - 1; i++)
                {
                    var path = paths[i];
                    newPath = newPath + path + "/";
                }
                newPath = newPath.TrimEnd('/');
                await PathChanged(newPath);
            }
        }
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton("..", GotoParentPath,
            "fas fa-folder");

        return base.SetToolbarItemsAsync();
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<VirtualFileExplorerManagement>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["FileContent"],
                        Visible = (data) => !data.As<FileInfoViewModel>().IsDirectory,
                        Clicked = async (data) =>
                        {
                            await FileContentModal.OpenAsync(data.As<FileInfoViewModel>().FilePath);
                        }
                   }
            });

        return base.SetEntityActionsAsync();
    }

    protected override async ValueTask SetTableColumnsAsync()
    {
        VirtualFileExplorerManagementTableColumns
            .AddRange(new TableColumn[]
            {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<VirtualFileExplorerManagement>(),
                    },
                    new TableColumn
                    {
                        Title = L["VirtualFileName"],
                        Sortable = true,
                        Data = nameof(FileInfoViewModel.FileName),
                        Component=typeof(FilePathComponent)
                    },
                    new TableColumn
                    {
                        Title = L["VirtualFileType"],
                        Sortable = true,
                        Data = nameof(FileInfoViewModel.FileType)
                    },
                    new TableColumn
                    {
                        Title = L["LastUpdateTime"],
                        Sortable = true,
                        Data = nameof(FileInfoViewModel.LastUpdateTime)
                    },
                    new TableColumn
                    {
                        Title = L["Size"],
                        Sortable = true,
                        Data = nameof(FileInfoViewModel.Length)
                    },
            });

        //VirtualFileExplorerManagementTableColumns.AddRange(await GetExtensionTableColumnsAsync(IdentityModuleExtensionConsts.ModuleName,
        //    IdentityModuleExtensionConsts.EntityNames.Role));

        await base.SetTableColumnsAsync();
    }
}
