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
    public partial class Party
    {
        [Inject]
        public IPartyService? PartyService { get; set; }

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
        private List<PartyResponse>? ItemsData;

        private PartyRequest? PartyModel = new();
        public PartyType SelectedPartyType { get; set; }
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
                if (PartyModel != null && PartyModel.Id != Guid.Empty)
                {
                    PartyModel.PartyType = (int)SelectedPartyType;
                    await PartyService.Update(PartyModel);
                    ToastService.ShowSuccess("Party updated successfully.");
                }
                else
                {
                    PartyModel.PartyType = (int)SelectedPartyType;
                    PartyModel.Id = Guid.NewGuid();
                    await PartyService.Add(PartyModel);
                    ToastService.ShowSuccess("Party save successfully.");
                    await BindDataAsync();
                }
                PartyModel = new PartyRequest();
                ShowModel = false;
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
        }

        private async Task OnAddNewItemClick()
        {
            PartyModel = new PartyRequest();
            SelectedPartyType = PartyType.Buyer;
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
            var PartyData = await PartyService.GetAll();

            ItemsData = PartyData.Select(x => new PartyResponse
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.Name,
                PartyType = x.PartyType
            }).ToList();

            totalCount = PartyData?.Count ?? 0;
            await SpinnerService.Hide();
            StateHasChanged();
        }

        private async Task OnEditItemClick(PartyResponse item)
        {
            if (ItemsData == null)
                ToastService.ShowError("Item data missing.");
            else
            {
                PartyModel = ItemsData?
                    .Select(x => new PartyRequest
                    {
                        Id = item.Id,
                        CreatedBy = item.CreatedBy,
                        Name = item.Name,
                        PartyType = item.PartyType,
                    }).FirstOrDefault(x => x.Id == item.Id);

                ShowModel = true;
                SelectedPartyType = (PartyType)PartyModel.PartyType;
            }

            await Task.CompletedTask;
        }
        private async Task OnDeleteItemClick(PartyResponse item)
        {
            await ConfirmService.Show($"Are you sure you want to delete {item.Name}?", "Yes",
        async () => await ConfirmedDelete(item), "Cancel",
        async () => await ConfirmService.Clear());
        }

        public async Task ConfirmedDelete(PartyResponse party)
        {
            try
            {
                await ConfirmService.Clear();
                await SpinnerService.Show();
                await PartyService.Delete(party.Id);
                await SpinnerService.Hide();

                ToastService.ShowSuccess($"{party.Name} deleted!");
                //await BindDataAsync();
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
