﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PublicTransportApi.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("PublicTransportApi.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string Line_IdentifierCannotBeNull {
            get {
                return ResourceManager.GetString("Line_IdentifierCannotBeNull", resourceCulture);
            }
        }
        
        internal static string Line_NameCannotBeNull {
            get {
                return ResourceManager.GetString("Line_NameCannotBeNull", resourceCulture);
            }
        }
        
        internal static string Generic_ExceptionOccured {
            get {
                return ResourceManager.GetString("Generic_ExceptionOccured", resourceCulture);
            }
        }
        
        internal static string Line_LineCouldNotBeFound {
            get {
                return ResourceManager.GetString("Line_LineCouldNotBeFound", resourceCulture);
            }
        }
        
        internal static string Schedule_RecurringDaysEmpty {
            get {
                return ResourceManager.GetString("Schedule_RecurringDaysEmpty", resourceCulture);
            }
        }
        
        internal static string Schedule_DateTimeEmpty {
            get {
                return ResourceManager.GetString("Schedule_DateTimeEmpty", resourceCulture);
            }
        }
        
        internal static string Schedule_ScheduleNotFound {
            get {
                return ResourceManager.GetString("Schedule_ScheduleNotFound", resourceCulture);
            }
        }
        
        internal static string SPL_SPLNotFound {
            get {
                return ResourceManager.GetString("SPL_SPLNotFound", resourceCulture);
            }
        }
        
        internal static string StopPoint_IdentifierEmpty {
            get {
                return ResourceManager.GetString("StopPoint_IdentifierEmpty", resourceCulture);
            }
        }
        
        internal static string StopPoint_NameEmpty {
            get {
                return ResourceManager.GetString("StopPoint_NameEmpty", resourceCulture);
            }
        }
        
        internal static string StopPoint_LatLongEmpty {
            get {
                return ResourceManager.GetString("StopPoint_LatLongEmpty", resourceCulture);
            }
        }
        
        internal static string StopPoint_NotFound {
            get {
                return ResourceManager.GetString("StopPoint_NotFound", resourceCulture);
            }
        }
    }
}
