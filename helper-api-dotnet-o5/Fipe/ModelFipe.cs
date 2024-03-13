using Br.Com.Parallelum.Fipe.Model;

namespace helper_api_dotnet_o5.Fipe;

public class ModelFipe
{
    public VehiclesType Tipo { get; private set; }
    public NamedCode Marca { get; private set; }
    public NamedCode Modelo { get; private set; }

    public List<NamedCode> Anos { get; set; }

    public ModelFipe(VehiclesType vehiclesType, NamedCode marca, NamedCode modelo)
    { 
        Tipo = vehiclesType;
        Marca = marca;
        Modelo = modelo;
        Anos = new();
    }


}

[Serializable]
public class InfoFipe
{
    public int vehicleType { get; set; }
    public string price { get; set; }
    public string brand { get; set; }
    public string model { get; set; }
    public int modelYear { get; set; }
    public string fuel { get; set; }
    public string codeFipe { get; set; }
    public string referenceMonth { get; set; }
    public string fuelAcronym { get; set; }
}