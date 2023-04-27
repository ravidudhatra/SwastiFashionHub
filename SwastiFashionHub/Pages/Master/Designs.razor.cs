using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Shared.Core.Common;
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

        [Inject]
        private IConfiguration? Configuration { get; set; }

        public string? ErrorMessage { get; set; }
        private List<IFormFile>? NewImages { get; set; }
        public bool ShowModel { get; set; } = false;

        private string _baseUrl = string.Empty;

        private List<DesignResponse>? ItemsData;
        private DesignRequest? DesignModel = new();

        protected override void OnInitialized()
        {
            _baseUrl = Configuration.GetValue<string>("BaseUrl");
        }

        private async Task HandleFileSelectionAsync(InputFileChangeEventArgs e)
        {
            ToastService.ClearAll();
            NewImages = new List<IFormFile>();
            try
            {
                foreach (var imageFile in e.GetMultipleFiles())
                {
                    byte[] fileBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.OpenReadStream(Constants.MaxAllowedSizeForImageUpload).CopyToAsync(memoryStream);

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

        private void OnShowModelClick()
        {
            DesignModel = new DesignRequest();
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
                Note = x.Note,
                DesignImages = x.DesignImages,
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
            if (ItemsData == null)
                ToastService.ShowError("Item data missing.");
            else
            {
                DesignModel = ItemsData?
                    .Select(x => new DesignRequest
                    {
                        Id = item.Id,
                        CreatedBy = item.CreatedBy,
                        Name = item.Name,
                        Images = item.DesignImages?.Select(y => new DesignImagesRequest
                        {
                            Id = y.Id,
                            DesignId = y.DesignId,
                            Extension = y.Extension,
                            FileType = y.FileType,
                            ImageUrl = y.ImageUrl,
                            Name = y.Name
                        }).ToList(),
                        Note = item.Note,
                    }).FirstOrDefault(x => x.Id == item.Id);

                ShowModel = true;
            }

            await Task.CompletedTask;
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

        private string GetImageLink(DesignImagesRequest item)
        {
            return string.Format("{0}/{1}", _baseUrl, item.ImageUrl);

        }

        private async Task ConfirmedImageDelete(Guid imageId)
        {
            try
            {
                await SpinnerService.Show();
                var imageToBeRemoved = DesignModel?.Images?.Where(x => x.Id == imageId).FirstOrDefault();
                if (imageToBeRemoved != null)
                    DesignModel?.Images?.Remove(imageToBeRemoved);

                await DesignService.DeleteDesignImage(imageId);

                var itemRemove = ItemsData?.Select(x => x.DesignImages.Where(x => x.Id == imageId)).FirstOrDefault();
                if (itemRemove != null)
                    ItemsData?.Where(x => x.DesignImages.Remove((DesignImageResponse)itemRemove));

                await ConfirmService.Clear();
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

        private async Task RemoveImage(Guid imageId)
        {
            await ConfirmService.Show($"Are you sure you want to remove this image?", "Yes",
               async () => await ConfirmedImageDelete(imageId), "Cancel",
               async () => await ConfirmService.Clear());
        }

    }
}
