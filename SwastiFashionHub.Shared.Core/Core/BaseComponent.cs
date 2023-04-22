using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core
{
    public abstract class BaseComponent : ComponentBase
    {
        /// <summary>
        /// The id of the component
        /// </summary>
        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString();



        /// <summary>
        /// The list of css classes
        /// </summary>
        [Parameter]
        public string Class { get; set; }



        /// <summary>
        /// Determines if this compoennt is enabled/disabled
        /// </summary>
        [Parameter]
        public bool Enabled { get; set; } = true;



        /// <summary>
		/// The height of component
		/// </summary>
		[Parameter]
        public string Height { get; set; }



        /// <summary>
        /// The list of css styles
        /// </summary>
        [Parameter]
        public string Style { get; set; }



        /// <summary>
		/// Determines if this compoennt is visible
		/// </summary>
		[Parameter]
        public bool Visible { get; set; } = true;



        /// <summary>
		/// The width of component
		/// </summary>
		[Parameter]
        public string Width { get; set; }
    }
}
