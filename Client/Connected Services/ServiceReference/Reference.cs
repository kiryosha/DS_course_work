﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IService_kurs")]
    public interface IService_kurs {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/login", ReplyAction="http://tempuri.org/IService_kurs/loginResponse")]
        System.Tuple<string, string, string, string, string> login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/login", ReplyAction="http://tempuri.org/IService_kurs/loginResponse")]
        System.Threading.Tasks.Task<System.Tuple<string, string, string, string, string>> loginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/drop_token", ReplyAction="http://tempuri.org/IService_kurs/drop_tokenResponse")]
        void drop_token(string username, string user_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/drop_token", ReplyAction="http://tempuri.org/IService_kurs/drop_tokenResponse")]
        System.Threading.Tasks.Task drop_tokenAsync(string username, string user_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/role_bd", ReplyAction="http://tempuri.org/IService_kurs/role_bdResponse")]
        string role_bd(string user_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/role_bd", ReplyAction="http://tempuri.org/IService_kurs/role_bdResponse")]
        System.Threading.Tasks.Task<string> role_bdAsync(string user_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search", ReplyAction="http://tempuri.org/IService_kurs/searchResponse")]
        string search(string email, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search", ReplyAction="http://tempuri.org/IService_kurs/searchResponse")]
        System.Threading.Tasks.Task<string> searchAsync(string email, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople", ReplyAction="http://tempuri.org/IService_kurs/addpeopleResponse")]
        void addpeople(string email, string fio, string address, string role, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople", ReplyAction="http://tempuri.org/IService_kurs/addpeopleResponse")]
        System.Threading.Tasks.Task addpeopleAsync(string email, string fio, string address, string role, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople_info", ReplyAction="http://tempuri.org/IService_kurs/addpeople_infoResponse")]
        void addpeople_info(string email, System.DateTime d_date, string phone_number, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople_info", ReplyAction="http://tempuri.org/IService_kurs/addpeople_infoResponse")]
        System.Threading.Tasks.Task addpeople_infoAsync(string email, System.DateTime d_date, string phone_number, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople_info_two", ReplyAction="http://tempuri.org/IService_kurs/addpeople_info_twoResponse")]
        void addpeople_info_two(string email, string card_number, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople_info_two", ReplyAction="http://tempuri.org/IService_kurs/addpeople_info_twoResponse")]
        System.Threading.Tasks.Task addpeople_info_twoAsync(string email, string card_number, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople_post", ReplyAction="http://tempuri.org/IService_kurs/addpeople_postResponse")]
        void addpeople_post(string email, string post, string salary, string role, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/addpeople_post", ReplyAction="http://tempuri.org/IService_kurs/addpeople_postResponse")]
        System.Threading.Tasks.Task addpeople_postAsync(string email, string post, string salary, string role, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/change_pass", ReplyAction="http://tempuri.org/IService_kurs/change_passResponse")]
        string change_pass(string new_pas, string old_pas, string user_id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/change_pass", ReplyAction="http://tempuri.org/IService_kurs/change_passResponse")]
        System.Threading.Tasks.Task<string> change_passAsync(string new_pas, string old_pas, string user_id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/deluser", ReplyAction="http://tempuri.org/IService_kurs/deluserResponse")]
        string deluser(string username, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/deluser", ReplyAction="http://tempuri.org/IService_kurs/deluserResponse")]
        System.Threading.Tasks.Task<string> deluserAsync(string username, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_people", ReplyAction="http://tempuri.org/IService_kurs/vivod_peopleResponse")]
        System.Data.DataTable vivod_people(int str, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_people", ReplyAction="http://tempuri.org/IService_kurs/vivod_peopleResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_peopleAsync(int str, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_staff", ReplyAction="http://tempuri.org/IService_kurs/vivod_staffResponse")]
        System.Data.DataTable vivod_staff(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_staff", ReplyAction="http://tempuri.org/IService_kurs/vivod_staffResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_staffAsync(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_buyer", ReplyAction="http://tempuri.org/IService_kurs/vivod_buyerResponse")]
        System.Data.DataTable vivod_buyer(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_buyer", ReplyAction="http://tempuri.org/IService_kurs/vivod_buyerResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_buyerAsync(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_provider", ReplyAction="http://tempuri.org/IService_kurs/vivod_providerResponse")]
        System.Data.DataTable vivod_provider(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_provider", ReplyAction="http://tempuri.org/IService_kurs/vivod_providerResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_providerAsync(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_admin", ReplyAction="http://tempuri.org/IService_kurs/vivod_adminResponse")]
        System.Data.DataTable vivod_admin(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_admin", ReplyAction="http://tempuri.org/IService_kurs/vivod_adminResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_adminAsync(string id, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_fio", ReplyAction="http://tempuri.org/IService_kurs/search_fioResponse")]
        System.Data.DataTable search_fio(string fio, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_fio", ReplyAction="http://tempuri.org/IService_kurs/search_fioResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> search_fioAsync(string fio, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_tovar", ReplyAction="http://tempuri.org/IService_kurs/search_tovarResponse")]
        string search_tovar(string name_tovar, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_tovar", ReplyAction="http://tempuri.org/IService_kurs/search_tovarResponse")]
        System.Threading.Tasks.Task<string> search_tovarAsync(string name_tovar, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_add_tovar", ReplyAction="http://tempuri.org/IService_kurs/search_add_tovarResponse")]
        void search_add_tovar(string name_tovar, string massa_tovar, string price_tovar, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_add_tovar", ReplyAction="http://tempuri.org/IService_kurs/search_add_tovarResponse")]
        System.Threading.Tasks.Task search_add_tovarAsync(string name_tovar, string massa_tovar, string price_tovar, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_add_tovar_on_sklad", ReplyAction="http://tempuri.org/IService_kurs/search_add_tovar_on_skladResponse")]
        void search_add_tovar_on_sklad(string name_tovar, string sklad_name, string count, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_add_tovar_on_sklad", ReplyAction="http://tempuri.org/IService_kurs/search_add_tovar_on_skladResponse")]
        System.Threading.Tasks.Task search_add_tovar_on_skladAsync(string name_tovar, string sklad_name, string count, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_tovar", ReplyAction="http://tempuri.org/IService_kurs/vivod_tovarResponse")]
        System.Data.DataTable vivod_tovar(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_tovar", ReplyAction="http://tempuri.org/IService_kurs/vivod_tovarResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_tovarAsync(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/searc_tovar_name", ReplyAction="http://tempuri.org/IService_kurs/searc_tovar_nameResponse")]
        System.Data.DataTable searc_tovar_name(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/searc_tovar_name", ReplyAction="http://tempuri.org/IService_kurs/searc_tovar_nameResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> searc_tovar_nameAsync(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/drop_tovar_name", ReplyAction="http://tempuri.org/IService_kurs/drop_tovar_nameResponse")]
        void drop_tovar_name(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/drop_tovar_name", ReplyAction="http://tempuri.org/IService_kurs/drop_tovar_nameResponse")]
        System.Threading.Tasks.Task drop_tovar_nameAsync(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_ing", ReplyAction="http://tempuri.org/IService_kurs/vivod_ingResponse")]
        System.Data.DataTable vivod_ing(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_ing", ReplyAction="http://tempuri.org/IService_kurs/vivod_ingResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_ingAsync(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_tovar_bez", ReplyAction="http://tempuri.org/IService_kurs/vivod_tovar_bezResponse")]
        System.Data.DataTable vivod_tovar_bez(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_tovar_bez", ReplyAction="http://tempuri.org/IService_kurs/vivod_tovar_bezResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_tovar_bezAsync(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_ing", ReplyAction="http://tempuri.org/IService_kurs/search_ingResponse")]
        System.Data.DataTable search_ing(string name_ing, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_ing", ReplyAction="http://tempuri.org/IService_kurs/search_ingResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> search_ingAsync(string name_ing, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/add_ing", ReplyAction="http://tempuri.org/IService_kurs/add_ingResponse")]
        void add_ing(string name_tovar, string name_ing, string fio, string count, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/add_ing", ReplyAction="http://tempuri.org/IService_kurs/add_ingResponse")]
        System.Threading.Tasks.Task add_ingAsync(string name_tovar, string name_ing, string fio, string count, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/change_data", ReplyAction="http://tempuri.org/IService_kurs/change_dataResponse")]
        System.Data.DataTable change_data(string user_id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/change_data", ReplyAction="http://tempuri.org/IService_kurs/change_dataResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> change_dataAsync(string user_id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/update_data", ReplyAction="http://tempuri.org/IService_kurs/update_dataResponse")]
        void update_data(string user_id, string table, string pole, string new_data, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/update_data", ReplyAction="http://tempuri.org/IService_kurs/update_dataResponse")]
        System.Threading.Tasks.Task update_dataAsync(string user_id, string table, string pole, string new_data, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/update_date", ReplyAction="http://tempuri.org/IService_kurs/update_dateResponse")]
        void update_date(string user_id, System.DateTime date_b, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/update_date", ReplyAction="http://tempuri.org/IService_kurs/update_dateResponse")]
        System.Threading.Tasks.Task update_dateAsync(string user_id, System.DateTime date_b, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_sklad", ReplyAction="http://tempuri.org/IService_kurs/search_skladResponse")]
        string search_sklad(string name, string user_id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_sklad", ReplyAction="http://tempuri.org/IService_kurs/search_skladResponse")]
        System.Threading.Tasks.Task<string> search_skladAsync(string name, string user_id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_ing_provider", ReplyAction="http://tempuri.org/IService_kurs/search_ing_providerResponse")]
        string search_ing_provider(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/search_ing_provider", ReplyAction="http://tempuri.org/IService_kurs/search_ing_providerResponse")]
        System.Threading.Tasks.Task<string> search_ing_providerAsync(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_sklad", ReplyAction="http://tempuri.org/IService_kurs/vivod_skladResponse")]
        System.Data.DataTable vivod_sklad(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/vivod_sklad", ReplyAction="http://tempuri.org/IService_kurs/vivod_skladResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> vivod_skladAsync(string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/add_sklad", ReplyAction="http://tempuri.org/IService_kurs/add_skladResponse")]
        void add_sklad(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/add_sklad", ReplyAction="http://tempuri.org/IService_kurs/add_skladResponse")]
        System.Threading.Tasks.Task add_skladAsync(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/delete_sklad", ReplyAction="http://tempuri.org/IService_kurs/delete_skladResponse")]
        void delete_sklad(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/delete_sklad", ReplyAction="http://tempuri.org/IService_kurs/delete_skladResponse")]
        System.Threading.Tasks.Task delete_skladAsync(string name, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/drop_ing", ReplyAction="http://tempuri.org/IService_kurs/drop_ingResponse")]
        string drop_ing(string name, string fio, string role_bd, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService_kurs/drop_ing", ReplyAction="http://tempuri.org/IService_kurs/drop_ingResponse")]
        System.Threading.Tasks.Task<string> drop_ingAsync(string name, string fio, string role_bd, string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService_kursChannel : Client.ServiceReference.IService_kurs, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service_kursClient : System.ServiceModel.ClientBase<Client.ServiceReference.IService_kurs>, Client.ServiceReference.IService_kurs {
        
        public Service_kursClient() {
        }
        
        public Service_kursClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service_kursClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service_kursClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service_kursClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Tuple<string, string, string, string, string> login(string username, string password) {
            return base.Channel.login(username, password);
        }
        
        public System.Threading.Tasks.Task<System.Tuple<string, string, string, string, string>> loginAsync(string username, string password) {
            return base.Channel.loginAsync(username, password);
        }
        
        public void drop_token(string username, string user_id) {
            base.Channel.drop_token(username, user_id);
        }
        
        public System.Threading.Tasks.Task drop_tokenAsync(string username, string user_id) {
            return base.Channel.drop_tokenAsync(username, user_id);
        }
        
        public string role_bd(string user_id) {
            return base.Channel.role_bd(user_id);
        }
        
        public System.Threading.Tasks.Task<string> role_bdAsync(string user_id) {
            return base.Channel.role_bdAsync(user_id);
        }
        
        public string search(string email, string role_bd, string token) {
            return base.Channel.search(email, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<string> searchAsync(string email, string role_bd, string token) {
            return base.Channel.searchAsync(email, role_bd, token);
        }
        
        public void addpeople(string email, string fio, string address, string role, string role_bd, string token) {
            base.Channel.addpeople(email, fio, address, role, role_bd, token);
        }
        
        public System.Threading.Tasks.Task addpeopleAsync(string email, string fio, string address, string role, string role_bd, string token) {
            return base.Channel.addpeopleAsync(email, fio, address, role, role_bd, token);
        }
        
        public void addpeople_info(string email, System.DateTime d_date, string phone_number, string role_bd, string token) {
            base.Channel.addpeople_info(email, d_date, phone_number, role_bd, token);
        }
        
        public System.Threading.Tasks.Task addpeople_infoAsync(string email, System.DateTime d_date, string phone_number, string role_bd, string token) {
            return base.Channel.addpeople_infoAsync(email, d_date, phone_number, role_bd, token);
        }
        
        public void addpeople_info_two(string email, string card_number, string role_bd, string token) {
            base.Channel.addpeople_info_two(email, card_number, role_bd, token);
        }
        
        public System.Threading.Tasks.Task addpeople_info_twoAsync(string email, string card_number, string role_bd, string token) {
            return base.Channel.addpeople_info_twoAsync(email, card_number, role_bd, token);
        }
        
        public void addpeople_post(string email, string post, string salary, string role, string role_bd, string token) {
            base.Channel.addpeople_post(email, post, salary, role, role_bd, token);
        }
        
        public System.Threading.Tasks.Task addpeople_postAsync(string email, string post, string salary, string role, string role_bd, string token) {
            return base.Channel.addpeople_postAsync(email, post, salary, role, role_bd, token);
        }
        
        public string change_pass(string new_pas, string old_pas, string user_id, string token) {
            return base.Channel.change_pass(new_pas, old_pas, user_id, token);
        }
        
        public System.Threading.Tasks.Task<string> change_passAsync(string new_pas, string old_pas, string user_id, string token) {
            return base.Channel.change_passAsync(new_pas, old_pas, user_id, token);
        }
        
        public string deluser(string username, string role_bd, string token) {
            return base.Channel.deluser(username, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<string> deluserAsync(string username, string role_bd, string token) {
            return base.Channel.deluserAsync(username, role_bd, token);
        }
        
        public System.Data.DataTable vivod_people(int str, string role_bd, string token) {
            return base.Channel.vivod_people(str, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_peopleAsync(int str, string role_bd, string token) {
            return base.Channel.vivod_peopleAsync(str, role_bd, token);
        }
        
        public System.Data.DataTable vivod_staff(string id, string role_bd, string token) {
            return base.Channel.vivod_staff(id, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_staffAsync(string id, string role_bd, string token) {
            return base.Channel.vivod_staffAsync(id, role_bd, token);
        }
        
        public System.Data.DataTable vivod_buyer(string id, string role_bd, string token) {
            return base.Channel.vivod_buyer(id, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_buyerAsync(string id, string role_bd, string token) {
            return base.Channel.vivod_buyerAsync(id, role_bd, token);
        }
        
        public System.Data.DataTable vivod_provider(string id, string role_bd, string token) {
            return base.Channel.vivod_provider(id, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_providerAsync(string id, string role_bd, string token) {
            return base.Channel.vivod_providerAsync(id, role_bd, token);
        }
        
        public System.Data.DataTable vivod_admin(string id, string role_bd, string token) {
            return base.Channel.vivod_admin(id, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_adminAsync(string id, string role_bd, string token) {
            return base.Channel.vivod_adminAsync(id, role_bd, token);
        }
        
        public System.Data.DataTable search_fio(string fio, string role_bd, string token) {
            return base.Channel.search_fio(fio, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> search_fioAsync(string fio, string role_bd, string token) {
            return base.Channel.search_fioAsync(fio, role_bd, token);
        }
        
        public string search_tovar(string name_tovar, string role_bd, string token) {
            return base.Channel.search_tovar(name_tovar, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<string> search_tovarAsync(string name_tovar, string role_bd, string token) {
            return base.Channel.search_tovarAsync(name_tovar, role_bd, token);
        }
        
        public void search_add_tovar(string name_tovar, string massa_tovar, string price_tovar, string role_bd, string token) {
            base.Channel.search_add_tovar(name_tovar, massa_tovar, price_tovar, role_bd, token);
        }
        
        public System.Threading.Tasks.Task search_add_tovarAsync(string name_tovar, string massa_tovar, string price_tovar, string role_bd, string token) {
            return base.Channel.search_add_tovarAsync(name_tovar, massa_tovar, price_tovar, role_bd, token);
        }
        
        public void search_add_tovar_on_sklad(string name_tovar, string sklad_name, string count, string role_bd, string token) {
            base.Channel.search_add_tovar_on_sklad(name_tovar, sklad_name, count, role_bd, token);
        }
        
        public System.Threading.Tasks.Task search_add_tovar_on_skladAsync(string name_tovar, string sklad_name, string count, string role_bd, string token) {
            return base.Channel.search_add_tovar_on_skladAsync(name_tovar, sklad_name, count, role_bd, token);
        }
        
        public System.Data.DataTable vivod_tovar(string role_bd, string token) {
            return base.Channel.vivod_tovar(role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_tovarAsync(string role_bd, string token) {
            return base.Channel.vivod_tovarAsync(role_bd, token);
        }
        
        public System.Data.DataTable searc_tovar_name(string name, string role_bd, string token) {
            return base.Channel.searc_tovar_name(name, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> searc_tovar_nameAsync(string name, string role_bd, string token) {
            return base.Channel.searc_tovar_nameAsync(name, role_bd, token);
        }
        
        public void drop_tovar_name(string name, string role_bd, string token) {
            base.Channel.drop_tovar_name(name, role_bd, token);
        }
        
        public System.Threading.Tasks.Task drop_tovar_nameAsync(string name, string role_bd, string token) {
            return base.Channel.drop_tovar_nameAsync(name, role_bd, token);
        }
        
        public System.Data.DataTable vivod_ing(string role_bd, string token) {
            return base.Channel.vivod_ing(role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_ingAsync(string role_bd, string token) {
            return base.Channel.vivod_ingAsync(role_bd, token);
        }
        
        public System.Data.DataTable vivod_tovar_bez(string role_bd, string token) {
            return base.Channel.vivod_tovar_bez(role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_tovar_bezAsync(string role_bd, string token) {
            return base.Channel.vivod_tovar_bezAsync(role_bd, token);
        }
        
        public System.Data.DataTable search_ing(string name_ing, string role_bd, string token) {
            return base.Channel.search_ing(name_ing, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> search_ingAsync(string name_ing, string role_bd, string token) {
            return base.Channel.search_ingAsync(name_ing, role_bd, token);
        }
        
        public void add_ing(string name_tovar, string name_ing, string fio, string count, string role_bd, string token) {
            base.Channel.add_ing(name_tovar, name_ing, fio, count, role_bd, token);
        }
        
        public System.Threading.Tasks.Task add_ingAsync(string name_tovar, string name_ing, string fio, string count, string role_bd, string token) {
            return base.Channel.add_ingAsync(name_tovar, name_ing, fio, count, role_bd, token);
        }
        
        public System.Data.DataTable change_data(string user_id, string token) {
            return base.Channel.change_data(user_id, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> change_dataAsync(string user_id, string token) {
            return base.Channel.change_dataAsync(user_id, token);
        }
        
        public void update_data(string user_id, string table, string pole, string new_data, string token) {
            base.Channel.update_data(user_id, table, pole, new_data, token);
        }
        
        public System.Threading.Tasks.Task update_dataAsync(string user_id, string table, string pole, string new_data, string token) {
            return base.Channel.update_dataAsync(user_id, table, pole, new_data, token);
        }
        
        public void update_date(string user_id, System.DateTime date_b, string token) {
            base.Channel.update_date(user_id, date_b, token);
        }
        
        public System.Threading.Tasks.Task update_dateAsync(string user_id, System.DateTime date_b, string token) {
            return base.Channel.update_dateAsync(user_id, date_b, token);
        }
        
        public string search_sklad(string name, string user_id, string token) {
            return base.Channel.search_sklad(name, user_id, token);
        }
        
        public System.Threading.Tasks.Task<string> search_skladAsync(string name, string user_id, string token) {
            return base.Channel.search_skladAsync(name, user_id, token);
        }
        
        public string search_ing_provider(string name, string role_bd, string token) {
            return base.Channel.search_ing_provider(name, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<string> search_ing_providerAsync(string name, string role_bd, string token) {
            return base.Channel.search_ing_providerAsync(name, role_bd, token);
        }
        
        public System.Data.DataTable vivod_sklad(string role_bd, string token) {
            return base.Channel.vivod_sklad(role_bd, token);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> vivod_skladAsync(string role_bd, string token) {
            return base.Channel.vivod_skladAsync(role_bd, token);
        }
        
        public void add_sklad(string name, string role_bd, string token) {
            base.Channel.add_sklad(name, role_bd, token);
        }
        
        public System.Threading.Tasks.Task add_skladAsync(string name, string role_bd, string token) {
            return base.Channel.add_skladAsync(name, role_bd, token);
        }
        
        public void delete_sklad(string name, string role_bd, string token) {
            base.Channel.delete_sklad(name, role_bd, token);
        }
        
        public System.Threading.Tasks.Task delete_skladAsync(string name, string role_bd, string token) {
            return base.Channel.delete_skladAsync(name, role_bd, token);
        }
        
        public string drop_ing(string name, string fio, string role_bd, string token) {
            return base.Channel.drop_ing(name, fio, role_bd, token);
        }
        
        public System.Threading.Tasks.Task<string> drop_ingAsync(string name, string fio, string role_bd, string token) {
            return base.Channel.drop_ingAsync(name, fio, role_bd, token);
        }
    }
}
