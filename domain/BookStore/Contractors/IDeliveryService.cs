﻿using System.Collections.Generic;

namespace BookStore.Contractors
{
    public interface IDeliverytService
    {
        string UniqueCode { get; }

        string Title { get; }

        Form CreateForm(Order order);

        Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values);

        OrderDelivery GetDelivery(Form form); //Сюда передаётся форма после последнего шага заполнения(IsFinal==true)
    }
}
