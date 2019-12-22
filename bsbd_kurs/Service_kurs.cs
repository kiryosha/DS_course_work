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
        private string Encoding_password(string password)
        {
            using (var hash = MD5.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private MySqlConnection Connection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=user_db;password='q1234q';database=confectionerydb;";
            MySqlConnection MySqlConnection = new MySqlConnection(connectionString);
            return MySqlConnection;
        }

        public string search_sessions(string token)
        {
            string result = "no";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("select token from sessions where token = @token", MySqlConnection);
                command.Parameters.AddWithValue("@token", token);
                command.Connection = MySqlConnection;
                MySqlDataReader MySqlReader = null;
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (token == Convert.ToString(MySqlReader["token"]))
                    {
                        MySqlReader.Close();
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
                MySqlCommand command = new MySqlCommand("SELECT username,password,user_id,role FROM people where username = @username and password = @password", MySqlConnection);
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("@password", Encoding_password(password));
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
                        return role;
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
                        return role;
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
                        return role;
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

        public string search(string email, string role_bd, string token)
        {
            string result = "no";
            try
            {
                string r = search_sessions(token);
                if (r == "yes")
                {
                    if (role_bd == "admin")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        MySqlDataReader MySqlReader = null;
                        MySqlCommand command = new MySqlCommand("SELECT email FROM people where email = @email", MySqlConnection);
                        command.Parameters.AddWithValue("@email", email);
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public void addpeople(string email, string fio, string address, string role, string role_bd, string token)
        {
            try
            {
                string result = search_sessions(token);
                if(result == "yes")
                {
                    if (role_bd == "admin")
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void addpeople_info(string email, DateTime d_date, string phone_number, string role_bd, string token)
        {
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin")
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void addpeople_info_two(string email, string card_number, string role_bd, string token)
        {
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin")
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void addpeople_post(string email, string post, string salary, string role, string role_bd, string token)
        {
            try 
            {

                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string change_pass(string new_pas, string old_pas, string user_id, string token)
        {
            string result = "no";
            string r = search_sessions(token);
            try
            {
                if(r == "yes")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("select user_id, password from people where user_id = @user_id and password = @password", MySqlConnection);
                    command.Parameters.AddWithValue("user_id", user_id);
                    command.Parameters.AddWithValue("@password", Encoding_password(old_pas));
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
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        
        public string deluser(string username, string role_bd, string token)
        {
            string result = "no";
            string r = search_sessions(token);
            try
            {
                if (r == "yes")
                {
                    if (role_bd == "admin")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        MySqlCommand command = new MySqlCommand("select username from people where username = @username", MySqlConnection);
                        command.Parameters.AddWithValue("username", username);
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public DataTable vivod_people(int str, string role_bd, string token)
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
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin" || role_bd == "staff")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        MySqlCommand command = new MySqlCommand("SELECT people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id limit 50 OFFSET @r2", MySqlConnection);
                        command.Parameters.AddWithValue("@r2", r2);
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command); ;
                        addapter.Fill(dt);
                    }
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }
        //id = fio
        public DataTable vivod_staff(string id, string role_bd, string token)
        {
            DataTable dt = new DataTable("people_staff");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    try
                    {
                        if (id != "")
                        {
                            MySqlCommand command = new MySqlCommand("SELECT people.fio AS ФИО,staff.post AS Должность,staff.salary AS Зарплата FROM people JOIN staff on people.user_id = staff.user_id where people.fio LIKE @id", MySqlConnection);
                            command.Parameters.AddWithValue("@id", "%" + id + "%");
                            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                            addapter.Fill(dt);
                        }
                        else
                        {
                            MySqlCommand command = new MySqlCommand("SELECT people.fio AS ФИО,staff.post AS Должность,staff.salary AS Зарплата FROM people JOIN staff on people.user_id = staff.user_id", MySqlConnection);
                            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                            addapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public DataTable vivod_buyer(string id, string role_bd, string token)
        {
            DataTable dt = new DataTable("people_buyer");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    try
                    {
                        if (id != "")
                        {
                            MySqlCommand command = new MySqlCommand("SELECT people.fio,buyer.club_card FROM people JOIN buyer on people.user_id = buyer.user_id where people.fio LIKE @id", MySqlConnection);
                            command.Parameters.AddWithValue("@id", "%" + id + "%");
                            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                            addapter.Fill(dt);
                        }
                        else
                        {
                            MySqlCommand command = new MySqlCommand("SELECT people.fio,buyer.club_card FROM people JOIN buyer on people.user_id = buyer.user_id", MySqlConnection);
                            MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                            addapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public DataTable vivod_provider(string id, string role_bd, string token)
        {
            DataTable dt = new DataTable("people_provider");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    if (id != "")
                    {
                        MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.fio,provider.rating FROM people JOIN provider on people.user_id = provider.user_id where people.fio LIKE @id", MySqlConnection);
                        command.Parameters.AddWithValue("@id", "%" + id + "%");
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                        addapter.Fill(dt);
                    }
                    else
                    {
                        MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.fio,provider.rating FROM people JOIN provider on people.user_id = provider.user_id", MySqlConnection);
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                        addapter.Fill(dt);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public DataTable vivod_admin(string id, string role_bd, string token)
        {
            DataTable dt = new DataTable("people_admin");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    if (id != "")
                    {
                        MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id where people.fio like @id", MySqlConnection);
                        command.Parameters.AddWithValue("@id", "%" + id + "%");
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                        addapter.Fill(dt);
                    }
                    else
                    {
                        MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id where people.role = 'admin'", MySqlConnection);
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                        addapter.Fill(dt);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public DataTable search_fio(string fio, string role_bd, string token)
        {
            DataTable dt = new DataTable("people"); ;
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin")
                    {

                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        MySqlCommand command = new MySqlCommand("SELECT people.user_id,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id where fio LIKE @fio", MySqlConnection);
                        command.Parameters.AddWithValue("@fio", "%" + fio + "%");
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                        addapter.Fill(dt);
                    }
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public string search_tovar(string name_tovar, string role_bd, string token) 
        {
            string result = "no";
            string r = search_sessions(token);
            if (r == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "buyer")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("select name_tovar from tovar where name_tovar = @name_tovar", MySqlConnection);
                    command.Parameters.AddWithValue("@name_tovar", name_tovar);
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
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return result;
        }

        public void search_add_tovar(string name_tovar, string massa_tovar, string price_tovar, string role_bd, string token)
        {
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff")
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
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
        }

        public void search_add_tovar_on_sklad(string name_tovar, string sklad_name, string count, string role_bd, string token)
        {
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff")
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
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
        }

        public DataTable vivod_tovar(string role_bd, string token)
        {
            DataTable dt = new DataTable("dt_tovar");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "buyer" || role_bd == "provider")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar, tovar.massa_tovar,tovar.price_tovar,tovar_on_sklad.count,sklad.sklad_name FROM tovar JOIN tovar_on_sklad on tovar.tovar_id = tovar_on_sklad.tovar_id JOIN sklad on tovar_on_sklad.sklad_id = sklad.sklad_id", MySqlConnection);
                    MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                    addapter.Fill(dt);
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }

            return dt;
        }

        public DataTable searc_tovar_name(string name, string role_bd, string token)
        {
            DataTable dt = new DataTable("dt_one_tovar");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "buyer")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar, tovar.massa_tovar,tovar.price_tovar,tovar_on_sklad.count,sklad.sklad_name FROM tovar JOIN tovar_on_sklad on tovar.tovar_id = tovar_on_sklad.tovar_id JOIN sklad on tovar_on_sklad.sklad_id = sklad.sklad_id where name_tovar like @name", MySqlConnection);
                    command.Parameters.AddWithValue("@name", "%" + name + "%");
                    MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                    addapter.Fill(dt);
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public void drop_tovar_name(string name, string role_bd, string token)
        {
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin" || role_bd == "staff")
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DataTable vivod_ing(string role_bd, string token)
        {
            DataTable dt = new DataTable("dt_ing");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "provider")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar,spec_ingredients.name_ing,people.fio,ingredients.ing_count FROM tovar JOIN ingredients on tovar.tovar_id = ingredients.tovar_id JOIN spec_ingredients on ingredients.id_ing = spec_ingredients.id_ing JOIN people on spec_ingredients.user_id = people.user_id", MySqlConnection);
                    MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                    addapter.Fill(dt);
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public DataTable vivod_tovar_bez(string role_bd, string token)
        {
            DataTable dt = new DataTable("tovar_bez");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "provider")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar FROM tovar WHERE tovar_id NOT IN (SELECT tovar_id FROM ingredients)", MySqlConnection);
                    MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                    addapter.Fill(dt);
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public DataTable search_ing(string name_ing, string role_bd, string token)
        {
            DataTable dt = new DataTable("dt_ing");
            string result = search_sessions(token);
            if (result == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "provider")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT tovar.name_tovar,spec_ingredients.name_ing,people.fio,ingredients.ing_count FROM tovar JOIN ingredients on tovar.tovar_id = ingredients.tovar_id JOIN spec_ingredients on ingredients.id_ing = spec_ingredients.id_ing JOIN people on spec_ingredients.user_id = people.user_id where spec_ingredients.name_ing like @name_ing", MySqlConnection);
                    command.Parameters.AddWithValue("@name_ing", "%" + name_ing + "%");
                    MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                    addapter.Fill(dt);
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                System.Environment.Exit(0);
            }
            return dt;
        }

        public void add_ing(string name_tovar, string name_ing, string fio, string count, string role_bd, string token)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();

            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin" || role_bd == "staff")
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DataTable change_data(string user_id, string token)
        {

            DataTable dt = new DataTable("people");
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT people.username,people.email,people.fio,people.address,people_info.birthday_date,people_info.phone_number,people_info_two.card_number FROM people JOIN people_info on people.user_id = people_info.user_id JOIN people_info_two ON people_info.user_id = people_info_two.user_id where people.user_id = @user_id", MySqlConnection);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                    addapter.Fill(dt);
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            } 
            return dt;
        }

        public void update_data(string user_id, string table, string pole, string new_data, string token)
        {
            MySqlConnection MySqlConnection = Connection();
            MySqlConnection.Open();
            try
            {
                string result = search_sessions(token);
                if (result == "yes")    
                {
                    if(table == "people" && pole == "username")
                    {
                        string sqlquery = "Update people set username = @new_data where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@new_data", new_data);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                    }
                    if(table == "people" && pole == "email")
                    {
                        string sqlquery = "Update people set email = @new_data where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@new_data", new_data);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                    }
                    if(table == "people" && pole == "fio")
                    {
                        string sqlquery = "Update people set fio = @new_data where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@new_data", new_data);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                    }
                    if (table == "people" && pole == "address")
                    {
                        string sqlquery = "Update people set address = @new_data where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@new_data", new_data);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                    }
                    if (table == "people_info" && pole == "phone_number")
                    {
                        string sqlquery = "Update people_info set phone_number = @new_data where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@new_data", new_data);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                    }
                    if (table == "people_info_two" && pole == "card_number")
                    {
                        string sqlquery = "Update people_info_two set card_number = @new_data where user_id = @user_id";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@new_data", new_data);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void update_date(string user_id, DateTime date_b, string token)
        {
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    MySqlConnection MySqlConnection = Connection();
                    MySqlConnection.Open();
                    string sqlquery = "Update people_info set birthday_date = @date where user_id = @user_id";
                    MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                    cmd.Parameters.AddWithValue("@date", date_b);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Connection = MySqlConnection;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string search_sklad(string name, string user_id, string token)
        {
            string result = "no";
            try
            {
                MySqlConnection MySqlConnection = Connection();
                MySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("select sklad_name from sklad where sklad_name = @sklad_name", MySqlConnection);
                command.Parameters.AddWithValue("@sklad_name", name);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public string search_ing_provider(string name, string role_bd, string token)
        {
            string result = "no";
            string r = search_sessions(token);
            if (r == "yes")
            {
                if (role_bd == "admin" || role_bd == "staff" || role_bd == "provider")
                {
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
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            return result;
        }

        public DataTable vivod_sklad(string role_bd, string token)
        {

            DataTable dt = new DataTable("sklad");
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin" || role_bd == "staff")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        MySqlCommand command = new MySqlCommand("select sklad_name, tovar_count from sklad", MySqlConnection);
                        MySqlDataAdapter addapter = new MySqlDataAdapter(command);
                        addapter.Fill(dt);
                    }
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public void add_sklad(string name, string role_bd, string token)
        {
            DataTable dt = new DataTable("sklad");
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        string sqlquery1 = "insert into sklad set sklad_name = @skald_name, tovar_count = 0";
                        MySqlCommand cmd1 = new MySqlCommand(sqlquery1, MySqlConnection);
                        cmd1.Parameters.AddWithValue("@skald_name", name);
                        cmd1.Connection = MySqlConnection;
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void delete_sklad(string name, string role_bd, string token)
        {
            DataTable dt = new DataTable("sklad");
            try
            {
                string result = search_sessions(token);
                if (result == "yes")
                {
                    if (role_bd == "admin")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        string sqlquery1 = "delete from sklad where sklad_name = @skald_name";
                        MySqlCommand cmd1 = new MySqlCommand(sqlquery1, MySqlConnection);
                        cmd1.Parameters.AddWithValue("@skald_name", name);
                        cmd1.Connection = MySqlConnection;
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string drop_ing(string name, string fio, string role_bd, string token)
        {
            string result = "no";
            string ing_id = "";
            try
            {
                string r = search_sessions(token);
                if (r == "yes")
                {
                    if (role_bd == "admin" || role_bd == "staff")
                    {
                        MySqlConnection MySqlConnection = Connection();
                        MySqlConnection.Open();
                        MySqlCommand command = new MySqlCommand("select name_ing,id_ing from spec_ingredients where name_ing = @name_ing and user_id = (select user_id from people where fio = @fio)", MySqlConnection);
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
                        if (result == "yes")
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
                    else
                    {
                        Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Опасность, кто-то хочет хакнуть!!!");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        public string add_buyer(string name, string pass, string fio, string address)
        {
            string result = "no";
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
                MySqlDataReader MySqlReader = null;
                MySqlCommand command = new MySqlCommand("SELECT username FROM people where username = @username", MySqlConnection);
                command.Parameters.AddWithValue("username", name);
                MySqlReader = command.ExecuteReader();
                while (MySqlReader.Read())
                {
                    if (name == Convert.ToString(MySqlReader["username"]))
                    {
                        result = "yes";
                        MySqlReader.Close();
                        break;
                    }
                }
                if(result == "no")
                {
                    MySqlReader.Close();
                    try
                    {
                        string sqlquery = "INSERT INTO people (user_id, username, email, password, fio, address, registration, role) VALUES(@user_id, @username, @email, @password, @fio, @address, @registration, 'user')";
                        MySqlCommand cmd = new MySqlCommand(sqlquery, MySqlConnection);
                        cmd.Parameters.AddWithValue("@user_id", r);
                        cmd.Parameters.AddWithValue("@username", name);
                        cmd.Parameters.AddWithValue("@email", name);
                        cmd.Parameters.AddWithValue("@password", Encoding_password(pass));
                        cmd.Parameters.AddWithValue("@fio", fio);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@registration", DateTime.Now);
                        cmd.Connection = MySqlConnection;
                        cmd.ExecuteNonQuery();

                        Random rnd1 = new Random();
                        string club_card = rnd1.Next(1000, 9999) + "-" + rnd1.Next(1000, 9999) + "-" + rnd1.Next(1000, 9999) + "-" + rnd1.Next(1000, 9999);
                        string sqlquery1 = "INSERT INTO buyer (user_id, club_card) VALUES(@user_id, @club_card)";
                        MySqlCommand cmd1 = new MySqlCommand(sqlquery1, MySqlConnection);
                        cmd1.Parameters.AddWithValue("@user_id", r);
                        cmd1.Parameters.AddWithValue("@club_card", club_card);
                        cmd1.Connection = MySqlConnection;
                        cmd1.ExecuteNonQuery();

                        string sqlquery2 = "INSERT INTO people_info (user_id) VALUES(@user_id)";
                        MySqlCommand cmd2 = new MySqlCommand(sqlquery2, MySqlConnection);
                        cmd2.Parameters.AddWithValue("@user_id", r);
                        cmd2.Connection = MySqlConnection;
                        cmd2.ExecuteNonQuery();

                        string sqlquery3 = "INSERT INTO people_info_two (user_id) VALUES(@user_id)";
                        MySqlCommand cmd3 = new MySqlCommand(sqlquery3, MySqlConnection);
                        cmd3.Parameters.AddWithValue("@user_id", r);
                        cmd3.Connection = MySqlConnection;
                        cmd3.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
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
    }
}
