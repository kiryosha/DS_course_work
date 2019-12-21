using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace bsbd_kurs
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService_kurs" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService_kurs
    {
        [OperationContract]
        Tuple<string, string, string, string, string> login(string username, string password);

        [OperationContract]
        void drop_token(string username, string user_id);

        [OperationContract]
        string role_bd(string user_id);

        [OperationContract]
        string search(string email, string role_bd, string token);

        [OperationContract]
        void addpeople(string email, string fio, string address, string role, string role_bd, string token);

        [OperationContract]
        void addpeople_info(string email, DateTime d_date, string phone_number, string role_bd, string token);

        [OperationContract]
        void addpeople_info_two(string email, string card_number, string role_bd, string token);

        [OperationContract]
        void addpeople_post(string email, string post, string salary, string role, string role_bd, string token);

        [OperationContract]
        string change_pass(string new_pas, string old_pas, string user_id, string token);

        [OperationContract]
        string deluser(string username, string role_bd, string token);

        [OperationContract]
        DataTable vivod_people(int str, string role_bd, string token);

        [OperationContract]
        DataTable vivod_staff(string id, string role_bd, string token);

        [OperationContract]
        DataTable vivod_buyer(string id, string role_bd, string token);

        [OperationContract]
        DataTable vivod_provider(string id, string role_bd, string token);

        [OperationContract]
        DataTable vivod_admin(string id, string role_bd, string token);

        [OperationContract]
        DataTable search_fio(string fio, string role_bd, string token);

        [OperationContract]
        string search_tovar(string name_tovar, string role_bd, string token);

        [OperationContract]
        void search_add_tovar(string name_tovar, string massa_tovar, string price_tovar, string role_bd, string token);

        [OperationContract]
        void search_add_tovar_on_sklad(string name_tovar, string sklad_name, string count, string role_bd, string token);

        [OperationContract]
        DataTable vivod_tovar(string role_bd, string token);

        [OperationContract]
        DataTable searc_tovar_name(string name, string role_bd, string token);

        [OperationContract]
        void drop_tovar_name(string name, string role_bd, string token);

        [OperationContract]
        DataTable vivod_ing(string role_bd, string token);

        [OperationContract]
        DataTable vivod_tovar_bez(string role_bd, string token);


        [OperationContract]
        DataTable search_ing(string name_ing, string role_bd, string token);

        [OperationContract]
        void add_ing(string name_tovar, string name_ing, string fio, string count, string role_bd, string token);

        [OperationContract]
        DataTable change_data(string user_id, string token);

        [OperationContract]
        void update_data(string user_id, string table, string pole, string new_data, string token);

        [OperationContract]
        void update_date(string user_id, DateTime date_b, string token);

        [OperationContract]
        string search_sklad(string name, string user_id, string token);

        [OperationContract]
        string search_ing_provider(string name, string role_bd, string token);

        [OperationContract]
        DataTable vivod_sklad(string role_bd, string token);

        [OperationContract]
        void add_sklad(string name, string role_bd, string token);

        [OperationContract]
        void delete_sklad(string name, string role_bd, string token);

        [OperationContract]
        string drop_ing(string name, string fio, string role_bd, string token);
    }
}
