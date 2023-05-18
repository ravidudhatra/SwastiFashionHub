using Microsoft.AspNetCore.Components;
using SwastiFashionHub.Shared.Core.Services.Interface;
using SwastiFashionHub.Shared.Core.Services;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Shared.Core.Enum;
using Radzen;
using Radzen.Blazor;
using SwastiFashionHub.Shared.Core.Exceptions;
using System.Data;
using Microsoft.JSInterop;

namespace SwastiFashionHub.Pages.Master
{
    public partial class Fabric
    {
        [Inject]
        public IFabricService? FabricService { get; set; }

        [Inject]
        public IToastService? ToastService { get; set; }

        [Inject]
        public SpinnerService? SpinnerService { get; set; }

        [Inject]
        public IConfirmService? ConfirmService { get; set; }

        [Inject]
        public DialogService? DialogService { get; set; }

        public string? ErrorMessage { get; set; }
        public bool ShowModel { get; set; } = false;
        private List<FabricResponse>? ItemsData;

        private FabricRequest? FabricModel = new();
        public FabricType SelectedFabricType { get; set; }
        int totalCount = 0;

        private Guid TableId { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                TableId = Guid.NewGuid();
                await BindDataAsync();

                var options = new
                {
                    paging = true,
                    searching = true,
                    ordering = true
                };

                await JsRuntime.InvokeAsync<DataTable>("dataTable.init", TableId.ToString(), options);
            }
        }
        private async Task HandleValidSubmit()
        {
            try
            {
                ToastService.ClearAll();
                if (FabricModel != null && FabricModel.Id != Guid.Empty)
                {
                    await FabricService.Update(FabricModel);
                    ToastService.ShowSuccess("Fabric updated successfully.");
                }
                else
                {
                    FabricModel.Id = Guid.NewGuid();
                    await FabricService.Add(FabricModel);
                    ToastService.ShowSuccess("Fabric save successfully.");
                }
            }
            catch (AppException ex)
            {
                if (ex.Exceptions.Count > 0)
                    ToastService.ShowWarning(String.Join(", ", ex.Exceptions));
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                await BindDataAsync();
                await JsRuntime.InvokeAsync<DataTable>("dataTable.refreshDataTable");
                FabricModel = new FabricRequest();
                ShowModel = false;
            }
        }

        private async Task OnAddNewItemClick()
        {
            FabricModel = new FabricRequest();
            ShowModel = true;
            StateHasChanged();
        }

        private void CloseModelClick()
        {
            ShowModel = false;
            StateHasChanged();
        }

        private async Task BindDataAsync()
        {
            await SpinnerService.Show();
            var FabricData = await FabricService.GetAll();

            ItemsData = FabricData.Select(x => new FabricResponse
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            totalCount = FabricData?.Count ?? 0;
            await SpinnerService.Hide();
            StateHasChanged();
        }

        private async Task OnEditItemClick(FabricResponse item)
        {
            if (ItemsData == null)
                ToastService.ShowError("Item data missing.");
            else
            {
                FabricModel = ItemsData?
                    .Select(x => new FabricRequest
                    {
                        Id = item.Id,
                        CreatedBy = item.CreatedBy,
                        Name = item.Name,
                    }).FirstOrDefault(x => x.Id == item.Id);

                ShowModel = true;
            }

            await Task.CompletedTask;
        }
        private async Task OnDeleteItemClick(FabricResponse item)
        {
            await ConfirmService.Show($"Are you sure you want to delete {item.Name}?", "Yes",
        async () => await ConfirmedDelete(item), "Cancel",
        async () => await ConfirmService.Clear());
        }

        public async Task ConfirmedDelete(FabricResponse Fabric)
        {
            try
            {
                await ConfirmService.Clear();
                await SpinnerService.Show();
                await FabricService.Delete(Fabric.Id);
                await SpinnerService.Hide();

                ToastService.ShowSuccess($"{Fabric.Name} deleted!");

                await BindDataAsync();
                StateHasChanged();
            }
            catch (AppException ex)
            {
                if (ex.Exceptions.Count > 0)
                    ToastService.ShowWarning(String.Join(", ", ex.Exceptions));
            }
            catch (Exception ex)
            {
                await SpinnerService.Hide();
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                await SpinnerService.Hide();
            }
        }
    }
}
