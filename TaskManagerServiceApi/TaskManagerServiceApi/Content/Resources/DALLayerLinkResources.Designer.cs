﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManagerServiceApi.Content.Resources {
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
    internal class DALLayerLinkResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DALLayerLinkResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TaskManagerServiceApi.Content.Resources.DALLayerLinkResources", typeof(DALLayerLinkResources).Assembly);
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
        ///   Looks up a localized string similar to /admin/email/?emailId={0}.
        /// </summary>
        internal static string checkForEmailUrl {
            get {
                return ResourceManager.GetString("checkForEmailUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/create-user.
        /// </summary>
        internal static string createUserUrl {
            get {
                return ResourceManager.GetString("createUserUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://localhost:52187.
        /// </summary>
        internal static string DalLayerUrl {
            get {
                return ResourceManager.GetString("DalLayerUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/{0}/?loginUser={1}.
        /// </summary>
        internal static string deleteUserUrl {
            get {
                return ResourceManager.GetString("deleteUserUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/{0}.
        /// </summary>
        internal static string editUserUrl {
            get {
                return ResourceManager.GetString("editUserUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /employees.
        /// </summary>
        internal static string getEmployeesUrl {
            get {
                return ResourceManager.GetString("getEmployeesUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /employees/{0}/tasks.
        /// </summary>
        internal static string getEmployeeTasksUrl {
            get {
                return ResourceManager.GetString("getEmployeeTasksUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /employees/task-status.
        /// </summary>
        internal static string getStatusListUrl {
            get {
                return ResourceManager.GetString("getStatusListUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /employees/{0}/tasks/count/?statusId={1}.
        /// </summary>
        internal static string getTaskCountUrl {
            get {
                return ResourceManager.GetString("getTaskCountUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /employees/{0}.
        /// </summary>
        internal static string getTaskDetailsUrl {
            get {
                return ResourceManager.GetString("getTaskDetailsUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/manager.
        /// </summary>
        internal static string managerUrl {
            get {
                return ResourceManager.GetString("managerUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/roles/{0}.
        /// </summary>
        internal static string roleIdUrl {
            get {
                return ResourceManager.GetString("roleIdUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/roles.
        /// </summary>
        internal static string rolesUrl {
            get {
                return ResourceManager.GetString("rolesUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /employees/UpdateTask.
        /// </summary>
        internal static string updateTaskUrl {
            get {
                return ResourceManager.GetString("updateTaskUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /admin/user-detail/.
        /// </summary>
        internal static string userDetailUrl {
            get {
                return ResourceManager.GetString("userDetailUrl", resourceCulture);
            }
        }
    }
}
