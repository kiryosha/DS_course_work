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
        string search(string email);

        [OperationContract]
        void addpeople(string email, string fio, string address, string role);

        [OperationContract]
        void addpeople_info(string email, DateTime d_date, string phone_number);

        [OperationContract]
        void addpeople_info_two(string email, string card_number);

        [OperationContract]
        void addpeople_post(string email, string post, string salary, string role);

        [OperationContract]
        string change_pass(string new_pas, string old_pas, string user_id);

        [OperationContract]
        string deluser(string username);

        [OperationContract]
        DataTable vivod_people(int str);

        [OperationContract]
        DataTable vivod_staff(string id);

        [OperationContract]
        DataTable vivod_buyer(string id);

        [OperationContract]
        DataTable vivod_provider(string id);

        [OperationContract]
        DataTable vivod_admin(string id);

        [OperationContract]
        DataTable search_fio(string fio);

        [OperationContract]
        string search_tovar(string name_tovar);

        [OperationContract]
        void search_add_tovar(string name_tovar, string massa_tovar, string price_tovar);

        [OperationContract]
        void search_add_tovar_on_sklad(string name_tovar, string sklad_name, string count);

        [OperationContract]
        DataTable vivod_tovar();

        [OperationContract]
        DataTable searc_tovar_name(string name);

        [OperationContract]
        void drop_tovar_name(string name);

        [OperationContract]
        DataTable vivod_ing();

        [OperationContract]
        DataTable vivod_tovar_bez();


        [OperationContract]
        DataTable search_ing(string name_ing);

        [OperationContract]
        void add_ing(string name_tovar, string name_ing, string fio, string count);

        [OperationContract]
        DataTable change_data(string user_id);

        [OperationContract]
        void update_data(string user_id, string table, string pole, string new_data);

        [OperationContract]
        void update_date(string user_id, DateTime date_b);

        [OperationContract]
        string search_sklad(string name);

        [OperationContract]
        string search_ing_provider(string name);

        [OperationContract]
        DataTable vivod_sklad();

        [OperationContract]
        void add_sklad(string name);

        [OperationContract]
        void delete_sklad(string name);

        [OperationContract]
        string drop_ing(string name, string fio);

    }
}
