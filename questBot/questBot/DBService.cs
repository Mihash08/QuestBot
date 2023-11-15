using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questBot
{
    class DBService
    {
        public static NpgsqlConnection? dBConnection;
        public static void Connect()
        {
            string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            var conn = new NpgsqlConnection(connectionString);


            conn.Open();

            dBConnection = conn;
        }

        public static void Disconnect() {  dBConnection?.Close(); }

        public static void InitCreate()
        {
            try
            {
                var cmd = new NpgsqlCommand("DROP TABLE IF EXISTS chats", dBConnection);
                cmd.ExecuteNonQuery();
                cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS chats( chat_id BIGINT PRIMARY KEY, step_id INT, start_time TIMESTAMP, points INT)", dBConnection);
                cmd.ExecuteNonQuery();
            } catch (Exception ex)
            {
                Console.WriteLine("Init create error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static int GetChatStep(long chatId)
        {
            NpgsqlDataReader reader = null;
            var cmd = new NpgsqlCommand("SELECT step_id FROM chats WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId }
                }
            };
            try
            {
                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return -1;
                }
                else
                {
                    return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Getting chat_step error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return -1;
        }
        public static DateTime GetStartTime(long chatId)
        {
            NpgsqlDataReader reader = null;
            var cmd = new NpgsqlCommand("SELECT start_time FROM chats WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId }
                }
            };
            try
            {
                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return reader.GetDateTime(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Getting start_time error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return DateTime.MinValue;
        }
        public static int GetPoints(long chatId)
        {
            NpgsqlDataReader reader = null;
            var cmd = new NpgsqlCommand("SELECT points FROM chats WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId }
                }
            };
            try
            {
                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return 0;
                }
                else
                {
                    return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Getting start_time error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return 0;
        }

        public static void SetStep(long chatId, int stepId)
        {
            var cmd = new NpgsqlCommand("UPDATE chats SET step_id = ($2) WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId },
                    new() { Value = stepId }
                }
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setting step error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void AddPoints(long chatId, int points)
        {
            Console.WriteLine("Adding points " + points);
            var cmd = new NpgsqlCommand("UPDATE chats SET points = points + ($2) WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId },
                    new() { Value = points }
                }
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setting points error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public static void RemovePoints(long chatId, int points)
        {
            Console.WriteLine("Removing points " + points);
            var cmd = new NpgsqlCommand("UPDATE chats SET points = points - ($2) WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId },
                    new() { Value = points }
                }
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setting points error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public static void SetPoints(long chatId, int points)
        {
            var cmd = new NpgsqlCommand("UPDATE chats SET points = ($2) WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId },
                    new() { Value = points }
                }
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setting points error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public static void SetStartTime(long chatId)
        {
            var cmd = new NpgsqlCommand("UPDATE chats SET start_time = ($2) WHERE chat_id = ($1)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId },
                    new() { Value = DateTime.Now }
                }
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setting start_time error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void AddChat(long chatId)
        {
            var cmd = new NpgsqlCommand("INSERT INTO chats (chat_id, step_id, points) VALUES (($1), 0, 0)", dBConnection)
            {
                Parameters =
                {
                    new() { Value = chatId }
                }
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
