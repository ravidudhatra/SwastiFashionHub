using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
//using Microsoft.AspNetCore.Components.Forms;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Data.Models;
using SwastiFashionHub.Shared.Core.Extensions;
using SwastiFashionHub.Shared.Core.Services;
using SwastiFashionHub.Shared.Core.Services.Interface;
using System.IO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;


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

        private List<string> SelectedImageIds { get; set; }
        private List<IFormFile> NewImages { get; set; }
        public bool ShowModel { get; set; } = false;

        private List<DesignResponse> ItemsData;
        private DesignRequest DesignModel = new();

        private async Task HandleFileSelectionAsync(InputFileChangeEventArgs e)
        {
            ToastService.ClearAll();
            NewImages = new List<IFormFile>();
            try
            {
                foreach (var imageFile in e.GetMultipleFiles())
                {
                    //using var memoryStream = new MemoryStream();
                    //await imageFile.OpenReadStream(maxAllowedSize: 1024 * 1024 * 1024).CopyToAsync(memoryStream);

                    //NewImages.Add(new FormFile(memoryStream, 0, memoryStream.Length, imageFile.Name, imageFile.Name));

                    byte[] fileBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.OpenReadStream(maxAllowedSize: 1024 * 1024 * 1024).CopyToAsync(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }

                    var contentDisposition = "form-data; name=\"" + imageFile.Name + "\"; filename=\"" + imageFile.Name + "\"";

                    NewImages.Add(new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, imageFile.Name, imageFile.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentDisposition = contentDisposition,
                        ContentType = imageFile.ContentType
                    });
                }

                DesignModel.NewImages = NewImages;
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }

        }

        private async Task HandleValidSubmit()
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
                    await BindDataAsync();
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                DesignModel = new DesignRequest();
                ShowModel = false;
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
            var designData = await DesignService.GetAll();

            ItemsData = designData.Select(x => new DesignResponse
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

        private async Task OnEditItemClick(DesignResponse item)
        {

        }

        private async Task OnDeleteItemClick(DesignResponse item)
        {
            await ConfirmService.Show($"Are you sure you want to delete {item.Name}?", "Yes",
                async () => await ConfirmedDelete(item), "Cancel",
                async () => await ConfirmService.Clear());
        }

        public async Task ConfirmedDelete(DesignResponse design)
        {
            try
            {
                await ConfirmService.Clear();
                await SpinnerService.Show();
                await DesignService.Delete(design.Id);
                await SpinnerService.Hide();

                //todo:close modal
                ToastService.ShowSuccess($"{design.Name} deleted!");
                await BindDataAsync();
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


        private string GetImageLink(string imageId)
        {
            return $"/api/designs/{DesignModel.Id}/images/{imageId}";
        }

        private async Task ConfirmedImageDelete(string imageId)
        {
            SelectedImageIds.Remove(imageId);
            DesignModel.ImageIds.Remove(imageId);

            //var response = await HttpClient.DeleteAsync($"/api/designs/{DesignModel.Id}/images/{imageId}");

            //if (!response.IsSuccessStatusCode)
            //{
            //    ErrorMessage = await response.Content.ReadAsStringAsync();
            //}
        }

        private async Task RemoveImage(string imageId)
        {
            await ConfirmService.Show($"Are you sure you want to remove this image?", "Yes",
               async () => await ConfirmedImageDelete(imageId), "Cancel",
               async () => await ConfirmService.Clear());
        }

    }
}
