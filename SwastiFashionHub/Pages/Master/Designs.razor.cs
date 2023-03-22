using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        public IAlertService AlertService { get; set; }

        [Inject]
        public SpinnerService SpinnerService { get; set; }

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
                await AlertService.Clear();
                if (DesignModel.Id > 0)
                {
                    await DesignService.Update(DesignModel);
                    await AlertService.Success("Design save successfully.");
                }
                else
                {
                    await DesignService.Add(DesignModel);
                    await AlertService.Success("Design updated successfully.");
                }

                await BindDataAsync();
                ShowModel = false;
            }
            catch (Exception ex)
            {
                await AlertService.Danger(ex.Message);
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
                DesignImage = x.DesignImage,
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

        /// <summary>
        /// This function execute when fallback image and validate it
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
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
                        await AlertService.Info(ErrorMessage);
                        return;
                    }
                    if (file.Size <= MaxFileSize)
                    {
                        DesignModel.DesignImage = Convert.ToBase64String(ReadFully(file.OpenReadStream(file.Size)));
                    }
                    else
                    {
                        ErrorMessage = "Invalid file size. Max file size is 512kb";
                        await AlertService.Info(ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                await AlertService.Clear();
                await AlertService.Warning("Unable to upload file");
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

    }
}
