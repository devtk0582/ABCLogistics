﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarrierIntegrationCore.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://wsbeta.fedex.com:443/web-services")]
        public string CarrierIntegrationCore_CreatePickupWebReference_PickupService {
            get {
                return ((string)(this["CarrierIntegrationCore_CreatePickupWebReference_PickupService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.webservicex.net/CurrencyConvertor.asmx")]
        public string CarrierIntegrationCore_CurrencyConverterWebReference_CurrencyConvertor {
            get {
                return ((string)(this["CarrierIntegrationCore_CurrencyConverterWebReference_CurrencyConvertor"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://wsbeta.fedex.com:443/web-services/rate")]
        public string CarrierIntegrationCore_RateServiceWebReference_RateService {
            get {
                return ((string)(this["CarrierIntegrationCore_RateServiceWebReference_RateService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://wwwcie.ups.com/webservices/Rate")]
        public string CarrierIntegrationCore_UPSRateWebReference_RateService {
            get {
                return ((string)(this["CarrierIntegrationCore_UPSRateWebReference_RateService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://wwwcie.ups.com/webservices/Pickup")]
        public string CarrierIntegrationCore_UPSPickupWebReference_PickupService {
            get {
                return ((string)(this["CarrierIntegrationCore_UPSPickupWebReference_PickupService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=localhost;Initial Catalog=ABCLogisticsShippingDB_Target;Integrated Se" +
            "curity=True")]
        public string ABCLogisticsShippingDB_TargetConnectionString {
            get {
                return ((string)(this["ABCLogisticsShippingDB_TargetConnectionString"]));
            }
        }
    }
}