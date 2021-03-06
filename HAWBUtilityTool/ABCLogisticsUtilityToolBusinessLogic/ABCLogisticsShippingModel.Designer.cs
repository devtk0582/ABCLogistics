﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace ABCLogisticsUtilityToolBusinessLogic
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ABCLogisticsShippingDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ABCLogisticsShippingDBEntities object using the connection string found in the 'ABCLogisticsShippingDBEntities' section of the application configuration file.
        /// </summary>
        public ABCLogisticsShippingDBEntities() : base("name=ABCLogisticsShippingDBEntities", "ABCLogisticsShippingDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ABCLogisticsShippingDBEntities object.
        /// </summary>
        public ABCLogisticsShippingDBEntities(string connectionString) : base(connectionString, "ABCLogisticsShippingDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ABCLogisticsShippingDBEntities object.
        /// </summary>
        public ABCLogisticsShippingDBEntities(EntityConnection connection) : base(connection, "ABCLogisticsShippingDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<activity> activities
        {
            get
            {
                if ((_activities == null))
                {
                    _activities = base.CreateObjectSet<activity>("activities");
                }
                return _activities;
            }
        }
        private ObjectSet<activity> _activities;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the activities EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToactivities(activity activity)
        {
            base.AddObject("activities", activity);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ABCLogisticsShippingDB_TargetModel", Name="activity")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class activity : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new activity object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="hbillno">Initial value of the hbillno property.</param>
        /// <param name="accountno">Initial value of the accountno property.</param>
        public static activity Createactivity(global::System.Int32 id, global::System.String hbillno, global::System.String accountno)
        {
            activity activity = new activity();
            activity.id = id;
            activity.hbillno = hbillno;
            activity.accountno = accountno;
            return activity;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String hbillno
        {
            get
            {
                return _hbillno;
            }
            set
            {
                OnhbillnoChanging(value);
                ReportPropertyChanging("hbillno");
                _hbillno = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("hbillno");
                OnhbillnoChanged();
            }
        }
        private global::System.String _hbillno;
        partial void OnhbillnoChanging(global::System.String value);
        partial void OnhbillnoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String origairp
        {
            get
            {
                return _origairp;
            }
            set
            {
                OnorigairpChanging(value);
                ReportPropertyChanging("origairp");
                _origairp = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("origairp");
                OnorigairpChanged();
            }
        }
        private global::System.String _origairp;
        partial void OnorigairpChanging(global::System.String value);
        partial void OnorigairpChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String destairp
        {
            get
            {
                return _destairp;
            }
            set
            {
                OndestairpChanging(value);
                ReportPropertyChanging("destairp");
                _destairp = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("destairp");
                OndestairpChanged();
            }
        }
        private global::System.String _destairp;
        partial void OndestairpChanging(global::System.String value);
        partial void OndestairpChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shipdept
        {
            get
            {
                return _shipdept;
            }
            set
            {
                OnshipdeptChanging(value);
                ReportPropertyChanging("shipdept");
                _shipdept = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shipdept");
                OnshipdeptChanged();
            }
        }
        private global::System.String _shipdept;
        partial void OnshipdeptChanging(global::System.String value);
        partial void OnshipdeptChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shipref
        {
            get
            {
                return _shipref;
            }
            set
            {
                OnshiprefChanging(value);
                ReportPropertyChanging("shipref");
                _shipref = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shipref");
                OnshiprefChanged();
            }
        }
        private global::System.String _shipref;
        partial void OnshiprefChanging(global::System.String value);
        partial void OnshiprefChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String recname
        {
            get
            {
                return _recname;
            }
            set
            {
                OnrecnameChanging(value);
                ReportPropertyChanging("recname");
                _recname = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("recname");
                OnrecnameChanged();
            }
        }
        private global::System.String _recname;
        partial void OnrecnameChanging(global::System.String value);
        partial void OnrecnameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String recaddr
        {
            get
            {
                return _recaddr;
            }
            set
            {
                OnrecaddrChanging(value);
                ReportPropertyChanging("recaddr");
                _recaddr = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("recaddr");
                OnrecaddrChanged();
            }
        }
        private global::System.String _recaddr;
        partial void OnrecaddrChanging(global::System.String value);
        partial void OnrecaddrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String recaddr1
        {
            get
            {
                return _recaddr1;
            }
            set
            {
                Onrecaddr1Changing(value);
                ReportPropertyChanging("recaddr1");
                _recaddr1 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("recaddr1");
                Onrecaddr1Changed();
            }
        }
        private global::System.String _recaddr1;
        partial void Onrecaddr1Changing(global::System.String value);
        partial void Onrecaddr1Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String reccity
        {
            get
            {
                return _reccity;
            }
            set
            {
                OnreccityChanging(value);
                ReportPropertyChanging("reccity");
                _reccity = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("reccity");
                OnreccityChanged();
            }
        }
        private global::System.String _reccity;
        partial void OnreccityChanging(global::System.String value);
        partial void OnreccityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String recstate
        {
            get
            {
                return _recstate;
            }
            set
            {
                OnrecstateChanging(value);
                ReportPropertyChanging("recstate");
                _recstate = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("recstate");
                OnrecstateChanged();
            }
        }
        private global::System.String _recstate;
        partial void OnrecstateChanging(global::System.String value);
        partial void OnrecstateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String reczip
        {
            get
            {
                return _reczip;
            }
            set
            {
                OnreczipChanging(value);
                ReportPropertyChanging("reczip");
                _reczip = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("reczip");
                OnreczipChanged();
            }
        }
        private global::System.String _reczip;
        partial void OnreczipChanging(global::System.String value);
        partial void OnreczipChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String recnotify
        {
            get
            {
                return _recnotify;
            }
            set
            {
                OnrecnotifyChanging(value);
                ReportPropertyChanging("recnotify");
                _recnotify = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("recnotify");
                OnrecnotifyChanged();
            }
        }
        private global::System.String _recnotify;
        partial void OnrecnotifyChanging(global::System.String value);
        partial void OnrecnotifyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String specinstr
        {
            get
            {
                return _specinstr;
            }
            set
            {
                OnspecinstrChanging(value);
                ReportPropertyChanging("specinstr");
                _specinstr = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("specinstr");
                OnspecinstrChanged();
            }
        }
        private global::System.String _specinstr;
        partial void OnspecinstrChanging(global::System.String value);
        partial void OnspecinstrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String instrdesc
        {
            get
            {
                return _instrdesc;
            }
            set
            {
                OninstrdescChanging(value);
                ReportPropertyChanging("instrdesc");
                _instrdesc = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("instrdesc");
                OninstrdescChanged();
            }
        }
        private global::System.String _instrdesc;
        partial void OninstrdescChanging(global::System.String value);
        partial void OninstrdescChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> pieces
        {
            get
            {
                return _pieces;
            }
            set
            {
                OnpiecesChanging(value);
                ReportPropertyChanging("pieces");
                _pieces = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("pieces");
                OnpiecesChanged();
            }
        }
        private Nullable<global::System.Decimal> _pieces;
        partial void OnpiecesChanging(Nullable<global::System.Decimal> value);
        partial void OnpiecesChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String desc1
        {
            get
            {
                return _desc1;
            }
            set
            {
                Ondesc1Changing(value);
                ReportPropertyChanging("desc1");
                _desc1 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("desc1");
                Ondesc1Changed();
            }
        }
        private global::System.String _desc1;
        partial void Ondesc1Changing(global::System.String value);
        partial void Ondesc1Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> weight
        {
            get
            {
                return _weight;
            }
            set
            {
                OnweightChanging(value);
                ReportPropertyChanging("weight");
                _weight = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("weight");
                OnweightChanged();
            }
        }
        private Nullable<global::System.Decimal> _weight;
        partial void OnweightChanging(Nullable<global::System.Decimal> value);
        partial void OnweightChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shipsign
        {
            get
            {
                return _shipsign;
            }
            set
            {
                OnshipsignChanging(value);
                ReportPropertyChanging("shipsign");
                _shipsign = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shipsign");
                OnshipsignChanged();
            }
        }
        private global::System.String _shipsign;
        partial void OnshipsignChanging(global::System.String value);
        partial void OnshipsignChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> shipsigndt
        {
            get
            {
                return _shipsigndt;
            }
            set
            {
                OnshipsigndtChanging(value);
                ReportPropertyChanging("shipsigndt");
                _shipsigndt = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("shipsigndt");
                OnshipsigndtChanged();
            }
        }
        private Nullable<global::System.DateTime> _shipsigndt;
        partial void OnshipsigndtChanging(Nullable<global::System.DateTime> value);
        partial void OnshipsigndtChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> totchgs
        {
            get
            {
                return _totchgs;
            }
            set
            {
                OntotchgsChanging(value);
                ReportPropertyChanging("totchgs");
                _totchgs = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("totchgs");
                OntotchgsChanged();
            }
        }
        private Nullable<global::System.Decimal> _totchgs;
        partial void OntotchgsChanging(Nullable<global::System.Decimal> value);
        partial void OntotchgsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shiptime
        {
            get
            {
                return _shiptime;
            }
            set
            {
                OnshiptimeChanging(value);
                ReportPropertyChanging("shiptime");
                _shiptime = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shiptime");
                OnshiptimeChanged();
            }
        }
        private global::System.String _shiptime;
        partial void OnshiptimeChanging(global::System.String value);
        partial void OnshiptimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String station
        {
            get
            {
                return _station;
            }
            set
            {
                OnstationChanging(value);
                ReportPropertyChanging("station");
                _station = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("station");
                OnstationChanged();
            }
        }
        private global::System.String _station;
        partial void OnstationChanging(global::System.String value);
        partial void OnstationChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shiptele
        {
            get
            {
                return _shiptele;
            }
            set
            {
                OnshipteleChanging(value);
                ReportPropertyChanging("shiptele");
                _shiptele = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shiptele");
                OnshipteleChanged();
            }
        }
        private global::System.String _shiptele;
        partial void OnshipteleChanging(global::System.String value);
        partial void OnshipteleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> onsite
        {
            get
            {
                return _onsite;
            }
            set
            {
                OnonsiteChanging(value);
                ReportPropertyChanging("onsite");
                _onsite = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("onsite");
                OnonsiteChanged();
            }
        }
        private Nullable<global::System.DateTime> _onsite;
        partial void OnonsiteChanging(Nullable<global::System.DateTime> value);
        partial void OnonsiteChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String hot
        {
            get
            {
                return _hot;
            }
            set
            {
                OnhotChanging(value);
                ReportPropertyChanging("hot");
                _hot = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("hot");
                OnhotChanged();
            }
        }
        private global::System.String _hot;
        partial void OnhotChanging(global::System.String value);
        partial void OnhotChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String company
        {
            get
            {
                return _company;
            }
            set
            {
                OncompanyChanging(value);
                ReportPropertyChanging("company");
                _company = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("company");
                OncompanyChanged();
            }
        }
        private global::System.String _company;
        partial void OncompanyChanging(global::System.String value);
        partial void OncompanyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String contact
        {
            get
            {
                return _contact;
            }
            set
            {
                OncontactChanging(value);
                ReportPropertyChanging("contact");
                _contact = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("contact");
                OncontactChanged();
            }
        }
        private global::System.String _contact;
        partial void OncontactChanging(global::System.String value);
        partial void OncontactChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String address1
        {
            get
            {
                return _address1;
            }
            set
            {
                Onaddress1Changing(value);
                ReportPropertyChanging("address1");
                _address1 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("address1");
                Onaddress1Changed();
            }
        }
        private global::System.String _address1;
        partial void Onaddress1Changing(global::System.String value);
        partial void Onaddress1Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String city
        {
            get
            {
                return _city;
            }
            set
            {
                OncityChanging(value);
                ReportPropertyChanging("city");
                _city = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("city");
                OncityChanged();
            }
        }
        private global::System.String _city;
        partial void OncityChanging(global::System.String value);
        partial void OncityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String state
        {
            get
            {
                return _state;
            }
            set
            {
                OnstateChanging(value);
                ReportPropertyChanging("state");
                _state = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("state");
                OnstateChanged();
            }
        }
        private global::System.String _state;
        partial void OnstateChanging(global::System.String value);
        partial void OnstateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String zip
        {
            get
            {
                return _zip;
            }
            set
            {
                OnzipChanging(value);
                ReportPropertyChanging("zip");
                _zip = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("zip");
                OnzipChanged();
            }
        }
        private global::System.String _zip;
        partial void OnzipChanging(global::System.String value);
        partial void OnzipChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String servdesc
        {
            get
            {
                return _servdesc;
            }
            set
            {
                OnservdescChanging(value);
                ReportPropertyChanging("servdesc");
                _servdesc = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("servdesc");
                OnservdescChanged();
            }
        }
        private global::System.String _servdesc;
        partial void OnservdescChanging(global::System.String value);
        partial void OnservdescChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String accountno
        {
            get
            {
                return _accountno;
            }
            set
            {
                OnaccountnoChanging(value);
                ReportPropertyChanging("accountno");
                _accountno = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("accountno");
                OnaccountnoChanged();
            }
        }
        private global::System.String _accountno;
        partial void OnaccountnoChanging(global::System.String value);
        partial void OnaccountnoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> itar
        {
            get
            {
                return _itar;
            }
            set
            {
                OnitarChanging(value);
                ReportPropertyChanging("itar");
                _itar = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("itar");
                OnitarChanged();
            }
        }
        private Nullable<global::System.Boolean> _itar;
        partial void OnitarChanging(Nullable<global::System.Boolean> value);
        partial void OnitarChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String carnet
        {
            get
            {
                return _carnet;
            }
            set
            {
                OncarnetChanging(value);
                ReportPropertyChanging("carnet");
                _carnet = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("carnet");
                OncarnetChanged();
            }
        }
        private global::System.String _carnet;
        partial void OncarnetChanging(global::System.String value);
        partial void OncarnetChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String creditcard_option
        {
            get
            {
                return _creditcard_option;
            }
            set
            {
                Oncreditcard_optionChanging(value);
                ReportPropertyChanging("creditcard_option");
                _creditcard_option = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("creditcard_option");
                Oncreditcard_optionChanged();
            }
        }
        private global::System.String _creditcard_option;
        partial void Oncreditcard_optionChanging(global::System.String value);
        partial void Oncreditcard_optionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String creditcard_number
        {
            get
            {
                return _creditcard_number;
            }
            set
            {
                Oncreditcard_numberChanging(value);
                ReportPropertyChanging("creditcard_number");
                _creditcard_number = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("creditcard_number");
                Oncreditcard_numberChanged();
            }
        }
        private global::System.String _creditcard_number;
        partial void Oncreditcard_numberChanging(global::System.String value);
        partial void Oncreditcard_numberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String employeeid
        {
            get
            {
                return _employeeid;
            }
            set
            {
                OnemployeeidChanging(value);
                ReportPropertyChanging("employeeid");
                _employeeid = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("employeeid");
                OnemployeeidChanged();
            }
        }
        private global::System.String _employeeid;
        partial void OnemployeeidChanging(global::System.String value);
        partial void OnemployeeidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> dim_weight
        {
            get
            {
                return _dim_weight;
            }
            set
            {
                Ondim_weightChanging(value);
                ReportPropertyChanging("dim_weight");
                _dim_weight = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("dim_weight");
                Ondim_weightChanged();
            }
        }
        private Nullable<global::System.Decimal> _dim_weight;
        partial void Ondim_weightChanging(Nullable<global::System.Decimal> value);
        partial void Ondim_weightChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> miles
        {
            get
            {
                return _miles;
            }
            set
            {
                OnmilesChanging(value);
                ReportPropertyChanging("miles");
                _miles = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("miles");
                OnmilesChanged();
            }
        }
        private Nullable<global::System.Decimal> _miles;
        partial void OnmilesChanging(Nullable<global::System.Decimal> value);
        partial void OnmilesChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String invoice_type
        {
            get
            {
                return _invoice_type;
            }
            set
            {
                Oninvoice_typeChanging(value);
                ReportPropertyChanging("invoice_type");
                _invoice_type = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("invoice_type");
                Oninvoice_typeChanged();
            }
        }
        private global::System.String _invoice_type;
        partial void Oninvoice_typeChanging(global::System.String value);
        partial void Oninvoice_typeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String invoice_number
        {
            get
            {
                return _invoice_number;
            }
            set
            {
                Oninvoice_numberChanging(value);
                ReportPropertyChanging("invoice_number");
                _invoice_number = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("invoice_number");
                Oninvoice_numberChanged();
            }
        }
        private global::System.String _invoice_number;
        partial void Oninvoice_numberChanging(global::System.String value);
        partial void Oninvoice_numberChanged();

        #endregion

    
    }

    #endregion

    
}
