using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using SwastiFashionHub.Data.Models;
using SwastiFashionHub.Shared.Core.Extensions;
using SwastiFashionHub.Shared.Core.Helper;
using SwastiFashionHub.Shared.Core.Models;
using SwastiFashionHub.Shared.Core.Services;
using SwastiFashionHub.Shared.Core.Services.Interface;


namespace SwastiFashionHub.Pages.Master
{
    public partial class Designs
    {
        [Inject]
        public IDesignService DesignService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public SpinnerService SpinnerService { get; set; }

        [Inject]
        public IConfirmService ConfirmService { get; set; }

        public string ErrorMessage { get; set; }

        public int MaxFileSize = 512000;
        public int MaxAllowedFiles = 1;


        public bool ShowModel { get; set; } = false;
        public List<ColumnDefinition> Columns { get; set; }
        private List<DesignViewModel> ItemsData;
        private Design DesignModel = new();

        public ElementReference dropZoneElement;
        public ElementReference inputFileContainer;

        private async Task HandleSubmit()
        {
            try
            {
                ToastService.ClearAll();
                if (DesignModel != null && DesignModel.Id != Guid.Empty)
                {
                    await DesignService.Update(DesignModel);
                    ToastService.ShowSuccess("Design updated successfully.");
                }
                else
                {
                    DesignModel.Id = Guid.NewGuid();
                    await DesignService.Add(DesignModel);
                    ToastService.ShowSuccess("Design save successfully.");
                }

                await BindDataAsync();
                ShowModel = false;
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }

        }

        private void ShowModelClick()
        {
            ShowModel = true;
            StateHasChanged();
        }

        private void CloseModelClick()
        {
            ShowModel = false;
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await BindDataAsync();
                StateHasChanged();
            }
        }

        private async Task BindDataAsync()
        {
            await SpinnerService.Show();

            Columns = DataTableHelper.BindDataTableColumn<DesignViewModel>();

            var designData = await DesignService.GetAll();

            ItemsData = designData.Select(x => new DesignViewModel
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.Name,
                Note = x.Note
            }).ToList();

            await SpinnerService.Hide();
            StateHasChanged();
        }


        /// <summary>
        /// when start inserting file for fallback image remove error message
        /// </summary>
        public void OnInputFile()
        {
            ErrorMessage = string.Empty;
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            ErrorMessage = string.Empty;

            await SpinnerService.Show();

            try
            {
                var file = e.GetMultipleFiles(MaxAllowedFiles)?.FirstOrDefault();
                if (file != null)
                {
                    if (!FileUploadExtension.ImageExt.Contains(file.Name.GetFileExtensionFromUrl()))
                    {
                        ErrorMessage = "File should be an image.";
                        ToastService.ShowInfo(ErrorMessage);
                        return;
                    }
                    if (file.Size <= MaxFileSize)
                    {
                        //DesignModel.DesignImages = Convert.ToBase64String(ReadFully(file.OpenReadStream(file.Size)));
                    }
                    else
                    {
                        ErrorMessage = "Invalid file size. Max file size is 512kb";
                        ToastService.ShowInfo(ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                await SpinnerService.Hide();
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        private async Task DeleteDesign(Design item)
        {
            await ConfirmService.Show($"Are you sure you want to delete {item.Name}?", "Yes",
                async () => await ConfirmedDelete(item), "Cancel",
                async () => await ConfirmService.Clear());
        }

        public async Task ConfirmedDelete(Design design)
        {
            try
            {
                await ConfirmService.Clear();
                await SpinnerService.Show();
                await DesignService.Delete(design.Id);
                await SpinnerService.Hide();

                //todo:close modal
                ToastService.ShowError($"{design.Name} deleted!");
                StateHasChanged();

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
