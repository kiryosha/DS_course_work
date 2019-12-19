using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;

namespace bsbd_kurs
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service_kurs" в коде и файле конфигурации.
    public class Service_kurs : IService_kurs
    {
        public string Encoding_password(string password)
        {
            using (var hash = MD5.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        public MySqlConnection Connection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=user_db;password='q1234q';database=confectionerydb;";
            MySqlConnection MySqlConnection = new MySqlConnection(connectionString);
            return MySqlConnection;
        }
        public Tuple<string, string, string, string, string> login(string username, string password)
        {
            string user_id = "";
            string result_connect = "no";
            string epassword;
            string role = "";
            string username_connect = "";
            try
            {
                epassword = Encoding_password(password);
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlDataReader MySqlReader = null;
                MySqlCommand command = new MySqlCommand("SELECT * FROM people", MySqlConnection);
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (username == Convert.ToString(MySqlReader["username"]) && epassword == Convert.ToString(MySqlReader["password"]))
                    {
                        result_connect = "yes";
                        user_id = Convert.ToString(MySqlReader["user_id"]);
                        role = Convert.ToString(MySqlReader["role"]);
                        username_connect = Convert.ToString(MySqlReader["username"]);
                        Console.WriteLine($"Пользователь {username} успешно вошел! - {DateTime.Now}");
                        MySqlReader.Close();
                        string sqlquery = "INSERT INTO sessions (user_id, token, date) VALUES(@user_id, @token, @date)";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@token", Encoding_password(user_id));
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Tuple.Create(result_connect, role, username_connect, Encoding_password(user_id), user_id);
        }

        public void drop_token(string username, string user_id)
        {
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery = "Delete from sessions where user_id = @user_id";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Пользователь {username} вышел! - {DateTime.Now}");
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string role_bd(string user_id)
        {
            string role = "";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlDataReader MySqlReader = null;
                MySqlCommand command = new MySqlCommand("SELECT * FROM staff where user_id = @user_id", MySqlConnection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Connection = MySqlConnection;
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (MySqlReader != null)
                    {
                        role = "staff";
                        MySqlReader.Close();
                        break;
                    }
                }
                MySqlReader.Close();
                MySqlDataReader MySqlReader2 = null;
                MySqlCommand command2 = new MySqlCommand("SELECT * FROM buyer where user_id = @user_id", MySqlConnection);
                command2.Parameters.AddWithValue("@user_id", user_id);
                command2.Connection = MySqlConnection;
                MySqlReader2 = command2.ExecuteReader();
                while (MySqlReader2.Read())
                {
                    if (MySqlReader2 != null)
                    {
                        role = "buyer";
                        MySqlReader2.Close();
                        break;
                    }
                }
                MySqlReader2.Close();
                MySqlDataReader MySqlReader3 = null;
                MySqlCommand command3 = new MySqlCommand("SELECT * FROM provider where user_id = @user_id", MySqlConnection);
                command3.Parameters.AddWithValue("@user_id", user_id);
                command3.Connection = MySqlConnection;
                MySqlReader3 = command3.ExecuteReader();
                while (MySqlReader3.Read())
                {
                    if (MySqlReader3 != null)
                    {
                        role = "provider";
                        MySqlReader3.Close();
                        break;
                    }
                }
                MySqlReader3.Close();
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return role;
        }

        public string search(string email)
        {
            string result = "no";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlDataReader MySqlReader = null;
                MySqlCommand command = new MySqlCommand("SELECT * FROM people", MySqlConnection);
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (email == Convert.ToString(MySqlReader["email"]))
                    {
                        result = "yes";
                        MySqlReader.Close();
                        break;
                    }
                }
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public void addpeople(string email, string fio, string address, string role)
        {
            try
            {
                string r = "";
                using (SHA1 shaM = new SHA1Managed())
                {
                    byte[] hash2 = shaM.ComputeHash(Encoding.UTF8.GetBytes(fio + address));
                    r = BitConverter.ToString(hash2).Replace("-", "").ToLower();
                }

                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery = "INSERT INTO people (user_id, username, password, email, fio, address, registration, role) VALUES(@user_id, @username, @password, @email, @fio, @address, @registration, @role)";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@user_id", r);
                cmd.Parameters.AddWithValue("@username", email);
                cmd.Parameters.AddWithValue("@password", Encoding_password("qwerty1"));
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@fio", fio);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@registration", DateTime.Now);
                if (role == "admin")
                {
                    cmd.Parameters.AddWithValue("@role", "admin");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@role", "user");
                }
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
                MySqlConnection.Close();
                Console.WriteLine($"Пользователь {email} успешно создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void addpeople_info(string email, DateTime d_date, string phone_number)
        {
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery = "INSERT INTO people_info set user_id = (select user_id from people where email = @email), birthday_date = @birthday_date, about = 'new user', phone_number = @phone_number, company = 'Our Company'";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@birthday_date", d_date);
                cmd.Parameters.AddWithValue("@phone_number", phone_number);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void addpeople_info_two(string email, string card_number)
        {
            try
            {
                Random rnd = new Random();
                int value = rnd.Next(10, 1000000);
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery = "INSERT INTO people_info_two set user_id = (select user_id from people where email = @email), card_number = @card_number, amount = @amount";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@amount", value);
                cmd.Parameters.AddWithValue("@card_number", card_number);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void addpeople_post(string email, string post, string salary, string role)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();

            try
            {
                if (role == "staff")
                {
                    string sqlquery3 = "INSERT INTO staff set user_id = (select user_id from people where email = @email), post = @post, salary = @salary";
                    MySqlCommand cmd3 = new MySqlCommand(sqlquery3, MySqlConnection);
                    cmd3.Parameters.AddWithValue("@email", email);
                    cmd3.Parameters.AddWithValue("@post", post);
                    cmd3.Parameters.AddWithValue("@salary", salary);
                    cmd3.Connection = MySqlConnection;
                    cmd3.ExecuteNonQuery();
                }

                if (role == "buyer")
                {
                    Random rnd1 = new Random();
                    string club_card = rnd1.Next(1000, 9999) + "-" + rnd1.Next(1000, 9999) + "-" + rnd1.Next(1000, 9999) + "-" + rnd1.Next(1000, 9999);
                    string sqlquery3 = "INSERT INTO buyer set user_id = (select user_id from people where email = @email), club_card = @club_card";
                    MySqlCommand cmd3 = new MySqlCommand(sqlquery3, MySqlConnection);
                    cmd3.Parameters.AddWithValue("@email", email);
                    cmd3.Parameters.AddWithValue("@club_card", club_card);
                    cmd3.Connection = MySqlConnection;
                    cmd3.ExecuteNonQuery();
                }

                if (role == "provider")
                {
                    Random rnd2 = new Random();
                    string sqlquery4 = "INSERT INTO provider set user_id = (select user_id from people where email = @email), rating = @rating";
                    MySqlCommand cmd4 = new MySqlCommand(sqlquery4, MySqlConnection);
                    cmd4.Parameters.AddWithValue("@rating", rnd2.Next(1, 10));
                    cmd4.Parameters.AddWithValue("@email", email);
                    cmd4.Connection = MySqlConnection;
                    cmd4.ExecuteNonQuery();
                }
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string change_pass(string new_pas, string old_pas, string user_id)
        {
            string result = "no";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("select * from people", MySqlConnection);
                MySqlDataReader MySqlReader = null;
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (Encoding_password(old_pas) == Convert.ToString(MySqlReader["password"]))
                    {
                        MySqlReader.Close();
                        string sqlquery = "UPDATE people SET password = @newpass where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@newpass", Encoding_password(new_pas));
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                        result = "yes";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        
        public string deluser(string username)
        {
            string result = "no";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("select * from people", MySqlConnection);
                MySqlDataReader MySqlReader = null;
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (username == Convert.ToString(MySqlReader["username"]))
                    {
                        MySqlReader.Close();
                        string sqlquery = "Delete from people where username = @username";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                        result = "yes";
                        Console.WriteLine($"Пользователь {username} удален!");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public DataTable vivod_people(int str)
        {
            DataTable dt = new DataTable("people");
            int r2;
            if (str == 1)
            {
                r2 = 0;
            }
            else
            {
                r2 = 50 * (str - 1);
            }
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id limit 50 " + "OFFSET " + r2.ToString(), MySqlConnection);
                MySqlDataAdapter addapter = new MySqlDataAdapter(command); ;
                addapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public DataTable vivod_staff(string id)
        {
            string add = "";
            if (id != "")
            {
                add = " where people.fio LIKE lower('%" + id + "%')";
            }
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.fio,staff.post,staff.salary FROM people JOIN staff on people.user_id = staff.user_id" + add, MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("people_staff");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable vivod_buyer(string id)
        {
            string add = "";
            if (id != "")
            {
                add = " where people.fio LIKE lower('%" + id + "%')";
            }
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.fio,buyer.club_card FROM people JOIN buyer on people.user_id = buyer.user_id" + add, MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("people_buyer");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable vivod_provider(string id)
        {
            string add = "";
            if (id != "")
            {
                add = " where people.fio LIKE lower('%" + id + "%')";
            }
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.fio,provider.rating FROM people JOIN provider on people.user_id = provider.user_id" + add, MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("people_provider");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable vivod_admin(string id)
        {
            string add = "";
            if (id != "")
            {
                add = " where people.fio LIKE lower('%" + id + "%')";
            }
            else
            {
                add = " where people.role = 'admin'";
            }
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id" + add, MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("people_admin");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable search_fio(string fio)
        {
            DataTable dt = new DataTable("people_admin"); ;
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id where fio LIKE lower('%" + fio + "%')", MySqlConnection);
                MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                addapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public string search_tovar(string name_tovar) 
        {
            string result = "no";
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("select * from tovar", MySqlConnection);
            MySqlDataReader MySqlReader = null;
            MySqlReader = command.ExecuteReader();
            while (MySqlReader.Read())
            {
                if (name_tovar == Convert.ToString(MySqlReader["name_tovar"]))
                {
                    MySqlReader.Close();
                    result = "yes";
                    break;
                }
            }
            return result;
        }

        public void search_add_tovar(string name_tovar, string massa_tovar, string price_tovar)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();

            try
            {
                string sqlquery = "INSERT INTO tovar set name_tovar = @name_tovar, massa_tovar = @massa_tovar, price_tovar = @price_tovar";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@name_tovar", name_tovar);
                cmd.Parameters.AddWithValue("@massa_tovar", massa_tovar);
                cmd.Parameters.AddWithValue("@price_tovar", price_tovar);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void search_add_tovar_on_sklad(string name_tovar, string sklad_name, string count)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();

            try
            {
                MySqlCommand command = new MySqlCommand("select sklad_id from sklad where sklad_name = @sklad_name", MySqlConnection);
                command.Parameters.AddWithValue("@sklad_name", sklad_name);
                command.Connection = MySqlConnection;
                command.ExecuteNonQuery();
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string id_sklad = reader["sklad_id"].ToString();
                reader.Close();

                string sqlquery = "INSERT INTO tovar_on_sklad set tovar_id = (select tovar_id from tovar where name_tovar = @name_tovar), sklad_id = @sklad_id, count = @count";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@name_tovar", name_tovar);
                cmd.Parameters.AddWithValue("@count", count);
                cmd.Parameters.AddWithValue("@sklad_id", id_sklad);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DataTable vivod_tovar()
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar, tovar.massa_tovar,tovar.price_tovar,tovar_on_sklad.count,sklad.sklad_name FROM tovar JOIN tovar_on_sklad on tovar.tovar_id = tovar_on_sklad.tovar_id JOIN sklad on tovar_on_sklad.sklad_id = sklad.sklad_id", MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("dt_tovar");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable searc_tovar_name(string name)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar, tovar.massa_tovar,tovar.price_tovar,tovar_on_sklad.count,sklad.sklad_name FROM tovar JOIN tovar_on_sklad on tovar.tovar_id = tovar_on_sklad.tovar_id JOIN sklad on tovar_on_sklad.sklad_id = sklad.sklad_id where name_tovar like lower('%" + name + "%')", MySqlConnection);
            command.Parameters.AddWithValue("@name.tovar", name);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("dt_one_tovar");
            addapter.Fill(dt);
            return dt;
        }

        public void drop_tovar_name(string name)
        {
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery = "Delete from tovar where name_tovar = @name_tovar";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@name_tovar", name);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
                MySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DataTable vivod_ing()
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar,spec_ingredients.name_ing,people.fio,ingredients.ing_count FROM tovar JOIN ingredients on tovar.tovar_id = ingredients.tovar_id JOIN spec_ingredients on ingredients.id_ing = spec_ingredients.id_ing JOIN people on spec_ingredients.user_id = people.user_id", MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("dt_ing");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable vivod_tovar_bez()
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar FROM tovar WHERE tovar_id NOT IN (SELECT tovar_id FROM ingredients)", MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("tovar_bez");
            addapter.Fill(dt);
            return dt;
        }

        public DataTable search_ing(string name_ing)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar,spec_ingredients.name_ing,people.fio,ingredients.ing_count FROM tovar JOIN ingredients on tovar.tovar_id = ingredients.tovar_id JOIN spec_ingredients on ingredients.id_ing = spec_ingredients.id_ing JOIN people on spec_ingredients.user_id = people.user_id where spec_ingredients.name_ing like lower('%" + name_ing + "%')", MySqlConnection);
            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("dt_ing");
            addapter.Fill(dt);
            return dt;
        }

        public void add_ing(string name_tovar, string name_ing, string fio, string count)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();

            try
            {
                string sqlquery = "INSERT INTO spec_ingredients set name_ing = @name_ing, user_id = (select user_id from people where fio = @fio)";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@name_ing", name_ing);
                cmd.Parameters.AddWithValue("@fio", fio);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();

                string sqlquery1 = "INSERT INTO ingredients set tovar_id = (select tovar_id from tovar where name_tovar = @name_tovar), id_ing = (select id_ing from spec_ingredients where name_ing = @name_ing), ing_count = @ing_count";
                MySqlCommand cmd1 = new MySqlCommand(sqlquery1, MySqlConnection);
                cmd1.Parameters.AddWithValue("@name_tovar", name_tovar);
                cmd1.Parameters.AddWithValue("@name_ing", name_ing);
                cmd1.Parameters.AddWithValue("@ing_count", count);
                cmd1.Connection = MySqlConnection;
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DataTable change_data(string user_id)
        {

            DataTable dt = new DataTable("people");
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("SELECT people.username,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id where people.user_id = @user_id", MySqlConnection);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                addapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public void update_data(string user_id, string table, string pole, string new_data)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            try
            {
                string sqlquery = "Update " + table + " set " + pole + " = '" + new_data + "' where user_id = '" + user_id + "'";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void update_date(string user_id, DateTime date_b)
        {
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery = "Update people_info set birthday_date = @date where user_id = '" + user_id + "'";
                MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                cmd.Parameters.AddWithValue("@date", date_b);
                cmd.Connection = MySqlConnection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string search_sklad(string name)
        {
            string result = "no";
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("select * from sklad", MySqlConnection);
            MySqlDataReader MySqlReader = null;
            MySqlReader = command.ExecuteReader();
            while (MySqlReader.Read())
            {
                if (name == Convert.ToString(MySqlReader["sklad_name"]))
                {
                    MySqlReader.Close();
                    result = "yes";
                    break;
                }
            }
            return result;
        }

        public string search_ing_provider(string name)
        {
            string result = "no";
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.fio as fio,provider.rating FROM people JOIN provider on people.user_id = provider.user_id where people.fio = @fio", MySqlConnection);
            command.Parameters.AddWithValue("@fio", name);
            command.Connection = MySqlConnection;
            MySqlDataReader MySqlReader = null;
            MySqlReader = command.ExecuteReader();
            while (MySqlReader.Read())
            {
                if (name == Convert.ToString(MySqlReader["fio"]))
                {
                    MySqlReader.Close();
                    result = "yes";
                    break;
                }
            }
            return result;
        }

        public DataTable vivod_sklad()
        {
            DataTable dt = new DataTable("sklad");
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("select sklad_name, tovar_count from sklad", MySqlConnection);
                MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                addapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public void add_sklad(string name)
        {
            DataTable dt = new DataTable("sklad");
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery1 = "insert into sklad set sklad_name = @skald_name, tovar_count = 0";
                MySqlCommand cmd1 = new MySqlCommand(sqlquery1, MySqlConnection);
                cmd1.Parameters.AddWithValue("@skald_name", name);
                cmd1.Connection = MySqlConnection;
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void delete_sklad(string name)
        {
            DataTable dt = new DataTable("sklad");
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                string sqlquery1 = "delete from sklad where sklad_name = @skald_name";
                MySqlCommand cmd1 = new MySqlCommand(sqlquery1, MySqlConnection);
                cmd1.Parameters.AddWithValue("@skald_name", name);
                cmd1.Connection = MySqlConnection;
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string drop_ing(string name, string fio)
        {
            string result = "no";
            string ing_id = "";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("select * from spec_ingredients where name_ing = @name_ing and user_id = (select user_id from people where fio = @fio)", MySqlConnection);
                command.Parameters.AddWithValue("@name_ing", name);
                command.Parameters.AddWithValue("@fio", fio);
                command.Connection = MySqlConnection;
                MySqlDataReader MySqlReader = null;
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (name == Convert.ToString(MySqlReader["name_ing"]))
                    {
                        ing_id = Convert.ToString(MySqlReader["id_ing"]);
                        MySqlReader.Close();
                        result = "yes";
                        break;
                    }
                }
                if(result == "yes")
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM spec_ingredients where name_ing = @name_ing", MySqlConnection);
                    cmd.Parameters.AddWithValue("@name_ing", name);
                    cmd.Connection = MySqlConnection;
                    cmd.ExecuteNonQuery();

                    MySqlCommand cmd1 = new MySqlCommand("DELETE FROM ingredients where id_ing = @ing_id", MySqlConnection);
                    cmd1.Parameters.AddWithValue("@name_ing", name);
                    cmd1.Parameters.AddWithValue("@ing_id", ing_id);
                    cmd1.Connection = MySqlConnection;
                    cmd1.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}
