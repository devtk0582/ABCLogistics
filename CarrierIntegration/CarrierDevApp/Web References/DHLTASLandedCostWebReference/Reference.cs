﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17929.
// 
#pragma warning disable 1591

namespace CarrierDevApp.DHLTASLandedCostWebReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LandedCostSoapBinding", Namespace="urn:LandedCost")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(FeeInfo))]
    public partial class LandedCostService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getLandedCostEstimateOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public LandedCostService() {
            this.Url = global::CarrierDevApp.Properties.Settings.Default.CarrierDevApp_DHLTASLandedCostWebReference_LandedCostService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getLandedCostEstimateCompletedEventHandler getLandedCostEstimateCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="urn:LandedCost", ResponseNamespace="urn:LandedCost")]
        [return: System.Xml.Serialization.SoapElementAttribute("objLandedCostResponse")]
        public LandedCostResponseObject getLandedCostEstimate(LandedCostInput objLandedCostInput, string referenceID) {
            object[] results = this.Invoke("getLandedCostEstimate", new object[] {
                        objLandedCostInput,
                        referenceID});
            return ((LandedCostResponseObject)(results[0]));
        }
        
        /// <remarks/>
        public void getLandedCostEstimateAsync(LandedCostInput objLandedCostInput, string referenceID) {
            this.getLandedCostEstimateAsync(objLandedCostInput, referenceID, null);
        }
        
        /// <remarks/>
        public void getLandedCostEstimateAsync(LandedCostInput objLandedCostInput, string referenceID, object userState) {
            if ((this.getLandedCostEstimateOperationCompleted == null)) {
                this.getLandedCostEstimateOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetLandedCostEstimateOperationCompleted);
            }
            this.InvokeAsync("getLandedCostEstimate", new object[] {
                        objLandedCostInput,
                        referenceID}, this.getLandedCostEstimateOperationCompleted, userState);
        }
        
        private void OngetLandedCostEstimateOperationCompleted(object arg) {
            if ((this.getLandedCostEstimateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getLandedCostEstimateCompleted(this, new getLandedCostEstimateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.dhl.com/xmlns/F.040601/customer")]
    public partial class LandedCostInput {
        
        private string countryCodeField;
        
        private string descriptionField;
        
        private string domainField;
        
        private string exportProductCodeField;
        
        private string insuranceCurrencyField;
        
        private string insuranceValueField;
        
        private string measurementTypeField;
        
        private string priceCurrencyField;
        
        private string priceValueField;
        
        private string productCodeField;
        
        private string receiverCountryField;
        
        private string shipmentCurrencyField;
        
        private string shipperCountryField;
        
        private string shipToStateField;
        
        private string totalQuantityField;
        
        private string transportationCurrencyField;
        
        private string transportationValueField;
        
        private string typeField;
        
        private string unitField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string countryCode {
            get {
                return this.countryCodeField;
            }
            set {
                this.countryCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string domain {
            get {
                return this.domainField;
            }
            set {
                this.domainField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string exportProductCode {
            get {
                return this.exportProductCodeField;
            }
            set {
                this.exportProductCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string insuranceCurrency {
            get {
                return this.insuranceCurrencyField;
            }
            set {
                this.insuranceCurrencyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string insuranceValue {
            get {
                return this.insuranceValueField;
            }
            set {
                this.insuranceValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string measurementType {
            get {
                return this.measurementTypeField;
            }
            set {
                this.measurementTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string priceCurrency {
            get {
                return this.priceCurrencyField;
            }
            set {
                this.priceCurrencyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string priceValue {
            get {
                return this.priceValueField;
            }
            set {
                this.priceValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string productCode {
            get {
                return this.productCodeField;
            }
            set {
                this.productCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string receiverCountry {
            get {
                return this.receiverCountryField;
            }
            set {
                this.receiverCountryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string shipmentCurrency {
            get {
                return this.shipmentCurrencyField;
            }
            set {
                this.shipmentCurrencyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string shipperCountry {
            get {
                return this.shipperCountryField;
            }
            set {
                this.shipperCountryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string shipToState {
            get {
                return this.shipToStateField;
            }
            set {
                this.shipToStateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string totalQuantity {
            get {
                return this.totalQuantityField;
            }
            set {
                this.totalQuantityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string transportationCurrency {
            get {
                return this.transportationCurrencyField;
            }
            set {
                this.transportationCurrencyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string transportationValue {
            get {
                return this.transportationValueField;
            }
            set {
                this.transportationValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string unit {
            get {
                return this.unitField;
            }
            set {
                this.unitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.dhl.com/xmlns/F.040601/customer")]
    public partial class FeeDetails {
        
        private string conditionsField;
        
        private string countryField;
        
        private string currencyField;
        
        private string domainField;
        
        private string feeAmountField;
        
        private string formulaField;
        
        private string maximumField;
        
        private string minimumField;
        
        private string typeField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string conditions {
            get {
                return this.conditionsField;
            }
            set {
                this.conditionsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string country {
            get {
                return this.countryField;
            }
            set {
                this.countryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string currency {
            get {
                return this.currencyField;
            }
            set {
                this.currencyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string domain {
            get {
                return this.domainField;
            }
            set {
                this.domainField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string feeAmount {
            get {
                return this.feeAmountField;
            }
            set {
                this.feeAmountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string formula {
            get {
                return this.formulaField;
            }
            set {
                this.formulaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string maximum {
            get {
                return this.maximumField;
            }
            set {
                this.maximumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string minimum {
            get {
                return this.minimumField;
            }
            set {
                this.minimumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.dhl.com/xmlns/F.040601/customer")]
    public partial class FeeInfo {
        
        private FeeDetails feeField;
        
        private string feeNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public FeeDetails fee {
            get {
                return this.feeField;
            }
            set {
                this.feeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string feeName {
            get {
                return this.feeNameField;
            }
            set {
                this.feeNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.dhl.com/xmlns/F.040601/customer")]
    public partial class LandedCostResponse {
        
        private FeeInfo[] feeInfoField;
        
        private LandedCostInput inputField;
        
        private string shipmentValueField;
        
        private string totalEstimatedFeesField;
        
        private string totalLandedCostField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public FeeInfo[] feeInfo {
            get {
                return this.feeInfoField;
            }
            set {
                this.feeInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public LandedCostInput input {
            get {
                return this.inputField;
            }
            set {
                this.inputField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string shipmentValue {
            get {
                return this.shipmentValueField;
            }
            set {
                this.shipmentValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string totalEstimatedFees {
            get {
                return this.totalEstimatedFeesField;
            }
            set {
                this.totalEstimatedFeesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string totalLandedCost {
            get {
                return this.totalLandedCostField;
            }
            set {
                this.totalLandedCostField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.dhl.com/xmlns/F.040601/customer")]
    public partial class LandedCostResponseObject {
        
        private LandedCostResponse responseField;
        
        private string referenceIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public LandedCostResponse response {
            get {
                return this.responseField;
            }
            set {
                this.responseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string referenceID {
            get {
                return this.referenceIDField;
            }
            set {
                this.referenceIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void getLandedCostEstimateCompletedEventHandler(object sender, getLandedCostEstimateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getLandedCostEstimateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getLandedCostEstimateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public LandedCostResponseObject Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LandedCostResponseObject)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591