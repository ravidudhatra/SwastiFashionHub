using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core
{
    public struct CssBuilder
    {
        /// <summary>
        /// Local variabless
        /// </summary>
        private string stringBuffer;



        /// <summary>
        /// Class constructors
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CssBuilder Default(string value) => new CssBuilder(value);

        public static CssBuilder Empty() => new CssBuilder();

        public CssBuilder(string value) => stringBuffer = value;



        /// <summary>
        /// Adds css class key and value
        /// </summary>
        /// <param name="value">The css ccl with value</param>
        /// <returns></returns>
        public CssBuilder AddValue(string value)
        {
            stringBuffer += value;
            return this;
        }



        /// <summary>
        /// Adds css lass to the css class list
        /// </summary>
        /// <param name="value">The css class value</param>
        /// <returns></returns>
        public CssBuilder AddClass(string value) => AddValue(" " + value);



        /// <summary>
        /// Adds css class to the css class list
        /// </summary>
        /// <param name="value">The css class value</param>
        /// <param name="when">The condittion to add class</param>
        /// <returns>The class builder</returns>
        public CssBuilder AddClass(string value, bool when = true) => when ? this.AddClass(value) : this;



        /// <summary>
        /// Builds css class string
        /// </summary>
        /// <returns>Css class string</returns>
        public string Build()
        {
            return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
        }



        /// <summary>
        /// Creates css class string
        /// </summary>
        /// <returns>Css class string</returns>
        public override string ToString() => Build();
    }
}
