﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace P10Borders.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("P10Borders.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to p {
        ///    padding: 5px;
        ///    background-color: lightgray;
        ///    width: 350px;
        ///    text-align: center;
        ///    margin: 20px auto;
        ///    font-size: 32px;
        ///    border-radius: 20px;
        ///}
        ///
        ///p:nth-child(1) {
        ///    border: 5px solid red;
        ///}
        ///
        ///p:nth-child(2) {
        ///    border: 5px solid green;
        ///}
        ///
        ///p:nth-child(3) {
        ///    border: 5px solid blue;
        ///}
        ///
        ///p:nth-child(1) &gt; span {
        ///    color: red;
        ///}
        ///
        ///p:nth-child(2) &gt; span {
        ///    color: green;
        ///}
        ///
        ///p:nth-child(3) &gt; span {
        ///    color: blue;
        ///}.
        /// </summary>
        internal static string borders {
            get {
                return ResourceManager.GetString("borders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Content-Type: text/html
        ///
        ///&lt;!DOCTYPE html&gt;
        ///&lt;html lang=&quot;en&quot;&gt;
        ///&lt;head&gt;
        ///    &lt;meta charset=&quot;UTF-8&quot;&gt;
        ///    &lt;title&gt;Borders&lt;/title&gt;
        ///    &lt;link rel=&quot;stylesheet&quot; href=&quot;styles/borders.css&quot; /&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///    &lt;p&gt;&lt;span&gt;Red&lt;/span&gt; Border&lt;/p&gt;
        ///    &lt;p&gt;&lt;span&gt;Green&lt;/span&gt; Border&lt;/p&gt;
        ///    &lt;p&gt;&lt;span&gt;Blue&lt;/span&gt; Border&lt;/p&gt;
        ///&lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        internal static string HTML {
            get {
                return ResourceManager.GetString("HTML", resourceCulture);
            }
        }
    }
}
