using System;
using System.Threading;
using DSAccessAgentAPI;

namespace DSAccessAgentTest
{
    class Program
    {
       static void Main(string[] args)
        {

            DSAccessAgent agent = DSAccessAgent.getInstance();

            // Подключение к очереди
            while(agent.Init()==false)
            { 
                Console.WriteLine("[Init] "+agent.getLastError());
                Thread.Sleep(2000);
            }

            // Выполнение запросов
            while (true)
            {
                {   // login =====================================================
                    try
                    {
                        LoginResult data = agent.login("www", "www", 4000);
                        Console.Write("[Ответ login]: ");
                        if (data.code != 0)
                            Console.WriteLine("[Ошибка] {0} {1}", data.code, data.message);
                        else  
                        {
                            Console.Write("Пользователь: {0}, ФИО {1} ", data.user, data.description);
                            if (data.expiration.Length == 0) Console.WriteLine("Постоянный пароль");
                            else Console.WriteLine("Временный пароль. Срок окончания: {0}", data.expiration);
                        }
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }

                Thread.Sleep(2000);
                {   // Change ====================================================
                    try
                    {
                        ChangeResult data = agent.change("www", "www72", "ttt", 4000);
                        Console.Write("[Ответ change]:");
                        if (data.code != 0)   Console.WriteLine("[Ошибка] {0} {1}", data.code, data.message);
                        else Console.WriteLine("[Сообщение]: {0}", data.message);

                    }  catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }                                     
                }
                //String answer = agent.Query("change;eee;eee;oper;", 1000);

                Thread.Sleep(1000);
            }
        } 
      }
}
