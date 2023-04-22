using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core
{
    public struct StyleBuilder
    {
        /// <summary>
        /// Local varriables
        /// </summary>
        private string stringBuffer;


        /// <summary>
        /// Class contructors
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StyleBuilder Default(string prop, string value) => new StyleBuilder(prop, value);

        public static StyleBuilder Default(string style) => Empty().AddStyle(style);

        public static StyleBuilder Empty() => new StyleBuilder();

        public StyleBuilder(string prop, string value) => stringBuffer = stringBuffer = $"{prop}:{value};";



        /// <summary>
        /// Adds style with key/value
        /// </summary>
        /// <param name="style">the  sttyle to be added</param>
        /// <returns></returns>
        public StyleBuilder AddStyle(string style) => !string.IsNullOrWhiteSpace(style) ? AddRaw($"{style};") : this;



        /// <summary>
        /// Adds style with key/value
        /// </summary>
        /// <param name="style">The style key/value</param>
        /// <returns>The style builder</returns>
        private StyleBuilder AddRaw(string style)
        {
            stringBuffer += style;
            return this;
        }


        /// <summary>
        /// Adds style to styles
        /// </summary>
        /// <param name="prop">The style property</param>
        /// <param name="value">The value oof style</param>
        /// <returns></returns>
        public StyleBuilder AddStyle(string prop, string value) => AddRaw($"{prop}:{value};");



        /// <summary>
        /// Adds style to styles
        /// </summary>
        /// <param name="prop">The style property</param>
        /// <param name="value">The value oof style</param>
        /// <param name="when">The condition to add style</param>
        /// <returns></returns>
        public StyleBuilder AddStyle(string prop, string value, bool when = true) => when ? this.AddStyle(prop, value) : this;



        /// <summary>
        /// Builds style string
        /// </summary>
        /// <returns>The style string</returns>
        public string Build()
        {
            return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
        }



        /// <summary>
        /// Returns style string
        /// </summary>
        /// <returns>The style string</returns>
        public override string ToString() => Build();
    }
}
