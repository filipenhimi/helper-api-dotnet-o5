using Br.Com.Parallelum.Fipe.Model;

namespace helper_api_dotnet_o5.Fipe;

public static class FipeUtils
{
    public static int GetId(this NamedCode obj)
    {
        int.TryParse(obj.Code, out int id);
        return id;
    }

    public static InfoFipe ToInfoFipe(this FipeResult obj)
    {
        if (obj == null) 
            return null;

        return new InfoFipe
        {
            vehicleType = obj.VehicleType,
            price = obj.Price,
            brand = obj.Brand,
            model = obj.Model,
            modelYear = obj.ModelYear,
            fuel = obj.Fuel,
            codeFipe = obj.CodeFipe,
            referenceMonth = obj.ReferenceMonth,
            fuelAcronym = obj.FuelAcronym, 
        };
    }   
}
