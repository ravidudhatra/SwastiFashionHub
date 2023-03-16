using Microsoft.AspNetCore.Components;
using SwastiFashionHub.Shared.Core.Models;
using SwastiFashionHub.Shared.Core.Services.Interface;
using System.ComponentModel;
using System.Reflection;

namespace SwastiFashionHub.Pages.Master
{
    public partial class Design
    {
        [Inject]
        public IDesignService DesignService { get; set; }

        public List<ColumnDefinition> Columns { get; set; }
        private List<DesignViewModel> ItemsData;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                BindDataAsync();
                StateHasChanged();
                await Task.CompletedTask;
            }
        }

        private async Task BindDataAsync()
        {
            Columns = new List<ColumnDefinition>();

            PropertyInfo[] properties = typeof(DesignViewModel).GetProperties();
            foreach (var property in properties)
            {
                string displayName = property.Name;
                //var propInfo = typeof(DesignViewModel).GetProperty("Name");
                //var displayNameAttribute = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                
                //if (displayNameAttribute.Count() > 0)
                //    displayName = (displayNameAttribute[0] as DisplayNameAttribute).DisplayName;

                Columns.Add(new ColumnDefinition { Title = displayName, Field = property.Name });
            }


            var designData = await DesignService.GetAll();

            ItemsData = designData.Select(x => new DesignViewModel
            {
                CreatedDate = x.CreatedDate,
                DesignImage = x.DesignImage,
                Id = x.Id,
                Name = x.Name,
                Note = x.Note
            }).ToList();

            StateHasChanged();
        }
    }
}
