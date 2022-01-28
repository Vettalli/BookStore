using BookStore.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace BookStore.Web
{
    public static class SessionExtensions
    {
        private const string key = "Cart";

        //Сохраняет значение корзины в сессии
        public static void Set(this ISession session, Cart value)
        {
            if (value == null)
            {
                return;
            }
            
            //class MemoryStream Этот класс обеспечивает произвольный доступ к данным, хранящимся в памяти, а не в физическом файле.
            //С помощью него можно записать последовательность байтов в область памяти
            //Поток - последовательность байтов, которые можно записать на диск или в память
            //Using позволяет освободить память, когда работа будет закончена, даже если возникло исключение
            using (var stream = new MemoryStream())

                //Типы BinaryWriter и BinaryReader позволяют читать и записывать в поток дискретные
                //типы данных в компактном двоичном формате. 
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.OrderId);
                writer.Write(value.TotalCount);
                writer.Write(value.TotalPrice);

                session.Set(key, stream.ToArray());  //ToArray() возвращает массив байтов. Устанавливает по ключу key значение, которое представляет массив байтов
            }
        }

        public static bool TryGetCart(this ISession session, out Cart value)
        {
            //Проверяем, если есть значение с таким ключом, то массив байтов будет записан в переменную buffer
            if (session.TryGetValue(key, out byte[] buffer))
            {
                using (var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var orderId = reader.ReadInt32();
                    var totalCount = reader.ReadInt32();
                    var totalPrice = reader.ReadDecimal();


                    value = new Cart(orderId)
                    {
                        TotalCount = totalCount,
                        TotalPrice = totalPrice
                    };

                    return true;
                }               
            }

            value = null;

            return false;
        }
    }
}