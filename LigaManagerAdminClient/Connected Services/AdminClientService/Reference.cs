﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LigaManagerAdminClient.AdminClientService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ModelBase", Namespace="http://schemas.datacontract.org/2004/07/LigaManagerServer.Models")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(LigaManagerAdminClient.AdminClientService.Match))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(LigaManagerAdminClient.AdminClientService.Team))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(LigaManagerAdminClient.AdminClientService.Bettor))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(LigaManagerAdminClient.AdminClientService.Bet))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(LigaManagerAdminClient.AdminClientService.Season))]
    public partial class ModelBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Match", Namespace="http://schemas.datacontract.org/2004/07/LigaManagerServer.Models")]
    [System.SerializableAttribute()]
    public partial class Match : LigaManagerAdminClient.AdminClientService.ModelBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LigaManagerAdminClient.AdminClientService.Team AwayTeamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AwayTeamScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LigaManagerAdminClient.AdminClientService.Team HomeTeamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HomeTeamScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MatchDayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LigaManagerAdminClient.AdminClientService.Season SeasonField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LigaManagerAdminClient.AdminClientService.Team AwayTeam {
            get {
                return this.AwayTeamField;
            }
            set {
                if ((object.ReferenceEquals(this.AwayTeamField, value) != true)) {
                    this.AwayTeamField = value;
                    this.RaisePropertyChanged("AwayTeam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AwayTeamScore {
            get {
                return this.AwayTeamScoreField;
            }
            set {
                if ((this.AwayTeamScoreField.Equals(value) != true)) {
                    this.AwayTeamScoreField = value;
                    this.RaisePropertyChanged("AwayTeamScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTime {
            get {
                return this.DateTimeField;
            }
            set {
                if ((this.DateTimeField.Equals(value) != true)) {
                    this.DateTimeField = value;
                    this.RaisePropertyChanged("DateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LigaManagerAdminClient.AdminClientService.Team HomeTeam {
            get {
                return this.HomeTeamField;
            }
            set {
                if ((object.ReferenceEquals(this.HomeTeamField, value) != true)) {
                    this.HomeTeamField = value;
                    this.RaisePropertyChanged("HomeTeam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HomeTeamScore {
            get {
                return this.HomeTeamScoreField;
            }
            set {
                if ((this.HomeTeamScoreField.Equals(value) != true)) {
                    this.HomeTeamScoreField = value;
                    this.RaisePropertyChanged("HomeTeamScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MatchDay {
            get {
                return this.MatchDayField;
            }
            set {
                if ((this.MatchDayField.Equals(value) != true)) {
                    this.MatchDayField = value;
                    this.RaisePropertyChanged("MatchDay");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LigaManagerAdminClient.AdminClientService.Season Season {
            get {
                return this.SeasonField;
            }
            set {
                if ((object.ReferenceEquals(this.SeasonField, value) != true)) {
                    this.SeasonField = value;
                    this.RaisePropertyChanged("Season");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Team", Namespace="http://schemas.datacontract.org/2004/07/LigaManagerServer.Models")]
    [System.SerializableAttribute()]
    public partial class Team : LigaManagerAdminClient.AdminClientService.ModelBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Bettor", Namespace="http://schemas.datacontract.org/2004/07/LigaManagerServer.Models")]
    [System.SerializableAttribute()]
    public partial class Bettor : LigaManagerAdminClient.AdminClientService.ModelBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NicknameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Firstname {
            get {
                return this.FirstnameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstnameField, value) != true)) {
                    this.FirstnameField = value;
                    this.RaisePropertyChanged("Firstname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Lastname {
            get {
                return this.LastnameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastnameField, value) != true)) {
                    this.LastnameField = value;
                    this.RaisePropertyChanged("Lastname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nickname {
            get {
                return this.NicknameField;
            }
            set {
                if ((object.ReferenceEquals(this.NicknameField, value) != true)) {
                    this.NicknameField = value;
                    this.RaisePropertyChanged("Nickname");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Bet", Namespace="http://schemas.datacontract.org/2004/07/LigaManagerServer.Models")]
    [System.SerializableAttribute()]
    public partial class Bet : LigaManagerAdminClient.AdminClientService.ModelBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AwayTeamScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LigaManagerAdminClient.AdminClientService.Bettor BettorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HomeTeamScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LigaManagerAdminClient.AdminClientService.Match MatchField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AwayTeamScore {
            get {
                return this.AwayTeamScoreField;
            }
            set {
                if ((this.AwayTeamScoreField.Equals(value) != true)) {
                    this.AwayTeamScoreField = value;
                    this.RaisePropertyChanged("AwayTeamScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LigaManagerAdminClient.AdminClientService.Bettor Bettor {
            get {
                return this.BettorField;
            }
            set {
                if ((object.ReferenceEquals(this.BettorField, value) != true)) {
                    this.BettorField = value;
                    this.RaisePropertyChanged("Bettor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTime {
            get {
                return this.DateTimeField;
            }
            set {
                if ((this.DateTimeField.Equals(value) != true)) {
                    this.DateTimeField = value;
                    this.RaisePropertyChanged("DateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HomeTeamScore {
            get {
                return this.HomeTeamScoreField;
            }
            set {
                if ((this.HomeTeamScoreField.Equals(value) != true)) {
                    this.HomeTeamScoreField = value;
                    this.RaisePropertyChanged("HomeTeamScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LigaManagerAdminClient.AdminClientService.Match Match {
            get {
                return this.MatchField;
            }
            set {
                if ((object.ReferenceEquals(this.MatchField, value) != true)) {
                    this.MatchField = value;
                    this.RaisePropertyChanged("Match");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Season", Namespace="http://schemas.datacontract.org/2004/07/LigaManagerServer.Models")]
    [System.SerializableAttribute()]
    public partial class Season : LigaManagerAdminClient.AdminClientService.ModelBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SequenceField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Sequence {
            get {
                return this.SequenceField;
            }
            set {
                if ((this.SequenceField.Equals(value) != true)) {
                    this.SequenceField = value;
                    this.RaisePropertyChanged("Sequence");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AdminClientService.IAdminClientService")]
    public interface IAdminClientService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILigaManagerService/GetMatches", ReplyAction="http://tempuri.org/ILigaManagerService/GetMatchesResponse")]
        LigaManagerAdminClient.AdminClientService.Match[] GetMatches(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILigaManagerService/GetMatches", ReplyAction="http://tempuri.org/ILigaManagerService/GetMatchesResponse")]
        System.Threading.Tasks.Task<LigaManagerAdminClient.AdminClientService.Match[]> GetMatchesAsync(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILigaManagerService/GetBets", ReplyAction="http://tempuri.org/ILigaManagerService/GetBetsResponse")]
        LigaManagerAdminClient.AdminClientService.Bet[] GetBets(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILigaManagerService/GetBets", ReplyAction="http://tempuri.org/ILigaManagerService/GetBetsResponse")]
        System.Threading.Tasks.Task<LigaManagerAdminClient.AdminClientService.Bet[]> GetBetsAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddBettor", ReplyAction="http://tempuri.org/IAdminClientService/AddBettorResponse")]
        bool AddBettor(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddBettor", ReplyAction="http://tempuri.org/IAdminClientService/AddBettorResponse")]
        System.Threading.Tasks.Task<bool> AddBettorAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateBettor", ReplyAction="http://tempuri.org/IAdminClientService/UpdateBettorResponse")]
        bool UpdateBettor(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateBettor", ReplyAction="http://tempuri.org/IAdminClientService/UpdateBettorResponse")]
        System.Threading.Tasks.Task<bool> UpdateBettorAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteBettor", ReplyAction="http://tempuri.org/IAdminClientService/DeleteBettorResponse")]
        bool DeleteBettor(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteBettor", ReplyAction="http://tempuri.org/IAdminClientService/DeleteBettorResponse")]
        System.Threading.Tasks.Task<bool> DeleteBettorAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddTeam", ReplyAction="http://tempuri.org/IAdminClientService/AddTeamResponse")]
        bool AddTeam(LigaManagerAdminClient.AdminClientService.Team team);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddTeam", ReplyAction="http://tempuri.org/IAdminClientService/AddTeamResponse")]
        System.Threading.Tasks.Task<bool> AddTeamAsync(LigaManagerAdminClient.AdminClientService.Team team);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateTeam", ReplyAction="http://tempuri.org/IAdminClientService/UpdateTeamResponse")]
        bool UpdateTeam(LigaManagerAdminClient.AdminClientService.Team team);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateTeam", ReplyAction="http://tempuri.org/IAdminClientService/UpdateTeamResponse")]
        System.Threading.Tasks.Task<bool> UpdateTeamAsync(LigaManagerAdminClient.AdminClientService.Team team);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteTeam", ReplyAction="http://tempuri.org/IAdminClientService/DeleteTeamResponse")]
        bool DeleteTeam(LigaManagerAdminClient.AdminClientService.Team team);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteTeam", ReplyAction="http://tempuri.org/IAdminClientService/DeleteTeamResponse")]
        System.Threading.Tasks.Task<bool> DeleteTeamAsync(LigaManagerAdminClient.AdminClientService.Team team);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteSeason", ReplyAction="http://tempuri.org/IAdminClientService/DeleteSeasonResponse")]
        bool DeleteSeason(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteSeason", ReplyAction="http://tempuri.org/IAdminClientService/DeleteSeasonResponse")]
        System.Threading.Tasks.Task<bool> DeleteSeasonAsync(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddSeason", ReplyAction="http://tempuri.org/IAdminClientService/AddSeasonResponse")]
        bool AddSeason(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddSeason", ReplyAction="http://tempuri.org/IAdminClientService/AddSeasonResponse")]
        System.Threading.Tasks.Task<bool> AddSeasonAsync(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateSeason", ReplyAction="http://tempuri.org/IAdminClientService/UpdateSeasonResponse")]
        bool UpdateSeason(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateSeason", ReplyAction="http://tempuri.org/IAdminClientService/UpdateSeasonResponse")]
        System.Threading.Tasks.Task<bool> UpdateSeasonAsync(LigaManagerAdminClient.AdminClientService.Season season);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteMatch", ReplyAction="http://tempuri.org/IAdminClientService/DeleteMatchResponse")]
        bool DeleteMatch(LigaManagerAdminClient.AdminClientService.Match match);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/DeleteMatch", ReplyAction="http://tempuri.org/IAdminClientService/DeleteMatchResponse")]
        System.Threading.Tasks.Task<bool> DeleteMatchAsync(LigaManagerAdminClient.AdminClientService.Match match);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddMatch", ReplyAction="http://tempuri.org/IAdminClientService/AddMatchResponse")]
        bool AddMatch(LigaManagerAdminClient.AdminClientService.Match match);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/AddMatch", ReplyAction="http://tempuri.org/IAdminClientService/AddMatchResponse")]
        System.Threading.Tasks.Task<bool> AddMatchAsync(LigaManagerAdminClient.AdminClientService.Match match);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateMatch", ReplyAction="http://tempuri.org/IAdminClientService/UpdateMatchResponse")]
        bool UpdateMatch(LigaManagerAdminClient.AdminClientService.Match match);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/UpdateMatch", ReplyAction="http://tempuri.org/IAdminClientService/UpdateMatchResponse")]
        System.Threading.Tasks.Task<bool> UpdateMatchAsync(LigaManagerAdminClient.AdminClientService.Match match);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/GenerateMatches", ReplyAction="http://tempuri.org/IAdminClientService/GenerateMatchesResponse")]
        void GenerateMatches();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminClientService/GenerateMatches", ReplyAction="http://tempuri.org/IAdminClientService/GenerateMatchesResponse")]
        System.Threading.Tasks.Task GenerateMatchesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminClientServiceChannel : LigaManagerAdminClient.AdminClientService.IAdminClientService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdminClientServiceClient : System.ServiceModel.ClientBase<LigaManagerAdminClient.AdminClientService.IAdminClientService>, LigaManagerAdminClient.AdminClientService.IAdminClientService {
        
        public AdminClientServiceClient() {
        }
        
        public AdminClientServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AdminClientServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminClientServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminClientServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public LigaManagerAdminClient.AdminClientService.Match[] GetMatches(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.GetMatches(season);
        }
        
        public System.Threading.Tasks.Task<LigaManagerAdminClient.AdminClientService.Match[]> GetMatchesAsync(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.GetMatchesAsync(season);
        }
        
        public LigaManagerAdminClient.AdminClientService.Bet[] GetBets(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.GetBets(bettor);
        }
        
        public System.Threading.Tasks.Task<LigaManagerAdminClient.AdminClientService.Bet[]> GetBetsAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.GetBetsAsync(bettor);
        }
        
        public bool AddBettor(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.AddBettor(bettor);
        }
        
        public System.Threading.Tasks.Task<bool> AddBettorAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.AddBettorAsync(bettor);
        }
        
        public bool UpdateBettor(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.UpdateBettor(bettor);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateBettorAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.UpdateBettorAsync(bettor);
        }
        
        public bool DeleteBettor(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.DeleteBettor(bettor);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteBettorAsync(LigaManagerAdminClient.AdminClientService.Bettor bettor) {
            return base.Channel.DeleteBettorAsync(bettor);
        }
        
        public bool AddTeam(LigaManagerAdminClient.AdminClientService.Team team) {
            return base.Channel.AddTeam(team);
        }
        
        public System.Threading.Tasks.Task<bool> AddTeamAsync(LigaManagerAdminClient.AdminClientService.Team team) {
            return base.Channel.AddTeamAsync(team);
        }
        
        public bool UpdateTeam(LigaManagerAdminClient.AdminClientService.Team team) {
            return base.Channel.UpdateTeam(team);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateTeamAsync(LigaManagerAdminClient.AdminClientService.Team team) {
            return base.Channel.UpdateTeamAsync(team);
        }
        
        public bool DeleteTeam(LigaManagerAdminClient.AdminClientService.Team team) {
            return base.Channel.DeleteTeam(team);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteTeamAsync(LigaManagerAdminClient.AdminClientService.Team team) {
            return base.Channel.DeleteTeamAsync(team);
        }
        
        public bool DeleteSeason(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.DeleteSeason(season);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteSeasonAsync(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.DeleteSeasonAsync(season);
        }
        
        public bool AddSeason(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.AddSeason(season);
        }
        
        public System.Threading.Tasks.Task<bool> AddSeasonAsync(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.AddSeasonAsync(season);
        }
        
        public bool UpdateSeason(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.UpdateSeason(season);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateSeasonAsync(LigaManagerAdminClient.AdminClientService.Season season) {
            return base.Channel.UpdateSeasonAsync(season);
        }
        
        public bool DeleteMatch(LigaManagerAdminClient.AdminClientService.Match match) {
            return base.Channel.DeleteMatch(match);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteMatchAsync(LigaManagerAdminClient.AdminClientService.Match match) {
            return base.Channel.DeleteMatchAsync(match);
        }
        
        public bool AddMatch(LigaManagerAdminClient.AdminClientService.Match match) {
            return base.Channel.AddMatch(match);
        }
        
        public System.Threading.Tasks.Task<bool> AddMatchAsync(LigaManagerAdminClient.AdminClientService.Match match) {
            return base.Channel.AddMatchAsync(match);
        }
        
        public bool UpdateMatch(LigaManagerAdminClient.AdminClientService.Match match) {
            return base.Channel.UpdateMatch(match);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateMatchAsync(LigaManagerAdminClient.AdminClientService.Match match) {
            return base.Channel.UpdateMatchAsync(match);
        }
        
        public void GenerateMatches() {
            base.Channel.GenerateMatches();
        }
        
        public System.Threading.Tasks.Task GenerateMatchesAsync() {
            return base.Channel.GenerateMatchesAsync();
        }
    }
}