using FoodDelivery2.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery2.Server.IService
{
    public interface IFoodOrderService
    {
        FoodOrder Save(FoodOrder oFoodOrder);
        FoodOrder GetSavedFoodOrder();
    }
}
